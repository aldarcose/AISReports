using SharedDbWorker;
using SharedDbWorker.Classes;
using Syncfusion.XlsIO;
using System.Collections.Generic;

namespace Reports
{
    public class ExcelRDExport : ExcelExport
    {
        private string sqlQuery;
        private IList<ReportField> fields;
        private IDictionary<string, string> paramsStringValues;
        private IDictionary<int, string> columnNames;
        private string name;

        public ExcelRDExport(Connection conn, IWorkbook workbook, string name)
            : base(conn, workbook)
        {
            this.name = name;
            columnNames = new Dictionary<int, string>();
        }

        public void InitQuery(string sqlQuery)
        {
            this.sqlQuery = sqlQuery;
        }

        public void InitFields(IList<ReportField> fields)
        {
            this.fields = fields;
        }

        public void InitParamsStringValues(
            IDictionary<string, string> paramsStringValues)
        {
            this.paramsStringValues = paramsStringValues;
        }

        /// <inheritdoc/>
        public override IWorkbook Execute(IProgressControl pc)
        {
            if (fields.Count != 0)
            {
                List<DbResult> dbData = PrepareData(pc);
                IWorksheet sheet = PrepareWorkSheet();
                SetWorkSheetHeader(sheet);
                int rowIndex = SetWorkSheetParameters(sheet);
                SetColumnNames(sheet, ++rowIndex);
                FillTable(sheet, ++rowIndex, dbData);

                // Auto Column Width
                sheet.UsedRange.AutofitColumns();
            }
            return workBook;
        }

        private IWorksheet PrepareWorkSheet()
        {
            var excelEngine = new ExcelEngine();
            IApplication app = excelEngine.Excel;
            workBook = app.Workbooks.Create();
            IWorksheet sheet = workBook.Worksheets[0];
            return sheet;
        }

        // Заголовок
        private void SetWorkSheetHeader(IWorksheet sheet)
        {
            string colName = GetExcelColumnName(fields.Count);
            sheet.Range[string.Format("A1:{0}1", colName)].Merge();
            sheet.Range["A1"].Text = name;
            IStyle style = workBook.Styles.Add("HeaderStyle");
            style.Font.Bold = true;
            style.Font.Size = 13;
            sheet.Range["A1"].CellStyle = style;
            sheet.Range["A1"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
        }

        // Параметры
        private int SetWorkSheetParameters(IWorksheet sheet)
        {
            int i = 2; // Номер строки
            string colName = GetExcelColumnName(fields.Count);
            foreach (var paramStringValue in paramsStringValues)
            {
                sheet[string.Format("A{0}:{1}{0}", i, colName)].Merge();
                sheet[string.Format("A{0}", i)].Text = string.Format("{0}: {1}",
                    paramStringValue.Key, paramStringValue.Value);
                i++;
            }
            return i;
        }

        // Названия колонок
        private void SetColumnNames(IWorksheet sheet, int rowIndex)
        {
            for (int colIndex = 1; colIndex <= fields.Count; colIndex++)
            {
                string colName = GetExcelColumnName(colIndex);
                string index = string.Format("{0}{1}", colName, rowIndex);
                sheet[index].Text = fields[colIndex - 1].Caption;
                sheet[index].BorderAround(ExcelLineStyle.Thin);
                sheet[index].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            }
        }

        private string GetCachedColumnName(int columnNumber)
        {
            if (!columnNames.ContainsKey(columnNumber))
                columnNames[columnNumber] = GetExcelColumnName(columnNumber);
            return columnNames[columnNumber];
        }

        // Данные
        private void FillTable(IWorksheet sheet, int startRowIndex, List<DbResult> dbData)
        {
            int rowIndex = startRowIndex;
            foreach (DbResult dbResult in dbData)
            {
                int j = 1;
                foreach (var obj in dbResult.Fields)
                {
                    string colName = GetCachedColumnName(j);
                    string index = string.Format("{0}{1}", colName, rowIndex);
                    sheet[index].Value2 = obj.ToString() == "$id" ? rowIndex - startRowIndex + 1 : obj;
                    sheet[index].BorderAround(ExcelLineStyle.Thin);
                    j++;
                }
                rowIndex++;
            }
        }

        private List<DbResult> PrepareData(IProgressControl pc)
        {
            var dbQuery = new DbQuery("");
            dbQuery.Sql = sqlQuery;
            return conn.GetResults(dbQuery, pc);
        }
    }
}
