using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Model.Classes;
using Model.Classes.Vaccination;

namespace ARMPlugin.Reports
{
    public partial class VaccinationReport : DevExpress.XtraReports.UI.XtraReport
    {

        VaccinationRepository repo;
        Patient patient;

        
        public VaccinationReport(Patient patient)
        {
            InitializeComponent();
            repo = new VaccinationRepository();
            this.patient = patient;
            InitForm();
        }

        
        public void InitForm()
        {
            if (patient!=null)
            {
                lblPatient.Text = patient.ToString();
                lblAddress.Text = patient.RegAddress.ToString();
                
                var items = repo.GetItems(patient.Id);
                tblVacItems.BeginInit();
                foreach(var item in items)
                {
                    XRTableRow row = new XRTableRow();
                    var cellDate = new XRTableCell() { WidthF = 90};
                    cellDate.Text = item.Date.Value.ToShortDateString();
                    row.Cells.Add(cellDate);
                    row.Cells.Add(new XRTableCell() { Text = item.PreparatName, WidthF=380});
                    row.Cells.Add(new XRTableCell() { Text = item.TypeName, WidthF=125, TextAlignment=DevExpress.XtraPrinting.TextAlignment.MiddleCenter });
                    row.Cells.Add(new XRTableCell() { Text = item.Dosage.ToString(), WidthF = 74, TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter });
                    row.Cells.Add(new XRTableCell() { Text = item.Series, WidthF = 145, TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter });

                    tblVacItems.Rows.Add(row);
                }
                tblVacItems.EndInit();

            }
        }

    }
}
