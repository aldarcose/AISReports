using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model;
using Model.Classes;
using Model.Classes.Vaccination;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars;
using DevExpress.Utils.Menu;
using DevExpress.XtraReports.UI;

namespace ARMPlugin.FormsAndControls
{
    public partial class VaccinationControl : DevExpress.XtraEditors.XtraUserControl
    {

        public Operator LoggedUser { get; set; }
        
        public Patient Patient {
            get { return patient; }
            set { patient =value; InitForm(); }
        }

        Patient patient;

        public VaccinationControl()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            var repo = new VaccinationRepository();
            if (Patient == null)
                return;
            grid.DataSource = repo.GetItems(patientId: Patient.Id);

            gv.OptionsBehavior.Editable = false;
            gv.Columns.Clear();
            gv.Columns.Add(new GridColumn() { 
                Visible = true, Caption = "Препарат", FieldName = "PreparatName"
            });
            
            gv.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Инфекция",
                FieldName = "InfectionName"
            });

            gv.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Вид",
                FieldName = "TypeName"
            });

            gv.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Дата",
                FieldName = "Date"
            });

            gv.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Доза",
                FieldName = "Dosage"
            });

            gv.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Серия",
                FieldName = "Series"
            });

            gv.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Реакция",
                FieldName = "ReactionName"
            });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            using(var form = new VaccinationEditItemForm(Global.Operator, Patient))
            {
                form.ShowDialog();
            }
            ReloadItems();
        }

        

        private void barManager_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void gv_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.Menu.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.User)
            {
                int rowHandle = e.HitInfo.RowHandle;
                e.Menu.Items.Clear();
                var itemChange = new DXMenuItem("Изменить", new EventHandler(OnChangeClick));
                itemChange.Tag = rowHandle;
                e.Menu.Items.Add(itemChange);

                var itemDelete = new DXMenuItem("Удалить", new EventHandler(OnDeleteClick));
                itemDelete.Tag = rowHandle;
                e.Menu.Items.Add(itemDelete);
            }
        }


        void OnChangeClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            var rowHandle = (int)item.Tag;
            var vacItem = gv.GetRow(rowHandle) as Vaccination;
            using (var form = new VaccinationEditItemForm(LoggedUser, Patient,vacItem.Id))
            {
                form.ShowDialog();
            }
            ReloadItems();
        }

        public void ReloadItems()
        {
            var repo = new VaccinationRepository();
            grid.DataSource = repo.GetItems(patientId: Patient.Id);
        }

        void OnDeleteClick(object sender, EventArgs e)
        {
            DXMenuItem item = sender as DXMenuItem;
            var rowHandle = (int)item.Tag;
            var vacItem = gv.GetRow(rowHandle) as Vaccination;
            var repo = new VaccinationRepository();
            repo.RemoveItem(vacItem.Id.Value);
            ReloadItems();
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using(var form = new Reports.VaccinationReport(patient) )
            {
                form.ShowPreviewDialog();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadItems();
        }



    }
}
