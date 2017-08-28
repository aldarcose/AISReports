﻿using System;
using System.Linq;
using SharedDbWorker;
using System.Collections.Generic;
using SharedDbWorker.Classes;

namespace Reports
{
    public class ExportQuery
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

        public ExportQuery(string rawSQL, List<ExportQuery> previousQueries)
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

        private void ParseSQL(List<ExportQuery> previousQueries)
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
                else if  (fieldNames.Count == 1)
                {
                    this.isScalar = true;
                    ParseFuncParamNames();
                }
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

        public object ExecuteScalarSQL(params string[] parameterValues)
        {
            string innerSql_ = innerSQL;
            if (parameterValues != null)
            {
                if (localParameterNames.Count != parameterValues.Length)
                    throw new InvalidOperationException(
                        string.Format("Запрос {0}: Количество значений и количество названий параметров не совпадают", name));

                var pairs = new Dictionary<string, string>();
                foreach (string paramValue in parameterValues)
                {
                    int i = Array.IndexOf(parameterValues, paramValue);
                    string pameterKey = localParameterNames[i];
                    pairs[pameterKey] = paramValue;
                }

                foreach (KeyValuePair<string, string> pair in pairs)
                    SetParameter(pair.Key, pair.Value, true);
            }
            object result;
            using (var db = new DbWorker())
            {
                var q = new DbQuery(name);
                q.Sql = innerSQL;
                result = db.GetScalarResult(q);
            }

            // Возврат значений локальных параметров
            localParameters.Clear();
            innerSQL = innerSql_;

            return result;
        }

        public List<DbResult> SelectSimple(IProgressControl pc)
        {
            List<DbResult> results = null;
            using (var db = new DbWorker())
            {
                var q = new DbQuery(name);
                q.Sql = innerSQL;
                results = db.GetResults(q, pc);
            }
            return results;
        }

        public void ExecuteNonQuery()
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery(string.Empty);
                q.Sql = innerSQL;
                db.Execute(q);
            }
        }
    }
}
