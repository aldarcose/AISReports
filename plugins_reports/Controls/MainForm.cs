using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SharedDbWorker.Classes;
using System.Threading;
using Reports.Controls;
using System.Drawing;
using System.Diagnostics;
using SharedDbWorker;

namespace Reports
{
    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
            InitTreeView();
        }

        private void InitTreeView()
        {
            ReportSchema schema = null;
            var lf = new LoadingForm
            {
                WaitForThis = new System.Threading.Tasks.Task(() =>
                {
                    schema = ReportManager.Instance.LoadDbReportSchema();
                })
            };
            lf.ShowDialog();
            if (schema == null) return;
            foreach (var folder in schema.Folders)
            {
                TreeNode treeNode = new TreeNode(folder.Name, 0, 0);
                treeView1.Nodes.Add(treeNode);
                PopulateTreeView(folder, treeNode);
            }

            foreach (var report in schema.Reports)
            {
                TreeNode treeNode = new TreeNode(report.Name, 1, 1);
                treeNode.Tag = report;
                treeView1.Nodes.Add(treeNode);
            }
        }

        private void PopulateTreeView(Folder folder, TreeNode nodeToAdd)
        {
            TreeNode reportTreeNode; TreeNode folderTreeNode;
            foreach (var report in folder.Reports)
            {
                reportTreeNode = new TreeNode(report.Name, 1, 1);
                reportTreeNode.Tag = report;
                nodeToAdd.Nodes.Add(reportTreeNode);
            }

            foreach (var subFolder in folder.Folders)
            {
                folderTreeNode = new TreeNode(subFolder.Name, 0, 0);
                nodeToAdd.Nodes.Add(folderTreeNode);
                PopulateTreeView(subFolder, folderTreeNode);
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            object tag = e.Node.Tag;
            Report report = tag as Report;
            if (report != null)
            {
                var connection = new Connection(ConnectionParameters.Instance);
                var paramerCollection = new ReportParameterCollection(report.Parameters);
                if (!report.IsDesigned)
                {
                    var parsForm = new ParametersForm();
                    parsForm.Text = report.Name;
                    parsForm.Value = paramerCollection;
                    var presenter = new ReportPresenter(connection, parsForm, this, report);
                    parsForm.ShowDialog();
                }
                else
                {
                    var reportDesignerForm = new ReportDesignerForm(paramerCollection);
                    var presenter = new ReportDesignerPresenter(
                        connection, this, reportDesignerForm, report);
                    reportDesignerForm.ShowDialog();
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IEnumerable<string> files = Directory.GetFiles(Path.GetTempPath(), "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => s.EndsWith(".xls") || s.EndsWith(".xlsx"));

            try
            {
                foreach (string file in files)
                    File.Delete(file);
            }
            catch (Exception) { }
        }

        public void Disable()
        {
            this.InvokeIfNeeded(() => Enabled = false);
        }

        public void Enable()
        {
            this.InvokeIfNeeded(() => Enabled = true);
        }
    }

    public interface IMainForm
    {
        void Disable();

        void Enable();
    }
}
