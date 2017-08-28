using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes;
using Model.Classes.Laboratory;
using System.Linq;
using System.Collections.Generic;
using Model.Classes.Codifiers;

namespace ARMPlugin.FormsAndControls
{
    public partial class LabOrderReport : DevExpress.XtraReports.UI.XtraReport
    {
        public LabOrderReport(LabOrder order)
        {
            InitializeComponent();
            if (order == null) return;

            order.Load(order.Id);
            
            if (order.HasResults)
                order.GetLabResults();

            if (order.Patient != null)
            {
                switch (order.Patient.Gender)
                {
                    case Gender.Unknown:
                        lbl_gender.Text = "Не указан";
                        break;
                    case Gender.Male:
                        lbl_gender.Text = "Мужской";
                        break;
                    case Gender.Female:
                        lbl_gender.Text = "Женский";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                lbl_age.Text = GetAge(DateTime.Now, order.Patient.BirthDate).ToString();
                if (order.Voenkomat.HasValue && order.Voenkomat.Value==true)
                {
                    if (!order.PatientData.Contains("Военкомат"))
                        order.PatientData = order.PatientData+" (Военкомат)";
                }
                
            }

            if (order.Doctor != null)
            {
                lbl_div.Text = order.DoctorDivision;
            }

            if (order.CreateTime!=default(DateTime))
            {
                xrLblRequestDate.Text = order.CreateTime.ToShortDateString();
            }

            if (order.Id != default(long))
            {
                xrLblNumber.Text = order.Id.ToString();
            }

            if (order.HasResults)
            {
                xrLblAnswerDate.Text = order.ResponseTime.ToShortDateString();
                xrLblExams.Text = string.Empty;
                xrLabel7.Text = "Результат исследований";
            }
            else
            {
                xrLblExams.Text = string.Empty;
                xrLblExams.Multiline = true;
                
                foreach(var exam in order.Exams)
                {
                    
                    xrLblExams.Text += exam.Name ;
                    //var parameters = string.Join(", ", exam.Parameters.Select(s=>s.Name));
                    //xrLblExams.Text = xrLblExams.Text + parameters + ")";

                    xrLblExams.Text += Environment.NewLine;
                }
                
                //var exams = string.Join("\n", order.Exams.Select(s => s.Name));
                //Detail.Controls.Add()
                //xrLblExams.Text = exams;
                xrLabel1.Text = string.Empty;
                xrLabel2.Text = string.Empty;
                xrLabel3.Text = string.Empty;
                xrLabel10.Text = string.Empty;
                xrLabel21.Text = string.Empty;
                order.Exams = null;
            }

            if (order.Patient.RegAddress != null)
            {
                lblAddress.Text = order.Patient.RegAddress.ToString();
            }
            if (order.Patient.FactAddress!=null)
            {
                lblAddress.Text = order.Patient.FactAddress.ToString();
            }

            if(order.Patient.UchastokDopId!=-1)
            {
                var medArea = new MedArea();
                medArea.LoadData(order.Patient.UchastokDopId);
                lblUchastok.Text = medArea.Name;
            }

            if (!string.IsNullOrEmpty(order.SendedLpuCode))
            {
                lblLPU.Text = CodifiersHelper.GetMOs().Where(c => c.Id.ToString() == order.SendedLpuCode).FirstOrDefault().Name;
            }

            if (!string.IsNullOrEmpty(order.MKBCode))
            {
                lblMKB.Text = order.MKBCode;
            }
            

            objectDataSource1.DataSource = order;
        }

        /// <summary>
        /// Возвращает кол-во полных лет
        /// </summary>
        /// <param name="date1">На данную дату</param>
        /// <returns></returns>
        public int GetAge(DateTime date1, DateTime date2)
        {
            int result = 0;
            // определяем количество лет
            var years = date1.Year - date2.Year;

            // вычисляем разницу месяцев
            var month = date1.Month - date2.Month;
            // если др не в этом месяце
            if (month != 0)
            {
                // если ДР в след. месяцах
                return (month < 0) ? years - 1 : years;
            }

            // если др в текущем месяце, вычисляем разницу дней
            var days = DateTime.Now.Day - date2.Day;
            // если др еще не было в этом месяце
            return (days < 0) ? years - 1 : years;
        }

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLblAbnorm.Text
            //((System.Data.DataRowView)GetCurrentRow()).Row[""];

            ExamParameter examParam = (ExamParameter)DetailReport1.GetCurrentRow();
            if (examParam!=null && examParam.Result!=null && examParam.Result.AbnormalFlag!=string.Empty)
            {
                xrLabel9.Font = new Font(xrLabel9.Font.FontFamily, xrLabel9.Font.Size, FontStyle.Bold);
            }
            else
            {
                xrLabel9.Font = new Font(xrLabel9.Font.FontFamily, xrLabel9.Font.Size, FontStyle.Regular);
            }
        }

    }
}
