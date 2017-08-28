using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using Model.Classes.Codifiers;
using Model.Classes.Registry;

namespace ARMPlugin.FormsAndControls
{
    public partial class AddNewVisitForm : XtraForm
    {
        private List<VisitPurpose> _purposes = null;

        public DateTime VisitDate { get; set; }
        public VisitPurpose Purpose { get; set; }

        public Patient Patient { get; set; }
        public long DoctorId { get; set; }
        public long TalonId { get; set; }

        public AddNewVisitForm()
        {
            InitializeComponent();

            var defaultPurposeId = 1; // ИД Лечебно-профилактическая

            VisitDate = DateTime.Now;
            _purposes = CodifiersHelper.GetVisitPurposes();
            Purpose = _purposes.FirstOrDefault(t => t.Id == defaultPurposeId);

            de_visitdate.DateTime = VisitDate;
            cmb_target.Properties.Items.AddRange(_purposes.Select(t=>t.Name).ToList());

            if (Purpose!=null)
                cmb_target.Text = Purpose.Name;
        }

        private void de_visitdate_DateTimeChanged(object sender, EventArgs e)
        {
            VisitDate = de_visitdate.DateTime;
        }

        private void cmb_target_SelectedValueChanged(object sender, EventArgs e)
        {
            var name = cmb_target.Text;
            Purpose = _purposes.FirstOrDefault(t => t.Name.Equals(name));
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (Purpose != null)
            {
                this.DialogResult = DialogResult.OK;

                var repo = new LocalRegistry() ;
                var visits = repo.GetAppointmentsWithoutTime(DoctorId, VisitDate.Date);

                if (visits.Any(p=>p.PatientId!=Patient.Id))
                    repo.SetAppointment(DoctorId, Patient.Id, VisitDate, withTime: false, userId: DoctorId, talonId:TalonId);
                //else
                //    XtraMessageBox.Show("Пациент уже записан на эту дату!", "Внимание", MessageBoxButtons.OK);
                
            }
            else
            {
                XtraMessageBox.Show("Укажите цель посещения", "Внимание", MessageBoxButtons.OK);
            }
        }
    }
}
