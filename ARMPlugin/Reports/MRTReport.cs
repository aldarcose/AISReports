using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes.Referral;
using System.Linq;


namespace ARMPlugin.Reports
{
    public partial class MRTReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MRTReport(Referral referral) 
        {
            InitializeComponent();

            xrLblNumber.Text = referral.Number.ToString();

            xrLblLpu.Text = referral.ReferralLpuFrom.ToString();

            //xrLblDestOrg.Text = referral.ReferralLpu.ToString();

            xrLblPolisNum.Text = string.Format("{0} {1}", referral.Patient.Policy.Serial, referral.Patient.Policy.Number);
            //if (referral.Patient.FactAddress != null)
            //{
            //    patient.FactAddress.UpdateText();
            //    xrLblActualAddr.Text = patient.FactAddress.ToString();
            //}

            xrLblFIO.Text = string.Format("{0} {1} {2}",
                    referral.Patient.LastName ?? string.Empty,
                    referral.Patient.FirstName ?? string.Empty,
                    referral.Patient.MidName ?? string.Empty);

            xrLblBDate.Text = referral.Patient.BirthDate.ToShortDateString();

            var patient = referral.Patient;

            if (patient.RegAddress != null)
            {
                patient.RegAddress.UpdateText();
                xrLblPatientAddr.Text = patient.RegAddress.ToString();
            }

            if (patient.Phones!=null)
            {
                var numbers= patient.Phones.Select(p=>p.Number);
                lblPhone.Text = string.Join(", ", numbers);
            }

            //xrLblWork.Text = referral.Patient.WorkPlace;

            var doctor = referral.Doctor;
            xrLblDocFIO.Text = string.Format("{0} {1}{2}",
                                    doctor.LastName,
                                    (doctor.FirstName != null) ? doctor.FirstName[0].ToString().ToUpper() : string.Empty,
                                    (doctor.MidName != null) ? doctor.MidName[0].ToString() : string.Empty);


            xrLblMKB.Text = referral.MkbCode;
            
            //if (referral.Patient.Policy != null)
            //{
            //    var smo = new Model.Classes.Codifiers.SMO();
            //    smo.LoadData(referral.Patient.Policy.SmoId);
            //    xrLblSMO.Text = smo.Name;
            //}

            xrTableCell3.Text = xrTableCell3.Text.Replace(":LpuName:", referral.ReferralLpu.FullName);

            if (referral.AgreedDate!=null)
            {
                xrTableCell3.Text = xrTableCell3.Text.Replace(":AgreedTime:", referral.AgreedDate.Value.TimeOfDay.ToString());
                xrTableCell3.Text = xrTableCell3.Text.Replace(":AgreedDate:", referral.AgreedDate.Value.Date.ToShortDateString());
            }
            else
            {
                xrTableCell3.Text = xrTableCell3.Text.Replace(":AgreedTime:", string.Empty);
                xrTableCell3.Text = xrTableCell3.Text.Replace(":AgreedDate:", string.Empty);
            }

            if (referral.ReferralReason!=null)
            {
                xrLabel12.Text = xrLabel12.Text.Replace(":Aim:", referral.ReferralReason.Name);
            }else
            {
                xrLabel12.Text = xrLabel12.Text.Replace(":Aim:", string.Empty);
            }

        }

    }
}
