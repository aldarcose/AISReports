using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes.Referral;
using System.Linq;

namespace ARMPlugin.Reports
{
    public partial class ReferralReport_057u_04 : DevExpress.XtraReports.UI.XtraReport
    {
        public ReferralReport_057u_04(Referral referral)
        {
            InitializeComponent();

            lblNumber.Text = referral.Number.ToString();
            lblOrg.Text = referral.ReferralLpu.ToString();
            var dopInfo = string.Empty;
            
            if (referral.ReferralLpuDep!=null)
            {
                dopInfo = referral.ReferralLpuDep.Name;
            }

            if (referral.ReferralSpeciality != null)
            {
                dopInfo+=" "+ referral.ReferralSpeciality.Name;
            }

            if (!string.IsNullOrEmpty(dopInfo))
            {
                lblDepSpec.Text =  dopInfo;
            }
            else
            {
                lblDepSpec.Text = string.Empty;
            }

            var patient = referral.Patient;
            lblFIO.Text = string.Format("{0} {1} {2}",
                    patient.LastName ?? string.Empty,
                    patient.FirstName ?? string.Empty,
                    patient.MidName ?? string.Empty);

            lblBirthDate .Text = referral.Patient.BirthDate.ToShortDateString();
            lblPolis.Text = string.Format("{0}", referral.Patient.Policy.Number);

            lblMKB.Text = referral.MkbCode;

            lblDate.Text = (referral.ReferralDate.HasValue) ? referral.ReferralDate.Value.ToShortDateString() : string.Empty;

            if (patient.RegAddress != null)
            {
                patient.RegAddress.UpdateText();
                lblAddress.Text = patient.RegAddress.ToString();
            }
            else
            {
                lblAddress.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(patient.WorkPlace))
            {
                lblWork.Text = patient.WorkPlace;
            }else
            {
                lblWork.Text = string.Empty;
            }

            if (patient.Lgotas!=null && patient.Lgotas.Count>0)
            {
                lblLgota.Text = patient.Lgotas.FirstOrDefault().LgotaCode;
            }
            else
            {
                lblLgota.Text = String.Empty;
            }

            var doctor = referral.Doctor;
            if (!string.IsNullOrEmpty(doctor.PositionName))
            {
                lblDolg.Text = doctor.PositionName;
            }
            else
            {
                lblDolg.Text = string.Empty;
            }

            lblDolg.Text += " " + doctor.FIO;

            if (referral.ReferralReason!=null)
            {
                lblReason.Text = referral.ReferralReason.Name;
            }else
            {
                lblReason.Text = string.Empty;
            }

            lblHead.Text = "__________________________________________";

            if (!string.IsNullOrEmpty(referral.Description))
            {
                lblReason.Text = referral.Description;
            }

        }

    }
}
