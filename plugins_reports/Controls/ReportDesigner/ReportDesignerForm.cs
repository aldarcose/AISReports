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
        List<GroupBox> groupList = new List<GroupBox>();
        int index;

        public ReportDesignerForm()
        {
            InitializeComponent();
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
