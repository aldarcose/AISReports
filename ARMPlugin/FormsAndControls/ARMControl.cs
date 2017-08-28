using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Model.Classes;
using Model.Classes.AddressPart;
using Model.Classes.Codifiers;
using Shared.FormsAndControls;
using SharedUtils.FormsAndControls;
using Model.Classes.Reestr;
using System.Globalization;
using DevExpress.XtraEditors.Controls;
using Model.Classes.Benefits;

namespace ARMPlugin.FormsAndControls
{
    public partial class ARMControl : PluginControl
    {
        private Patient _patient = null;
        private List<MedArea> _uchastki = null;
        private List<SocStatus> _socStatuses = null;
        private List<Region> _regions = null;
        private List<SMO> _smoRegionList = null;

        public ARMControl()
        {
            InitializeComponent();
            tapControl1.DataChanged += TapDataChanged;
            labResearchControl1.CreateOrderAction += CreateOrderAction;
            labResearchControl1.OpenOrderAction += OpenOrderAction;
            labResearchControl1.IsAddButtonVisible = true;
        }

        private void OpenOrderAction()
        {
            var rep = new LabOrderReport(labResearchControl1.SelectedLabOrder);
            rep.ShowPreview();
        }

        
        private void CreateOrderAction()
        {
            var @operator = this.ControlTemplate.HostForm.GetOperator();
            if (@operator != null)
            {
                var doctor = new Doctor();
                doctor.LoadData(@operator.DoctorId);

                var f = new AddOrderForm(this._patient, doctor);
                f.ShowDialog();
            }
        }

        private void TapDataChanged(object sender, EventArgs eventArgs)
        {
            OnCommandChanged(sender, eventArgs);
        }

        public event EventHandler Changed;

        public void OnChanged()
        {
            if (Changed != null)
                Changed(this, null);
        }

        public override void OnControlAttaching()
        {
            base.OnControlAttaching();

            InitForm();
        }

        private void InitForm()
        {
            if (cmb_gender.Properties.Items.Count == 0)
            {
                cmb_gender.Properties.Items.Add("");
                cmb_gender.Properties.Items.Add("Мужской");
                cmb_gender.Properties.Items.Add("Женский");
            }

            _socStatuses = _socStatuses ?? CodifiersHelper.GetSocStatuses();
            if (cmb_socStatus.Properties.Items.Count ==0)
            {
                cmb_socStatus.Properties.Items.AddRange(_socStatuses.Select(t=>t.Name).ToList());
            }

            _uchastki = _uchastki ?? CodifiersHelper.GetUchaski();
            if (cmb_uchastok.Properties.Items.Count == 0)
            {
                foreach (var source in _uchastki.Where(t => t.Type != 1))
                {
                    cmb_uchastok.Properties.Items.Add(source.Name);
                }
            }
            if (cmb_dopUchastok.Properties.Items.Count == 0)
            {
                foreach (var source in _uchastki.Where(t => t.Type == 1))
                {
                    cmb_dopUchastok.Properties.Items.Add(source.Name);
                }
            }
            
            _regions = _regions ?? CodifiersHelper.GetRegions();
            
            if (cmb_polSmoRegion.Properties.Items.Count == 0)
                cmb_polSmoRegion.Properties.Items.AddRange(_regions.Select(t=>t.Name).ToList());
            
            tapControl1.ControlTemplate = this.ControlTemplate;

            SetControlEnabled();

            luAttachLPU.Properties.DataSource = CodifiersHelper.GetMOs();
            luAttachLPU.Properties.DisplayMember = "FullName";
            luAttachLPU.Properties.ValueMember = "Code";
            luAttachLPU.Properties.ShowFooter = false;
            luAttachLPU.Properties.ShowHeader = false;
            luAttachLPU.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luAttachLPU.Properties.Columns.Clear();
            luAttachLPU.Properties.Columns.Add(new LookUpColumnInfo() { FieldName="Code", Visible=false });
            luAttachLPU.Properties.Columns.Add(new LookUpColumnInfo() { FieldName = "FullName", Visible = true });


            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ru-Ru"));

            

        }

        private void SetControlEnabled()
        {
            this.Enabled = _patient != null;
        }

        public Patient CurrentPatient
        {
            get { return _patient; }
        }

        private void LoadPatientData(int patientId)
        {
            var patient = new Patient();
            if (!patient.LoadData(patientId))
                patient = null;

            InitFields(patient);

            tapControl1.Patient = patient;

            labResearchControl1.Patient = patient;
            //labResearchControl1.RefreshLabOrders();

            referralControl1.Patient = patient;
            var @operator = this.ControlTemplate.HostForm.GetOperator();
            Global.Operator = @operator;
            referralControl1.Operator = @operator;

            //прививки
            vaccinationControl1.LoggedUser = @operator;
            vaccinationControl1.Patient = patient;

            //Диспансерный учет
            dispenseryControl1.LoggedUser = @operator;
            dispenseryControl1.Patient = patient;

            //больничный лист
            seakLeaveControl1.Operator = @operator;
            seakLeaveControl1.Patient = patient;

            //ДопДисп
            dopDispControl1.LoggedUser = @operator;
            dopDispControl1.Patient = patient;

            SetControlEnabled();

            //поставить ФИО пациента в заголовок окна
            this.ControlTemplate.HostForm.FormCaptionText = patient.ToString();
        }

        private void InitFields(Patient patient)
        {
            if (patient != null)
            {
                te_fio.Text = string.Format("{0} {1} {2}", patient.LastName, patient.FirstName, patient.MidName);
                te_fio.Properties.ReadOnly = true;

                de_bornDate.DateTime = patient.BirthDate;

                cmb_gender.SelectedIndex = patient.Gender == Gender.Unknown
                    ? 0
                    : (patient.Gender == Gender.Male ? 1 : 2);
                te_bornPlace.Text = patient.BirthPlace;
                patient.RegAddress.UpdateText();
                patient.FactAddress.UpdateText();
                btne_regPlace.Text = patient.RegAddress.Text;
                btne_factPlace.Text = patient.FactAddress.Text;
                if (patient.Document != null)
                {
                    if (patient.Document.DateBeg.HasValue)
                        de_docDateBeg.DateTime = patient.Document.DateBeg.Value;
                    cmb_docType.Text = patient.Document.Type.Name;
                    te_docSerial.Text = patient.Document.Serial;
                    te_docNumber.Text = patient.Document.Number;
                    te_docOrg.Text = patient.Document.Organization;
                }

                if (patient.Policy != null)
                {
                    cmb_polType.Text = patient.Policy.Type.Name;
                    if (patient.Policy.DateBeg.HasValue && patient.Policy.DateBeg.Value != DateTime.MinValue)
                        de_polDateBeg.DateTime = patient.Policy.DateBeg.Value;
                    if (patient.Policy.DateEnd.HasValue && patient.Policy.DateEnd.Value != DateTime.MinValue)
                        de_polDateEnd.DateTime = patient.Policy.DateEnd.Value;
                    te_polSerial.Text = patient.Policy.Serial;
                    te_polNumber.Text = patient.Policy.Number;
                }

                if (patient.UchastokId > 0)
                {
                    cmb_uchastok.Text = _uchastki.First(t => t.Type != 1 && t.Id == patient.UchastokId).Name;
                }

                if (patient.UchastokDopId > 0)
                {
                    cmb_dopUchastok.Text = _uchastki.First(t => t.Type == 1 && t.Id == patient.UchastokDopId).Name;
                }

                if (!string.IsNullOrEmpty(patient.Policy.SmoRegionCode))
                {
                    var result = _regions.FirstOrDefault(t => t.Code.Equals(patient.Policy.SmoRegionCode));
                    cmb_polSmoRegion.Text = (result !=null) ? result.Name : string.Empty;
                }
                    

                if (_smoRegionList != null)
                {
                    var smo = _smoRegionList.FirstOrDefault(t => t.Id == patient.Policy.SmoId);

                    if (smo != null)
                        cmb_polSmo.Text = smo.Name;
                }

                if (patient.SocStatusId != -1)
                {
                    var result = _socStatuses.FirstOrDefault(t => t.Id == patient.SocStatusId);
                    cmb_socStatus.Text = (result!=null) ?  result.Name : string.Empty;
                }

                this.phoneControl1.Phones = patient.Phones;

                if (patient.Fluorography != null)
                {
                    te_fluo_place.Text = patient.Fluorography.LpuName;
                    if (patient.Fluorography.Date.HasValue)
                        te_fluo_date.Text = patient.Fluorography.Date.Value.ToString("dd.MM.yyyy");
                    te_fluo_num.Text = patient.Fluorography.Num.ToString();
                    te_risk_group.Text = patient.GetRiskGroupName();
                }
                else
                {
                    te_fluo_place.Text = string.Empty;
                    te_fluo_date.Text = string.Empty;
                    te_fluo_num.Text = string.Empty;
                    te_risk_group.Text = string.Empty;

                }

                grid_lgotas.DataSource = patient.Lgotas;

                _patient = patient;

                luAttachLPU.EditValue = patient.LpuAttachCode;

                ShowMessages();

                OnChanged();
            }

        }

        private void ShowMessages()
        {
            
            if (!_patient.Attached)
            {
                XtraMessageBox.Show("Данный пациент не прикреплен к поликлинике!", "Пациент", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            
            if (_patient.Fluorography != null && _patient.Fluorography.Date.HasValue)
            {
                if ((DateTime.Now - _patient.Fluorography.Date.Value).Days > 365)
                {
                    XtraMessageBox.Show("Флюорография проводилась более года!", "Флюорография", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        public void FindPatient(object obj)
        {
            //tapControl1.SaveAction();
            
            if (!tapControl1.IsSaved)
            {
                XtraMessageBox.Show("Сохраните изменения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
            
            var @operator = this.ControlTemplate.HostForm.GetOperator();
            var findForm = new PatientFindForm(@operator);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                int patientId = findForm.SelectedPatientId;

                if (patientId != -1)
                {
                    LoadPatientData(patientId);
                    //this.ControlTemplate.Commands.Where(c=>c.Name==)
                }

            }
        }

        public bool CanSaveChanges(object obj)
        {
            if (tapControl1.CanSaveFunc != null)
                return tapControl1.CanSaveFunc();
            return false;
        }

        public void SaveChanges(object obj)
        {
            if (tapControl1.SaveAction != null)
                tapControl1.SaveAction();
        }

        private void btne_regPlace_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_patient.RegAddress.Text) && _patient.RegAddress.Region == null)
                _patient.RegAddress.LoadAddressByPatientId(_patient.PatientId, AddressType.Reg);

            if (editAddress(_patient.RegAddress))
            {
                btne_regPlace.Text = _patient.RegAddress.Text;
            }
        }

        private void btne_factPlace_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_patient.FactAddress.Text) && _patient.FactAddress.Region == null)
                _patient.FactAddress.LoadAddressByPatientId(_patient.PatientId, AddressType.Fact);
            if (editAddress(_patient.FactAddress))
            {
                btne_factPlace.Text = _patient.FactAddress.Text;
            }
        }

        /// <summary>
        /// Вызывает форму редактирования адреса
        /// </summary>
        /// <param name="editAddress">Адрес для редактирования</param>
        /// <returns>true если адрес был отредактирован</returns>
        private bool editAddress(Address editAddress)
        {
            var addressSelectForm = new AddressSelectorForm(editAddress);
            if (addressSelectForm.ShowDialog() == DialogResult.OK)
            {
                editAddress.Copy(addressSelectForm.EditedAddress);
                return true;
            }
            return false;
        }

        private void cmb_polSmoRegion_SelectedValueChanged(object sender, EventArgs e)
        {
            var region = _regions.FirstOrDefault(t => t.Name.Equals(cmb_polSmoRegion.Text));
            if (region != null)
            {
                _smoRegionList = CodifiersHelper.GetRegionSmo(region.Code);

                cmb_polSmo.Properties.Items.Clear();
                cmb_polSmo.Properties.Items.AddRange(_smoRegionList.Select(t=>t.Name).ToList());
            }
        }

        private void cmb_uchastok_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_dopUchastok_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ShowRecipeForm(object obj)
        {
            if (_patient.Lgotas != null && _patient.Lgotas.Count > 0)
            {
                //var @operator = this.ControlTemplate.HostForm.GetOperator();
                tapControl1.ShowRecipe();
            }
            else
            {

                XtraMessageBox.Show("Пациент не имеет льгот. Выписка рецепта невозможна!");
            }
        }

        public bool CanShowRecipeForm(object obj)
        {
            return _patient != null;
        }

        public bool CanCreateTalon(object obj)
        {
            return _patient != null;
        }

        public void CreateTalon(object obj)
        {
            tapControl1.CreateNewTalon();
        }


        public void GetDoctorQueue(object obj)
        {
            
            if (_patient==null)
                XtraMessageBox.Show("Выберите пациента");
            using(var form = new DoctorQueue(_patient))
            {
                var result = form.ShowDialog();
            }
        }

        public void GetLocalDoctorQueue(object obj)
        {
            if (_patient == null)
                XtraMessageBox.Show("Выберите пациента");

            if (Global.Operator!=null)
            {
                var doctor = new Doctor();
                doctor.LoadData(Global.Operator.DoctorId);

                using (var form = new SharedUtils.FormsAndControls.RegistryForm(_patient, doctor))
                {
                    form.Operator = Global.Operator;
                    var result = form.ShowDialog();
                }

            }
            else
            {
                using (var form = new RegistryForm(_patient))
                {
                    form.Operator = Global.Operator;
                    var result = form.ShowDialog();
                }
            }


        }


        public void GetWaitingList(object obj)
        {
            if (_patient == null)
            {
                XtraMessageBox.Show("Выберите пациента");
                return;
            }
                
            using (var form = new RegistryWaitingListForm(_patient))
            {
                var result = form.ShowDialog();
            }
        }


        internal void GetUnresidentReestrXML(object obj)
        {
            using (var form = new ReestForm(ReestrTypes.Unresident))
            {
                form.ShowDialog();
            }
        }
        
        internal void GetReestrXML(object obj)
        {
            using(var form = new ReestForm(ReestrTypes.Service))
            {
                form.ShowDialog();
            }
        }

        internal void GetDDReestrXML(object obj)
        {
            using (var form = new ReestForm(ReestrTypes.Disp))
            {
                form.ShowDialog();
            }
        }

        public void UpdateServices (object obj)
        {
            var converter = new Converter();
            converter.UpdateReestr();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }

        private void btnAddLgota_Click(object sender, EventArgs e)
        {
            var user=this.ControlTemplate.HostForm.GetOperator();
            using(var form = new LocalLgotaForm(_patient.Id, user))
            {
                form.ShowDialog();
            }
            grid_lgotas.DataSource = _patient.Lgotas;
            grid_lgotas.RefreshDataSource();
        }

        private void btnDeleteLgota_Click(object sender, EventArgs e)
        {

            var user = this.ControlTemplate.HostForm.GetOperator();
            if (!user.Permissions.Any(c=>c.Id==(long)Permissions.Admin))
            {
                XtraMessageBox.Show("Удаление недоступно!");
                return;
            }
            
            var rowHandle = gridView1.GetSelectedRows().FirstOrDefault();
            var item = gridView1.GetRow(rowHandle) as Lgota;
            if (item.LgotaCode.Length != 2)
            {
                XtraMessageBox.Show("Выберите региональную льготу");
                return;
            }
            else
            {
                var repo = new BenefitRepository();
                var patientBenefit = repo.Load(_patient.Id, item.LgotaCode);
                repo.Delete(patientBenefit);
            }
            grid_lgotas.DataSource = _patient.Lgotas;
            grid_lgotas.RefreshDataSource();

        }

        private void btnChange_Click(object sender, EventArgs e)
        {

            var rowHandle = gridView1.GetSelectedRows().FirstOrDefault();
            var item = gridView1.GetRow(rowHandle) as Lgota;
            if (item.LgotaCode.Length!=2)
            {
                XtraMessageBox.Show("Выберите региональную льготу");
                return;
            }
            else
            {
                var repo = new BenefitRepository();

                var patientBenefit = repo.Load(_patient.Id, item.LgotaCode);
                var user = this.ControlTemplate.HostForm.GetOperator();
                using (var form = new LocalLgotaForm(patientBenefit, user))
                {
                    form.ShowDialog();
                }
                grid_lgotas.DataSource = _patient.Lgotas;
                grid_lgotas.RefreshDataSource();
            }
            
        }

        
    }
}
