using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Model;
using Model.Classes;
using Model.Classes.Codifiers;
using Model.Classes.Referral;
using DbCaching;
using ARMPlugin.Reports;



namespace ARMPlugin.FormsAndControls
{
    public partial class ReferralForm : XtraForm
    {
        private List<ReferralLpu> _lpus = null;
        private List<ReferralReason> _reasons = null;
        private List<ReferralType> _types = null;
        private List<MedSpeciality> _specs = null;
        private List<Doctor> _doctors = null;
        private List<Mkb> _mkbs = null;
        private List<ReferralCancel> _cancels = null;
        private List<ReferralMedForm> _forms = null;

        public Referral Referral { get; private set; }
        

        public ReferralForm(Referral referral)
        {
            InitializeComponent();

            Referral = referral;

            InitForm();
        }

        public ReferralForm()
        {
            InitCache();
        }

        public  void InitCache()
        {
             _lpus = DbCache.GetItem("referral_lpus") as List<ReferralLpu>;
            if (_lpus == null)
            {
                _lpus = CodifiersHelper.GetReferralLpus();
                DbCache.SetItem("referral_lpus", _lpus, 1800);
            }

             _reasons = DbCache.GetItem("referral_reasons") as List<ReferralReason>;
            if (_reasons == null)
            {
                _reasons = CodifiersHelper.GetReferralReasons();
                DbCache.SetItem("referral_reasons", _reasons, 1800);
            }

             _types = DbCache.GetItem("referral_types") as List<ReferralType>;
            if (_types == null)
            {
                _types = CodifiersHelper.GetReferralTypes();
                DbCache.SetItem("referral_types", _types, 1800);
            }

             _doctors = DbCache.GetItem("referral_doctors") as List<Doctor>;
            if (_doctors == null)
            {
                var task = Task.Factory.StartNew(
                    ()=>{
                        _doctors = CodifiersHelper.GetDoctors();
                        DbCache.SetItem("referral_doctors", _doctors, 1200);
                        }
                    );
            }


            _specs = DbCache.GetItem("referral_specs") as List<MedSpeciality>;
            if (_specs == null)
            {
                _specs = CodifiersHelper.GetMedSpecialities();
                DbCache.SetItem("referral_specs", _specs, 1200);
            }

            _mkbs = DbCache.GetItem("referral_mkbs") as List<Mkb>;
            if (_mkbs == null)
            {
                _mkbs = CodifiersHelper.GetMkbs();
                DbCache.SetItem("referral_mkbs", _mkbs, 1800);
            }


            _forms = DbCache.GetItem("referral_forms") as List<ReferralMedForm>;
            if (_forms == null)
            {
                _forms = CodifiersHelper.GetReferralForms();
                DbCache.SetItem("referral_forms", _forms, 1800);
            }

            _cancels = DbCache.GetItem("referral_cancels") as List<ReferralCancel>;
            if (_cancels == null)
            {
                _cancels = CodifiersHelper.GetReferralCancels();
                DbCache.SetItem("referral_forms", _cancels, 1800);
            }
        }
        

        private void InitForm()
        {

            InitCache();

            cmb_ref_type.Properties.Items.Clear();
            cmb_ref_reason.Properties.Items.Clear();
            cmb_speciality.Properties.Items.Clear();
            cmb_doctor.Properties.Items.Clear();
            cmb_lpu.Properties.Items.Clear();
            //cmb_mkb.Properties.Items.Clear();
            
            cmb_ref_type.Properties.Items.AddRange(_types);
            cmb_ref_reason.Properties.Items.AddRange(_reasons);
            cmb_speciality.Properties.Items.AddRange(_specs);
            cmb_doctor.Properties.Items.AddRange(_doctors);
            //cmb_lpu.Properties.Items.AddRange(_lpus);
            //cmb_mkb.Properties.Items.AddRange(_mkbs);
            cmb_cancel.Properties.Items.AddRange(_cancels);
            cmb_form.Properties.Items.AddRange(_forms);

            if (Referral.ReferralTypeId.HasValue)
                cmb_ref_type.SelectedItem  = _types.FirstOrDefault(t => t.Id == Referral.ReferralTypeId.Value);
            else
                cmb_ref_type.SelectedIndex = 0;

            if (Referral.DoctorId.HasValue)
                cmb_doctor.SelectedItem = _doctors.FirstOrDefault(t => t.Id == Referral.DoctorId.Value);
            else if (Referral.Operator != null)
                cmb_doctor.SelectedItem = _doctors.FirstOrDefault(t => t.Id == Referral.Operator.DoctorId);
            else
                cmb_doctor.SelectedIndex = 0;

            if (Referral.ReferralReasonId.HasValue)
                cmb_ref_reason.SelectedItem = _reasons.FirstOrDefault(t => t.Id == Referral.ReferralReasonId.Value);
            else
                cmb_ref_reason.SelectedIndex = 0;

            if (Referral.ReferralLpu!=null)
            {
                cmb_lpu.TextChanged -= cmb_lpu_TextChanged;
                cmb_lpu.Properties.Items.Add(Referral.ReferralLpu);
                cmb_lpu.SelectedIndex = 0;
                cmb_lpu.TextChanged += cmb_lpu_TextChanged;
                //cmb_lpu.SelectedItem = _lpus.FirstOrDefault(t=>t.Id==Referral.ReferralLpu.Id);
            }

            if (Referral.ReferralSpecialityId.HasValue)
            {
                cmb_speciality.SelectedItem = _specs.FirstOrDefault(t => t.Id == Referral.ReferralSpecialityId.Value);
            }

            te_fio.Text = string.Format("{0} {1} {2}", Referral.Patient.LastName, Referral.Patient.FirstName,
                Referral.Patient.MidName);
            
            de_ref_date.DateTime = Referral.ReferralDate.Value;
            
            de_birthdate.DateTime = Referral.Patient.BirthDate;

            if (Referral.IsNotLocal.HasValue)
            {
                chk_inogorod.Checked = Referral.IsNotLocal.Value;
            }

            if (Referral.IsVoenkomat.HasValue)
            {
                chk_voenkomat.Checked = Referral.IsVoenkomat.Value;
            }

            var mkb = _mkbs.FirstOrDefault(t => t.Code.Equals(Referral.MkbCode));
            if (mkb != null)
            {
                //cmb_mkb.SelectedItem = mkb;
                mkbSearchControl1.SelectedCode = mkb.Code;
            }

            if (Referral.ReferralReasonId.HasValue)
            {
                cmb_ref_reason.SelectedItem = _reasons.FirstOrDefault(t => t.Id == Referral.ReferralReasonId);
            }

            if (Referral.ReferralDate.HasValue)
            {
                de_ref_date.Text = Referral.ReferralDate.Value.ToShortDateString();
            }

            if (Referral.PaymentDate.HasValue)
            {
                de_payment_date.Text = Referral.PaymentDate.Value.ToShortDateString();
            }

            if (Referral.HospitalDate.HasValue)
            {
                de_hosp_date.Text = Referral.HospitalDate.Value.ToShortDateString();
            }

            if (!string.IsNullOrEmpty(Referral.Description))
            {
                memo_desc.Text = Referral.Description;
            }

            if (Referral.Number.HasValue)
            {
                te_number.Text = Referral.Number.Value.ToString();
            }

            if (Referral.Cancel != null)
            {
                cmb_cancel.SelectedItem = Referral.Cancel;
            }

            if (Referral.MedForm != null)
            {
                cmb_form.SelectedItem = Referral.MedForm;
            }

            if (Referral.AgreedDate!=null)
            {
                deAgreedDateTime.EditValue = Referral.AgreedDate;
            }
            
            
        }

        private void cmb_ref_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmb_ref_type.SelectedItem as ReferralType;
            if (item != null)
                Referral.ReferralTypeId = item.Id;
        }

        private void cmb_lpu_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnLpuSelected();
        }

        private void OnLpuSelected()
        {
            var lpu = cmb_lpu.SelectedItem as ReferralLpu;
            if (lpu != null)
            {
                te_address.Text = lpu.Address;
                te_ogrn.Text = lpu.Ogrn;

                cmb_deps.Properties.Items.Clear();
                cmb_deps.EditValue = null;
                cmb_deps.Properties.Items.AddRange(lpu.Departments);
                if (Referral.ReferralLpuDepartmentId.HasValue)
                    cmb_deps.SelectedItem = lpu.Departments.FirstOrDefault(t => t.Id == Referral.ReferralLpuDepartmentId.Value);
                
                Referral.ReferralLpu = lpu;
                Referral.ReferralLpuId = lpu.Id;
            }
        }

        private void cmb_ref_reason_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmb_ref_reason.SelectedItem as ReferralReason;
            if (item != null)
            {
                Referral.ReferralReasonId = item.Id;
                Referral.ReferralReason = item;
            }
        }

        private void cmb_lpu_TextChanged(object sender, EventArgs e)
        {
            
            var filter = cmb_lpu.Text.ToUpperInvariant();
            if (!string.IsNullOrEmpty(filter))
            {
                cmb_lpu.Properties.Items.Clear();

                var filters = filter.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                cmb_lpu.Properties.Items.AddRange(
                    _lpus.Where(t => filters.Any(f => t.FullName.ToUpperInvariant().Contains(f)) ||
                             (string.IsNullOrEmpty(t.FullName) &&
                              filters.Any(f => t.Name.ToUpperInvariant().Contains(f))) ||
                             filters.Any(f => t.Code.ToUpperInvariant().Contains(f))).ToList()
                    );

                cmb_lpu.ShowPopup();
            }
            else
            {
                cmb_lpu.CancelPopup();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var msg = string.Empty;
            if (mkbSearchControl1.SelectedItem!=null)
            {
                Referral.MkbCode = mkbSearchControl1.SelectedItem.Code;
            }

            if (deAgreedDateTime.EditValue!=null)
            {
                Referral.AgreedDate = (DateTime?)deAgreedDateTime.EditValue;
            }
            
            if (!Referral.CanSave(Referral.Operator, out msg))
            {
                XtraMessageBox.Show(msg, "Не сохранено!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Referral.Id = null;
                Referral.Save(Referral.Operator);
                if (Referral.Id.HasValue)
                {
                    te_number.Text = Referral.Number.ToString();
                    XtraMessageBox.Show("Успешно сохранено!", "Направление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (!Referral.Id.HasValue) 
            {
                XtraMessageBox.Show("Предварительно сохраните", "Невозможно распечатать", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var referral = ReferralRepository.GetItem(Referral.Id);
                
            if (referral.ReferralTypeId==1) //госпитализация
            {
                var preview = new HospReport(referral);
                preview.ShowPreview();
            }
            else if (referral.ReferralTypeId == 13 || referral.ReferralTypeId == 12)
            {
                var preview = new BakReport(referral);
                preview.ShowPreview();
            }
            else if (referral.ReferralTypeId == 14) //МРТ
            {
                var preview = new MRTReport(referral);
                preview.ShowPreview();
            }
            else
            {
                var preview = new ReferralReport_057u_04(referral);
                preview.ShowPreview();
            }

        }

        private void cmb_deps_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmb_deps.SelectedItem as ReferralLpuDepartment;
            if (item != null)
                Referral.ReferralLpuDepartmentId = item.Id;
        }

        private void cmb_speciality_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmb_speciality.SelectedItem as MedSpeciality;
            if (item != null)
                Referral.ReferralSpecialityId = item.Id;
        }

        private void memo_desc_EditValueChanged(object sender, EventArgs e)
        {
            var item = memo_desc.Text;
            if (item != null)
                Referral.Description = item;
        }

        private void de_ref_date_DateTimeChanged(object sender, EventArgs e)
        {
            var item = de_ref_date.DateTime;
            Referral.ReferralDate = item;
        }

        private void de_payment_date_DateTimeChanged(object sender, EventArgs e)
        {
            Referral.PaymentDate = de_payment_date.DateTime;
        }

        private void de_hosp_date_DateTimeChanged(object sender, EventArgs e)
        {
            Referral.HospitalDate = de_hosp_date.DateTime;
        }

        private void cmb_doctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = cmb_doctor.SelectedItem as Doctor;
            Referral.DoctorId = item.Id;
        }

        private void chk_inogorod_CheckedChanged(object sender, EventArgs e)
        {
            Referral.IsNotLocal = chk_inogorod.Checked;}

        private void chk_voenkomat_CheckedChanged(object sender, EventArgs e)
        {
            Referral.IsVoenkomat = chk_voenkomat.Checked;
        }

        private void cmb_cancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Referral.Cancel = (cmb_cancel.SelectedItem as ReferralCancel);
        }

        private void cmb_form_SelectedIndexChanged(object sender, EventArgs e)
        {
            Referral.MedForm = (cmb_form.SelectedItem as ReferralMedForm);
        }

        

        
    }
}
