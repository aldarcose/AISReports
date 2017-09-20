using SharedDbWorker;
using SharedDbWorker.Classes;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Reports
{
    public class ReportLoader : IDisposable
    {
        private const string querySQL = "select excel_xml, def_xml from public.stat_tab where stat_id = {0}";
        private IWorkbook workBook;
        private ExcelEngine excelEngine;
        private List<ReportQuery> queries;
        private bool loadReportQueris;

        public ReportLoader(
            ExcelEngine excelEngine,
            bool loadReportQueris = true)
        {
            this.excelEngine = excelEngine;
            this.loadReportQueris = loadReportQueris;
        }

        public virtual void Load(int id)
        {
            DbResult result = null;
            using (var db = new DbWorker())
            {
                var q = new DbQuery(string.Empty);
                q.Sql = string.Format(QuerySql, id);
                result = db.GetResult(q);
            }

            workBook = LoadExcelReport(result.Fields[0]);
            if (loadReportQueris)
                queries = ParseQueries(result.Fields[1]);
        }

        public IWorkbook WorkBook
        {
            get { return workBook; }
        }

        public List<ReportQuery> ReportQueries
        {
            get { return queries; }
        }

        protected virtual string QuerySql
        {
            get { return querySQL; }
        }

        private List<ReportQuery> ParseQueries(object queriesData)
        {
            var result = new List<ReportQuery>();
            if (queriesData == DBNull.Value) return null;
            var byteArray = (byte[])queriesData;
            string queriesText = Encoding.UTF8.GetString(byteArray);
            if (queriesText == "NULL") return null;

            XDocument queriesDoc = null;
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
                result.Add(new ReportQuery(el.Value, result));
            return result;
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
                    "Ошибка при сохранении отчета: {0}", ex.Message);
            }
            return workbook;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            excelEngine.Dispose();
        }
    }

    public class ReportDesignerLoader : ReportLoader
    {
        private const string querySQL = "select excel_xml, def_xml, poles_xml from public.stat_tab where stat_id = {0}";

        public ReportDesignerLoader(ExcelEngine excelEngine)
            : base(excelEngine, false)
        {
        }

        /// <inheritdoc>
        public override void Load(int id)
        {
            base.Load(id);
        }

        /// <inheritdoc>
        protected override string QuerySql
        {
            get { return querySQL; }
        }
    }
}
