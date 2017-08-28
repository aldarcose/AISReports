using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NetrikaServices;
using Model.Netrika;
using DevExpress.XtraGrid.Columns;

namespace ARMPlugin.FormsAndControls
{
    public partial class RegistryHistoryForm : DevExpress.XtraEditors.XtraForm
    {

        private string idPatient=null;
        private int? idLpu=null;
        public RegistryHistoryForm(int? idLpu, string idPatient)
        {
            InitializeComponent();
            this.idPatient = idPatient;
            this.idLpu = idLpu;
            ReloadGridHistory();
            
        }

        private void ReloadGridHistory()
        {
            using(var client = new Registry())
            {
                var result = client.GetPatientHistory(idLpu, idPatient);
                gridHistory.DataSource = result;

                gvHistory.Columns.Clear();
                GridColumn docCol = gvHistory.Columns.AddField("Doctor");
                docCol.Caption="Врач";
                docCol.Visible = true;

                GridColumn visitDate = gvHistory.Columns.AddField("VisitStart");
                visitDate.DisplayFormat.FormatString = "g";
                visitDate.OptionsColumn.AllowEdit=false;
                visitDate.Caption = "Время приема";
                visitDate.Visible = true;

                
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            var pHistory = gvHistory.GetFocusedRow() as PatientHistory;
            using(var client = new Registry())
            {
                if (client.CreateClaimForRefusal(idLpu,idPatient, pHistory.IdAppointment))
                {
                    ReloadGridHistory();
                }else
                {
                    XtraMessageBox.Show("Не удалось отменить запись");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}