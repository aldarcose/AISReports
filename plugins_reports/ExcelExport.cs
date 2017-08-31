using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using SharedDbWorker.Classes;
using Syncfusion.XlsIO;
using SharedDbWorker;

namespace Reports
{
    /// <summary>
    /// Экспорт в Excel
    /// </summary>
    public class ExcelExport : IOperation
    {
        private List<ExportQuery> queries;
        private IWorkbook workBook;
        private Dictionary<string, string> paramValues;

        public ExcelExport(IWorkbook workBook)
        {
            this.workBook = workBook;
        }

        public void SetQueries(List<ExportQuery> list)
        {
            queries = new List<ExportQuery>();
            queries.AddRange(list);
        }

        public void InitParameters(Dictionary<string, string> paramValues)
        {
            if (queries == null)
                throw new InvalidOperationException("Queries is null");
            foreach (var eQuery in queries)
            {
                if (string.IsNullOrEmpty(eQuery.InnerSql)) continue;
                foreach (var pair in paramValues)
                    eQuery.SetParameter(pair.Key, pair.Value);
            }
            this.paramValues = paramValues;
        }

        private void ReplaceWithParameterValues(IRange cell)
        {
            string result = cell.Value.TrimStart('#');
            if (result.StartsWith("=")) return;
            foreach (var paramPair in paramValues)
                if (result.IndexOf(paramPair.Key) >= 0)
                    result = result.Replace(paramPair.Key, paramPair.Value);
            cell.Value = result;
        }

        private ExportQuery FindQuery(string text)
        {
            return queries.FirstOrDefault(q => text.IndexOf(q.Name) > 0);
        }

        private string[] ExtractParameterValues(string text)
        {
            string paramsText = text.Contains("(") ?
                            text.Substring(text.IndexOf("(") + 1, 
                            text.IndexOf(")") - text.IndexOf("(") - 1) 
                            : null;
            return paramsText != null ? paramsText.Split(',') : null;
        }

        private void ProcessWorkSheet(IProgressControl pc, IWorksheet sheet)
        {
            int progress = 0;
            pc.SetStatus(string.Format("Обработка данных... ({0})", sheet.Name));
            if (!queries.Any(q => q.FieldNames != null && q.FieldNames.Count > 1))
                pc.SetMaximum(sheet.UsedCells.Length);

            int i = 0;
            while (i < sheet.UsedCells.Count())
            {
                IRange cell = sheet.UsedCells[i];
                if (!string.IsNullOrEmpty(cell.Value) && cell.Value.StartsWith("#"))
                {
                    var query = FindQuery(cell.Value);
                    if (query == null)
                    {
                        ReplaceWithParameterValues(cell); i++;
                        continue;
                    }
                    if (query.NonQuery)
                    {
                        query.ExecuteNonQuery();
                    }
                    else if (query.IsEmpty)
                    {
                        cell.Value2 = query.EmptyValue;
                    }
                    else if (query.IsScalar)
                    {
                        cell.Value2 = query.ExecuteScalarSQL(null, null, ExtractParameterValues(cell.Value));
                    }
                    else
                    {
                        // Иначе списочный тип
                        i += ProcessWorkSheetList(pc, sheet, query);
                    }
                }

                if (!queries.Any(q => q.FieldNames != null && q.FieldNames.Count > 1))
                    pc.SetProgress(progress++);

                i++;
            }
        }

        private int ProcessWorkSheetList(IProgressControl pc, IWorksheet sheet, ExportQuery query)
        {
            pc.SetStatus("Инициализация запроса...");
            List<DbResult> results = query.SelectSimple(pc);
            pc.SetStatus("Идет обработка данных...");

            int lastColNum = 0;
            int lastRowNum = 0;
            foreach (var cell in sheet.UsedCells)
            {
                if (string.IsNullOrEmpty(cell.Value)) continue;
                
                if (cell.Value.Equals(string.Format("#{0}", query.Name)))
                {
                    lastRowNum = cell.LastRow;
                    break;
                }
                else if (cell.Value.StartsWith("#"))
                {
                    // Установка параметров в отчете
                    ReplaceWithParameterValues(cell);
                }
            }

            string xlFieldValue;
            Dictionary<int, string> fieldPositions = new Dictionary<int, string>();
            int j = 0;
            while (j < sheet.Rows[lastRowNum-1].Columns.Length && 
                sheet.Rows[lastRowNum-1].Columns[j].Value != string.Empty)
            {
                fieldPositions[j] = sheet.Rows[lastRowNum - 1].Columns[j].Value;
                j++;
            }
            lastColNum = j;

            sheet.DeleteRow(lastRowNum);
            if (results.Count != 0)
                sheet.InsertRow(lastRowNum, results.Count);

            Func<DbResult, int, int, object> GetValue = (dbResult, xlRowNum, xlColNum) =>
            {
                fieldPositions.TryGetValue(xlColNum, out xlFieldValue);
                
                // В ячейке формула
                if (xlFieldValue.StartsWith("="))
                    return EvaluateFormula(xlFieldValue, xlRowNum);

                if (xlFieldValue.StartsWith("#") && !xlFieldValue.Equals(string.Format("#{0}", query.Name)))
                {
                    // В ячейке находится скалярная функция (запрос)
                    var funcQuery = FindQuery(xlFieldValue);
                    if (funcQuery != null)
                        return funcQuery.ExecuteScalarSQL(
                            query.FieldNames,
                            dbResult.Fields,
                            ExtractParameterValues(xlFieldValue));
                }
                string searchFormat = xlColNum == 0 ? "#{0}" : "#:{0}:";
                int index = query.FieldNames.FindIndex(fn =>
                    xlFieldValue.Equals(string.Format(searchFormat, fn)));
                if (index < 0 || dbResult.Fields[index].Equals("$id"))
                    return xlRowNum - lastRowNum + 2;
                return dbResult.Fields[index];
            };

            pc.SetProgress(0);
            int i = lastRowNum - 1;
            int progress = 0;
            foreach (DbResult result in results)
            {
                for (j = 0; j < lastColNum; j++)
                {
                    sheet.Rows[i].Columns[j].Value2 = GetValue(result, i, j);
                    sheet.Rows[i].Columns[j].BorderAround(ExcelLineStyle.Thin);
                }
                pc.SetProgress(progress++);
                i++;
            }

            for (j = 0; j < lastColNum; j++ )
                if (sheet.Rows[i].Columns[j].Value.StartsWith("=SUM", StringComparison.InvariantCultureIgnoreCase))
                {
                    string addressLocal = sheet.Rows[i].Columns[j].AddressLocal;
                    string column = GetColumn(addressLocal);
                    sheet.Rows[i].Columns[j].Formula = string.Format("=SUM({0}{1}:{2}{3})",
                        column, lastRowNum, column, i);
                }

            // Количество новых ячеек
            return results.Count * query.FieldNames.Count;
        }

        private string GetColumn(string addressLocal)
        {
            return string.Join("", addressLocal.Where(c => Char.IsLetter(c)));
        }

        private string EvaluateFormula(string formula, int rowNum)
        {
            var chuncks = formula.Split('+');
            if (chuncks.Length != 0)
            {
                var result = new List<string>();
                foreach (var chunk in chuncks)
                    result.Add(string.Format("{0}{1}", GetColumn(chunk), rowNum + 1));

                return "=" + string.Join("+", result);
            }
            return formula;
        }
        
        /// <inheritdoc/>
        public IWorkbook Execute(IProgressControl pc)
        {
            foreach (IWorksheet sheet in workBook.Worksheets)
                if (sheet.UsedCells.Length != 0)
                    ProcessWorkSheet(pc, sheet);

            return workBook;
        }
    }
}
