using SharedDbWorker;
using SharedDbWorker.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Reports
{
    public class ReportManager
    {
        private static readonly ReportManager instance = new ReportManager();

        public static ReportManager Instance
        {
            get { return instance; }
        }

        #region querySQL
        private const string querySQL =
            @"with recursive t as (
               select array[stat_id] as hierarchy
                     , stat_id
                     , parent_id
                     , desk
                     , period_xml
                     , 1 as level
               from  public.stat_tab
               where parent_id = -1
               union all
               select t.hierarchy || a.stat_id
                     , a.stat_id
                     , a.parent_id
                     , a.desk
                     , a.period_xml
                     , t.level + 1 as level
               from public.stat_tab a
               join t on a.parent_id = t.stat_id
               )
            select stat_id
                  , parent_id
                  , level
                  , desk
                  , period_xml
            from t
            order by hierarchy";
        #endregion

        private List<DbResult> LoadDbReports()
        {
            List<DbResult> results = null;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("Reports");
                q.Sql = querySQL;
                results = db.GetResults(q);
            }
            return results;
        }

        public ReportSchema LoadDbReportSchema()
        {
            // Схема отчетов
            ReportSchema schema = new ReportSchema();
            // Загрузка отчетов
            var dbReports = LoadDbReports();

            Dictionary<int, Folder> folders = new Dictionary<int, Folder>();
            Folder tempFolder;
            Func<int, Folder> GetFolder = id =>
            {
                if (!folders.TryGetValue(id, out tempFolder))
                    return null;
                return tempFolder;
            };

            foreach (var dbReport in dbReports)
            {
                if (dbReport.Fields[3] == DBNull.Value) continue;

                object paramsData = dbReport.Fields[4];
                string entryName = (string)dbReport.Fields[3];
                int id = (int)dbReport.Fields[0];
                int parent_id = (int)dbReport.Fields[1];
                int level = (int)dbReport.Fields[2];

                string paramsText = GetStringParameters(paramsData);
                if (!string.IsNullOrEmpty(paramsText))
                {
                    // Пропуск пустых отчетов
                    if (paramsText.Equals("NULL") || paramsText.Equals(Environment.NewLine))
                        continue;

                    bool isDesignedReport = false;
                    var report = new Report(id, entryName);
                    List<ReportParameter> parameters = ParseParameters(paramsText, ref isDesignedReport);
                    if (parameters == null)
                        continue;
                    
                    report.IsDesigned = isDesignedReport;
                    report.Parameters.AddRange(parameters);

                    if (level == 1)
                        schema.Reports.Add(report);
                    else
                        GetFolder(parent_id).Reports.Add(report);
                }
                else
                {
                    Folder folder = new Folder(entryName, level);
                    if (level != 1)
                    {
                        GetFolder(parent_id).Folders.Add(folder);
                    }

                    folders[id] = folder;
                }
            }

            // Корневые папки
            foreach (Folder fldr in folders.Values)
            {
                if (fldr.Level != 1) continue;
                schema.Folders.Add(fldr);
            }
            return schema;
        }

        private string GetStringParameters(object paramsData)
        {
            if (paramsData == DBNull.Value) return null;
            var byteArray = (byte[])paramsData;
            return Encoding.UTF8.GetString(byteArray);
        }

        private List<ReportParameter> ParseParameters(string paramsText, ref bool isDesignedReport)
        {
            var result = new List<ReportParameter>();
            XDocument paramsDoc = null;
            try
            {
                paramsDoc = XDocument.Parse(paramsText);
            }
            catch (Exception)
            {
                // workaround: Data at the root level is invalid. Line 1, position 1
                string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                if (paramsText.StartsWith(byteOrderMarkUtf8))
                    paramsText = paramsText.Remove(0, byteOrderMarkUtf8.Length);
                paramsDoc = XDocument.Parse(paramsText);
            }

            XElement firstElement = paramsDoc.Root.Elements("panel").FirstOrDefault();
            isDesignedReport = firstElement != null ? firstElement.Attribute("sections") != null : false;

            foreach (XElement el in paramsDoc.Root.Elements("panel"))
            {
                XAttribute sqlAttr = el.Attribute("sql");
                string typeText = el.Attribute("type").Value;
                if (string.IsNullOrEmpty(typeText))
                {
                    if (isDesignedReport)
                        result.Add(new ReportParameter(null, el.Attribute("title").Value, ReportParameterType.Unknown, null));
                    continue;
                }

                var groupNameAttribute = el.Attribute("groupName");
                string groupName = isDesignedReport && groupNameAttribute != null ? groupNameAttribute.Value : null;
                string comparedExpr = isDesignedReport ? el.Attribute("define").Value : null;

                ReportParameterType pType = ReportParameter.ParseParameterType(typeText);
                if (pType == ReportParameterType.Unknown)
                    throw new InvalidOperationException(string.Format("Неизвестный тип параметра: {0}", typeText));

                var parameter = isDesignedReport ? 
                    new ReportParameter(
                        el.Attribute("name").Value, 
                        el.Attribute("title").Value, 
                        pType, sqlAttr != null ? el.Attribute("sql").Value : null, groupName, comparedExpr) :
                    new ReportParameter(
                        el.Attribute("name").Value, 
                        el.Attribute("title").Value,
                        pType, sqlAttr != null ? el.Attribute("sql").Value : null);

                result.Add(parameter);
            }

            return result;
        }
    }
}
