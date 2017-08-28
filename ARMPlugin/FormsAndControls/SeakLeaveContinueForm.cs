using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using DevExpress.XtraEditors.Controls;
using Model.Classes.SeakLeave;
using Model;

namespace ARMPlugin.FormsAndControls
{
    public partial class SeakLeaveContinueForm : DevExpress.XtraEditors.XtraForm
    {
        SeakLeaveRepository repo = new SeakLeaveRepository();
        Operator loggedUser;
        SeakLeaveItem slItem;
        
        public SeakLeaveContinueForm(SeakLeaveItem slItem, Operator loggedUser)
        {
            InitializeComponent();
            InitForm();
            this.loggedUser = loggedUser;
            this.slItem = slItem;
        }

        private void InitForm()
        {
            //Доктор
            lueDoctor.Properties.DataSource = CodifiersHelper.GetAllDoctors();
            lueDoctor.Properties.ShowHeader = false;
            lueDoctor.Properties.Columns.Clear();
            lueDoctor.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 75) { Visible = true }
                );
            lueDoctor.Properties.Columns.Add(
                new LookUpColumnInfo("FIO", "ФИО", 200) { Visible = true }
                );
            lueDoctor.Properties.DisplayMember = "FIO";
            lueDoctor.Properties.ValueMember = "Id";
            lueDoctor.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueDoctor.Properties.AutoSearchColumnIndex = 1;

            //lueVkHead
            lueVkHead.Properties.DataSource = CodifiersHelper.GetAllDoctors();
            lueVkHead.Properties.ShowHeader = false;
            lueVkHead.Properties.Columns.Clear();
            lueVkHead.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 75) { Visible = true }
                );
            lueVkHead.Properties.Columns.Add(
                new LookUpColumnInfo("FIO", "ФИО", 200) { Visible = true }
                );
            lueVkHead.Properties.DisplayMember = "FIO";
            lueVkHead.Properties.ValueMember = "Id";
            lueVkHead.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueVkHead.Properties.AutoSearchColumnIndex = 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private SeakLeaveExtend ReadData()
        {
            var item = new SeakLeaveExtend();
            if (deStart.EditValue!=null)
                item.DateFrom = (DateTime)deStart.EditValue;
            if (deDateEnd.EditValue!=null)
                item.DateFor = (DateTime)deDateEnd.EditValue;
            if (lueDoctor.EditValue != null)
                item.DoctorId = (long)lueDoctor.EditValue;
            if (lueVkHead.EditValue != null)
                item.ChiefDoctorId = (long)lueVkHead.EditValue;
            return item;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var item = ReadData();
            var message = string.Empty;
            if (repo.CanSaveExtend(item, out message))
            {
                if (slItem.Extends==null)
                {
                    slItem.Extends = new List<SeakLeaveExtend>();
                }
                if (slItem.Extends.Count==0)
                {
                    item.TypeId = 1;
                }
                else
                {
                    item.TypeId = 0;
                }
                slItem.Extends.Add(item);
                this.Close();
            }
            else
                XtraMessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        

        
    }
}