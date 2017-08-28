using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using DevExpress.XtraReports.UI;
using Model.Classes;
using SharedUtils.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class RecipeReport_okud3108805 : DevExpress.XtraReports.UI.XtraReport
    {
        public RecipeReport_okud3108805(Recipe recipe, int pagenum)
        {
            InitializeComponent();

            
            var barcodeText = BarCodeHelper.Encode(recipe);

            
            barCode.Text = barcodeText;
            
            //save image
            //var image = barCode.ToImage();
            //image.Save("testCode.jpg");


            var patient = new Patient(recipe.PatientId);
            var doctor = new Doctor();
            doctor.LoadData(recipe.DoctorId);

            lbl_doctor_fio.Text = string.Format("{0} {1} {2} ({3})", doctor.LastName, doctor.FirstName, doctor.MidName, doctor.GetDivisionName());
            if (!string.IsNullOrEmpty(doctor.FedCode))
            {
                for (int i = 0; i < doctor.FedCode.Length; i++)
                {
                    var str = doctor.FedCode[i].ToString(CultureInfo.InvariantCulture);
                    switch (i)
                    {
                        case 0:
                            cell_doctor_code1.Text = str;
                            break;
                        case 1:
                            cell_doctor_code2.Text = str;
                            break;
                        case 2:
                            cell_doctor_code3.Text = str;
                            break;
                        case 3:
                            cell_doctor_code4.Text = str;
                            break;
                        case 4:
                            cell_doctor_code5.Text = str;
                            break;
                    }
                }
            }

            lbl_dtd.Text = recipe.MedicamentDtd;

            lbl_fio.Text = string.Format("{0} {1} {2}", patient.LastName, patient.FirstName, patient.MidName);
            if (!string.IsNullOrEmpty(patient.Snils))
            {
                patient.Snils = patient.Snils.Trim();
                for (int i = 0; i < patient.Snils.Length; i++)
                {
                    var str = patient.Snils[i].ToString(CultureInfo.InvariantCulture);
                    table_snils.Rows[0].Cells[i].Text = str;
                }
            }

            if (!string.IsNullOrEmpty(patient.Policy.Serial))
            {
                patient.Policy.Serial = patient.Policy.Serial.Trim();
                for (int i = 0; i < patient.Policy.Serial.Length; i++)
                {
                    var str = patient.Policy.Serial[i].ToString(CultureInfo.InvariantCulture);
                    table_policy.Rows[0].Cells[i].Text = str;
                }
            }
            if (!string.IsNullOrEmpty(patient.Policy.Number))
            {
                patient.Policy.Number = patient.Policy.Number.Trim();
                var start_number = 7;
                for (int i = 0; i < patient.Policy.Number.Length; i++)
                {
                    var str = patient.Policy.Number[i].ToString(CultureInfo.InvariantCulture);
                    table_policy.Rows[0].Cells[start_number + i].Text = str.Trim();
                }
            }
            patient.RegAddress.UpdateText();
            lbl_medcard_number.Text = patient.MedCardNum;

            var patient_days = patient.BirthDate.ToString("dd");
            var patient_months = patient.BirthDate.ToString("MM");
            var patient_years = patient.BirthDate.ToString("yyyy");

            cell_birthdate_d1.Text = patient_days[0].ToString();
            cell_birthdate_d2.Text = patient_days[1].ToString();
            cell_birthdate_m1.Text = patient_months[0].ToString();
            cell_birthdate_m2.Text = patient_months[1].ToString();
            cell_birthdate_y1.Text = patient_years[0].ToString();
            cell_birthdate_y2.Text = patient_years[1].ToString();
            cell_birthdate_y3.Text = patient_years[2].ToString();
            cell_birthdate_y4.Text = patient_years[3].ToString();

            var d_days = recipe.DischargeDate.ToString("dd");
            var d_months = recipe.DischargeDate.ToString("MM");
            var d_years = recipe.DischargeDate.ToString("yyyy");

            cell_date_d1.Text = d_days[0].ToString();
            cell_date_d2.Text = d_days[1].ToString();
            cell_date_m1.Text = d_months[0].ToString();
            cell_date_m2.Text = d_months[1].ToString();
            cell_date_y1.Text = d_years[0].ToString();
            cell_date_y2.Text = d_years[1].ToString();
            cell_date_y3.Text = d_years[2].ToString();
            cell_date_y4.Text = d_years[3].ToString();

            switch (recipe.Validity)
            {
                case 15:
                    line_valid_15d.Visible = true;
                    lbl_valid_month.Font = new Font(lbl_valid_month.Font, FontStyle.Strikeout);
                    lbl_valid_3months.Font = new Font(lbl_valid_3months.Font, FontStyle.Strikeout);
                    break;
                case 30:
                    line_valid_30d.Visible = true;
                    lbl_valid_3months.Font = new Font(lbl_valid_3months.Font, FontStyle.Strikeout);
                    break;
                case 90:
                    line_valid_90d.Visible = true;
                    lbl_valid_month.Font = new Font(lbl_valid_month.Font, FontStyle.Strikeout);
                    break;
            }

            switch (recipe.PayPercent)
            {
                case PayPercent.Free:
                    line_100p.Visible = true;
                    break;
                case PayPercent.P50:
                    line_50p.Visible = true;
                    break;
                default:
                    break;
            }

            switch (recipe.RevenueSource)
            {
                case RevenueType.Federal:
                    line_federal.Visible = true;
                    break;
                case RevenueType.Region:
                    line_region.Visible = true;
                    break;
                case RevenueType.Municipal:
                    line_municipal.Visible = true;
                    break;
                default:
                    break;}

            var diabetRegex = new Regex("^E1[0-4]");
            var m = diabetRegex.Match(recipe.MkbCode);

            recipe.BenefitCode = recipe.BenefitCode.Trim();
            if (recipe.BenefitCode.Equals("30") && m.Success)
                recipe.BenefitCode = "930";

            lblPageNum.Text = "Стр. " + pagenum;

            objectDataSource1.DataSource = recipe;
        }

    }
}
