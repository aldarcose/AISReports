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
using Model.Classes;
using DevExpress.XtraEditors.Controls;
using Model;

namespace ARMPlugin.FormsAndControls
{
    public partial class DopDispDiagnosForm : DevExpress.XtraEditors.XtraForm
    {
        DopDispDiagnos diagnos;
        DopDispRepository repo = new DopDispRepository();
        Operator loggedUser;

        public DopDispDiagnosForm(DopDispDiagnos diagnos, Operator loggedUser)
        {
            InitializeComponent();
            this.diagnos = diagnos;
            this.loggedUser = loggedUser;
            if (diagnos != null)
                dopDispDiagnosBindingSource.Add(diagnos);
            InitForm();
        }

        public DopDispDiagnosForm(Operator loggedUser)
        {
            InitializeComponent();
            this.diagnos = new DopDispDiagnos();
            this.loggedUser = loggedUser;
            dopDispDiagnosBindingSource.Add(diagnos);
            InitForm();
        }

        private void InitForm()
        {
            //МКБ
            MKBCodeLookUpEdit.Properties.DataSource = CodifiersHelper.GetMkbs();
            MKBCodeLookUpEdit.Properties.ShowHeader = false;
            MKBCodeLookUpEdit.Properties.Columns.Clear();
            MKBCodeLookUpEdit.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 40) { Visible = true }
                );
            MKBCodeLookUpEdit.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            MKBCodeLookUpEdit.Properties.DisplayMember = "Code";
            MKBCodeLookUpEdit.Properties.ValueMember = "Code";
            MKBCodeLookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            MKBCodeLookUpEdit.Properties.AutoSearchColumnIndex = 0;

            //Вид диагноза
            DiseaseVidIdLookUpEdit.Properties.DataSource = CodifiersHelper.GetDiseaseTypes();
            DiseaseVidIdLookUpEdit.Properties.ShowHeader = false;
            DiseaseVidIdLookUpEdit.Properties.Columns.Clear();
            DiseaseVidIdLookUpEdit.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 40) { Visible = false }
                );
            DiseaseVidIdLookUpEdit.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            DiseaseVidIdLookUpEdit.Properties.DisplayMember = "Name";
            DiseaseVidIdLookUpEdit.Properties.ValueMember = "Id";
            DiseaseVidIdLookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            DiseaseVidIdLookUpEdit.Properties.AutoSearchColumnIndex = 1;

            //Стадия
            StageIdLookUpEdit.Properties.DataSource = CodifiersHelper.GetDiseaseStadias();
            StageIdLookUpEdit.Properties.ShowHeader = false;
            StageIdLookUpEdit.Properties.Columns.Clear();
            StageIdLookUpEdit.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 40) { Visible = false }
                );
            StageIdLookUpEdit.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            StageIdLookUpEdit.Properties.DisplayMember = "Name";
            StageIdLookUpEdit.Properties.ValueMember = "Id";
            StageIdLookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            StageIdLookUpEdit.Properties.AutoSearchColumnIndex = 1;

            //Doctor
            DoctorIdLookUpEdit.Properties.DataSource = CodifiersHelper.GetDoctors();
            DoctorIdLookUpEdit.Properties.ShowHeader = false;
            DoctorIdLookUpEdit.Properties.Columns.Clear();
            DoctorIdLookUpEdit.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 40) { Visible = false }
                );
            DoctorIdLookUpEdit.Properties.Columns.Add(
                new LookUpColumnInfo("FIO", "ФИО", 200) { Visible = true }
                );
            DoctorIdLookUpEdit.Properties.DisplayMember = "FIO";
            DoctorIdLookUpEdit.Properties.ValueMember = "Id";
            DoctorIdLookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            DoctorIdLookUpEdit.Properties.AutoSearchColumnIndex = 1;

            //Etap
            var etapUtems = new Dictionary<string, string>();
            etapUtems.Add("Первый", "Первый");
            etapUtems.Add("Второй", "Второй");
            EtapLookUpEdit.Properties.DataSource = etapUtems.ToList();
            EtapLookUpEdit.Properties.ShowHeader = false;
            EtapLookUpEdit.Properties.DisplayMember = "Value";
            EtapLookUpEdit.Properties.ValueMember = "Key";
            EtapLookUpEdit.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            EtapLookUpEdit.Properties.AutoSearchColumnIndex = 1;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPrevDiagnos_Click(object sender, EventArgs e)
        {

        }
    
    
    }
}