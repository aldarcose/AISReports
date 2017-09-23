using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reports.Controls
{
    /// <summary>
    /// Форма мастера отчетов
    /// </summary>
    public partial class ReportDesignerForm : Form, IReportDesignerForm
    {
        private List<GroupBox> groupList = new List<GroupBox>();
        private int index;
        private ReportParameterCollection parameterCollection;
        private IList<ReportField> reportFields;

        #region Bindings
        BindingList<ParameterViewModel> parametersVM = new BindingList<ParameterViewModel>();
        BindingList<FieldViewModel> fieldsVM = new BindingList<FieldViewModel>();
        #endregion

        public ReportDesignerForm(ReportParameterCollection parameterCollection)
        {
            InitializeComponent();
            this.parameterCollection = parameterCollection;
            // bindings
            parametersGridView.DataSource = parametersVM;
            fieldsGridView.DataSource = fieldsVM;

            InitParametersTreeView();
        }

        public void SetReportFields(IList<ReportField> reportFields)
        {
            this.reportFields = reportFields;
            InitFieldsTreeView();
        }

        #region Init Tree Views

        private void InitParametersTreeView()
        {
            foreach (var parameterGroup in parameterCollection.Where(p => string.IsNullOrEmpty(p.Name)))
            {
                TreeNode parameterGroupNode = new TreeNode(parameterGroup.Caption);
                parametersTreeView.Nodes.Add(parameterGroupNode);
                PopulateParametersTreeView(parameterGroup.Caption,
                    parameterCollection.Where(p => p.GroupName == parameterGroup.Caption),
                    parameterGroupNode);
            }
        }

        private void PopulateParametersTreeView(
            string groupName, IEnumerable<ReportParameter> groupParameters, TreeNode nodeToAdd)
        {
            TreeNode parameterTreeNode;
            foreach (var parameter in groupParameters)
            {
                parameterTreeNode = new TreeNode(parameter.Caption);
                parameterTreeNode.Tag = parameter;
                nodeToAdd.Nodes.Add(parameterTreeNode);
            }
        }

        private void InitFieldsTreeView()
        {
            foreach (var fieldGroup in reportFields.Where(f => string.IsNullOrEmpty(f.Name)))
            {
                TreeNode fieldGroupNode = new TreeNode(fieldGroup.Caption);
                fieldsTreeView.Nodes.Add(fieldGroupNode);
                PopulateFieldsTreeView(fieldGroup.Caption,
                    reportFields.Where(p => p.GroupCaption == fieldGroup.Caption),
                    fieldGroupNode);
            }
        }

        private void PopulateFieldsTreeView(
            string groupName, IEnumerable<ReportField> groupFields, TreeNode nodeToAdd)
        {
            TreeNode fieldTreeNode;
            foreach (var field in groupFields)
            {
                fieldTreeNode = new TreeNode(field.Caption);
                fieldTreeNode.Tag = field;
                nodeToAdd.Nodes.Add(fieldTreeNode);
            }
        }

        #endregion

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (index < groupList.Count - 1)
                groupList[++index].BringToFront();
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (index > 0)
                groupList[--index].BringToFront();
        }

        private void ReportDesignerForm_Load(object sender, EventArgs e)
        {
            groupList.Add(groupBox1);
            groupList.Add(groupBox2);
            groupList[index].BringToFront();
        }

        private void closeButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void parametersTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ReportParameter parameter = e.Node.Tag as ReportParameter;
            if (parameter != null)
            {
                if (parameter.Type == ReportParameterType.CheckExpression)
                    parametersVM.Add(new ParameterViewModel(parameter, parameter.ComparedExpression, "Да"));
                else
                {
                    var parameterForm = new ParametersForm();
                    parameterForm.Text = "Параметры выборки";
                    parameterForm.Value = new ReportParameterCollection(parameter);
                    parameterForm.OK += (s_, e_) =>
                    {
                        Dictionary<string, Tuple<string, object>> parameterValues = e_.ParametersValues;
                        if (parameterValues != null && parameterValues.Any())
                        {
                            var firstParameterValue = parameterValues.First();
                            object value = firstParameterValue.Value.Item2;
                            parametersVM.Add(new ParameterViewModel(parameter, value));
                        }
                    };
                    parameterForm.ShowDialog();
                }
            }
        }

        #region DataGridViews context menu items
        private void parametersGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = parametersGridView.HitTest(e.X, e.Y);
                parametersGridView.ClearSelection();
                if (hti.RowIndex >= 0)
                    parametersGridView.Rows[hti.RowIndex].Selected = true;
            }
        }

        private void DeleteParameterRow_Click(object sender, EventArgs e)
        {
            int rowToDelete = parametersGridView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (rowToDelete >= 0)
            {
                parametersGridView.Rows.RemoveAt(rowToDelete);
                parametersGridView.ClearSelection();
            }
        }

        private void fieldsGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = fieldsGridView.HitTest(e.X, e.Y);
                fieldsGridView.ClearSelection();
                if (hti.RowIndex >= 0)
                    fieldsGridView.Rows[hti.RowIndex].Selected = true;
            }
        }

        private void DeleteFieldRow_Click(object sender, EventArgs e)
        {
            Int32 rowToDelete = fieldsGridView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            if (rowToDelete >= 0)
            {
                fieldsGridView.Rows.RemoveAt(rowToDelete);
                fieldsGridView.ClearSelection();
            }
        }
        #endregion

        private void parametersGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                foreach (DataGridViewRow r in gridView.Rows)
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
            }
        }
    }

    #region ViewModels

    public class FieldViewModel
    {
        private ReportField field;

        public FieldViewModel(ReportField field)
        {
            this.field = field;
        }

        [DisplayName("Название поля")]
        public string Caption
        {
            get { return field.Caption; }
        }
    }

    public class ParameterViewModel
    {
        private ReportParameter parameter;
        private object value;
        private string stringValue;

        public ParameterViewModel(ReportParameter parameter, object value)
        {
            this.parameter = parameter;
            this.value = value;
        }

        public ParameterViewModel(ReportParameter parameter, object value, string stringValue)
            : this(parameter, value)
        {
            this.stringValue = stringValue;
        }

        [DisplayName("Параметр")]
        public string Caption
        {
            get { return parameter.Caption; }
        }

        [Browsable(false)]
        public object Value
        {
            get { return value; }
        }

        [DisplayName("Значение")]
        public string StringValue 
        {
            get 
            {
                if (string.IsNullOrEmpty(stringValue))
                    stringValue = ParseStringValue();
                return stringValue; 
            } 
        }

        private string ParseStringValue()
        {
            switch(parameter.Type)
            {
                case ReportParameterType.Enum:
                case ReportParameterType.Text:
                    return value.ToString();
                case ReportParameterType.VarText:
                    var varText = (Tuple<ComparisonType, string>)value;
                    ComparisonType compType = varText.Item1;
                    string text = varText.Item2;
                    string description = Utils.GetEnumDescription(compType);
                    if (compType == ComparisonType.IsEmpty || compType == ComparisonType.IsNotEmty)
                        return description;
                    return string.Format("{0} {1}", description, text);
                case ReportParameterType.Boolean:
                    return (bool)value ? "Да" : "Нет";
                case ReportParameterType.Date :
                    return ((DateTime)value).ToShortDateString();
                case ReportParameterType.Period:
                    var datePeriod = (Tuple<DateTime, DateTime>)value;
                    return string.Format("с {0:dd.MM.yyyy} по {1:dd.MM.yyyy}", datePeriod.Item1, datePeriod.Item2);
                case ReportParameterType.TimePeriod:
                    var timePeriod = (Tuple<DateTime, DateTime>)value;
                    return string.Format("с {0:HH.mm.ss} по {1:HH.mm.ss}", timePeriod.Item1, timePeriod.Item2);
                case ReportParameterType.IntPeriod:
                    var intPeriod = (Tuple<int, int>)value;
                    return string.Format("с {0:d} по {1:d}", intPeriod.Item1, intPeriod.Item2);
                case ReportParameterType.FloatPeriod:
                    var floatPeriod = (Tuple<decimal, decimal>)value;
                    return string.Format("с {0:n2} по {1:n2}", floatPeriod.Item1, floatPeriod.Item2);
                case ReportParameterType.Query:
                    return ((SharedDbWorker.Classes.DbResult)value).Fields[1].ToString();
            }
            return null;
        }
    }

    #endregion

    public interface IReportDesignerForm
    {
        void SetReportFields(IList<ReportField> reportFields);
    }
}
