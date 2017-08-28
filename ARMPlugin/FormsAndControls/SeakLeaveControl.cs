using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes.SeakLeave;
using Model;
using Model.Classes;
using DevExpress.XtraGrid.Columns;

namespace ARMPlugin.FormsAndControls
{
    public partial class SeakLeaveControl : DevExpress.XtraEditors.XtraUserControl
    {
        SeakLeaveRepository repo = new SeakLeaveRepository();

        public Patient Patient { get; set; }

        public Operator Operator { get; set; }



        public SeakLeaveControl()
        {
            
            InitializeComponent();
            InitForm();
        }

        public void InitForm()
        {
            gridview.OptionsBehavior.Editable = false;
            gridview.Columns.Clear();
            gridview.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Номер",
                FieldName = "Number"
            });
            gridview.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Дата начала",
                FieldName = "DateBegin"
            });
            gridview.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Дата окончания",
                FieldName = "DateEnd"
            });
            gridview.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Тип листка",
                FieldName = "TypeName"
            });

            //gridview.Columns.Add(new GridColumn()
            //{
            //    Visible = true,
            //    Caption = "Врач, открывший ВН",
            //    FieldName = "Doctor.FIO"
            //});

            gridview.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Причина",
                FieldName = "CauseName"
            });
            
            
            if (Patient == null)
                return;

            ReloadGrid();


        }

        private void ReloadGrid()
        {
            if (Patient != null)
            {
                var items = repo.GetItems(Patient.Id);
                grid.DataSource = items;
                //grid.Refresh();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadGrid();
        }

        private void btnNewDoc_Click(object sender, EventArgs e)
        {

            
            using (var form = new SickLeaveEditForm(Patient, Operator) )
            {
                form.ShowDialog();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var item = gridview.GetFocusedRow() as SeakLeaveListItem;
            if (item == null)
                return;
            using(var form  = new SickLeaveEditForm(item.Id.Value, Patient, Operator))
            {
                form.ShowDialog();
            }
            ReloadGrid();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var item = gridview.GetFocusedRow() as SeakLeaveListItem;
            if (item!=null && item.Id.HasValue)
            {
                repo.Delete(item.Id.Value);
                ReloadGrid();
            }

        }

        

        
    }
}
