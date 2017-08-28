using SharedDbWorker.Classes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Reports.Controls
{
    /// <summary>
    /// Фабрика редакторов
    /// </summary>
    internal class EditFactory
    {
        public static Control CreateEdit(ReportParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            ComboBoxEdit edit;
            switch(parameter.Type)
            {
                case ReportParameterType.Date: return new DateTimePicker();
                case ReportParameterType.IntPeriod: return new IntPeriodEdit();
                case ReportParameterType.FloatPeriod: return new FloatPeriodEdit();
                case ReportParameterType.Period: return new PeriodEdit();
                case ReportParameterType.Text: return new TextBox();
                case ReportParameterType.Query :
                    edit = new ComboBoxEdit();
                    edit.QuerySql = parameter.Sql;
                    edit.ItemToText = o =>
                        {
                            var result = (DbResult)o;
                            var obj = result.Fields[1];
                            if (obj == DBNull.Value)
                                return string.Empty;
                            return obj.ToString();
                        };
                    edit.DoQuery();
                    return edit;
                case ReportParameterType.Enum:
                    edit = new ComboBoxEdit();
                    List<object> list = new List<object>();
                    var chuncks = parameter.Sql.Split(new string[] { "|" },
                        StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in chuncks)
                        list.Add(s);

                    edit.ItemToText = o =>
                    {
                        string text = (string)o;
                        if (!string.IsNullOrEmpty(text))
                            return text.Substring(text.IndexOf("=") + 1,
                                text.Length - text.IndexOf("=") - 1);
                        return string.Empty;
                    };
                    edit.SetObjects(list);
                    return edit;
            }

            throw new InvalidOperationException(
                "No editor for additional data type " + parameter.Type.ToString());
        }
    }
}
