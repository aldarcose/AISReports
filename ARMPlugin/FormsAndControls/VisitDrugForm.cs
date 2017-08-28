using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using DbCaching;
using System.Text.RegularExpressions;
using Model;

namespace ARMPlugin.FormsAndControls
{
    public partial class VisitDrugForm : DevExpress.XtraEditors.XtraForm
    {
        private List<Medicament> _medicaments = null;

        public Visit Visit
        {
            get;
            set;
        }

        public long? OperatorId
        {
            get;
            set;
        }

        public long? PatientId
        {
            get;
            set;
        }

        public Drug Drug
        {
            get;
            set;
        }

        public VisitDrugForm(long? operatorId, Visit visit)
        {
            InitializeComponent();
            Visit = visit;
            OperatorId = operatorId;
            InitForm();
        }

        public VisitDrugForm(long? operatorId, Visit visit,Drug drug)
        {
            InitializeComponent();
            Visit = visit;
            OperatorId = operatorId;
            InitForm();
            SetDrugInfo(drug);
            Drug = drug;
        }

        private void InitForm()
        {
            if (DbCache.GetItem("medicaments") != null)
            {
                _medicaments = DbCache.GetItem("medicaments") as List<Medicament>;
            }
            else
            {
                _medicaments = CodifiersHelper.GetMedicaments();
                DbCache.SetItem("medicaments", _medicaments, 3600);
            }
            cBoxLS.Properties.Items.AddRange(
                _medicaments.Select(t => string.Format("{0} ({3}), {1}, {2}, {4}", 
                t.TrnmRus, t.Doze, t.Producer, t.Code, t.FormName)).ToArray() 
                );

            var repo = new DiagnoseRepository();
            var result=repo.GetByTalon(Visit.TalonId);
            cBoxDiagnose.Properties.Items.AddRange(result.Select(t=>t.MkbCode).ToArray());

            lueInjections.Properties.ValueMember = "Id";
            lueInjections.Properties.DisplayMember = "Name";
            lueInjections.Properties.ShowHeader = false;
            lueInjections.Properties.DataSource= CodifiersHelper.GetInjectionTypes().ToArray();
        }

        private void SetDrugInfo(Drug drug)
        {
            memoEditComment.Text = drug.Comments;
            if (drug.DateBegin.HasValue)
                dateBegin.Text = drug.DateBegin.Value.ToShortDateString();
            if (drug.DateEnd.HasValue)
            {
                dateEnd.Text = drug.DateEnd.Value.ToShortDateString();
            }

            txtDose.Text = drug.Dose;
            
            if (drug.InjectionTypeId.HasValue)
                lueInjections.EditValue = drug.InjectionTypeId;
            
            txtLSName.Text=drug.MedicamentName;
            cBoxDiagnose.SelectedItem = drug.MkbCode;
            txtSigna.Text = drug.Signa;

            if (!string.IsNullOrEmpty(drug.MedicamentCode))
            {
                //cBoxLS.SelectedItem = _medicaments.Where(p => p.Code == drug.MedicamentCode).FirstOrDefault();
                var result = _medicaments.Where(p => p.Code == drug.MedicamentCode)
                              .Select(t => string.Format("{0} ({3}), {1}, {2}, {4}", t.TrnmRus, t.Doze, t.Producer, t.Code, t.FormName))
                              .FirstOrDefault();
                cBoxLS.EditValue = result;
            }

            OperatorId=drug.OperatorId;
            PatientId=drug.PatientId;

        }

        private Drug GetDrugInfo()
        {
            var drug = new Drug();
            drug.Comments = memoEditComment.Text;
            DateTime _dateBegin = new DateTime();
            if (DateTime.TryParse(dateBegin.Text, out _dateBegin))
                drug.DateBegin = _dateBegin;
            DateTime _dateEnd = new DateTime();
            if (DateTime.TryParse(dateEnd.Text, out _dateEnd))
                drug.DateEnd = _dateEnd;
            drug.Dose = txtDose.Text;
            if (lueInjections.EditValue!=null)
                drug.InjectionTypeId = Convert.ToInt64(lueInjections.EditValue);
            drug.MedicamentName = txtLSName.Text;
            drug.MkbCode = cBoxDiagnose.Text;
            drug.Signa = txtSigna.Text;

            if (SelectedMedicament!=null)
            {
                drug.MedicamentCode = SelectedMedicament.Code;
            }

            drug.OperatorId = OperatorId;
            drug.VisitId = Visit.Id;
            drug.PatientId = Visit.PatientId;

            return drug;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            var drug = GetDrugInfo();
            if (Drug!=null && Drug.Id.HasValue)
                drug.Id = Drug.Id;
            var repo = new DrugRepository();
            string error = null;
            if (repo.CanSave(drug, out error))
            {
                repo.Save(drug, Visit.Id);
                XtraMessageBox.Show("Данные сохранены", "Сохранение", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(error, "Валидация", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private Medicament SelectedMedicament
        {
            get
            {
                var filter = cBoxLS.Text;
                if (string.IsNullOrEmpty(filter))
                    return null;

                var m = Regex.Match(filter, @".*?\((\d+)\)");
                if (m.Success)
                {
                    var code = m.Groups[1].Value;
                    var medicament =
                        _medicaments.FirstOrDefault(
                            t =>
                                t.Code.Equals(code));
                    return medicament;
                }
                return null;
            }
        }

        private void cBoxLS_TextChanged(object sender, EventArgs e)
        {
            var filter = cBoxLS.Text;
            if (!string.IsNullOrEmpty(filter))
            {
                var normalFilter = filter.ToLower();
                cBoxLS.Properties.Items.Clear();

                cBoxLS.Properties.Items.AddRange(
                        _medicaments.Where(
                            t => t.TrnmRus.ToLower().StartsWith(normalFilter) || t.TrnmRus.ToLower().StartsWith(filter))
                            .Select(t => string.Format("{0} ({3}), {1}, {2}, {4}", t.TrnmRus, t.Doze, t.Producer, t.Code, t.FormName))
                            .ToList());
                if (cBoxLS.Properties.Items.Count == 0 && SelectedMedicament != null)
                {
                    txtSigna.Text = SelectedMedicament.Signa;
                    txtLSName.Text = SelectedMedicament.MnnRus;
                    txtDose.Text = SelectedMedicament.Doze;
                }

            }
        }
    }
}