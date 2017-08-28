using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class VisitResultReport : DevExpress.XtraReports.UI.XtraReport
    {
        public VisitResultReport(VisitReportData data)
        {
            InitializeComponent();
            lbl_fio.Text = data.PatientFio;
            if (data.VisitDate.HasValue)
                lbl_service_date.Text = data.VisitDate.Value.ToString("dd.MM.yyyy");
            lbl_service_place.Text = data.ServicePlace;
            lbl_speciality.Text = data.DoctorSpec;
            lbl_anamnez.Text = data.Anamnez;
            lbl_complains.Text = data.Complains;
            lbl_obj_status.Text = data.ObjectStatus;
            lbl_diagnosis.Text = data.Diagnosis;
            lbl_tap_closed.Text = data.TalonStatus;
            lbl_ill_paper.Text = data.IllPaper;
            lbl_next_visit_date.Text = data.NextVisitDate;
            lbl_doc_fio.Text = data.DoctorFio;
            lblDrugs.Text = data.Drugs;
            lblRecipes.Text = data.Recipes;
            
        }

    }

    public class VisitReportData
    {
        public string PatientFio { get; set; }
        public DateTime? VisitDate { get; set; }
        public string ServicePlace { get; set; }
        public string ServiceTarget { get; set; }
        public string Anamnez { get; set; }
        public string Complains { get; set; }
        public string ObjectStatus { get; set; }
        public string Diagnosis { get; set; }
        public string TalonStatus { get; set; }
        public string IllPaper { get; set; }
        public string NextVisitDate { get; set; }
        public string DoctorFio { get; set; }
        public string DoctorSpec { get; set; }
        public string Drugs { get; set; }
        public string Recipes { get; set; }
    }
}
