using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes.Registry;
using Model.Classes;
using SharedDbWorker;
using Model;
using System.Linq;

namespace ARMPlugin.Reports
{
    public partial class RegistryTalonReport : DevExpress.XtraReports.UI.XtraReport
    {
        public RegistryTalonReport(long talonDoctorId, Patient patient, Operator @operator=null)
        {
            InitializeComponent();
            this.talonDoctorId = talonDoctorId;
            this.patient = patient;
            this.@operator = @operator;
            InitForm();
        }

        private long talonDoctorId;
        private Talon talon;
        private Patient patient;
        private long docId;
        private Doctor doctor;
        private Operator @operator;


        private void InitForm()
        {
            this.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            
            var registry = new LocalRegistry();
            var apptHistItem = registry.GetAppointmentHistoryItem(talonDoctorId);
            doctor = new Doctor();
            doctor.LoadData(apptHistItem.DoctorId.Value);
            patient = new Patient();
            patient.LoadData(apptHistItem.PatientId.Value);

            lblOperator.Text = string.Format("оператор {0} {1} {2}",@operator.LastName, @operator.FirstName, @operator.MiddleName);
            lblDateCreate.Text = apptHistItem.CreateDateTime.Value.Date.ToShortDateString();
            lblTimeCreate.Text = apptHistItem.CreateDateTime.Value.ToShortTimeString();
            lblCabinet.Text = apptHistItem.Cabinet;
            lblApptDateTime.Text = apptHistItem.AppointmentDateTime.ToString();
            lblDoctor.Text = doctor.ToString();
            lblPolis.Text = patient.Policy.Serial+ " " +patient.Policy.Number;
            lblAmbCard.Text = patient.MedCardNum;
            lblPatFIO.Text = string.Format("{0} {1} {2}", patient.LastName, patient.FirstName, patient.MidName);
            lblSMO.Text = patient.Policy.SmoName;
            lblBirthDate.Text = patient.BirthDate.ToShortDateString();
            if (patient.Gender == Gender.Male)
                lblPol.Text = "мужской";
            else if (patient.Gender == Gender.Female)
                lblPol.Text = "женский";
            else
                lblPol.Text = string.Empty;
            lblTerritor.Text = patient.Policy.SmoRegion;
            patient.RegAddress.UpdateText();
            lblAddress.Text = patient.RegAddress.ToString();
  
            if (patient.CategoryId!=0)
            {
                lblCategory.Text = CodifiersHelper.GetBenefitCategories().FirstOrDefault(c => c.Id == patient.CategoryId).Name; 
            }else
            {
                lblCategory.Text = "нет";
            }

            //Группы инвалидности
            if (patient.Lgotas.Any() && patient.Lgotas.Where(l => l.LgotaCode == "081" || l.LgotaCode == "082" || l.LgotaCode == "083").Any())
            {
                var group = patient.Lgotas.FirstOrDefault(l => l.LgotaCode == "081" || l.LgotaCode == "082" || l.LgotaCode == "083").LgotaName;    
                lblInvGroup.Text = group;
                lblGroup.Text = group;
            }
            if (!patient.Lgotas.Any())
            {
                lblInvGroup.Text = "нет";
                lblGroup.Text = "нет";
            }


            if (patient.SocStatusId!=-1)
            {
                lblSocialStatus.Text = CodifiersHelper.GetSocStatuses().
                                    Where(f=>f.Id == patient.SocStatusId).FirstOrDefault().Name;
            }

            if (patient.UchastokId!=-1)
            {
                lblUchastok.Text = CodifiersHelper.GetUchaski().FirstOrDefault(f=>f.Id==patient.UchastokId).Name;
            }

            if (!string.IsNullOrEmpty(patient.WorkPlace))
            {
                lblWorkPlace.Text = patient.WorkPlace;
            }

            if (patient.Snils!=null)
            {
                lblSNILS.Text = patient.Snils;
            }

            if (patient.LpuAttachCode!=null)
            {
                lblAttach.Text = CodifiersHelper.GetLPUName(patient.LpuAttachCode);
            }

            
        }


    }
}
