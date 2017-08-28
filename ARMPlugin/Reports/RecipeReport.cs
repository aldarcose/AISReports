using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using DevExpress.XtraReports.UI;
using Model.Classes;
using SharedUtils.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class RecipeReport : DevExpress.XtraReports.UI.XtraReport
    {
        public RecipeReport(Recipe recipe)
        {
            InitializeComponent();

            var barcode = BarCodeHelper.Encode(recipe);

            barCode.Text = barcode;

            var patient = new Patient(recipe.PatientId);
            var doctor = new Doctor();
            doctor.LoadData(recipe.DoctorId);

            lbl_doctor_fio.Text = string.Format("{0} {1} {2}", doctor.LastName, doctor.FirstName, doctor.MidName);
            lbl_doctor_fed_code.Text = doctor.FedCode;

            lbl_fio.Text = string.Format("{0} {1} {2}", patient.LastName, patient.FirstName, patient.MidName);
            lbl_snils.Text = patient.Snils;
            lbl_birthdate.Text = patient.BirthDate.ToString("dd.MM.yyyy");

            var days = recipe.DischargeDate.ToString("dd");
            var months = recipe.DischargeDate.ToString("MM");
            var years = recipe.DischargeDate.ToString("yyyy");
            cell_dischargeDate_d1.Text = days[0].ToString();
            cell_dischargeDate_d2.Text = days[1].ToString();
            cell_dischargeDate_m1.Text = months[0].ToString();
            cell_dischargeDate_m2.Text = months[1].ToString();
            cell_dischargeDate_y1.Text = years[0].ToString();
            cell_dischargeDate_y2.Text = years[1].ToString();
            cell_dischargeDate_y3.Text = years[2].ToString();
            cell_dischargeDate_y4.Text = years[3].ToString();

            switch (recipe.Validity)
            {
                case 5:
                    line_5d.Visible = true;
                    breakline_valid_3months.Visible = true;
                    breakline_valid_month.Visible = true;
                    break;
                case 10:
                    line_10d.Visible = true;
                    breakline_valid_3months.Visible = true;
                    breakline_valid_month.Visible = true;
                    break;
                case 30:
                    line_30d.Visible = true;
                    breakline_valid_3months.Visible = true;
                    break;
                case 90:
                    line_90d.Visible = true;
                    breakline_valid_month.Visible = true;
                    break;
            }

            switch (recipe.PayPercent)
            {
                case PayPercent.Free:
                    line_free.Visible = true;
                    break;
                case PayPercent.P50:
                    line_half.Visible = true;
                    break;
                default:
                    break;
            }

            switch (recipe.RevenueSource)
            {
                case RevenueType.Federal:
                    line_fed.Visible = true;
                    break;
                case RevenueType.Region:
                    line_subject1.Visible = true;
                    line_subject2.Visible = true;
                    break;
                case RevenueType.Municipal:
                    line_municip.Visible = true;
                    break;
                default:
                    break;
            }

            lbl_dtd.Text = recipe.MedicamentDtd;

            var diabetRegex = new Regex("^E1[0-4]");
            var m = diabetRegex.Match(recipe.MkbCode);

            if (recipe.BenefitCode.Equals("30") && m.Success)
                recipe.BenefitCode = "930";

            objectDataSource1.DataSource = recipe;
        }

    }
}
