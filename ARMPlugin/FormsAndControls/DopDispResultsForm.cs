using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ARMPlugin.FormsAndControls
{
    public partial class DopDispResultsForm : DevExpress.XtraEditors.XtraForm
    {
        public DopDispResultsForm()
        {
            InitializeComponent();
        }

        private void resultsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        public void InitForm()
        {
            var values = new String[] { "по матери", "по отцу" };
            InhBloodCmb.Properties.Items.Add(values);
        }
    }
}