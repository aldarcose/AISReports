﻿using SharedDbWorker;
using SharedDbWorker.Classes;
using Syncfusion.XlsIO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Reports
{
    public class ExcelRDExport : ExcelExportBase<ReportDesignerQuery>
    {
        private string sqlQuery;
        private IList<ReportField> fields;
        private IDictionary<string, string> paramsStringValues;
        private IDictionary<int, string> columnNames;
        private IDictionary<string, string> paramValues2;
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

        public void InitParameters(IDictionary<string, string> paramValues2)
        {
            if (queries == null)
                throw new InvalidOperationException("Queries is null");
            
            // Предыиниализация параметров
            PreInitQueries(paramValues2);

            foreach (var eQuery in queries.ToArray())
            {
                if (string.IsNullOrEmpty(eQuery.InnerSql)) continue;
                foreach (var pair in paramValues2)
                    eQuery.SetParameter(pair.Key, pair.Value);

                eQuery.SetTrueForAllParameters("mkb");
                // Если запрос содержит больше одного поля то
                // переносим его в параметры
                if (eQuery.FieldNames.Count != 1)
                {
                    var dbResults = eQuery.SelectSimple(conn, null);
                    var dbResult = dbResults.FirstOrDefault();
                    if (dbResult != null)
                    {
                        for (int i = 0; i < dbResult.Fields.Count; i++)
                        {
                            string key = ":" + dbResult.FieldNames[i] + ":";
                            string text = Utils.ToString(dbResult.Fields[i]);
                            if (i == 0)
                                paramValues[eQuery.Name] = new Tuple<string, object>(text, null);
                            else
                                paramValues[key] = new Tuple<string, object>(text, null);
                        }
                    }
                    queries.Remove(eQuery);
                }
            }
            this.paramValues2 = paramValues2;
        }

        private void PreInitQueries(IDictionary<string, string> paramValues2)
        {
            var queryWithParams = queries.SingleOrDefault(q => q.IsParamatersQuery);
            if (queryWithParams != null)
            {
                foreach (var pair in paramValues2)
                    queryWithParams.SetParameter(pair.Key, pair.Value);
                queries.Remove(queryWithParams);

                var dbResults = queryWithParams.SelectSimple(conn, null);
                var dbResult = dbResults.FirstOrDefault();
                if (dbResult != null)
                {
                    foreach (var eQuery in queries)
                        for (int i = 0; i < dbResult.Fields.Count; i++)
                        {
                            string key = ":" + dbResult.FieldNames[i] + ":";
                            string text = Utils.ToString(dbResult.Fields[i]);

                            eQuery.SetParameter(key, text);
                            if (i == 0)
                                paramValues[queryWithParams.Name] = new Tuple<string, object>(text, null);
                            else
                                paramValues[key] = new Tuple<string, object>(text, null);
                        }
                }
            }
        }

        /// <inheritdoc/>
        protected override void ReplaceWithParameterValues(IRange cell)
        {
            if (cell.Text == "#:list_params:")
            {
                cell.Value = string.Join(", ", paramsStringValues.Select(p => 
                        string.Format("{0}:{1}", p.Key, p.Value)));
                return;
            }
            base.ReplaceWithParameterValues(cell);
        }

        /// <inheritdoc/>
        public override Tuple<string, IWorkbook> Execute(IProgressControl pc)
        {
            if (fields != null && fields.Count != 0)
            {
                List<DbResult> dbData = null;
                try
                {
                    dbData = LoadData(pc);
                }
                catch(Exception ex)
                {
                    if (ex.GetType().FullName == "Npgsql.NpgsqlException")
                    {
                        return new Tuple<string, IWorkbook>(sqlQuery + Environment.NewLine + ex.Message, null);
                    }
                    else
                        throw ex;
                }

                IWorksheet sheet = PrepareWorkSheet();
                SetWorkSheetHeader(sheet);
                int rowIndex = SetWorkSheetParameters(sheet);
                SetColumnNames(sheet, ++rowIndex);
                FillTable(pc, sheet, ++rowIndex, dbData);

                // Auto Column Width
                sheet.UsedRange.AutofitColumns();
            }
            else
            {
                base.Execute(pc);
            }

            return new Tuple<string, IWorkbook>(null, workBook);
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
            string colName = GetCachedColumnName(fields.Count);
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
                string colName = GetCachedColumnName(colIndex);
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
        private void FillTable(IProgressControl pc, IWorksheet sheet, int startRowIndex, List<DbResult> dbData)
        {
            pc.SetStatus("Обработка данных...");
            pc.SetProgress(0);
            int progress = 0;
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

                pc.SetProgress(progress++);
                rowIndex++;
            }
        }

        private List<DbResult> LoadData(IProgressControl pc)
        {
            pc.SetStatus("Инициализация запроса...");
            var dbQuery = new DbQuery("");
            dbQuery.Sql = sqlQuery;
            return conn.GetResults(dbQuery, pc);
        }
    }
}
