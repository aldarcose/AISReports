using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Model.Classes.Benefits;
using Model.Classes;
using Model;

namespace ARMPlugin.FormsAndControls
{
    public partial class DispenseryEditForm : DevExpress.XtraEditors.XtraForm
    {

        BenefitRepository repo;
        PatientBenefit patientBenefit;
        Operator user;

        public DispenseryEditForm(long? patientId, Operator user)
        {
            InitializeComponent();
            this.user = user;
            repo = new BenefitRepository();
            Init();
            patientBenefit = new PatientBenefit() { PatientId = patientId };
        }

        public DispenseryEditForm(PatientBenefit patientBenefit, Operator user)
        {
            InitializeComponent();
            this.user = user;
            repo = new BenefitRepository();
            Init();
            this.patientBenefit = patientBenefit;
            InitFormPatientBenefit();
        }

        private void Init()
        {

            //Справочник льгот
            lueLgot.Properties.DataSource = repo.GetBenefits();
            lueLgot.Properties.ShowHeader = false;
            lueLgot.Properties.Columns.Clear();
            lueLgot.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 10) { Visible = false }
                );
            lueLgot.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueLgot.Properties.DisplayMember = "Name";
            lueLgot.Properties.ValueMember = "Code";
            lueLgot.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueLgot.Properties.AutoSearchColumnIndex = 1;

            //МКБ
            lueDiagnos.Properties.DataSource = CodifiersHelper.GetMkbs();
            lueDiagnos.Properties.ShowHeader = false;
            lueDiagnos.Properties.Columns.Clear();
            lueDiagnos.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 40) { Visible = true }
                );
            lueDiagnos.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueDiagnos.Properties.DisplayMember = "Code";
            lueDiagnos.Properties.ValueMember = "Code";
            lueDiagnos.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueDiagnos.Properties.AutoSearchColumnIndex = 0;

            
            //Доктор
            lueDoctor.Properties.DataSource = CodifiersHelper.GetAllDoctors();
            lueDoctor.Properties.ShowHeader = false;
            lueDoctor.Properties.Columns.Clear();
            lueDoctor.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 75) { Visible = true }
                );
            lueDoctor.Properties.Columns.Add(
                new LookUpColumnInfo("FIO", "ФИО", 200) { Visible = true }
                );
            lueDoctor.Properties.DisplayMember = "FIO";
            lueDoctor.Properties.ValueMember = "Id";
            lueDoctor.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueDoctor.Properties.AutoSearchColumnIndex = 1;

            //ЛПУ
            lueLpu.Properties.DataSource = CodifiersHelper.GetMOs().OrderBy(x=>x.Name);
            lueLpu.Properties.ShowHeader = false;
            lueLpu.Properties.Columns.Clear();
            lueLpu.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 10) { Visible = false }
                );
            lueLpu.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueLpu.Properties.DisplayMember = "Name";
            lueLpu.Properties.ValueMember = "Id";
            lueLpu.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueLpu.Properties.AutoSearchColumnIndex = 1;

            //форма взятия на учет
            lueForm.Properties.DataSource = repo.GetEntryReasons();
            lueForm.Properties.ShowHeader = false;
            lueForm.Properties.Columns.Clear();
            lueForm.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueForm.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueForm.Properties.DisplayMember = "Name";
            lueForm.Properties.ValueMember = "Id";
            lueForm.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueForm.Properties.AutoSearchColumnIndex = 1;

            
            //Льготный документ
            lueDocument.Properties.DataSource = repo.GetDocuments();
            lueDocument.Properties.ShowHeader = false;
            lueDocument.Properties.Columns.Clear();
            lueDocument.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueDocument.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueDocument.Properties.DisplayMember = "Name";
            lueDocument.Properties.ValueMember = "Id";
            lueDocument.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueDocument.Properties.AutoSearchColumnIndex = 1;

            //Эпикриз
            lueEpicris.Properties.DataSource = repo.GetEpicrisItems();
            lueEpicris.Properties.ShowHeader = false;
            lueEpicris.Properties.Columns.Clear();
            lueEpicris.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueEpicris.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueEpicris.Properties.DisplayMember = "Name";
            lueEpicris.Properties.ValueMember = "Id";
            lueEpicris.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueEpicris.Properties.AutoSearchColumnIndex = 1;

            //Причина снятия
            lueReason.Properties.DataSource = repo.GetRemovalReasons();
            lueReason.Properties.ShowHeader = false;
            lueReason.Properties.Columns.Clear();
            lueReason.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueReason.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueReason.Properties.DisplayMember = "Name";
            lueReason.Properties.ValueMember = "Id";
            lueReason.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueReason.Properties.AutoSearchColumnIndex = 1;

            lueLpu.EditValue = "2301001";
            lueDoctor.EditValue = user.DoctorId;
        }

        private void InitPatientBenefit()
        {
            if (lueLgot.EditValue!=null)
                patientBenefit.BenefitId = lueLgot.EditValue.ToString();
            if (lueDiagnos.EditValue!=null)
                patientBenefit.Diagnosis = (string)lueDiagnos.EditValue;
            patientBenefit.DoctorId = (long?)lueDoctor.EditValue;
            if (lueLpu.EditValue!=null)
                patientBenefit.EntryLPUId = lueLpu.EditValue.ToString();
            patientBenefit.EntryDate = (DateTime?)deEntryDate.EditValue;
            if (lueForm.EditValue!=null)
                patientBenefit.EntryReasonId = (long?)lueForm.EditValue;
            patientBenefit.RemovalDate = (DateTime?)deRemovalDate.EditValue;
            patientBenefit.RemovalReasonId = (long?)lueReason.EditValue;
            patientBenefit.NextVisit = (DateTime?)deNextDate.EditValue;
            patientBenefit.DocumentId = (long?)lueDocument.EditValue;
            patientBenefit.LKKDate = (DateTime?)deLKKDate.EditValue;
            patientBenefit.EpicrisId = (long?)lueEpicris.EditValue;
            
        }

        private void InitFormPatientBenefit()
        {
            if (patientBenefit.BenefitId!=null)
                lueLgot.EditValue = patientBenefit.BenefitId;
            lueDiagnos.EditValue = patientBenefit.Diagnosis;
            lueDoctor.EditValue = patientBenefit.DoctorId;
            lueLpu.EditValue = patientBenefit.EntryLPUId;
            deEntryDate.EditValue = patientBenefit.EntryDate;
            lueForm.EditValue = patientBenefit.EntryReasonId;
            deRemovalDate.EditValue = patientBenefit.RemovalDate;
            lueReason.EditValue = patientBenefit.RemovalReasonId;
            deNextDate.EditValue = patientBenefit.NextVisit;
            lueDocument.EditValue = patientBenefit.DocumentId;
            deLKKDate.EditValue = patientBenefit.LKKDate;
            lueEpicris.EditValue = patientBenefit.EpicrisId;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            InitPatientBenefit();
            var errors = repo.Validate(patientBenefit, withLgot: false);
            if (errors.Count()>0)
            {
                var text = string.Join(Environment.NewLine,errors);
                XtraMessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                repo.Save(patientBenefit);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}