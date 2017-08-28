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
    public partial class RegistryWaitingListItemForm : DevExpress.XtraEditors.XtraForm
    {
        public RegistryWaitingListItemForm(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
        }

        public RegistryWaitingListItemForm(Patient patient, WaitingListItem wlItem)
        {
            InitializeComponent();
            this.patient = patient;
            this.wlItem = wlItem;
    
        }

        Patient patient = null;
        WaitingListItem wlItem = null;
        LocalRegistry registry = new LocalRegistry();
        WaitingListRepository repo = new WaitingListRepository();

        private void InitForm()
        {
            //Получаем список специальностей из расписания
            var specList = registry.GetSpecialities();
            lueSpec.Properties.ShowHeader = false;
            lueSpec.Properties.DataSource = specList;
            lueSpec.Properties.DisplayMember = "Name";
            lueSpec.Properties.ValueMember = "Id";
            lueSpec.Properties.Columns.Clear();
            lueSpec.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name"));


            if (this.wlItem != null)
            {
                deDateBegin.EditValue = wlItem.DateBegin;
                deDateEnd.EditValue = wlItem.DateEnd;
                lueSpec.EditValue = wlItem.SpecialityId;
            }
            else
            {
                deDateBegin.EditValue = DateTime.Now;
                deDateEnd.EditValue = DateTime.Now.AddDays(14);
            }

            rgRecordType.SelectedIndex = 0;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (wlItem == null)
                wlItem = new WaitingListItem();
            wlItem.PatientId = patient.Id;
            wlItem.SpecialityId = (long?)lueSpec.EditValue; 
            wlItem.DoctorId = (lueDoctor.EditValue!=null) ? (long?)lueDoctor.EditValue : null;
            wlItem.DateBegin = deDateBegin.EditValue as DateTime?;
            wlItem.DateEnd = deDateEnd.EditValue as DateTime?;
            wlItem.Patient = patient;

            string error = string.Empty;
            wlItem.IsGroup = rgRecordType.SelectedIndex!=0;

            if(!repo.Validate(wlItem,out error))
            {
                MessageBox.Show("Изменения не сохранены: "+ error);
                return;
            }

            if (wlItem.IsGroup.Value)
            {
                if (!repo.AddGroupItems(patient, wlItem))
                {
                    MessageBox.Show("Изменения не сохранены");
                    return;
                }
                this.Close();
                return;
            }
            
            if (repo.AddOrUpdateItem(wlItem))
            {
                this.Close();
                return;
            }
            
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void RegistryWaitingListItemForm_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void lueSpec_EditValueChanged(object sender, EventArgs e)
        {
            var specId = (long?)lueSpec.EditValue;

            var registry = new LocalRegistry();

            var docList = registry.GetDoctors(specId, (DateTime)deDateEnd.EditValue);

            lueDoctor.Properties.DataSource = docList;
            lueDoctor.Properties.Columns.Clear();
            //lueDoctor.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id","ИД"));
            lueDoctor.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FIO", "ФИО"));
            lueDoctor.Properties.ValueMember = "Id";
            lueDoctor.Properties.DisplayMember = "FIO";
            lueDoctor.Properties.ShowHeader = false;

            if (wlItem!=null && wlItem.DoctorId.HasValue)
                lueDoctor.EditValue = wlItem.DoctorId;
        }

    }
}