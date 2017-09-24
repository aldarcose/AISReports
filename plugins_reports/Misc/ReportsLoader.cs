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
        private ExcelEngine excelEngine;
        private List<T> queries;
        protected XDocument queriesDoc;
        protected DbResult dbResult;

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
            this.queries = ParseQueries(dbResult.Fields[1]);
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

        protected virtual List<T> ParseQueries(object queriesData)
        {
            var result = new List<T>();
            if (queriesData == DBNull.Value) return null;
            var byteArray = (byte[])queriesData;
            string queriesText = Encoding.UTF8.GetString(byteArray);
            if (queriesText == "NULL") return null;

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

            return result;
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
        private Dictionary<string, ReportDesignerQuery> queriesDict;
        private List<ReportField> fields;

        public ReportDesignerLoader(ExcelEngine excelEngine)
            : base(excelEngine)
        {
        }

        /// <inheritdoc>
        protected override string QuerySql
        {
            get { return querySQL; }
        }

        public Dictionary<string, ReportDesignerQuery> QueriesDict
        {
            get 
            { 
                if(queriesDict == null)
                    queriesDict = ReportQueries.ToDictionary(q => q.Name, q => q);
                return queriesDict; 
            }
        }

        public List<ReportField> ReportFields
        {
            get { return fields; }
        }

        /// <inheritdoc>
        public override void Load(int id)
        {
            base.Load(id);
            this.fields = ParseFields(dbResult.Fields[2]);
        }

        private List<ReportField> ParseFields(object fieldsData)
        {
            var result = new List<ReportField>();
            if (fieldsData == DBNull.Value) return null;
            var byteArray = (byte[])fieldsData;
            string text = Encoding.UTF8.GetString(byteArray);
            if (text == "NULL") return null;

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

            foreach (XElement el in fieldsDoc.Root.Elements("panel"))
            {
                result.Add(new ReportField(
                    el.Attribute("name").Value,
                    el.Attribute("title").Value,
                    el.Attribute("groupName") != null ? el.Attribute("groupName").Value : null,
                    el.Attribute("define").Value,
                    el.Attribute("sections").Value));
            }

            return result;
        }

        /// <inheritdoc>
        protected override List<ReportDesignerQuery> ParseQueries(object queriesData)
        {
            var result = base.ParseQueries(queriesData);

            // todo: 
            // В запросах мастера отчета могут быть ссылки на другие отчеты.
            // В таком случае параметры и поля этих отчетов автоматом добавляются в 
            // текущий отчет. fucking logic

            return result;
        }
    }
}
