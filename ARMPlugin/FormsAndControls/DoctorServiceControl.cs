using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using Model.Classes.Codifiers;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class DoctorServiceControl : XtraUserControl
    {
        private List<Service> _services = null;

        public event EventHandler<ServiceAddEventHandler> ServiceAdding;
        public DoctorServiceControl()
        {
            InitializeComponent();
        }

        private long _operatorId = -1;
        private Visit _visit = null;
        public void InitForm(long operatorId, Visit visit)
        {
            _operatorId = operatorId;
            _visit = visit;
            if (_operatorId != _visit.DoctorId)
            {
                btn_service.Enabled = false;
                return;
            }
            btn_service.Enabled = true;
            RefreshData();
        }

        public void RefreshData()
        {
            if (_visit.VisitDate == DateTime.MinValue || _operatorId < 1)
                return;

            grid_services.DataSource = null;
            var q = new DbQuery("GetServices");
            q.Sql =
                "SELECT s.name, s.code, c.date_begin, c.date_end, c.cost, e.sp_uslug_id as service_id  FROM eq_uslug_doctor_tab e " +
                "JOIN eq_uslug_cost_tab c ON c.sp_uslug_id = e.sp_uslug_id AND sp_lpu_id = get_lpu_number()::bigint " +
                "AND c.sp_kind_paid_id = 0 AND @visitDate::date BETWEEN date_begin AND date_end " +
                "LEFT JOIN codifiers.sp_uslug_tab s ON s.id = e.sp_uslug_id " +
                "WHERE e.doctor_id = @doctorId ORDER BY 1,2;";
            q.AddParamWithValue("@visitDate", _visit.VisitDate);
            q.AddParamWithValue("@doctorId", _operatorId);

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
                            Code = DbResult.GetString(result.Fields[1], ""),
                            Id = DbResult.GetNumeric(result.GetByName("service_id"), -1)
                        };
                        _services.Add(item);
                    }
                    grid_services.DataSource = _services;
                    CustomizeGridControl();
                }
            }
        }

        private void CustomizeGridControl()
        {
            var col = grid_services_view.Columns["Id"];
            if (col != null)
                col.Visible = false;

            col = grid_services_view.Columns["Code"];
            if (col != null)
                col.Caption = "Код услуги";

            col = grid_services_view.Columns["Name"];
            if (col != null)
                col.Caption = "Услуга";
        }

        public void ClearServiceList()
        {
            _services = null;
        }

        private void repo_service_btn_Click(object sender, EventArgs e)
        {

        }

        private void grid_services_view_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks > 1)
            {
                btn_service_Click(this, null);
            }
        }

        private void btn_service_Click(object sender, EventArgs e)
        {
            if (grid_services_view.SelectedRowsCount > 0)
            {
                var selectedService = grid_services_view.GetFocusedRow() as Service;

                var f = new AddServiceForm();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (ServiceAdding != null)
                    {
                        var evArgs = new ServiceAddEventHandler()
                        {
                            Service = selectedService,
                            Count = f.Count,
                            Mkb = f.SelectedMkb
                        };
                        ServiceAdding(this, evArgs);
                    }
                    
                }
            }
        }
    }

    public class Service
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ServiceAddEventHandler : EventArgs
    {
        public Service Service { get; set; }
        public Mkb Mkb { get; set; }
        public int Count { get; set; }
    }
}
