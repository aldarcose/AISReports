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
            ReportSchema schema = ReportManager.Instance.LoadDbReportSchema();
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
                var parsForm = new ParametersForm();
                parsForm.Report = report;
                parsForm.Text = report.Name;
                parsForm.Value = new ReportParameterCollection(report.Parameters);
                var presenter = new ReportPresenter(parsForm, this);
                parsForm.ShowDialog();
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
