using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using SharedDbWorker.Classes;
using Syncfusion.XlsIO;
using SharedDbWorker;
using System.Text.RegularExpressions;

namespace Reports
{
    /// <summary>
    /// Экспорт в Excel
    /// </summary>
    public class ExcelExport : IOperation
    {
        private List<ReportQuery> queries;
        private IWorkbook workBook;
        private Dictionary<string, Tuple<string, object>> paramValues;
        private Connection conn;

        public ExcelExport(Connection conn, IWorkbook workBook)
        {
            this.conn = conn;
            this.workBook = workBook;
        }

        public void SetQueries(List<ReportQuery> list)
        {
            queries = new List<ReportQuery>();
            queries.AddRange(list);
        }

        public void InitParameters(Dictionary<string, Tuple<string, object>> paramValues)
        {
            if (queries == null)
                throw new InvalidOperationException("Queries is null");
            foreach (var eQuery in queries)
            {
                if (string.IsNullOrEmpty(eQuery.InnerSql)) continue;
                foreach (var pair in paramValues)
                    eQuery.SetParameter(pair.Key, pair.Value.Item1);
            }
            this.paramValues = paramValues;
        }

        private void ReplaceWithParameterValues(IRange cell)
        {
            string result = cell.Value.TrimStart('#');
            if (result.StartsWith("=")) return;
            foreach (var paramPair in paramValues)
                if (result.IndexOf(paramPair.Key) >= 0)
                    result = result.Replace(paramPair.Key, paramPair.Value.Item1);
            cell.Value = result;
        }

        private ReportQuery FindQuery(string text)
        {
            var chuncks = text.TrimStart('#').Split('(');
            return queries.FirstOrDefault(q => q.Name.Equals(chuncks[0]));
        }

        private string[] ExtractParameterValues(string text)
        {
            HashSet<string> hashSet = new HashSet<string>();

            string paramsText = text.Contains("(") ?
                            text.Substring(text.IndexOf("(") + 1, 
                            text.LastIndexOf(")") - text.IndexOf("(") - 1) 
                            : null;

            if (paramsText == null) return null;

            foreach (string value in paramsText.Split(','))
                hashSet.Add(value.Trim().TrimStart(':').TrimEnd(':'));

            return hashSet.ToArray();
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
                IRange cell = sheet.UsedCells[i]; string cellValue = cell.Value;
                if (!string.IsNullOrEmpty(cellValue) && cellValue.StartsWith("#"))
                {
                    if (cellValue.StartsWith("#=")) 
                        cell.Value2 = EvaluateFormula(cell.Column, cell.Row, cellValue);

                    var query = FindQuery(cellValue);
                    if (query == null)
                    {
                        ReplaceWithParameterValues(cell); i++;
                        continue;
                    }
                    // Дополнительные параметры
                    string[] localParVals = ExtractParameterValues(cellValue);
                    if (query.NonQuery)
                    {
                        query.ExecuteNonQuery(conn);
                        cell.Value2 = null;
                    }
                    else if (query.IsEmpty)
                    {
                        cell.Value2 = query.EmptyValue;
                    }
                    else if (query.IsScalar)
                    {
                        cell.Value2 = query.ExecuteScalarSQL(conn, null, null, localParVals);
                    }
                    else
                    {
                        // Иначе списочный тип
                        i += ProcessWorkSheetList(pc, sheet, query, localParVals);
                    }
                }

                if (!queries.Any(q => q.FieldNames != null && q.FieldNames.Count > 1))
                    pc.SetProgress(progress++);

                i++;
            }
        }

        private int ProcessWorkSheetList(
            IProgressControl pc, IWorksheet sheet, ReportQuery query,
            params string[] localParameterValues)
        {
            pc.SetStatus("Инициализация запроса...");
            List<DbResult> results = query.SelectSimple(conn, pc, localParameterValues);
            pc.SetStatus("Идет обработка данных...");

            int firstColNum = 0;
            int firstRowNum = 0;
            foreach (var cell in sheet.UsedCells)
            {
                string value = cell.Value;
                
                if (string.IsNullOrEmpty(value)) continue;
                
                if (value.Equals(string.Format("#{0}", query.Name)) ||
                    value.IndexOf(string.Format("#{0}(", query.Name)) >= 0)
                {
                    firstRowNum = cell.LastRow;
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
            while (j < sheet.Rows[firstRowNum-1].Columns.Length && 
                sheet.Rows[firstRowNum-1].Columns[j].Value != string.Empty)
            {
                fieldPositions[j] = sheet.Rows[firstRowNum - 1].Columns[j].Value;
                j++;
            }
            firstColNum = j;

            sheet.DeleteRow(firstRowNum);
            if (results.Count != 0)
                sheet.InsertRow(firstRowNum, results.Count);

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
                        return funcQuery.ExecuteScalarSQL(conn,
                            query.FieldNames,
                            dbResult.Fields,
                            ExtractParameterValues(xlFieldValue));
                }
                string searchFormat = xlColNum == 0 ? "#{0}" : "#:{0}:";
                int index = query.FieldNames.FindIndex(fn =>
                    xlFieldValue.Equals(string.Format(searchFormat, fn)));

                if (index < 0) return xlFieldValue;
                if (dbResult.Fields[index].Equals("$id")) 
                    return xlRowNum - firstRowNum + 2;
                return dbResult.Fields[index];
            };

            pc.SetProgress(0);
            int i = firstRowNum - 1;
            int progress = 0;
            foreach (DbResult result in results)
            {
                for (j = 0; j < firstColNum; j++)
                {
                    sheet.Rows[i].Columns[j].Value2 = GetValue(result, i, j);
                    sheet.Rows[i].Columns[j].BorderAround(ExcelLineStyle.Thin);
                }
                pc.SetProgress(progress++);
                i++;
            }

            for (j = 0; j < firstColNum; j++ )
                if (sheet.Rows[i].Columns[j].Value.StartsWith("=SUM", StringComparison.InvariantCultureIgnoreCase))
                {
                    string addressLocal = sheet.Rows[i].Columns[j].AddressLocal;
                    string column = GetColumn(addressLocal);
                    sheet.Rows[i].Columns[j].Formula = string.Format("=SUM({0}{1}:{2}{3})",
                        column, firstRowNum, column, i);
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

        private string EvaluateFormula(int colNum, int rowNum, string cellValue)
        {
            List<string> list = new List<string>();

            cellValue = cellValue.Remove(0, 2);
            string[] chuncks = cellValue.Split('+');

            foreach (var chunk in chuncks)
            {
                int colShift = chunk.EndsWith("C") ? 0 : Int32.Parse(Regex.Match(chunk, @"\d+").Value);
                int rowShift = chunk.StartsWith("RC") ? 0 : Int32.Parse(Regex.Match(chunk, @"\d+").Value);
                list.Add(string.Format("{0}{1}", GetExcelColumnName(colNum - colShift), rowNum - rowShift));
            }

            return "=" + string.Join("+", list);
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
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
