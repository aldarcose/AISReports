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

            ComboBoxEdit comboBoxEdit;
            switch(parameter.Type)
            {
                case ReportParameterType.Date: return new DateTimePicker();
                case ReportParameterType.IntPeriod: return new IntPeriodEdit();
                case ReportParameterType.FloatPeriod: return new FloatPeriodEdit();
                case ReportParameterType.Period: return new PeriodEdit();
                case ReportParameterType.Text: return new TextBox();
                case ReportParameterType.VarText: return new VarTextEdit();
                case ReportParameterType.Boolean: return new CheckBox();
                case ReportParameterType.TimePeriod :
                    var edit = new PeriodEdit(); edit.SetFormat(DateTimePickerFormat.Time);
                    return edit;
                case ReportParameterType.Query :
                    comboBoxEdit = new ComboBoxEdit();
                    comboBoxEdit.QuerySql = parameter.Sql;
                    comboBoxEdit.ItemToText = o =>
                        {
                            var result = (DbResult)o;
                            var obj = result.Fields[1];
                            if (obj == DBNull.Value)
                                return string.Empty;
                            return obj.ToString();
                        };
                    comboBoxEdit.DoQuery();
                    return comboBoxEdit;
                case ReportParameterType.Enum:
                    comboBoxEdit = new ComboBoxEdit();
                    List<object> list = new List<object>();
                    var chuncks = parameter.Sql.Split(new string[] { "|" },
                        StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in chuncks)
                        list.Add(s);

                    comboBoxEdit.ItemToText = o =>
                    {
                        string text = (string)o;
                        if (!string.IsNullOrEmpty(text))
                            return text.Substring(text.IndexOf("=") + 1,
                                text.Length - text.IndexOf("=") - 1);
                        return string.Empty;
                    };
                    comboBoxEdit.SetObjects(list);
                    return comboBoxEdit;
                case ReportParameterType.Diagn:
                    return new DiagnosisEdit();
            }

            throw new InvalidOperationException(
                "No editor for additional data type " + parameter.Type.ToString());
        }
    }
}
