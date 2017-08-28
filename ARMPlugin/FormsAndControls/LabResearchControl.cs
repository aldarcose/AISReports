using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ARMPlugin.Presenter;
using DevExpress.Charts.Native;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Model;
using Model.Classes;
using Model.Classes.Laboratory;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace ARMPlugin.FormsAndControls
{
    public partial class LabResearchControl : XtraUserControl, ILabResearchControl
    {
        private DbQuery _ordersQuery;
        private bool _isAddButtonVisible;

        public LabResearchControl()
        {
            InitializeComponent();
            IsAddButtonVisible = true;
            btn_preview.Enabled = true;
        }

        public Patient Patient { get; set; }

        private List<LabOrder> LabOrders { get; set; }
        public event EventHandler<LabOrderResearchEventArgs> LabOrderSelected;

        public void RefreshLabOrders()
        {
            if (Patient != null)
            {
                GetLabOrders();

                grid_lab.DataSource = LabOrders;
            }
        }

        public DbQuery OrdersQuery
        {
            get
            {
                if (_ordersQuery == null)
                {
                    _ordersQuery = new DbQuery("GetLabOrders")
                    {
                        Sql = "SELECT id FROM laboratory.\"order\" where dan_id = @dan_id order by id desc;"
                    };
                    _ordersQuery.AddParamWithValue("@dan_id", Patient.PatientId);
                }
                else
                {
                    _ordersQuery.CommandParams.Clear();
                    _ordersQuery.AddParamWithValue("@dan_id", Patient.PatientId);
                }
                return _ordersQuery;
            }
            set { _ordersQuery = value; }
        }

        private void GetLabOrders()
        {
            LabOrders = new List<LabOrder>();
            var repo = new LabRepository();
            LabOrders=repo.GetOrders(Patient.PatientId).ToList();
        }

        public LabOrder SelectedLabOrder
        {
            get
            {
                if (grid_labView.SelectedRowsCount > 0)
                {
                    return grid_labView.GetFocusedRow() as LabOrder;
                }
                return null;
            }
        }
        public Action CreateOrderAction { get; set; }

        public bool IsAddButtonVisible
        {
            get { return _isAddButtonVisible; }
            set
            {
                _isAddButtonVisible = value;
                layout_btnAdd_Item.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
            }
        }

        public Action OpenOrderAction { get; set; }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (CreateOrderAction != null)
            {
                CreateOrderAction();
                RefreshLabOrders();
            }
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            if (OpenOrderAction != null)
            {
                OpenOrderAction();
            }
        }

        private void grid_labView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var order = SelectedLabOrder;

            if (order != null)
            {
                if (LabOrderSelected != null)
                {
                    LabOrderSelected(this, new LabOrderResearchEventArgs()
                    {
                        SelectedLabOrder = order
                    });
                }

                //btn_preview.Enabled = order.HasResults;
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            RefreshLabOrders();
        }
    }
}
