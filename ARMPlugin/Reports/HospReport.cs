using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes.Referral;
using System.Linq;

namespace ARMPlugin.FormsAndControls
{
    public partial class HospReport : DevExpress.XtraReports.UI.XtraReport
    {
        public HospReport(Referral referral)
        {
            InitializeComponent();

            xrLblLpu.Text = referral.ReferralLpuFrom.ToString();
            xrLblOGRN.Text = referral.ReferralLpuFrom.Ogrn;
            xrLblAddr.Text = referral.ReferralLpuFrom.Address;
            xrLblDestOrg.Text = referral.ReferralLpu.ToString();
            xrLblNumber.Text = referral.Number.ToString();
                

            xrLblPolisNum.Text = string.Format("{0} {1}", referral.Patient.Policy.Serial, referral.Patient.Policy.Number);

            //if (referral.Patient.Lgotas != null && referral.Patient.Lgotas.Count > 0)
            //{
            //     var result= referral.Patient.Lgotas.Where(s => s.IsValid(DateTime.Now)).FirstOrDefault();
            //     if (result != null)
            //         xrLblBenefitCode.Text = result.LgotaCode;
            //}

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

            xrLblDate.Text = (referral.ReferralDate.HasValue) ? referral.ReferralDate.Value.ToShortDateString() : string.Empty;
            xrLblMKB.Text = referral.MkbCode;

        }

    }
}
