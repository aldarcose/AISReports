using System;
using System.Linq;
using SharedDbWorker;
using System.Collections.Generic;
using SharedDbWorker.Classes;

namespace Reports
{
    public class ReportQuery
    {
        private Dictionary<string, string> globalParameters = new Dictionary<string, string>();
        private Dictionary<string, string> localParameters = new Dictionary<string, string>();
        private List<string> localParameterNames;
        private string innerSQL;
        private string rawSQL;
        private string name;
        private bool isEmpty;
        private string emptyValue;
        private bool isScalar;
        private bool nonQuery;
        private List<string> fieldNames;

        /// <summary>Название</summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>Тело запроса</summary>
        public string InnerSql
        {
            get { return innerSQL; }
        }

        /// <summary></summary>
        public bool NonQuery
        {
            get { return nonQuery; }
        }

        /// <summary>Запрос пуст</summary>
        public bool IsEmpty
        {
            get { return isEmpty; }
        }

        /// <summary>Запрос скалярного типа</summary>
        public bool IsScalar
        {
            get { return isScalar; }
        }

        /// <summary>Пустое значение</summary>
        public string EmptyValue
        {
            get { return emptyValue; }
        }

        /// <summary>Поля(алиасы) запроса</summary>
        public List<string> FieldNames
        {
            get { return fieldNames; }
        }

        public void SetParameter(string key, string value, bool isLocal = false)
        {
            if (isLocal)
                this.localParameters[key] = value;
            else
                this.globalParameters[key] = value;
            this.innerSQL = innerSQL.Replace(key, value);
        }

        public ReportQuery(string rawSQL, List<ReportQuery> previousQueries)
        {
            this.rawSQL = NormalizeSQL(rawSQL);
            ParseSQL(previousQueries);
        }

        private string NormalizeSQL(string sql)
        {
            return sql
                .Replace("&amp;", "&")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&quot;", "\"")
                .Replace("&apos;", "'")
                .TrimEnd(';')
                .Trim();
        }

        private string GetQueryPrefix(string query, string indexOfText)
        {
            string result = query.Substring(0, query.IndexOf(indexOfText,
                        StringComparison.InvariantCultureIgnoreCase)).Trim();
            bool isFunc = result.Contains("(");
            return isFunc ? result.Substring(0, result.IndexOf("(")) : result;
        }

        private void ParseSQL(List<ReportQuery> previousQueries)
        {
            if (rawSQL.IndexOf(";") > 0)
            {
                this.nonQuery = true;
                this.name = rawSQL.Split(' ').First();
                this.innerSQL = rawSQL.Replace(name, string.Empty);
            }
            else if (rawSQL.IndexOf("select", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                this.innerSQL = rawSQL.Substring(rawSQL.IndexOf("select",
                    StringComparison.InvariantCultureIgnoreCase));
                this.name = GetQueryPrefix(rawSQL, "select");
                this.fieldNames = SqlParser.ParseSqlFields(innerSQL);

                if (fieldNames.Count > 1)
                {
                    // Первое поле запроса имеет название самого запроса
                    fieldNames[0] = name;
                }
                else if (fieldNames.Count == 1)
                {
                    this.isScalar = true;
                }

                ParseFuncParamNames();
            }
            else if (rawSQL.IndexOf(" ") < 0) // Пустой запрос
            {
                this.name = rawSQL;
                this.isEmpty = true;
                this.isScalar = true;
            }
            else
            {
                this.name = GetQueryPrefix(rawSQL, " ");
                this.innerSQL = rawSQL.Substring(rawSQL.IndexOf(" ",
                    StringComparison.InvariantCultureIgnoreCase));

                foreach (var exportQuery in previousQueries)
                    if (innerSQL.IndexOf(exportQuery.Name, StringComparison.InvariantCultureIgnoreCase) > 0)
                    {
                        innerSQL = innerSQL.Remove(innerSQL.IndexOf("("), innerSQL.IndexOf(")") - innerSQL.IndexOf("(") + 1);
                        innerSQL = innerSQL.Replace(exportQuery.Name, exportQuery.InnerSql);
                        localParameterNames = exportQuery.localParameterNames;
                    }

                if (innerSQL.IndexOf("select", StringComparison.InvariantCultureIgnoreCase) < 0)
                {
                    this.isEmpty = true;
                    this.emptyValue = innerSQL.Trim();
                }
                this.isScalar = true;
            }
        }

        private void ParseFuncParamNames()
        {
            string sqlPrefix = rawSQL.Substring(0, rawSQL.IndexOf("select",
                StringComparison.InvariantCultureIgnoreCase)).Trim();
            bool isFunc = sqlPrefix.Contains("(");
            if (isFunc)
            {
                string paramsText = sqlPrefix.Substring(sqlPrefix.IndexOf("(") + 1,
                    sqlPrefix.IndexOf(")") - sqlPrefix.IndexOf("(") - 1);
                this.localParameterNames = new List<string>(paramsText.Split(','));
            }
        }

        private void SetLocalParameterValues(params string[] localParameterValues)
        {
            var pairs = new Dictionary<string, string>();
            for (int i = 0; i < localParameterNames.Count; i++)
            {
                string pameterKey = localParameterNames[i];
                pairs[pameterKey] = localParameterValues[i].Trim('"');
            }

            foreach (KeyValuePair<string, string> pair in pairs)
            {
                SetParameter(string.Format(":{0}:", pair.Key), pair.Value, true);
                SetParameter(pair.Key, pair.Value, true);
            }
        }

        public object ExecuteScalarSQL(
            Connection conn,
            List<string> additionalParamNames,
            List<object> additionalParamValues,
            params string[] localParameterValues)
        {
            string savedInnerSql = innerSQL;
            if (localParameterValues != null)
            {
                if (localParameterNames.Count == localParameterValues.Length)
                {
                    SetLocalParameterValues(localParameterValues);
                }
                else if (localParameterNames.Count == 1)
                {
                    SetParameter(localParameterNames[0],
                        string.Join(",", localParameterValues).Trim('"'), true);
                }
                else
                {
                    return null;
                }
            }

            // Дополнительные параметры из другого запроса O_o
            if (additionalParamNames != null)
                for (int i = 0; i < additionalParamNames.Count; i++)
                {
                    string valueText = Utils.ToString(additionalParamValues[i]);
                    string key1 = string.Format(":{0}:", additionalParamNames[i]);
                    string key2 = additionalParamNames[i];

                    SetParameter(key1, valueText, true);
                    SetParameter(key2, valueText, true);
                }

            var q = new DbQuery(name);
            q.Sql = innerSQL;
            object result = conn.GetScalarResult(q);
            // Возврат значений локальных параметров
            localParameters.Clear();
            innerSQL = savedInnerSql;

            return result;
        }

        public List<DbResult> SelectSimple(
            Connection conn, IProgressControl pc,
            params string[] localParameterValues)
        {
            string savedInnerSql = innerSQL;
            List<DbResult> results = null;
            var q = new DbQuery(name);
            if (localParameterValues != null)
            {
                if (localParameterNames.Count == localParameterValues.Length)
                {
                    SetLocalParameterValues(localParameterValues);
                }
                else if (localParameterNames.Count == 1)
                {
                    SetParameter(localParameterNames[0],
                        string.Join(",", localParameterValues).Trim('"'), true);
                }
                else
                {
                    return null;
                }
            }

            q.Sql = innerSQL;
            results = conn.GetResults(q, pc);
            innerSQL = savedInnerSql;

            return results;
        }

        public void ExecuteNonQuery(Connection conn)
        {
            var q = new DbQuery(string.Empty);
            q.Sql = innerSQL;
            conn.ExecuteStatement(q);
        }
    }
}
