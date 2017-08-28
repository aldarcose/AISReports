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
using Model.Classes.Registry;

namespace ARMPlugin.FormsAndControls
{
    public partial class RegistryWaitingListForm : DevExpress.XtraEditors.XtraForm
    {
        public RegistryWaitingListForm(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            InitForm();

        }

        WaitingListRepository repo = new WaitingListRepository();
        Patient patient = null;
        WaitingListItem selectedItem;

        private void InitForm()
        {
            ReloadData();
            gvWaitList.OptionsBehavior.Editable = false;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using(var form = new RegistryWaitingListItemForm(patient))
            {
                var result= form.ShowDialog();
            }
            ReloadData();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            using (var form = new RegistryWaitingListItemForm(patient, selectedItem))
            {
                var result = form.ShowDialog();
            }
            ReloadData();
        }

        private void ReloadData()
        {
            gridWaitList.DataSource = repo.GetItems(patient.Id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            repo.RemoveItem(selectedItem.Id);
            ReloadData();
        }

        private void gvWaitList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            selectedItem = gvWaitList.GetRow(e.FocusedRowHandle) as WaitingListItem;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

    }
}