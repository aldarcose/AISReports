using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedDbWorker.Classes;

namespace Reports.Controls
{
    /// <summary>
    /// Представление параметров отчета
    /// </summary>
    public partial class ParametersView : UserControl
    {
        private ReportParameterCollection parametersData;

        public ParametersView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие изменения высоты
        /// </summary>
        public event EventHandler<AdditionalDataEditEventArgs> HeightChanged;

        /// <summary>
        /// Значение
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ReportParameterCollection Value
        {
            get { return parametersData; }
            set
            {
                parametersData = value;

                if (parametersData == null)
                {
                    SuspendLayout();
                    verticalLayout.Controls.Clear();
                    ResumeLayout(false);
                    return;
                }

                SuspendLayout();

                // Удаление старых контролов
                verticalLayout.Controls.Clear();
                // Создание новых контролов в соответствии с параметрами
                int height = 0;
                foreach (var parameter in parametersData)
                {
                    // Описание
                    Label label = new Label();
                    label.MinimumSize = new Size(150, 17);
                    label.MaximumSize = new Size(150, 0);
                    label.Size = new Size(150, 17);
                    label.AutoSize = true;
                    label.Margin = new Padding(0, 3, 0, 0);
                    label.Text = parameter.Caption;

                    Control edit = EditFactory.CreateEdit(parameter);

                    edit.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    edit.Margin = new Padding(0, 0, 0, 0);

                    // Редактор с описанием
                    ControlPair labeledEdit = new ControlPair();
                    labeledEdit.Dock = DockStyle.Fill;
                    labeledEdit.Margin = new Padding(3, 2, 3, 2);
                    labeledEdit.Height = edit.Height;
                    labeledEdit.Control1 = label;
                    labeledEdit.Control2 = edit;
                    labeledEdit.Name = "edit" + parameter.Name;

                    verticalLayout.Controls.Add(labeledEdit);

                    // Высота редактора
                    height += labeledEdit.Height + labeledEdit.Margin.Vertical;
                }

                // Высота редактора
                int dy = (Padding.Vertical + height) - Height;
                if (HeightChanged != null)
                    HeightChanged(this, new AdditionalDataEditEventArgs(dy));

                ResumeLayout(false);
            }
        }

        public Dictionary<string, Tuple<string, object>> GetParametersValues()
        {
            if (parametersData == null) return null;
            var result = new Dictionary<string, Tuple<string, object>>();
            
            foreach (var par in parametersData)
            {
                ControlPair edit = (ControlPair)this.Controls.Find("edit" + par.Name, true)[0];
                if (typeof(IParameter).IsAssignableFrom(edit.Control2.GetType()))
                {
                    object value = ((IParameter)edit.Control2).Value;
                    switch(par.Type)
                    {
                        case ReportParameterType.Period:
                            var datePeriod = (Tuple<DateTime, DateTime>)value;
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(string.Format("{0:yyyy-MM-dd}", datePeriod.Item1), datePeriod);
                            result[string.Format(":{0}_2_:", par.Name)] = new Tuple<string, object>(string.Format("{0:yyyy-MM-dd}", datePeriod.Item2), datePeriod);
                            break;
                        case ReportParameterType.TimePeriod:
                            var timePeriod = (Tuple<DateTime, DateTime>)value;
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(null, timePeriod);
                            break;
                        case ReportParameterType.FloatPeriod:
                            var floatPeriod = (Tuple<decimal, decimal>)value;
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(floatPeriod.Item1.ToString(), floatPeriod);
                            result[string.Format(":{0}_2_:", par.Name)] = new Tuple<string, object>(floatPeriod.Item2.ToString(), floatPeriod);
                            break;
                        case ReportParameterType.IntPeriod:
                            var intPeriod = (Tuple<int, int>)value;
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(intPeriod.Item1.ToString(), intPeriod);
                            result[string.Format(":{0}_2_:", par.Name)] = new Tuple<string, object>(intPeriod.Item2.ToString(), intPeriod);
                            break;
                        case ReportParameterType.Query:
                            var dbResult = (DbResult)value;
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(dbResult.Fields[0].ToString(), dbResult);
                            result[string.Format(":{0}_2_:", par.Name)] = new Tuple<string, object>(dbResult.Fields[1].ToString(), dbResult);
                            break;
                        case ReportParameterType.Enum:
                            string text = value.ToString();
                            string enumText = text.Substring(0, text.IndexOf("="));
                            // Параметр перечислимого типа в выражении может быть
                            // слева или справа. В xml это никак не указано, поэтому
                            // выставляем сразу два значения.
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(enumText, text);
                            result[string.Format(":{0}_2_:", par.Name)] = new Tuple<string, object>(enumText, text);
                            break;
                    }
                }
                else 
                {
                    switch(par.Type)
                    {
                        case ReportParameterType.Text:
                            var textBox = edit.Control2 as TextBox;
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(textBox.Text, textBox.Text);
                            break;
                        case ReportParameterType.Date:
                            var dateEdit = edit.Control2 as DateTimePicker;
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(string.Format("{0:yyyy-MM-dd}", dateEdit.Value), dateEdit.Value);
                            break;
                        case ReportParameterType.Boolean:
                            var checkBox = edit.Control2 as CheckBox;
                            result[string.Format(":{0}_1_:", par.Name)] = new Tuple<string, object>(null, checkBox.Checked);
                            break;
                    }
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Событие изменения высоты редактора
    /// </summary>
    public class AdditionalDataEditEventArgs : EventArgs
    {
        // Дельта изменения высоты
        private int dy = 0;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dy">Дельта изменения высоты</param>
        public AdditionalDataEditEventArgs(int dy)
        {
            this.dy = dy;
        }

        /// <summary>
        /// Дельта изменения высоты
        /// </summary>
        public int Dy
        {
            get { return dy; }
        }
    }
}
