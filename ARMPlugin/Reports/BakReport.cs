using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes.Referral;
using System.Linq;

namespace ARMPlugin.FormsAndControls
{
    public partial class BakReport : DevExpress.XtraReports.UI.XtraReport
    {
        public BakReport(Referral referral)
        {
            InitializeComponent();

            xrLblNumber.Text = referral.Number.ToString();
            
            xrLblLpu.Text = referral.ReferralLpuFrom.ToString();
            
            xrLblDestOrg.Text = referral.ReferralLpu.ToString();
                

            xrLblPolisNum.Text = string.Format("{0} {1}", referral.Patient.Policy.Serial, referral.Patient.Policy.Number);

            xrLblFIO.Text = string.Format("{0} {1} {2}",
                    referral.Patient.LastName ?? string.Empty,
                    referral.Patient.FirstName ?? string.Empty,
                    referral.Patient.MidName ?? string.Empty);

            xrLblBDate.Text = referral.Patient.BirthDate.ToShortDateString();
            
            var patient = referral.Patient;
            
            if (patient.RegAddress!=null)
            {
                patient.RegAddress.UpdateText();
                xrLblPatientAddr.Text = patient.RegAddress.ToString();
            }
            
            xrLblWork.Text = referral.Patient.WorkPlace;
            
            var doctor = referral.Doctor;
            xrLblDocFIO.Text = string.Format("{0} {1}{2}",
                                    doctor.LastName,
                                    (doctor.FirstName != null) ? doctor.FirstName[0].ToString().ToUpper() : string.Empty,
                                    (doctor.MidName != null) ? doctor.MidName[0].ToString() : string.Empty);

            
            xrLblMKB.Text = referral.MkbCode;
            if (referral.Patient.Policy!=null)
            {
                var smo = new Model.Classes.Codifiers.SMO();
                smo.LoadData(referral.Patient.Policy.SmoId);
                xrLblSMO.Text = smo.Name;
            }
            
            if (referral.Patient.FactAddress!=null)
            {
                patient.FactAddress.UpdateText();
                xrLblActualAddr.Text = patient.FactAddress.ToString();
            }

        }

    }
}
