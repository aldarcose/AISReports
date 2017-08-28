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
using Model;
using Model.Classes.SeakLeave;
using DevExpress.XtraEditors.Controls;

namespace ARMPlugin.FormsAndControls
{
    public partial class SickLeaveEditForm : DevExpress.XtraEditors.XtraForm
    {
        Patient patient;
        Operator loggedUser;
        SeakLeaveRepository repo = new SeakLeaveRepository();
        SeakLeaveItem slItem;

        public SickLeaveEditForm(long slItemId, Patient patient, Operator user)
        {
            InitializeComponent();
            InitForm();
            this.patient = patient;
            loggedUser = user;
            slItem = repo.GetItem(slItemId);
            if (this.patient != null)
                InitData();
        }

        public SickLeaveEditForm( Patient patient, Operator user)
        {
            InitializeComponent();
            InitForm();
            this.patient = patient;
            loggedUser = user;
            slItem = new SeakLeaveItem();
            if (this.patient != null)
                InitData();
        }



        private void InitForm()
        {
            
            //luePol
            Dictionary<int, string> polDic = new Dictionary<int, string>();
            polDic.Add(-1, "");
            polDic.Add(1, "м");
            polDic.Add(2, "ж");

            luePol.Properties.DataSource = polDic.ToList();
            luePol.Properties.DisplayMember = "Value";
            luePol.Properties.ValueMember = "Key";


            //luework
            lueWork.Properties.DataSource = repo.GetWorkPlaceItems();
            lueWork.Properties.DisplayMember = "Name";
            lueWork.Properties.ValueMember = "Id";
            lueWork.Properties.ShowHeader = false;
            lueWork.Properties.Columns.Clear();
            lueWork.Properties.Columns.Add( 
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueWork.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Name", 100) { Visible = true }
                );
            lueWork.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueWork.Properties.AutoSearchColumnIndex = 1;

            //lueCode
            lueCode.Properties.DataSource = repo.GetCauses();
            lueCode.Properties.DisplayMember = "Code";
            lueCode.Properties.ValueMember = "Id";
            lueCode.Properties.ShowHeader = false;
            lueCode.Properties.Columns.Clear();
            lueCode.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueCode.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 100) { Visible = true }
                );
            lueCode.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueCode.Properties.AutoSearchColumnIndex = 1;

            //lueCodeDop
            lueCodeDop.Properties.DataSource = repo.GetCausesAdditional();
            lueCodeDop.Properties.DisplayMember = "Code";
            lueCodeDop.Properties.ValueMember = "Id";
            lueCodeDop.Properties.ShowHeader = false;
            lueCodeDop.Properties.Columns.Clear();
            lueCodeDop.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueCodeDop.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 100) { Visible = true }
                );
            lueCodeDop.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueCodeDop.Properties.AutoSearchColumnIndex = 1;

            //
            //lueCode
            lueCodeChng.Properties.DataSource = repo.GetCauses();
            lueCodeChng.Properties.DisplayMember = "Code";
            lueCodeChng.Properties.ValueMember = "Id";
            lueCodeChng.Properties.ShowHeader = false;
            lueCodeChng.Properties.Columns.Clear();
            lueCodeChng.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueCodeChng.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 100) { Visible = true }
                );
            lueCodeChng.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueCodeChng.Properties.AutoSearchColumnIndex = 1;

            //lueTalon
            lueTalon.Properties.DataSource = repo.GetCauses();
            lueTalon.Properties.DisplayMember = "Code";
            lueTalon.Properties.ValueMember = "Id";
            lueTalon.Properties.ShowHeader = false;
            lueTalon.Properties.Columns.Clear();
            lueTalon.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = true }
                );
            lueTalon.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueTalon.Properties.AutoSearchColumnIndex = 0;

            //lueViolationCode
            lueViolationCode.Properties.DataSource = repo.GetViolations();
            lueViolationCode.Properties.DisplayMember = "Name";
            lueViolationCode.Properties.ValueMember = "Id";
            lueViolationCode.Properties.ShowHeader = false;
            lueViolationCode.Properties.Columns.Clear();
            lueViolationCode.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueViolationCode.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Name", 100) { Visible = true }
                );
            lueViolationCode.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueViolationCode.Properties.AutoSearchColumnIndex = 1;

            //ЛПУ
            lueLpu.Properties.DataSource = CodifiersHelper.GetMOs().OrderBy(x => x.Name);
            lueLpu.Properties.ShowHeader = false;
            lueLpu.Properties.Columns.Clear();
            lueLpu.Properties.Columns.Add(
                new LookUpColumnInfo("Code", "Code", 10) { Visible = false }
                );
            lueLpu.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueLpu.Properties.DisplayMember = "Name";
            lueLpu.Properties.ValueMember = "Id";
            lueLpu.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueLpu.Properties.AutoSearchColumnIndex = 1;


            //lueOtherCode
            lueOtherCode.Properties.DataSource = repo.GetOtherCauses();
            lueOtherCode.Properties.ShowHeader = false;
            lueOtherCode.Properties.Columns.Clear();
            lueOtherCode.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueOtherCode.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueOtherCode.Properties.DisplayMember = "Name";
            lueOtherCode.Properties.ValueMember = "Id";
            lueOtherCode.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueOtherCode.Properties.AutoSearchColumnIndex = 1;

            //lueMSEInvGroup
            lueMSEInvGroup.Properties.DataSource = repo.GetInvalidGroups();
            lueMSEInvGroup.Properties.ShowHeader = false;
            lueMSEInvGroup.Properties.Columns.Clear();
            lueMSEInvGroup.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            lueMSEInvGroup.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            lueMSEInvGroup.Properties.DisplayMember = "Name";
            lueMSEInvGroup.Properties.ValueMember = "Id";
            lueMSEInvGroup.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueMSEInvGroup.Properties.AutoSearchColumnIndex = 1;

            



        }

        private void InitData()
        {
            if (patient == null)
                return;

            txtFam.Text = patient.LastName;
            txtNam.Text = patient.FirstName;
            txtMid.Text = patient.MidName;
            deDr.EditValue = patient.BirthDate;
            luePol.EditValue = patient.Gender;

            

            //lueContinue
            lueContinue.Properties.DataSource = repo.GetPrevSeakLeaveItems(patient.Id);
            lueContinue.Properties.ShowHeader = false;
            lueContinue.Properties.Columns.Clear();

            lueContinue.Properties.Columns.Add(
                new LookUpColumnInfo("Number", "Название", 200) { Visible = true }
                );
            lueContinue.Properties.DisplayMember = "Number";
            lueContinue.Properties.ValueMember = "Number";
            lueContinue.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueContinue.Properties.AutoSearchColumnIndex = 1;
            layoutControlItem10.Enabled = false;


            if (slItem!=null)
            {
               
                if (slItem.TypeId.HasValue && slItem.TypeId==1)
                {
                    ceFirst.Checked = true;
                }

                if (slItem.TypeId.HasValue && slItem.TypeId == 9)
                {
                    ceFirst.Checked = false;
                    lueContinue.EditValue = slItem.PrevItemId;
                }

                
                if (slItem.DateIssue.HasValue)
                    deIssueDate.EditValue = slItem.DateIssue.Value;

                if (slItem.LpuId.HasValue)
                    lueLpu.EditValue = slItem.LpuId;

                if (slItem.WorkId.HasValue)
                {
                    lueWork.EditValue = slItem.WorkId;
                    ceMainWork.EditValue = true;
                }
                else
                {
                    if (patient.WorkPlaceId.HasValue)
                    {
                        lueWork.EditValue = patient.WorkPlaceId;
                        ceMainWork.EditValue = true;
                    }
                        
                }
                    
                
                if (slItem.OtherWorkId.HasValue)
                {
                    lueWork.EditValue = slItem.OtherWorkId;
                    ceWorkPart.EditValue = true;
                }

                if (slItem.Jobless.HasValue && slItem.Jobless.Value)
                {
                    ceEmpService.EditValue = true;
                    lueWork.EditValue = null;
                }
                    
                if (string.IsNullOrEmpty(slItem.Number))
                    txtNumber.Text = slItem.Number;

                if (slItem.CauseId.HasValue)
                    lueCode.EditValue = slItem.CauseId.Value;

                if (slItem.CauseAdditionalId.HasValue)
                    lueCodeDop.EditValue = slItem.CauseAdditionalId.Value;

                if (slItem.CauseChangeId.HasValue)
                    lueCodeChng.EditValue = slItem.CauseChangeId.Value;

                if (slItem.TalonId.HasValue)
                    lueTalon.EditValue = slItem.TalonId.Value;

                if (string.IsNullOrEmpty(slItem.Diagnos))
                    mkbSearchControl1.SelectedCode = slItem.Diagnos;

                if (slItem.PregnancyTwelveFlag.HasValue)
                    cePregnance.EditValue = slItem.PregnancyTwelveFlag.Value;

                if (slItem.Date1.HasValue)
                    deDate1.EditValue = slItem.Date1.Value;

                if (slItem.Date2.HasValue)
                    deDate2.EditValue = slItem.Date2.Value;

                if (slItem.RegimentViolationId.HasValue)
                    lueViolationCode.EditValue = slItem.RegimentViolationId.Value;

                if (slItem.RegimentViolationDate.HasValue)
                    deViolationDate.EditValue = slItem.RegimentViolationDate.Value;

                if (slItem.InvalId.HasValue)
                    lueMSEInvGroup.EditValue = slItem.InvalId.Value;

                if (slItem.Extends!=null && slItem.Extends.Count>0)
                    gridControl.DataSource = slItem.Extends;

                if (slItem.DateStartWork.HasValue)
                    deStartWork.EditValue = slItem.DateStartWork.Value;


                if (slItem.OtherId.HasValue)
                    lueOtherCode.EditValue = slItem.OtherId.Value;

                if (slItem.OtherDate.HasValue)
                    deOtherDate.EditValue = slItem.OtherDate.Value;

                if (!string.IsNullOrEmpty(slItem.Number))
                {
                    txtNumber.Text = slItem.Number;
                }


            }
        }


        private SeakLeaveItem ReadData()
        {

            txtFam.Text = patient.LastName;
            txtNam.Text = patient.FirstName;
            txtMid.Text = patient.MidName;
            deDr.EditValue = patient.BirthDate;
            luePol.EditValue = patient.Gender;
            slItem.PatientId = patient.Id;


            if (ceFirst.Checked == false && lueContinue.EditValue != null)
            {
                slItem.TypeId = 9; //Продолжение
                slItem.PrevItemId = lueContinue.EditValue.ToString();
            }else
            {
                slItem.TypeId = 1; //Первичный
            }
            

            if (deIssueDate.EditValue!=null)
                slItem.DateIssue= (DateTime)deIssueDate.EditValue;

            if (lueLpu.EditValue!=null)
                     slItem.LpuId = (long)lueLpu.EditValue;

            if (lueWork.EditValue!=null && (bool)ceMainWork.EditValue==true)
            {
                slItem.WorkId = (long)lueWork.EditValue;
                slItem.OtherWorkId = null;
            }

            if (lueWork.EditValue != null && (bool)ceWorkPart.EditValue == true)
            {
                slItem.OtherWorkId = (long)lueWork.EditValue;
                slItem.WorkId = null;
            }

            if (!string.IsNullOrEmpty(txtNumber.Text))
                slItem.Number = txtNumber.Text;

            if (lueCode.EditValue!=null)
                slItem.CauseId = (long)lueCode.EditValue;

            if (lueCodeDop.EditValue!=null)
                slItem.CauseAdditionalId = (long)lueCodeDop.EditValue;

            if (lueCodeChng.EditValue!=null)
                slItem.CauseChangeId = (long)lueCodeChng.EditValue;

            if (lueTalon.EditValue!=null)
                slItem.TalonId = (long)lueTalon.EditValue;

            if (mkbSearchControl1.SelectedCode!=null)
                slItem.Diagnos= mkbSearchControl1.SelectedCode;

            if (cePregnance.EditValue!=null)
                slItem.PregnancyTwelveFlag = (bool)cePregnance.EditValue;

            if (deDate1.EditValue!=null)
                slItem.Date1 = (DateTime)deDate1.EditValue;

            if (deDate2.EditValue!=null)
                slItem.Date2 = (DateTime)deDate2.EditValue;

            if (lueViolationCode.EditValue!=null)
                slItem.RegimentViolationId = (long)lueViolationCode.EditValue;

            if (deViolationDate.EditValue!=null)
                slItem.RegimentViolationDate = (DateTime)deViolationDate.EditValue;

            if (lueMSEInvGroup.EditValue!=null)
                slItem.InvalId=(long)lueMSEInvGroup.EditValue;

            if (deStartWork.EditValue!=null)
                slItem.DateStartWork = (DateTime)deStartWork.EditValue;

            if (lueOtherCode.EditValue!=null)
                slItem.OtherId = (long)lueOtherCode.EditValue;

            if (deOtherDate.EditValue!=null)
                slItem.OtherDate= (DateTime)deOtherDate.EditValue;

            if (ceEmpService.EditValue != null)
                slItem.Jobless = (bool)ceEmpService.EditValue;

            //slItem.Extends.Clear();
            //for (int i = 0; i < gridView.DataRowCount; i++ )
            //{
            //    var ext = gridView.GetRow(i) as SeakLeaveExtend;
            //    slItem.Extends.Add(ext);
            //}

            if (slItem.Extends!=null && slItem.Extends.Count>0)
            {
                slItem.DateBegin =  slItem.Extends.Min(c => c.DateFrom);
                slItem.DateEnd = slItem.Extends.Max(c => c.DateFor);
            }

            return slItem;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddWork_Click(object sender, EventArgs e)
        {
            using(var form = new SeakLeaveAddWorkForm(loggedUser))
            {
                form.ShowDialog();
                var workPlace = form.WorkPlace;
                if (workPlace != null)
                {
                    lueWork.Properties.DataSource = repo.GetWorkPlaceItems();
                    lueWork.EditValue = workPlace.Id;
                }
                    
            }
        }

        

        private void btnAddExtend_Click(object sender, EventArgs e)
        {
            using(var form = new SeakLeaveContinueForm(slItem, loggedUser) )
            {
                form.ShowDialog();
            }
            gridControl.DataSource = slItem.Extends;
        }

        private void btnDelExtend_Click(object sender, EventArgs e)
        {
            var item = gridView.GetFocusedRow() as SeakLeaveExtend;
            if (item!=null)
                repo.DeleteExtend(item.Id.Value);
            slItem.Extends = repo.GetExtends(slItem.Id.Value).ToList();
            gridControl.DataSource = slItem.Extends;
        }

        private void lueLpu_EditValueChanged(object sender, EventArgs e)
        {
            if (lueLpu.EditValue == null)
                return;
            var lpuInfo = CodifiersHelper.GetMO((long)lueLpu.EditValue);
            if (lpuInfo != null)
            {
                txtAdr.Text = lpuInfo.Address;
                txtOGRN.Text = lpuInfo.Ogrn;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            slItem = ReadData();
            var message = string.Empty;
            if (repo.CanSave(slItem,out message))
            {
               repo.AddOrUpdate(slItem, loggedUser.Id);
            }else
            {
                XtraMessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ceMainWork_CheckedChanged(object sender, EventArgs e)
        {
            ceWorkPart.EditValue = false;
            ceEmpService.EditValue = false;
        }

        private void ceWorkPart_CheckedChanged(object sender, EventArgs e)
        {
            ceMainWork.EditValue = false;
            ceEmpService.EditValue = false;
        }

        private void ceEmpService_CheckedChanged(object sender, EventArgs e)
        {
            ceWorkPart.EditValue = false;
            ceMainWork.EditValue = false;
            lueWork.EditValue = null;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values["prev_number"] = slItem.PrevItemId;
            values["first"] = "v";
            values["copy"] = "";
            var mo  = CodifiersHelper.GetMO(slItem.LpuId.Value);
            if (mo==null)
            {
                mo = CodifiersHelper.GetMO(2301001);
            }
            values["lpu_name"] = mo.Name;
            values["lpu_address"] = mo.Address;
            values["lpu_ogrn"] = mo.Ogrn;
            values["blist_date"] = slItem.DateIssue.Value.ToShortDateString();
            values["p_surname"] = patient.LastName;
            values["p_name"] = patient.FirstName;
            values["p_midname"] = patient.MidName;
            values["birth_date"] = patient.BirthDate.ToShortDateString();
            if (patient.Gender == Gender.Male)
                values["male_sex"] = "v";
            if (patient.Gender == Gender.Female)
                values["female_sex"] = "v";
            
            if (slItem.CauseId.HasValue)
                values["dis_code"] = repo.GetCause(slItem.CauseId.Value).Code;
            
            if (slItem.CauseAdditionalId.HasValue)
                values["dis_addcode"] = repo.GetCauseAdditional(slItem.CauseAdditionalId.Value).Code;
            
            if(slItem.CauseChangeId.HasValue)
                values["dis_changecode"] = repo.GetCause(slItem.CauseChangeId.Value).Code;

            if (string.IsNullOrEmpty(slItem.Number))
            {
                values["base_number"] = slItem.Number;
            }
            
            if (slItem.WorkId.HasValue)
            {
                values["job_place"] = repo.GetWorkPlaceItem(slItem.WorkId.Value).Name;
                values["base_job"] = "v";
            }

            if (slItem.OtherWorkId.HasValue)
            {
                values["job_place"] = repo.GetWorkPlaceItem(slItem.OtherWorkId.Value).Name;
                values["add_job"] = "v";
            }

            if (slItem.Jobless.HasValue && slItem.Jobless.Value)
            {
                values["jobless"] = "v";
            }

            if (slItem.DateBegin.HasValue)
            {
                values["date_begin"] = slItem.DateBegin.Value.ToShortDateString();
            }

            if (slItem.DateEnd.HasValue)
            {
                values["date_expired"] = slItem.DateEnd.Value.ToShortDateString();
            }

            if (string.IsNullOrEmpty(slItem.SanNumber))
            {
                values["sanatorium_number"] = slItem.SanNumber;
            }

            if (string.IsNullOrEmpty(slItem.SanOgrn))
            {
                values["sanatorium_ogrn"] = slItem.SanOgrn;
            }

            if (slItem.PregnancyTwelveFlag.HasValue && slItem.PregnancyTwelveFlag.Value)
            {
                values["pregnancy_yes"] = "v";
            }else
            {
                values["pregnancy_no"] = "v";
            }
            
            if (slItem.RegimentViolationId.HasValue)
            {
                values["violence_code"] = repo.GetViolation(slItem.RegimentViolationId.Value).Code;
            }
            
            if (slItem.RegimentViolationDate.HasValue)
            {
                values["violence_date"] = slItem.RegimentViolationDate.Value.ToShortDateString();
            }

            if (slItem.MSENapravDate.HasValue)
            {
                values["mse_date"] = slItem.MSENapravDate.Value.ToShortDateString();
            }

            if (slItem.MSERegDate.HasValue)
            {
                values["mse_reg_date"] = slItem.MSERegDate.Value.ToShortDateString();
            }

            if (slItem.InvalId.HasValue)
            {
                values["inval_set"] = repo.GetInvalidGroup(slItem.InvalId.Value).Name;
            }

            if (slItem.MSEExamineDate.HasValue)
            {
                values["mse_examine_date"] = slItem.MSEExamineDate.Value.ToShortDateString();
            }

            if (slItem.Extends.Count>0)
            {
                for(int i=0; i<slItem.Extends.Count; i++ )
                {
                    if (i > 2)
                        continue;
                    var ext = slItem.Extends[i];
                    var id = (i + 1).ToString();
                    values["disable_begin_"+id] = ext.DateFrom.Value.ToShortDateString();
                    values["disable_expired_" + id] = ext.DateFor.Value.ToShortDateString();
                    values["doctor_job_" + id] = ext.DoctorPosition.ToLower().Replace("врач-", "");
                    values["doctor_codename_" + id] = ext.DoctorFIO;
                    values["vkchief_" + id] = ext.ChiefDoctorPosition.ToLower().Replace("врач-", "");
                    values["vkchief_name_" + id] = ext.ChiefDoctorFIO;
                }
            }

            if (slItem.DateStartWork.HasValue)
            {
                values["job_begin_date"] = slItem.DateStartWork.Value.ToShortDateString();
            }

            if (slItem.OtherId.HasValue)
            {
                values["job_other_code"] = repo.GetOtherCause(slItem.OtherId.Value).Code;
            }

            if (slItem.OtherDate.HasValue)
            {
                values["job_other_date"] = slItem.OtherDate.Value.ToShortDateString();
            }



            using(var form = new SeakLeavePrintForm(values))
            {
                
            }


        }

        private void ceFirst_CheckedChanged(object sender, EventArgs e)
        {
            if (ceFirst.Checked==false)
            {
                layoutControlItem10.Enabled = true;
                if (string.IsNullOrEmpty(slItem.PrevItemId))
                {
                    lueContinue.EditValue = slItem.PrevItemId;
                }

            }else
            {
                layoutControlItem10.Enabled = false;
                lueContinue.EditValue = null;
                
            }
        }

        
    }
}