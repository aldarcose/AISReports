using SharedDbWorker;
using SharedDbWorker.Classes;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace Reports
{
    public abstract class ReportLoaderBase<T> : IDisposable
        where T : ReportQuery
    {
        private IWorkbook workBook;
        protected ExcelEngine excelEngine;
        protected XDocument queriesDoc;
        protected DbResult dbResult;
        protected List<T> queries;

        public ReportLoaderBase(ExcelEngine excelEngine)
        {
            this.excelEngine = excelEngine;
        }

        public virtual void Load(int id)
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery(string.Empty);
                q.Sql = string.Format(QuerySql, id);
                dbResult = db.GetResult(q);
            }

            this.workBook = LoadExcelReport(dbResult.Fields[0]);
            ParseQueries(dbResult.Fields[1]);
        }

        public IWorkbook WorkBook
        {
            get { return workBook; }
        }

        public List<T> ReportQueries
        {
            get { return queries; }
        }

        protected abstract string QuerySql { get; }

        protected virtual void ParseQueries(object queriesData)
        {
            var result = new List<T>();
            if (queriesData == DBNull.Value) return;
            var byteArray = (byte[])queriesData;
            string queriesText = Encoding.UTF8.GetString(byteArray);
            if (queriesText == "NULL") return;

            try
            {
                queriesDoc = XDocument.Parse(queriesText);
            }
            catch (Exception)
            {
                // workaround: Data at the root level is invalid. Line 1, position 1
                string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                if (queriesText.StartsWith(byteOrderMarkUtf8))
                    queriesText = queriesText.Remove(0, byteOrderMarkUtf8.Length);
                queriesDoc = XDocument.Parse(queriesText);
            }

            foreach (XElement el in queriesDoc.Root.Elements("def"))
                result.Add(GetQueryObject(el.Value, result));

            queries = result;
        }

        private T GetQueryObject(string rawSql, List<T> previousQueries)
        {
            Type type = typeof(T);
            var ctor = type.GetConstructor(new[] { typeof(string), typeof(List<T>) });
            if (ctor != null)
                return (T)ctor.Invoke(new object[] { rawSql, previousQueries });
            
            ctor = type.GetConstructor(new[] { typeof(string) });
            return (T)ctor.Invoke(new object[] { rawSql });;
        }

        private IWorkbook LoadExcelReport(object reportData)
        {
            IWorkbook workbook = null;
            IApplication app = excelEngine.Excel;
            var byteArray = (byte[])reportData;
            if (Encoding.UTF8.GetString(byteArray) == "NULL")
                return null;
            var ms = new MemoryStream(byteArray);
            try
            {
                workbook = app.Workbooks.Open(ms, ExcelOpenType.Automatic);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceWarning(
                    "Ошибка при открытии отчета: {0}", ex.Message);
            }
            return workbook;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            excelEngine.Dispose();
        }
    }

    public class ReportLoader : ReportLoaderBase<ReportQuery>
    {
        private const string querySQL = "select excel_xml, def_xml from public.stat_tab where stat_id = {0}";

        public ReportLoader(ExcelEngine excelEngine)
            : base(excelEngine)
        {
        }

        /// <inheritdoc>
        protected override string QuerySql
        {
            get { return querySQL; }
        }
    }

    public class ReportDesignerLoader : ReportLoaderBase<ReportDesignerQuery>
    {
        private const string querySQL = "select excel_xml, def_xml, poles_xml from public.stat_tab where stat_id = {0}";
        private List<ReportField> fields;
        private List<ReportParameter> attachedParameters;
        private List<ReportField> attachedFields;

        public ReportDesignerLoader(ExcelEngine excelEngine)
            : base(excelEngine)
        {
        }

        /// <inheritdoc>
        protected override string QuerySql
        {
            get { return querySQL; }
        }

        public List<ReportField> ReportFields
        {
            get { return fields; }
        }

        public List<ReportParameter> AttachedParameters
        {
            get { return attachedParameters; }
        }

        public List<ReportField> AttachedFields
        {
            get { return attachedFields; }
        }

        /// <inheritdoc>
        public override void Load(int id)
        {
            base.Load(id);
            ParseFields(dbResult.Fields[2]);
        }

        private void ParseFields(object fieldsData)
        {
            var result = new List<ReportField>();
            if (fieldsData == DBNull.Value) return;
            var byteArray = (byte[])fieldsData;
            string text = Encoding.UTF8.GetString(byteArray);
            if (text == "NULL") return;

            XDocument fieldsDoc = null; 
            try
            {
                fieldsDoc = XDocument.Parse(text);
            }
            catch (Exception)
            {
                // workaround: Data at the root level is invalid. Line 1, position 1
                string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                if (text.StartsWith(byteOrderMarkUtf8))
                    text = text.Remove(0, byteOrderMarkUtf8.Length);
                queriesDoc = XDocument.Parse(text);
            }

            bool isGrouping;
            foreach (XElement el in fieldsDoc.Root.Elements("panel"))
            {
                bool.TryParse(el.Attribute("isGrouping").Value, out isGrouping);
                result.Add(new ReportField(
                    el.Attribute("name").Value,
                    el.Attribute("title").Value,
                    el.Attribute("groupName") != null ? el.Attribute("groupName").Value : null,
                    el.Attribute("define").Value, isGrouping,
                    el.Attribute("sections").Value));
            }

            fields = result;
        }

        /// <inheritdoc>
        protected override void ParseQueries(object queriesData)
        {
            base.ParseQueries(queriesData);

            // В запросах мастера отчета могут быть ссылки на другие отчеты.
            // В таком случае параметры и поля этих отчетов автоматом добавляются в 
            // текущий отчет.

            // Прикрепленные отчеты
            ReportDesignerLoader attachedReportLoader = new ReportDesignerLoader(excelEngine);
            foreach (XElement xReport in queriesDoc.Root.Elements("otchet"))
            {
                int attachedReportId = int.Parse(xReport.Attribute("prikOtchetId").Value);
                string attachedLeftJoinQuery = xReport.Attribute("prikZapros").Value;

                queries.Add(new ReportDesignerQuery(attachedLeftJoinQuery));
                Report attachedReport = ReportSchema.Instance.FindReport(attachedReportId);
                // Запросы
                attachedReportLoader.Load(attachedReportId);
                queries.AddRange(attachedReportLoader.ReportQueries.Where(q => q.IsJoinExpression));
                // Параметры
                attachedParameters = new List<ReportParameter>();
                attachedParameters.AddRange(attachedReport.Parameters);
                // Поля
                attachedFields = new List<ReportField>();
                attachedFields.AddRange(attachedReportLoader.ReportFields);
            }
        }
    }
}
