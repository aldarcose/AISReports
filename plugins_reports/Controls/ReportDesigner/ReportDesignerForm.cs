using SharedDbWorker.Classes;
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
        private IList<ReportDesignerQuery> reportQueries;

        #region Bindings
        BindingList<ParameterViewModel> parametersVM = new BindingList<ParameterViewModel>();
        BindingList<FieldViewModel> fieldsVM = new BindingList<FieldViewModel>();
        #endregion

        public ReportDesignerForm(ReportParameterCollection parametersCollection)
        {
            InitializeComponent();
            // bindings
            parametersGridView.DataSource = parametersVM;
            fieldsGridView.DataSource = fieldsVM;

            InitParametersTreeView(parametersCollection);
        }

        /// <contentfrom cref="IReportDesignerForm.SetReportFields" />
        public void SetReportFields(IList<ReportField> reportFields)
        {
            if (reportFields.Count == 0)
            {
                groupBox2.Hide();
                nextButton.Text = "Создать отчет";
                nextButton.Click -= nextButton_Click;
                nextButton.Click += buttonOK_Click;
            }
            InitFieldsTreeView(reportFields);
        }

        /// <contentfrom cref="IReportDesignerForm.SetReportQueries" />
        public void SetReportQueries(IList<ReportDesignerQuery> reportQueries)
        {
            this.reportQueries = reportQueries;
        }

        /// <contentfrom cref="IReportDesignerForm.CreateReport" />
        public event EventHandler<ReportDesignerEventArgs> CreateReport;

        #region Init Tree Views

        private void InitParametersTreeView(ReportParameterCollection parametersCollection)
        {
            foreach (var parameterGroup in parametersCollection.Where(p => string.IsNullOrEmpty(p.Name)))
            {
                TreeNode parameterGroupNode = new TreeNode(parameterGroup.Caption);
                parametersTreeView.Nodes.Add(parameterGroupNode);
                PopulateParametersTreeView(parameterGroup.Caption,
                    parametersCollection.Where(p => p.GroupName == parameterGroup.Caption),
                    parameterGroupNode);
            }

            TreeNode fieldTreeNode;
            foreach (var field in parametersCollection.Where(p => 
                string.IsNullOrEmpty(p.GroupName) && !string.IsNullOrEmpty(p.ComparisonExpression)))
            {
                fieldTreeNode = new TreeNode(field.Caption);
                fieldTreeNode.Tag = field;
                parametersTreeView.Nodes.Add(fieldTreeNode);
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

        private void InitFieldsTreeView(IList<ReportField> reportFields)
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

        public void AddParameters(IList<ReportParameter> parameters)
        {
            InitParametersTreeView(new ReportParameterCollection(parameters));
        }

        public void AddFields(IList<ReportField> fields)
        {
            InitFieldsTreeView(fields);
        }

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
            if (parameter == null) return;
            if (parametersVM.Any(pvm => pvm.Name == parameter.Name))
            {
                MessageBox.Show("Параметр уже добавлен", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (parameter.Type == ReportParameterType.CheckExpression)
                parametersVM.Add(new ParameterViewModel(parameter, parameter.ComparisonExpression, "Да"));
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

        private void fieldsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ReportField field = e.Node.Tag as ReportField;
            if (field == null) return;
            if (fieldsVM.Any(fv => fv.Name == field.Name))
            {
                MessageBox.Show("Поле уже добавлено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            fieldsVM.Add(new FieldViewModel(field));
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
            if (gridView != null)
            {
                foreach (DataGridViewRow r in gridView.Rows)
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
            }
        }

        private void showQueryButton_Click(object sender, EventArgs e)
        {
            new QueryForm(ConstructQueryText()).ShowDialog();
        }

        private string ConstructQueryText()
        {
            HashSet<string> leftJoinTables = new HashSet<string>();
            Dictionary<string, ReportDesignerQuery> queriesDict = reportQueries.ToDictionary(q => q.Name, q => q);
            List<string> returnFields = new List<string>();
            List<string> returnParameters = new List<string>();
            List<string> leftJoins = new List<string>();
            List<string> groupingFields  =new List<string>();

            returnFields.AddRange(fieldsVM.Select(fVM => fVM.Expression));
            returnParameters.AddRange(parametersVM.Select(pVM => pVM.Expression));

            foreach (var table in fieldsVM.SelectMany(f => f.Tables))
                leftJoinTables.Add(table);
            foreach (var table in parametersVM.SelectMany(p => p.Tables))
                leftJoinTables.Add(table);
            leftJoins.AddRange(queriesDict.Where(q => leftJoinTables.Contains(q.Key)).Select(q => q.Value.InnerSql));

            if (fieldsVM.Any(f => f.Field.IsGrouping))
                groupingFields.AddRange(fieldsVM.Where(f => !f.Field.IsGrouping).Select(fVM => fVM.Expression));
            string groupBySection = null;
            if (groupingFields.Any())
                groupBySection = "group by " + string.Join(", ", groupingFields);

            ReportDesignerQuery mainQuery = queriesDict["zapros"];
            string queryText =
                mainQuery.InnerSql
                .Replace(":return_fields:", string.Join(", ", returnFields))
                .Replace(":sections:", string.Join(" ", leftJoins))
                .Replace(":where_section:", "where " + string.Join(" and ", returnParameters))
                .Replace(":group_by_section:", groupBySection).Replace(":having_section:", "");

            return queryText;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CreateReport != null)
                CreateReport(this, new ReportDesignerEventArgs(null,
                    new List<ReportField>(fieldsVM.Select(f => f.Field)),
                    new Dictionary<string, string>(parametersVM.ToDictionary(p => p.Caption, p => p.StringValue)),
                    ConstructQueryText()));

            DialogResult = DialogResult.OK;
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

        [Browsable(false)]
        public string Expression
        {
            get { return field.Expression; }
        }

        [Browsable(false)]
        public List<string> Tables
        {
            get { return field.Tables; }
        }

        [Browsable(false)]
        public string Name
        {
            get { return field.Name; }
        }

        [Browsable(false)]
        public ReportField Field
        {
            get { return field; }
        }
    }

    public class ParameterViewModel
    {
        private ReportParameter parameter;
        private object value;
        private string stringValue;
        private string expression;

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

        [Browsable(false)]
        public string Expression
        {
            get 
            {
                if (string.IsNullOrEmpty(expression))
                    expression = GetExpression();
                return expression; 
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return parameter.Name; }
        }

        [Browsable(false)]
        public List<string> Tables
        {
            get { return parameter.Tables; }
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
                    return ((DbResult)value).Fields[1].ToString();
            }
            return null;
        }

        private string GetExpression()
        {
            switch (parameter.Type)
            {
                case ReportParameterType.Enum:
                case ReportParameterType.Text:
                    return string.Format("{0} = {1}", parameter.ComparisonExpression, value);
                case ReportParameterType.VarText:
                    var varText = (Tuple<ComparisonType, string>)value;
                    ComparisonType compType = varText.Item1;
                    string text = varText.Item2; string result = null;
                    switch(compType)
                    {
                        case ComparisonType.IsEmpty: result = string.Format("{0} is null", parameter.ComparisonExpression); break;
                        case ComparisonType.IsNotEmty: result = string.Format("{0} is not null", parameter.ComparisonExpression); break;
                        case ComparisonType.Contains: result = string.Format("upper({0}) like upper('%{1}%')", parameter.ComparisonExpression, text); break;
                        case ComparisonType.EndsWith: result = string.Format("upper({0}) like upper('%{1}')", parameter.ComparisonExpression, text); break;
                        case ComparisonType.StartsWith: result = string.Format("upper({0}) like upper('{1}%')", parameter.ComparisonExpression, text); break;
                        case ComparisonType.Equels: result = string.Format("{0} = {1}", parameter.ComparisonExpression, text); break;
                    }
                    return result;
                case ReportParameterType.Boolean:
                    var chunks = parameter.ComparisonExpression.Split(new string[] { "~|~" }, StringSplitOptions.None);
                    return (bool)value ? chunks[0] : chunks[1];
                case ReportParameterType.CheckExpression:
                    return parameter.ComparisonExpression;
                case ReportParameterType.Date:
                    return string.Format("{0} = '{1:yyyy-MM-dd}'", parameter.ComparisonExpression, (DateTime)value);
                case ReportParameterType.Period:
                    var datePeriod = (Tuple<DateTime, DateTime>)value;
                    return string.Format("{0} >= '{1:yyyy-MM-dd}' and {0} <= '{2:yyyy-MM-dd}'",
                        parameter.ComparisonExpression, datePeriod.Item1, datePeriod.Item2);
                case ReportParameterType.TimePeriod:
                    var timePeriod = (Tuple<DateTime, DateTime>)value;
                    return string.Format("{0} >= '{1:HH:mm:ss}' and {0} <= '{2:HH:mm:ss}'",
                        parameter.ComparisonExpression, timePeriod.Item1, timePeriod.Item2);
                case ReportParameterType.IntPeriod:
                    var intPeriod = (Tuple<int, int>)value;
                    return string.Format("{0} >= {1} and {0} <= {2}", 
                        parameter.ComparisonExpression, intPeriod.Item1, intPeriod.Item2);
                case ReportParameterType.FloatPeriod:
                    var floatPeriod = (Tuple<decimal, decimal>)value;
                    return string.Format("{0} >= {1:n2} and {0} <= {2:n2}", 
                        parameter.ComparisonExpression, floatPeriod.Item1, floatPeriod.Item2);
                case ReportParameterType.Query:
                    var id = ((DbResult)value).Fields[0];
                    if (id.GetType() == typeof(String))
                        return string.Format("{0} = '{1}'", parameter.ComparisonExpression, id);
                    return string.Format("{0} = {1}", parameter.ComparisonExpression, id);
            }
            return null;
        }
    }

    #endregion

    public interface IReportDesignerForm
    {
        void SetReportFields(IList<ReportField> reportFields);

        void SetReportQueries(IList<ReportDesignerQuery> reportQueries);

        event EventHandler<ReportDesignerEventArgs> CreateReport;

        void AddParameters(IList<ReportParameter> parameters);

        void AddFields(IList<ReportField> fields);
    }
}
