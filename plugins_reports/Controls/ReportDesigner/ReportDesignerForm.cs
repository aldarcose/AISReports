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
    public partial class ReportDesignerForm : Form
    {
        private List<GroupBox> groupList = new List<GroupBox>();
        private int index;
        private ReportParameterCollection parameters;

        public ReportDesignerForm(ReportParameterCollection parameters)
        {
            InitializeComponent();
            this.parameters = parameters;

            InitParametersTreeView();
        }

        private void InitParametersTreeView()
        {
            foreach (var parameterGroup in parameters.Where(p => string.IsNullOrEmpty(p.Name)))
            {
                TreeNode parameterGroupNode = new TreeNode(parameterGroup.Caption);
                parametersTreeView.Nodes.Add(parameterGroupNode);
                PopulateParametersTreeView(parameterGroup.Caption,
                    parameters.Where(p => p.GroupName == parameterGroup.Caption),
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
                parameterTreeNode.Tag = parameters;
                nodeToAdd.Nodes.Add(parameterTreeNode);
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            groupList.Add(groupBox1);
            groupList.Add(groupBox2);
            groupList[index].BringToFront();
        }

        private void closeButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
