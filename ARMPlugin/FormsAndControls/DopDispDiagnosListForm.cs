using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes.DopDisp;

namespace ARMPlugin.FormsAndControls
{
    public partial class DopDispDiagnosListForm : DevExpress.XtraEditors.XtraForm
    {
        long patientId;
        DopDispRepository repo;
        public DopDispDiagnosListForm(long patientId)
        {
            InitializeComponent();
            this.patientId = patientId;
            repo = new DopDispRepository();

        }

        private void InitData()
        {
            if (patientId==null)
                return;

            gridControl1.DataSource = repo.GetAllDiagnosList(patientId);
            
           
        }

        

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}