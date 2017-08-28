using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class ProvidedServicesControl : XtraUserControl
    {
        private List<Service> _services = null;
        public ProvidedServicesControl()
        {
            InitializeComponent();
            
        }

        private Visit _visit = null;

        public void InitForm(long @operator, Visit visit)
        {
            _visit = visit;
            btn_delete.Enabled = @operator == visit.DoctorId;
            
            RefreshData();
        }


        public void RefreshData()
        {
            if (_visit == null)
                return;
            
            gridControl.DataSource = null;

            var q = new DbQuery("GetProvidedServices");
            q.Sql = "SELECT c.name, o.kol_vo, o.id, o.sp_uslug_id, o.inference_diagnosis FROM okaz_uslug_tab o " +
                "LEFT JOIN codifiers.sp_uslug_tab c ON c.id = o.sp_uslug_id " +
                "WHERE table_link_name = 'visit_tab' AND  table_link_id = @visitId;";
            q.AddParamWithValue("@visitId", _visit.Id);
            using (var db = new DbWorker())
            {
                var results = db.GetResults(q);
                if (results != null && results.Count > 0)
                {
                    _services = new List<Service>();
                    foreach (var result in results)
                    {
                        var item = new Service()
                        {
                            Name = DbResult.GetString(result.Fields[0], ""),
                            Count = (int) DbResult.GetNumeric(result.Fields[1], 0),
                            Id = DbResult.GetNumeric(result.Fields[2], -1),
                            ServiceId = DbResult.GetNumeric(result.Fields[3], -1),
                            Diagn = DbResult.GetString(result.Fields[4], "")
                        };
                        _services.Add(item);
                    }
                    gridControl.DataSource = _services;
                }

                CustomizeGridControl();
            }

        }

        private void CustomizeGridControl()
        {
            var col = grid_control_view.Columns["Id"];
            if (col != null)
                col.Visible = false;

            col = grid_control_view.Columns["ServiceId"];
            if (col != null)
                col.Visible = false;

            col = grid_control_view.Columns["Name"];
            if (col != null)
                col.Caption = "Услуга";

            col = grid_control_view.Columns["Count"];
            if (col != null)
                col.Caption = "Количество";

            col = grid_control_view.Columns["Diagn"];
            if (col != null)
                col.Caption = "Диагноз";

            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (grid_control_view.SelectedRowsCount == 0)
                return;

            if (
                XtraMessageBox.Show("Вы собираетесь удалить оказанную услугу. Продолжить?", "Внимание",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var serviceToDelete = grid_control_view.GetFocusedRow() as Service;
                if (serviceToDelete != null)
                {
                    using (var db = new DbWorker())
                    {
                        var q = new DbQuery("DeleteService");
                        q.Sql = "DELETE FROM okaz_uslug_tab WHERE id = @id;";
                        q.AddParamWithValue("@id", serviceToDelete.Id);
                        var r = db.Execute(q);

                        if (r < 1)
                        {
                            // не удалилось
                        }
                    }
                }
                RefreshData();
            }
        }
        internal class Service
        {
            public long Id { get; set; }

            public string Name { get; set; }
            public int Count { get; set; }
            
            public long ServiceId { get; set; }
            public string Diagn { get; set; }
        }
    }
}
