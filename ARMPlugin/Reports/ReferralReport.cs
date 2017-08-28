using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using DevExpress.XtraReports.UI;
using Model.Classes.Referral;

namespace ARMPlugin.FormsAndControls
{
    public partial class ReferralReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ReferralReport(Referral referral):base()
        {
            InitializeComponent();

            xrLabelNumber.Text = referral.Number.ToString();
            xrLabelLpuFrom.Text = referral.ReferralLpuFrom.ToString();
            xrLabelLpuTo.Text = referral.ReferralLpu.ToString();
            xrLabelSpec.Text = (referral.ReferralSpeciality!=null) ? referral.ReferralSpeciality.Name :string.Empty;
            var patient = referral.Patient;
            xrLabelFIO.Text = string.Format("{0} {1} {2}", 
                    patient.LastName ?? string.Empty, 
                    patient.FirstName ?? string.Empty, 
                    patient.MidName ?? string.Empty);
            xrLabelDr.Text = referral.Patient.BirthDate.ToShortDateString();
            xrLabelPolicy.Text = string.Format("{0} {1}", referral.Patient.Policy.Serial,referral.Patient.Policy.Number);

            xrLabelType.Text = (referral.ReferralType!=null)? referral.ReferralType.Name : string.Empty;

            xrLabelDiagn.Text = referral.MkbCode;
            var doctor = referral.Doctor;
            xrLabelDoctor.Text = string.Format("{0} {1}{2}",
                                    doctor.LastName, 
                                    (doctor.FirstName!=null)? doctor.FirstName[0].ToString().ToUpper() : string.Empty, 
                                    (doctor.MidName!=null) ? doctor.MidName[0].ToString() : string.Empty);

            xrLabelCECDate.Text = string.Empty;
            xrLabelCECHeadDoctor.Text = string.Empty;

            xrLabelDate.Text = (referral.ReferralDate.HasValue) ? referral.ReferralDate.Value.ToShortDateString() : string.Empty;

            if (patient.RegAddress != null)
            {
                patient.RegAddress.UpdateText();
                xrLabelAddress.Text = patient.RegAddress.ToString();
            }
                
        }

    }
}
