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
using Model.Classes.SeakLeave;
using Model;


namespace ARMPlugin.FormsAndControls
{
    public partial class SeakLeaveAddWorkForm : DevExpress.XtraEditors.XtraForm
    {
        private Operator loggedUser;
        private WorkPlaceItem workPlaceItem;

        public WorkPlaceItem WorkPlace { 
            get {
                return workPlaceItem;
                } 
        }

        public SeakLeaveAddWorkForm(Operator loggedUser)
        {
            InitializeComponent();
            InitForm();
            this.loggedUser = loggedUser;
        }

        public void InitForm()
        {
            var mask = "\\p{Lu}+";
            
            txtShort.Properties.Mask.EditMask = mask;
            txtShort.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            txtFull.Properties.Mask.EditMask = mask;
            txtFull.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var repo = new SeakLeaveRepository();
            workPlaceItem = new WorkPlaceItem() { FullName = txtFull.Text, Name = txtShort.Text };
            repo.AddWorkPlaceItem(workPlaceItem, loggedUser.Id);
        }
    }
}