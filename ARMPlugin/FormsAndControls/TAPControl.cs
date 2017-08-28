using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Model;
using Model.Classes;
using Model.Classes.Codifiers;
using Model.Classes.Referral;
using Shared.FormsAndControls;
using SharedDbWorker;
using SharedDbWorker.Classes;
using SharedPrintWorker.Classes;
using SharedPrintWorker.Interfaces;
using SharedUtils.FormsAndControls;
using DbCaching;

namespace ARMPlugin.FormsAndControls
{
    public partial class TAPControl : PluginControl
    {
        const string _anamnezPatternCode = "200000";
        const string _complaintPatternCode = "300000";
        const string _objStatusPatternCode = "400000";

        //признак того, что нужно загрузить все талоны
        private bool _load_all_talons = false;
        
        private CustomStringConnectionParameters _connectionParameters = null;
        private List<Diagnose> _diagnoses = null;
        private Patient _patient = null;
        private Operator _operator = null;
        private List<Talon> _talons = null;

        // codifiers //
        private List<PaymentType> _paymentTypes = null;
        private List<FirstVisitCondition> _firstVisitConditions = null;
        private List<VisitPurpose> _visitPurposes = null;
        private List<ServicePlace> _servicePlaces = null;
        private List<CureResult> _cureResults = null;
        private List<TraumaType> _traumaTypes = null;
        private List<MO> _mos = null;

        private bool UiLoading = true;

        public event EventHandler DataChanged;
        public Func<bool> CanSaveFunc;
        public Action SaveAction;

        public bool IsSaved
        {
            get
            {
                var visit = GetSelectedVisit();
                var talon = GetSelectedTalon();

                var message = string.Empty;

                if (visit != null && !visit.IsSaved )
                {
                    return false;
                }

                if (talon != null && !talon.IsSaved)
                {
                    return false;
                }

                return true;
            }

        }

        public TAPControl()
        {
            InitializeComponent();
            //_connectionParameters = new CustomStringConnectionParameters("XpoProvider=Postgres;" + DbWorker.ConnectionString);
            InitForm();
            CanSaveFunc = CanSave;
            SaveAction = Save;
        }

        private bool CanSave()
        {
            var visit = GetSelectedVisit();
            var talon = GetSelectedTalon();
            
            var message = string.Empty;

            if (visit != null  && visit.IsSaved && !visit.CanSave(_operator, out message))
            {
                return false;
            }
            
            if (talon!=null && !talon.IsSaved)
            {
                if (!talon.CanSave(_operator, out message))
                {
                    return false;
                }
            }

            return true;

            //if (Patient == null || _talons == null)
            //    return false;

            //var result = Patient != null && _talons != null && _talons.Any(t => !t.IsSaved);

            //if (!result)
            //{
            //    result = _talons.Any(t => t.Visits != null && t.Visits.Any(v => !v.IsSaved));
            //}
            //return result;
        }

        private void Save()
        {
            var visit = GetSelectedVisit();
            var talon = GetSelectedTalon();
            var message = string.Empty;

            if (visit != null)
            {
                ReadVisitData(visit);
                if (talon == null)
                {
                    talon = GetTalonForSelectedVisit();

                    if (visit.CanSave(_operator, out message))
                    {
                        visit.Save(_operator);
                    }
                    else
                    {
                        XtraMessageBox.Show("Нельзя сохранить визит от " + visit.VisitDate + ".\n" + message);
                    }
                }
            }

            
            if (talon != null)
            {
                if (!talon.IsSaved)
                {
                    if (talon.CanSave(_operator, out message))
                    {
                        talon.Save(_operator);
                        CanSaveFunc = delegate { return true; };
                    }
                    else
                    {
                        XtraMessageBox.Show("Нельзя сохранить талон " + talon.Id + ".\n" + message);
                        CanSaveFunc = delegate { return false; };
                    }
                }
            }
        }

        public void InitForm()
        {
            UiLoading = true;

            _paymentTypes = _paymentTypes ?? CodifiersHelper.GetPaymentTypes();
            _firstVisitConditions = _firstVisitConditions ?? CodifiersHelper.GetFirstVisitConditions();
            _visitPurposes = _visitPurposes ?? CodifiersHelper.GetVisitPurposes();
            _servicePlaces = _servicePlaces ?? CodifiersHelper.GetServicePlaces();
            _cureResults = _cureResults ?? CodifiersHelper.GetCureResults();
            _traumaTypes = _traumaTypes ?? CodifiersHelper.GetTraumaTypes();
            _mos = _mos ?? CodifiersHelper.GetMOs();

            cmb_paymentType.Properties.Items.AddRange(_paymentTypes.Select(t=>t.Name).ToList());
            cmb_patientStateBeforeVisit.Properties.Items.AddRange(_firstVisitConditions.Select(t=>t.Name).ToList());
            cmb_serviceAim.Properties.Items.AddRange(_visitPurposes.Select(t=>t.Name).ToList());
            cmb_servicePlace.Properties.Items.AddRange(_servicePlaces.Select(t=>t.Name).ToList());
            cmb_trauma_types.Properties.Items.AddRange(_traumaTypes.Select(t=>t.Name).ToList());
            cmb_caseState.Properties.Items.AddRange(_cureResults.Select(t=>t.Name).ToList());

            cmb_lpuSent.Properties.Items.AddRange(_mos.Select(t=>string.IsNullOrEmpty(t.FullName) ? t.Name : t.FullName).ToList());


            if (textTemplateControl1==null)
            {
                textTemplateControl1 = new TextTemplateControl();
            }

            if (textTemplateControl2==null)
            {
                textTemplateControl2 = new TextTemplateControl();
            }

            if (_operator!=null)
            {
                textTemplateControl1.InitTextControl(_anamnezPatternCode, _operator.Id);
                textTemplateControl2.InitTextControl(_objStatusPatternCode, _operator.Id);
            }
            

            labResearchControl1.Patient = Patient;
            labResearchControl1.OrdersQuery = new DbQuery("Empty")
            {
                Sql = "select 1;"
            };
            labResearchControl1.CreateOrderAction += CreateOrderAction;
            labResearchControl1.OpenOrderAction += OpenOrderAction;

            UiLoading = false;
        }

        private void OpenOrderAction()
        {
            var rep = new LabOrderReport(labResearchControl1.SelectedLabOrder);
            rep.ShowPreview();
        }

        private void CreateOrderAction()
        {
            var talon = GetSelectedTalon();
            if (talon != null)
            {
                var doctor = new Doctor();
                doctor.LoadData(talon.DoctorIdTo);

                var f = new AddOrderForm(Patient, doctor) {NewLabOrder = {TalonId = talon.Id}};
                f.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Выделите случай (талон), в рамках которого создается направление");
            }
        }

        public Patient Patient
        {
            get { return _patient; }
            set
            {
                _patient = value;
                if (value != null)
                {
                    labResearchControl1.Patient = value;
                    _load_all_talons = false;
                    GetTalonTree();
                    chk_doctor_CheckedChanged(this, null);
                    chk_halfYear_CheckedChanged(this, null);
                    CanSaveFunc = CanSave;
                    
                }

                UpdateUi();
            }
        }


        private void UpdateUi()
        {
            Enabled = _patient != null;

            cmb_servicePlace.ReadOnly = !IsVisitSelected;
            cmb_serviceAim.ReadOnly = !IsVisitSelected;
            chk_firstVisit.ReadOnly = !IsVisitSelected;
            te_serviceDoctor.ReadOnly = !IsVisitSelected;
            te_serviceNurse.ReadOnly = !IsVisitSelected;
            chk_san_paper.ReadOnly = !IsVisitSelected;

            te_anamnez.ReadOnly = !IsVisitSelected;
            te_complains.ReadOnly = !IsVisitSelected;
            te_objStatus.ReadOnly = !IsVisitSelected;

            btn_addDiagn.Enabled = IsVisitSelected;
            btn_editDiagn.Enabled = IsVisitSelected;
            btn_deleteDiagn.Enabled = IsVisitSelected;

            doctorServiceControl1.Enabled = IsVisitSelected;

            cmb_paymentType.ReadOnly = !(IsVisitSelected || IsCaseSelected);
            cmb_patientStateBeforeVisit.ReadOnly = !(IsVisitSelected || IsCaseSelected);
            cmb_lpuSent.ReadOnly = !(IsVisitSelected || IsCaseSelected);
            cmb_caseState.ReadOnly = !(IsVisitSelected || IsCaseSelected);

            
            if (_diagnoses != null)
            {
                var isTrauma = _diagnoses.Any(t => t.IsTrauma);
                var talon = IsCaseSelected ? GetSelectedTalon() : GetTalonForSelectedVisit();
                if (talon != null)
                {
                    talon.IsTrauma = isTrauma;
                    layout_trauma_type.Visibility = isTrauma ? LayoutVisibility.Always : LayoutVisibility.Never;
                }
            }
        }

        private void GetTalons()
        {
            DateTime startTime = DateTime.Now;

            _talons = _talons ?? new List<Talon>();
            _talons.Clear();

            _talons.AddRange(Talon.PreLoad(Patient));
            
            #region oldCode
            /*using (var db = new DbWorker())
            {
                var q = new DbQuery("GetTalonIds");
                if (_load_all_talons)
                {
                    q.Sql = "Select talon_id from public.talon_tab where dan_id = @patientId;";
                }
                else
                {
                    q.Sql = "Select talon_id from public.talon_tab where (current_date - date_visit < 31) and (result_id in (10, 11, 12, 15, 100000, 200000, 300000, 3200000, 3300000) or result_id is null) and dan_id = @patientId;";
                }
                q.AddParamWithValue("@patientId", _patient.PatientId);
                var results = db.GetResults(q);

                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var id = DbResult.GetNumeric(result.Fields[0], -1);
                        if (id != -1)
                        {
                            var item = new Talon();
                            item.LoadData(id);
                            item.IsSaved = true;
                            _talons.Add(item);
                        }
                        
                    }
                }
            }*/
            #endregion
            
            DateTime endTime = DateTime.Now;
            TimeSpan s = endTime - startTime;

            Console.WriteLine("Время получения талонов:" + s.Milliseconds);
        }
        
        private void GetTalonTree()
        {
            GetTalons();
            RefreshTalonTree();
        }

        private void RefreshTalonTree()
        {
            treeList_talon.ClearNodes();
            _operator = this.ControlTemplate.HostForm.GetOperator();

            const string header1 = "Случай";
            const string header2 = "Посещение";

            treeList_talon.BeginUnboundLoad();
            foreach (var talon in _talons)
            {
                var obj1 = new object[]
                {
                    string.Format("{0} {1} - {2}", header1, talon.Id, talon.MainDiagnose),
                    talon.Id,
                    talon.CureResult._End == 1,
                    talon.CreateRecordDate,
                    talon.DoctorIdTo
                };

                var talonNode = treeList_talon.AppendNode(obj1, null);

                // set node images
                if (talon.CureResult._End == 1)
                {
                    talonNode.StateImageIndex = 2;
                }
                else
                {
                    if (talon.DoctorIdTo == _operator.DoctorId)
                    {
                        if (_talons.All(t => t.Id <= talon.Id))
                        {
                            talonNode.StateImageIndex = 0;
                        }
                        else
                        {
                            talonNode.StateImageIndex = 1;
                        }

                        if ((talon.CureResultId == -1 || talon.CureResultId == 0) && (string.IsNullOrEmpty(talon.MainDiagnose) || (talon.Visits == null || talon.Visits.Count == 0)))
                        //if ( (string.IsNullOrEmpty(talon.MainDiagnose) || (talon.Visits == null || talon.Visits.Count == 0)) )
                        {
                            talonNode.StateImageIndex = 5;
                        }

                        //if (talon.CureResultId==-1)
                        //{
                        //    talonNode.StateImageIndex = 5;
                        //}
                    }
                    else
                    {
                        if (talon.CureResultId == -1)
                            talonNode.StateImageIndex = 5;
                        else
                            talonNode.StateImageIndex = 3;
                    }
                }

                foreach (var visit in talon.Visits)
                {
                    var obj2 = new object[]
                    {
                        string.Format("{0} от {1}", header2,
                            (visit.VisitDate.HasValue ? visit.VisitDate.Value.ToShortDateString() : "-")),
                        visit.Id,
                        visit.IsComplete,
                        visit.CreateRecordDate,
                        visit.DoctorId
                    };
                    treeList_talon.AppendNode(obj2, talonNode);
                }
            }
            treeList_talon.EndUnboundLoad();

            treeList_talon.BeginSort();
            treeList_talon.Columns.ColumnByName(treeListColId.Name).SortOrder = SortOrder.Descending;
            treeList_talon.EndSort();

            if (treeList_talon.Nodes != null && treeList_talon.Nodes.Count > 0)
            {
                treeList_talon.MoveFirst();
                //var firstNode = treeList_talon.GetNodeByVisibleIndex(0);
                //treeList_talon.SetFocusedNode(firstNode);
            }
        }

        private FilterCondition _doctorCondition = null;
        private FilterCondition _halfYearCondition = null;
        private void chk_halfYear_CheckedChanged(object sender, EventArgs e)
        {
            if (_halfYearCondition == null)
                _halfYearCondition = new FilterCondition(FilterConditionEnum.Between, this.treeListColCreateDate, DateTime.Now.AddMonths(-6) , DateTime.Now);

            if (_halfYearCondition != null)
            {
                if (chk_halfYear.Checked)
                {
                    if (!treeList_talon.FilterConditions.Contains(_halfYearCondition))
                        treeList_talon.FilterConditions.Add(_halfYearCondition);
                }
                else
                {
                    if (treeList_talon.FilterConditions.Contains(_halfYearCondition))
                        treeList_talon.FilterConditions.Remove(_halfYearCondition);
                }
            }
            treeList_talon.Refresh();
        }

        private void chk_doctor_CheckedChanged(object sender, EventArgs e)
        {
            if (_doctorCondition == null)
                _doctorCondition = new FilterCondition(FilterConditionEnum.NotEquals, this.treeListColDoctorId, _operator.Id);

            if (_doctorCondition != null)
            {
                if (chk_doctor.Checked)
                {
                    if (!treeList_talon.FilterConditions.Contains(_doctorCondition))
                        treeList_talon.FilterConditions.Add(_doctorCondition);
                }
                else
                {
                    if (treeList_talon.FilterConditions.Contains(_doctorCondition))
                        treeList_talon.FilterConditions.Remove(_doctorCondition);
                }
            }

            treeList_talon.Refresh();
        }

        public bool IsCaseSelected { get; private set; }
        public bool IsVisitSelected { get; private set; }

        public long SelectedId { get; private set; }

        private void treeList_talon_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;

            UiLoading = true;

            IsCaseSelected = e.Node.Level == 0;
            IsVisitSelected = !IsCaseSelected;

            long id = DbResult.GetNumeric(e.Node[this.treeListColId], -1);

            if (id == -1)
                return;

            if (IsCaseSelected)
            {   
                SelectedId = id;
                CaseSelected();
            }
            else
            {
                SelectedId = id;
                VisitSelected();
            }

            //RefreshResearch();

            UpdateUi();
            UiLoading = false;
        }

        private void RefreshResearch()
        {
            var @case = GetSelectedTalon();
            if (@case != null)
            {
                labResearchControl1.OrdersQuery = new DbQuery("LabOrderIdsQuery")
                {
                    Sql = "SELECT id FROM laboratory.\"order\" where talon_id = @talon_id order by id;"
                };

                labResearchControl1.OrdersQuery.AddParamWithValue("talon_id", @case.Id);
                if (IsReseachPageOpen)
                {
                    labResearchControl1.RefreshLabOrders();
                }

                labResearchControl1.IsAddButtonVisible = !@case.IsTalonClosed && !string.IsNullOrEmpty(@case.MainDiagnose);
            }

        }

        private Talon GetSelectedTalon()
        {
            if (_talons != null && IsCaseSelected)
            {
                var talon = _talons.FirstOrDefault(t => t.Id == SelectedId);
                if (talon != null && !talon.IsLoaded)
                {
                    talon.LoadData(talon.Id);
                    talon.IsSaved = true;
                }
                return talon;
            }
            return null;
        }

        private Talon GetTalonForSelectedVisit()
        {
            if (_talons != null && IsVisitSelected)
            {
                var talon = _talons.FirstOrDefault(t => t.Visits.Any(v => v.Id == SelectedId));
                if (talon != null && !talon.IsLoaded)
                {
                    talon.LoadData(talon.Id);talon.IsSaved = true;
                }
                return talon;
            }
            return null;
        }

        private Visit GetSelectedVisit()
        {
            if (_talons != null && IsVisitSelected)
            {
                var talon = _talons.FirstOrDefault(t => t.Visits.Any(v => v.Id == SelectedId));
                if (talon != null)
                {
                    var visit = talon.Visits.First(t=>t.Id == SelectedId);
                    if (!visit.IsLoaded)
                    {
                        visit.LoadData(visit.Id);
                        visit.IsSaved = true;
                    }
                    return visit;
                }
            }
            return null;
        }

        private void CaseSelected()
        {
            ClearDiagnGrid();

            SetVisitInfo(null);

            var talon = GetSelectedTalon();
            
            if (talon != null)
            {
                SetCaseInfo(talon);

                GetCaseDiagnoses(talon);

                lbl_b_paper.Text = talon.BlistId != -1 ? talon.Blist.Cause.Name : "Не выписан";
            }
        }
        private void SetCaseInfo(Talon talon)
        {
            var payment = _paymentTypes.FirstOrDefault(t=>t.Id == talon.PaymentTypeId);
            if (payment != null)
                cmb_paymentType.SelectedItem = payment.Name;
            else 
                cmb_paymentType.Text = string.Empty;

            var state = _cureResults.FirstOrDefault(t => t.Id == talon.CureResultId);
            if (state != null)
                cmb_caseState.SelectedItem = state.Name;
            else
                cmb_caseState.Text = string.Empty;

            var firstCondition = _firstVisitConditions.FirstOrDefault(t => t.Id == talon.FirstVisitConditionId);
            if (firstCondition != null)
                cmb_patientStateBeforeVisit.SelectedItem = firstCondition.Name;
            else
                cmb_patientStateBeforeVisit.Text = string.Empty;
            if (talon.LpuNaprCode!=null)
            {
                cmb_lpuSent.Text = _mos.Where(m => m.Code == talon.LpuNaprCode).Select(c=>c.Name).First();
            }
            else
            {
                cmb_lpuSent.Text = string.Empty;
            }
        }

        private void GetCaseDiagnoses(Talon talon)
        {
            _diagnoses = new List<Diagnose>();
            using (var dbWorker = new DbWorker())
            {
                var query = new DbQuery("GetDiagnoses");
                query.Sql =
                    "SELECT d.id " +
                    "FROM diagn_tab d " +
                    "WHERE d.talon_id = @caseId " +
                    "ORDER BY d.id desc;";
                query.AddParamWithValue("@caseId", talon.Id);

                var results = dbWorker.GetResults(query);

                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var id = DbResult.GetNumeric(result.GetByName("id"), -1);
                        if (id != -1)
                        {
                            var item = new Diagnose();
                            item.LoadData(id);
                            item.IsSaved = true;
                            _diagnoses.Add(item);
                        }
                    }
                    _diagnoses.Sort((a, b) => a.DiagnoseTypeSort.CompareTo(b.DiagnoseTypeSort) );
                }
            }

            if (_diagnoses != null)
            {
                grid_diagnoses.DataSource = _diagnoses;
                CustomizeDiagnoseGridControl();
            }

            
        }

        private void CustomizeDiagnoseGridControl()
        {
            var col = grid_diagnosesView.Columns["Id"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["SetDate"];
            if (col != null)
            {
                col.VisibleIndex = 0;
                col.Caption = "Дата постановки диагноза";
            }

            col = grid_diagnosesView.Columns["MkbCode"];
            if (col != null)
            {
                col.VisibleIndex = 1;
                col.Caption = "Код МКБ";
            }

            col = grid_diagnosesView.Columns["MkbName"];
            if (col != null)
            {
                col.VisibleIndex = 2;
                col.Caption = "МКБ";
            }

            col = grid_diagnosesView.Columns["DiagnoseType"];
            if (col != null)
            {
                col.VisibleIndex = 3;
                col.Caption = "Тип диагноза";
            }

            col = grid_diagnosesView.Columns["DiseaseCharacter"];
            if (col != null)
            {
                col.VisibleIndex = 4;
                col.Caption = "Характер диагноза";
            }

            col = grid_diagnosesView.Columns["IsTrauma"];
            if (col != null)
            {
                col.VisibleIndex = 5;
                col.Caption = "Травма";
            }

            col = grid_diagnosesView.Columns["Description"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["IsFirst"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["LabConfirmed"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["IsRegistrered"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["DiagnoseTypeId"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["DiagnoseTypeSort"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["DiseaseStadiaId"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["DiseaseStadia"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["DiseaseCharacterId"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["RecordDate"];
            if (col !=null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["DoctorId"];
            if (col !=null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["PatientId"];
            if (col !=null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["TalonId"];
            if (col !=null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["IsLoading"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["IsLoaded"];
            if (col != null)
                col.Visible = false;

            col = grid_diagnosesView.Columns["IsSaved"];
            if (col != null)
                col.Visible = false;
        }

        private void ClearDiagnGrid()
        {
            if (_diagnoses != null)
            {
                _diagnoses.Clear();
                grid_diagnoses.RefreshDataSource();
            }
        }

        private void VisitSelected()
        {
            var visit = GetSelectedVisit();
            if (visit != null)
            {
                var talon = GetTalonForSelectedVisit();
                if (talon != null)
                {
                    SetCaseInfo(talon);
                    GetCaseDiagnoses(talon);
                }

                SetVisitInfo(visit);
                SetTextTemplates(visit);

                providedServicesControl1.InitForm(_operator.Id, visit);

                doctorServiceControl1.InitForm(_operator.Id, visit);

                visitRecipes1.InitForm(_operator.Id, visit);
            }
        }

        private void SetVisitInfo(Visit v)
        {
            if (v == null)
            {
                chk_firstVisit.Checked = false;
                chk_san_paper.Checked = false;
                cmb_servicePlace.Text = string.Empty;
                cmb_serviceAim.Text = string.Empty;
                te_serviceDoctor.Text = string.Empty;
                te_serviceNurse.Text = string.Empty;
            }
            else
            {
                chk_firstVisit.Checked = v.IsFirst;
                chk_san_paper.Checked = v.HasSanCard;
                var servicePlace = _servicePlaces.FirstOrDefault(t => t.Id.Equals(v.ServicePlaceId));
                if (servicePlace != null)
                    cmb_servicePlace.Text = servicePlace.Name;
                else
                    cmb_servicePlace.Text = string.Empty;
                var purpose = _visitPurposes.FirstOrDefault(t => t.Id.Equals(v.PurposeId));
                if (purpose != null)
                    cmb_serviceAim.Text = purpose.Name;
                else
                    cmb_serviceAim.Text = string.Empty;
                if (v.DoctorId != -1)
                {
                    var personId = GetPersonIdFromDoctor(v.DoctorId);
                    if (personId != -1)
                    {
                        var person = new Person();
                        person.LoadData(personId);
                        var shortFio = string.Format("{0} {1}{2}", person.LastName,
                            string.IsNullOrEmpty(person.FirstName) ? string.Empty : person.FirstName[0] + ".",
                            string.IsNullOrEmpty(person.MidName) ? string.Empty : person.MidName[0] + ".");
                        te_serviceDoctor.Text = shortFio;
                    }
                }
                if (v.NurseId != -1)
                {
                    var personId = GetPersonIdFromDoctor(v.NurseId);
                    if (personId != -1)
                    {
                        var person = new Person();
                        person.LoadData(personId);
                        var shortFio = string.Format("{0} {1}{2}", person.LastName,
                            string.IsNullOrEmpty(person.FirstName) ? string.Empty : person.FirstName[0] + ".",
                            string.IsNullOrEmpty(person.MidName) ? string.Empty : person.MidName[0] + ".");
                        te_serviceNurse.Text = shortFio;
                    }
                }
            }
        }

        private long GetPersonIdFromDoctor(long id)
        {
            long result = -1;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetPersonId");
                q.Sql = "Select dan_id from doctor_tab where id=@id limit 1;";
                q.AddParamWithValue("@id", id);
                result =  DbResult.GetNumeric(db.GetScalarResult(q), -1);
            }
            return result;
        }

        private void SetTextTemplates(Visit v)
        {
            var anamnezes = v.TextAnamnez.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            te_anamnez.Text = string.Empty;
            foreach (var anamnez in anamnezes)
            {
                te_anamnez.MaskBox.AppendText(string.Format("{0}\r\n", anamnez.TrimEnd()));
            }

            var complains = v.TextComplaint.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            te_complains.Text = string.Empty;
            foreach (var complain in complains)
            {
                te_complains.MaskBox.AppendText(string.Format("{0}\r\n", complain.TrimEnd()));
            }

            var objStatuses = v.TextObjStatus.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            te_objStatus.Text = string.Empty;
            foreach (var objStatuse in objStatuses)
            {
                te_objStatus.MaskBox.AppendText(string.Format("{0}\r\n", objStatuse.TrimEnd()));
            }
        }

        private void textTemplate_anamnez_PatternDoubleClicked(object sender, SharedUtils.FormsAndControls.PatternEventArgs e)
        {
            if (e.PatternCode.Equals(_anamnezPatternCode))
            {
                te_anamnez.MaskBox.AppendText(string.IsNullOrEmpty(te_anamnez.Text)
                    ? string.Format("{0}", e.Value)
                    : string.Format("\r\n{0}", e.Value));
                VisitChanged(sender, null);
            }

            if (e.PatternCode.Equals(_complaintPatternCode))
            {
                te_complains.MaskBox.AppendText(string.IsNullOrEmpty(te_complains.Text)
                    ? string.Format("{0}", e.Value)
                    : string.Format("\r\n{0}", e.Value));
                VisitChanged(sender, null);
            }
                
        }

        private void textTemplate_objStatus_PatternDoubleClicked(object sender, SharedUtils.FormsAndControls.PatternEventArgs e)
        {
            te_objStatus.MaskBox.AppendText(string.IsNullOrEmpty(te_objStatus.Text) 
                    ? string.Format("\r\n{0}", e.Value)
                    : string.Format("{0}", e.Value));
            
            VisitChanged(sender, null);
        }

        private void br_btn_print_result_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var btn = sender as BarButtonItem;

            PrintResult();
        }

        private void PrintResult()
        {
            Save();
            if (IsVisitSelected)
            {
                var visit = GetSelectedVisit();

                if (visit != null)
                {

                    var visitData = new VisitReportData();
                    
                    visitData.PatientFio = string.Format("{0} {1} {2}", Patient.LastName, Patient.FirstName, Patient.MidName);
                    visitData.VisitDate = visit.VisitDate;
                    visitData.ServicePlace = cmb_servicePlace.Text;
                    visitData.ServiceTarget = cmb_serviceAim.Text;
                    visitData.Anamnez = te_anamnez.Text;
                    visitData.Complains = te_complains.Text;
                    visitData.ObjectStatus = te_objStatus.Text;

                    if (_diagnoses.Count > 0)
                    {
                        var sb = new StringBuilder();
                        foreach (var diagnosis in _diagnoses)
                        {
                            sb.AppendLine(string.Format("{0}: {1}-{2}{3}", diagnosis.DiagnoseType, diagnosis.MkbCode, diagnosis.MkbName, Environment.NewLine));
                        }
                        visitData.Diagnosis = sb.ToString();
                    }
                    visitData.TalonStatus = cmb_caseState.Text;
                    visitData.IllPaper = lbl_b_paper.Text;
                    visitData.NextVisitDate = de_nextVisitDate.Text;
                    visitData.DoctorFio = te_serviceDoctor.Text;
                    var doc = new Doctor();
                    doc.LoadData(visit.DoctorId);
                    visitData.DoctorSpec = doc.GetSpecialityName();

                    var drugs = new DrugRepository().GetByPatient(Patient.Id, visit.Id);
                    
                    if (drugs!=null && drugs.Count()>0)
                    {
                        var text = string.Join(Environment.NewLine, drugs.Select(t=>t.MedicamentName+" "+t.Signa));
                        visitData.Drugs = text;
                    }

                    var recipes = new RecipeRepository().GetRecipesByVisit(visit);
                    if (recipes!=null && recipes.Count()>0)
                    {
                        var _medicaments = new List<Medicament>();

                        if (DbCache.GetItem("medicaments") == null)
                        {
                            _medicaments = CodifiersHelper.GetMedicaments();
                            DbCache.SetItem("medicaments", _medicaments, 3600);
                        }
                        else
                        {
                            _medicaments = DbCache.GetItem("medicaments") as List<Medicament>;
                        }
                        
                        foreach (var r in recipes)
                        {
                            r.MedicamentName = _medicaments.Where(s => s.Code == r.MedicamentFedCode.ToString()).Select(s => s.Mnn).FirstOrDefault();
                        }
                        
                        var text = string.Join(Environment.NewLine, recipes.Select(t => 
                                string.Format("{0}/{1} {2} {3} {4}",t.Serial, t.Number, t.MedicamentName,t.Doze,t.Signa) )
                            );
                        visitData.Recipes = string.Join(Environment.NewLine, text);
                    }

                    var report = new VisitResultReport(visitData);
                    report.ShowPreview();

                    /*var talon = _talons.FirstOrDefault(t => t.Id == visit.TalonId);
                    if (talon != null)
                    {
                        visit.Print(btn.Caption, new object[]{Patient, talon, visit});
                    }*/
                    /*
                    var path = Path.Combine(Environment.CurrentDirectory, @"print\результаты посещения.html");
                    if (File.Exists(path))
                    {
                        var content = File.ReadAllText(path);

                        var printContent = content.Replace("<myPatient>", string.Format("{0} {1} {2}", Patient.LastName, Patient.FirstName, Patient.MidName));
                        if (visit.VisitDate.HasValue)
                            printContent = printContent.Replace("<myCurrentDate>", visit.VisitDate.Value.ToShortDateString());

                        printContent = printContent.Replace("<myMestObs>", cmb_servicePlace.Text);
                        printContent = printContent.Replace("<myTargetPos>", cmb_serviceAim.Text);

                        printContent = printContent.Replace("<myAnamnez>", te_anamnez.Text.Replace("\n", "<br/>"));
                        printContent = printContent.Replace("<myZhalobi>", te_complains.Text.Replace("\n", "<br/>"));
                        printContent = printContent.Replace("<myObjStatus>", te_objStatus.Text.Replace("\n", "<br/>"));

                        if (_diagnoses.Count > 0)
                        {
                            var sb = new StringBuilder();
                            foreach (var diagnosis in _diagnoses)
                            {
                                sb.AppendLine(string.Format("{0}: {1}-{2}<br>", diagnosis.DiagnoseType, diagnosis.MkbCode, diagnosis.MkbName));
                            }

                            printContent = printContent.Replace("<myDiagnoz>", sb.ToString());
                        }

                        printContent = printContent.Replace("<myResult>", cmb_caseState.Text.Replace("\n", "<br/>"));
                        printContent = printContent.Replace("<myBolList>", lbl_b_paper.Text.Replace("\n", "<br/>"));
                        printContent = printContent.Replace("<myDateNext>", de_nextVisitDate.Text);
                        printContent = printContent.Replace("<myFIOdoct>", te_serviceDoctor.Text);

                        //PrintHtmlPage(printContent);

                        Graphics graphics = this.CreateGraphics();
                        var dpiHorizontal = graphics.DpiX;
                        var dpiVertical = graphics.DpiY;
                        IPrintWorker printer = new PrintWorker((int)dpiHorizontal, (int)dpiVertical);
                        printer.PrintHtmlContent(printContent);
                    }*/
                }
            }
        }

        private void PrintHtmlPage(string content)
        {
            // CreateInDb a WebBrowser instance. 
            var webBrowserForPrinting = new WebBrowser();

            // Add an event handler that prints the document after it loads.
            webBrowserForPrinting.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(PrintDocument);

            // Set the Url property to load the document.
            //webBrowserForPrinting.Url = new Uri(@"\\myshare\help.html");
            webBrowserForPrinting.DocumentText = content;
        }

        private void PrintDocument(object sender,
            WebBrowserDocumentCompletedEventArgs e)
        {
            var browser = sender as WebBrowser;

            if (browser != null)
            {
                // Print the document now that it is fully loaded.
                // browser.Print();

                browser.ShowPrintDialog();

                /*
                var key = @"Software\Microsoft\Internet Explorer\PageSetup";
                
                // read current user settings for header and footer
                var _footer = Registry.CurrentUser.OpenSubKey(key).
                          GetValue("footer").ToString();
                var _header = Registry.CurrentUser.OpenSubKey(key).
                          GetValue("header").ToString();

                Registry.CurrentUser.OpenSubKey(key, true).
                  SetValue("footer", "");
                Registry.CurrentUser.OpenSubKey(key, true).
                  SetValue("header", "");

                browser.ShowPrintDialog();

                Registry.CurrentUser.OpenSubKey(key, true).
                  SetValue("footer", _footer);
                Registry.CurrentUser.OpenSubKey(key, true).
                  SetValue("header", _header);
                */
                
                // Dispose the WebBrowser now that the task is complete. 
                browser.Dispose();
            }
        }

        public void ShowRecipe()
        {
            if (IsCaseSelected) return;

            var visit = GetSelectedVisit();
            if (visit != null)
            {

                var recipeForm = new RecipeForm();
                recipeForm.Operator = _operator;
                recipeForm.InitForm(_patient, visit);
                if (recipeForm.ShowDialog() == DialogResult.OK)
                {

                }
            }
            else
            {
                XtraMessageBox.Show("Выписка рецептов возможна только в пределах визита!");
            }
        }
        private void grid_diagnoses_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !IsCaseSelected)
            {
                popupMenu_print_talon.ShowPopup(MousePosition);
            }
        }
        private void treeList_talon_MouseUp(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && tree.State == TreeListState.Regular)
            {
                Point pt = tree.PointToClient(MousePosition);
                TreeListHitInfo info = tree.CalcHitInfo(pt);
                if (info.HitInfoType == HitInfoType.Empty)
                {
                    popupMenu_talon_tree.ShowPopup(MousePosition);
                }
                else
                {
                    if (info.HitInfoType == HitInfoType.Cell)
                    {
                        tree.FocusedNode = info.Node;

                        if (IsCaseSelected)
                        {
                            popupMenu_talon_tree.AddItem(br_btn_add_visit);
                        }
                        else
                        {
                            popupMenu_talon_tree.AddItem(br_btn_delete_visit);
                        }
                        popupMenu_talon_tree.ShowPopup(MousePosition);
                    }
                }
            }
        }
        private void popupMenu_talon_tree_CloseUp(object sender, EventArgs e)
        {
            popupMenu_talon_tree.ItemLinks.Clear();
            //br_check_load_all.Checked = _load_all_talons;
            popupMenu_talon_tree.AddItem(br_btn_refresh_talon_tree);
            //popupMenu_talon_tree.AddItem(br_check_load_all);
        }
        private void br_btn_refresh_talon_tree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTalonTree();
        }
        private void br_btn_add_visit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddVisit();
        }
        private void br_btn_delete_visit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteVisit();
        }

        private void AddVisit()
        {
            if (IsCaseSelected)
            {
                var talon = GetSelectedTalon();
                if (talon != null)
                {
                    if(talon.IsTalonClosed)
                    {
                        XtraMessageBox.Show("Случай закрыт!", "Внимание",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var newVisitParams = new AddNewVisitForm() { Patient = Patient, DoctorId = _operator.DoctorId, TalonId=SelectedId};
                    if (newVisitParams.ShowDialog() == DialogResult.OK)
                    {
                        var date = newVisitParams.VisitDate;

                        if (
                            _talons.Any(t=>t.Visits.Any(v=>v.VisitDate.HasValue && v.VisitDate.Value.Date.Equals(date.Date) && v.DoctorId == _operator.DoctorId)))
                        {
                            XtraMessageBox.Show("На текущую дату уже существует визит к данному доктору!", "Внимание",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (
                            talon.Visits.Any(
                                t =>
                                    t.VisitDate.HasValue && t.VisitDate.Value.Date.Equals(date.Date) &&
                                    t.DoctorId == _operator.DoctorId))
                        {
                            XtraMessageBox.Show("На текущую дату уже существует визит к данному доктору!", "Внимание",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else{
                            var visit = new Visit();
                            string message = string.Empty;
                            if (visit.CanCreateInDb(_operator, out message))
                            {
                                visit.TalonId = talon.Id;
                                visit.PatientId = Patient.PatientId;
                                visit.DoctorId = _operator.DoctorId;
                                visit.ServicePlaceId = _servicePlaces.First(t => t.Code.Equals("П")).Id;

                                visit.PurposeId = newVisitParams.Purpose.Id;
                                visit.VisitDate = newVisitParams.VisitDate;

                                try
                                {
                                    visit.CreateInDb(_operator);

                                    talon.AddVisit(visit);

                                    RefreshTalonTree();
                                }
                                catch (DbWorkerException ex)
                                {
                                    XtraMessageBox.Show(ex.Message, "SQL execute error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                XtraMessageBox.Show("Добавление визита не разрешено!", "Внимание", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }
        private void DeleteVisit()
        {
            if (IsVisitSelected)
            {
                var visit = GetSelectedVisit();
                if (visit != null)
                {
                    string message = string.Empty;
                    if (visit.CanDeleteFromDb(_operator, out message))
                    {
                        if (XtraMessageBox.Show("Вы собираетесь удалить визит. Продолжить?", "Внимание",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var talon = GetTalonForSelectedVisit();
                            if (talon != null)
                            {
                                talon.DeleteVisit(visit, _operator);
                                RefreshTalonTree();
                            }
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Удаление визита не разрешено!\n"+message, "Внимание", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }


                }
            }
        }

        private void btn_setNextVisitDate_Click(object sender, EventArgs e)
        {

            //var patientId = Patient.PatientId;
            Doctor doctor=new Doctor();
            if (Global.Operator!=null && Global.Operator.DoctorId!=-1)
            {
                doctor.LoadData(Global.Operator.DoctorId);
                using (var form = new SharedUtils.FormsAndControls.RegistryForm(Patient, doctor) { Operator = Global.Operator })
                {
                    form.ShowDialog();
                }
            }
            
            //MessageBox.Show("Заглушка: Пока не работает. След. посещение назначается в АРМе медсестрой");
            /*
            sQueueInData InData;
	        sQueueOutData OutData;
	        QLibrary qlib("queue");
	        QSqlSelectCursor cursor(QString::null,DBase);
	        cursor.exec("SELECT * FROM dan_tab WHERE dan_id = "+QString::number(danID)+";");
	        cursor.next();

	        InData.dan_id = danID;
	        InData.doctor_id = QString::number(operatorID);
	        InData.talon_id = currentTalon;
	        InData.flCanChangeDoctor = false;
	        InData.flCreateTalon = false;
	        InData.flOnlyWithTime = false;
	        InData.operator_id = operatorID;
	        InData.ser_pol = cursor.value("ser_pol").toString();
	        InData.num_pol = cursor.value("num_pol").toString();
	        InData.strcom_id = cursor.value("strcom_id").toInt();
	        InData.flagWho = tr("В");
	        InData.PrintButName = tr("Записать");
	        InData.kod_fak = cursor.value("kod_fak").toInt();
	        InData.kod_vuz = cursor.value("kod_vuz").toInt();
	        InData.num_group = cursor.value("num_group").toString();
	        showqueue qu = (showqueue) qlib.resolve( "showqueue" );
	        if (qu)
	        {
		        if(qu(this, elserver, DBase, &InData, &OutData))
		        {
			        visitNextDate->setData(OutData.dateVisit);
			        if (visitNextDate->getData().toDate().toString("dd.MM.yyyy")!=QString::null)
			        {
				        QString sql = "UPDATE talon_tab SET date_next = '"+visitNextDate->getData().toString()+"' WHERE talon_id = "+QString::number(currentTalon)+";";
				        if (!cursor.exec(sql))
				        {
					        QMessageBox::warning(this,tr("Ошибка сохранения"),cursor.lastQuery()+"\n"+cursor.lastError().databaseText(),"OK");
					        return;
				        }

			        }
		        }
	        }else
	        {
		        QMessageBox::critical(this, tr("Очередь"), tr("Библиотека queue не найдена."));
	        }*/
        }

        public void CreateNewTalon()
        {
            Talon t = new Talon();
            var message = string.Empty;
            if (t.CanCreateInDb(_operator, out message))
            {
                var f = new AddNewTalonForm();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    t.PatientId = _patient.PatientId;
                    t.DoctorIdTo = _operator.DoctorId;
                    t.PaymentTypeId = f.PaymentType.Id;
                    //t.VisitDate = DateTime.Now;

                    try
                    {
                        t.CreateInDb(_operator);
                    }
                    catch (Exception)
                    {
                        XtraMessageBox.Show("Ошибка создания в базе!", "Внимание", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }

                    /*var v = new Visit();
                    if (v.CanCreateInDb(_operator, out message))
                    {
                        v.TalonId = t.Id;
                        v.PatientId = _patient.PatientId;
                        v.DoctorId = _operator.DoctorId;
                        v.ServicePlaceId = _servicePlaces.First(s => s.Code.Equals("П")).Id;
                        v.PurposeId = f.Purpose.Id;
                        v.VisitDate = f.VisitDate;

                        try
                        {
                            v.CreateInDb(_operator);
                        }
                        catch
                        {
                            XtraMessageBox.Show("Ошибка создания в базе!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        

                        t.AddVisit(v);
                    }
                    else
                    {
                        XtraMessageBox.Show("Добавление визита не разрешено!", "Внимание", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                    }*/

                    _talons.Add(t);

                    RefreshTalonTree();
                }
            }
            else
            {
                XtraMessageBox.Show("Добавление талона не разрешено!\n" + message, "Внимание", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void grid_diagnosesView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            
        }

        private void btn_addDiagn_Click(object sender, EventArgs e)
        {
            var visit = GetSelectedVisit();
            if (visit != null)
            {

                var talon = GetTalonForSelectedVisit();
                if (talon != null)
                {
                    var d = new Diagnose();
                    d.Id = -1;
                    d.TalonId = talon.Id;
                    d.PatientId = Patient.PatientId;
                    d.DoctorId = _operator.DoctorId;
                    d.SetDate = visit.VisitDate.Value;
                    d.RecordDate = DateTime.Now;
                    EditDiagnose(d);
                }
            }
        }

        private void btn_editDiagn_Click(object sender, EventArgs e)
        {
            var diagnose = GetSelectedDiagnose();
            if (diagnose != null)
            {
                var message = "";
                if (diagnose.CanSave(_operator, out message))
                {
                    EditDiagnose(diagnose);
                }
                else
                {
                    XtraMessageBox.Show(message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_deleteDiagn_Click(object sender, EventArgs e)
        {
            DeleteDiagnose();
        }

        private Diagnose GetSelectedDiagnose()
        {
            if (IsVisitSelected)
            {
                var selectedRows = grid_diagnosesView.GetSelectedRows();
                if (selectedRows.Count() > 0)
                {
                    //int.TryParse(grid_diagnosesView.GetRowCellValue(selectedRows[0], "Id").ToString(), out id);
                    return grid_diagnosesView.GetFocusedRow() as Diagnose;
                }
            }
            return null;
        }

        private void changeDiagnOnTree(TreeListNode talonNode, Talon talon)
        {
            talonNode.SetValue(treeListColName, string.Format("{0} {1} - {2}", "Случай", talon.Id, talon.MainDiagnose));
        }

        private void EditDiagnose(Diagnose diagnose)
        {
            var editDiagnForm = new EditDiagnForm(_operator, diagnose, _diagnoses);
            if (editDiagnForm.ShowDialog() == DialogResult.OK)
            {
                var visit = GetSelectedVisit();
                var talon = GetTalonForSelectedVisit();
                
                var mainId = 200001; // код основного диагноза
                var mainDiagnose = _diagnoses.FirstOrDefault(t => t.DiagnoseTypeId == mainId);
                if (mainDiagnose != null)
                {
                    talon.MainDiagnose = mainDiagnose.MkbCode;
                    OnDataChanged();
                    talon.Save(_operator);
                    changeDiagnOnTree(treeList_talon.FocusedNode.ParentNode, talon);
                }
                /*else
                {
                    talon.MainDiagnose = null;
                }*/

                // сохраняем измененные (возможно) диагнозы
                foreach (var diagnosis in _diagnoses.Where(t=>t.Id != -1 && !t.IsSaved))
                {
                    diagnosis.Save(_operator);
                }

                //сохраняем новый диагноз
                var newDiagnose = _diagnoses.FirstOrDefault(t => t.Id == -1);
                if (newDiagnose!=null)
                    newDiagnose.Save(_operator);

                grid_diagnoses.RefreshDataSource();

                TalonChanged(this, null);

                UpdateUi();
            }
        }

        private void DeleteDiagnose()
        {
            if (IsVisitSelected)
            {
                var diagnose = GetSelectedDiagnose();
                string message = string.Empty;
                if (diagnose.CanDeleteFromDb(_operator, out message))
                {
                    if (XtraMessageBox.Show("Подтвердите удаление диагноза. Удалить?", "Внимание",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        diagnose.DeleteFromDb(_operator);
                        if (_diagnoses.Contains(diagnose))
                        {
                            _diagnoses.Remove(diagnose);
                            grid_diagnoses.RefreshDataSource();
                            
                            TalonChanged(this, null);

                            UpdateUi();
                        }

                    }
                }
                else
                {
                    XtraMessageBox.Show("Удаление диагноза не разрешено!\n" + message, "Внимание", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                }
                
            }
        }

        private void OnDataChanged()
        {
            if (DataChanged != null)
            {
                
                DataChanged(this, null);
            }
        }

        private void ReadTalonData(Talon talon)
        {
            talon.IsSaved = false;

            var paymentType = _paymentTypes.FirstOrDefault(t => cmb_paymentType.Text.Equals(t.Name));
            if (paymentType != null)
            {
                talon.PaymentTypeId = paymentType.Id;
            }

            var caseState = _cureResults.FirstOrDefault(t => cmb_caseState.Text.Equals(t.Name));
            if (caseState != null)
            {
                talon.CureResultId = caseState.Id;
            }

            var firstVisitSate = _firstVisitConditions.FirstOrDefault(t => cmb_patientStateBeforeVisit.Text.Equals(t.Name));
            if (firstVisitSate != null)
            {
                talon.FirstVisitConditionId = firstVisitSate.Id;
            }

            var traumaType = _traumaTypes.FirstOrDefault(t => cmb_trauma_types.Text.Equals(t.Name));
            if (traumaType != null)
            {
                talon.TraumaTypeId = traumaType.Id;
            }

            var lpuSent = _mos.FirstOrDefault(t => cmb_lpuSent.Text.Equals(t.Name));
            if (lpuSent != null)
            {
                talon.LpuNaprCode = lpuSent.Code;
            }
        }

        private void ReadVisitData(Visit visit)
        {
            
            visit.IsSaved = false;

            var purpose = _visitPurposes.FirstOrDefault(t => cmb_serviceAim.Text.Equals(t.Name));
            if (purpose != null)
            {
                visit.PurposeId = purpose.Id;
            }

            var place = _servicePlaces.FirstOrDefault(t => cmb_servicePlace.Text.Equals(t.Name));
            if (place != null)
            {
                visit.ServicePlaceId = place.Id;
            }

            visit.IsFirst = chk_firstVisit.Checked;

            visit.HasSanCard = chk_san_paper.Checked;

            visit.TextAnamnez = te_anamnez.Text.Trim();
            visit.TextComplaint = te_complains.Text.Trim();
            visit.TextObjStatus = te_objStatus.Text.Trim();

        }

        private void TalonChanged(object sender, EventArgs e)
        {
            if (UiLoading)
                return;

            if (IsCaseSelected)
            {
                var talon = GetSelectedTalon();
                ReadTalonData(talon);
                OnDataChanged();
            }
            else
            {
                if (IsVisitSelected)
                {
                    var visit = GetSelectedVisit();
                    var talon = GetTalonForSelectedVisit();
                    if (talon != null)
                    {
                        ReadTalonData(talon);
                        OnDataChanged();
                    }
                }
            }
        }

        private void VisitChanged(object sender, EventArgs e)
        {
            if (UiLoading)
                return;
            if (IsVisitSelected)
            {
                var visit = GetSelectedVisit();
                ReadVisitData(visit);
                OnDataChanged();
            }
        }

        private void doctorServiceControl1_Load(object sender, EventArgs e)
        {
            doctorServiceControl1.ServiceAdding += doctorServiceControl1_ServiceAdding;
        }

        void doctorServiceControl1_ServiceAdding(object sender, ServiceAddEventHandler e)
        {
            var visit = GetSelectedVisit();
            if (visit != null)
            {

                var talon = GetTalonForSelectedVisit();
                if (talon != null)
                {
                    if (e.Service != null && e.Mkb != null)
                    {
                        using (var db = new DbWorker())
                        {
                            var q = new DbQuery("SetService");
                            q.Sql =
                                "INSERT INTO " +
                                "okaz_uslug_tab(" +
                                "table_link_name, sp_lpu_for_id, sp_lpu_id, " +
                                "sp_uslug_id, " +
                                "date_okaz,  " +
                                "kol_vo, " +
                                "table_link_id, " +
                                "sp_kind_paid_id, " +
                                "doctor_id, " +
                                "cost, " +
                                "dan_id, " +
                                "inference_diagnosis)" +
                                " VALUES(" +
                                "'visit_tab', get_lpu_number()::bigint, get_lpu_number()::bigint," +
                                "@sp_uslug_id," +
                                "@date_okaz," +
                                "@count," +
                                "@table_link_id," +
                                "@payment_type," +
                                "@doctor_id," +
                                "(SELECT cost FROM eq_uslug_cost_tab WHERE sp_uslug_id = @sp_uslug_id AND sp_lpu_id = get_lpu_number()::bigint AND @date_okaz BETWEEN date_begin AND date_end LIMIT 1)," +
                                "@patient," +
                                "@diagnose);";

                            q.AddParamWithValue("@sp_uslug_id", e.Service.Id);
                            q.AddParamWithValue("@date_okaz", visit.VisitDate);
                            q.AddParamWithValue("@count", e.Count);
                            q.AddParamWithValue("@table_link_id", visit.Id);
                            q.AddParamWithValue("@payment_type", talon.PaymentTypeId);
                            q.AddParamWithValue("@doctor_id", _operator.DoctorId);
                            q.AddParamWithValue("@patient", Patient.PatientId);
                            q.AddParamWithValue("@diagnose", e.Mkb.Code);

                            var r = db.Execute(q);
                            if (r > 0)
                            {
                                
                            }
                        }
                        // обновляем список оказанных услуг
                        providedServicesControl1.RefreshData();
                    }
                }
            }
        }

        private void te_anamnez_Enter(object sender, EventArgs e)
        {
            if (_operator!=null)
                textTemplateControl1.InitTextControl(_anamnezPatternCode,_operator.Id);
        }

        private void te_complains_Enter(object sender, EventArgs e)
        {
            if (_operator!=null)
                textTemplateControl1.InitTextControl(_complaintPatternCode,_operator.Id);
        }

        public bool IsReseachPageOpen
        {
            get { return tab_content.SelectedTabPageIndex == tab_labResearches.TabIndex; }
        }

        private void tab_content_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            
        }

        private void br_btn_print_npr_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintAppointment();
        }

        private void PrintAppointment()
        {
            var visit = GetSelectedVisit();
            if (visit != null)
            {
                var referral = new Referral();
                referral.DoctorId = visit.DoctorId;
                referral.Patient = Patient;
                referral.Operator = _operator;
                referral.MkbCode = GetTalonForSelectedVisit().MainDiagnose;
                referral.TalonId = GetTalonForSelectedVisit().Id;
                var f = new ReferralForm(referral);
                f.ShowDialog();
            }
        }

        private void br_check_load_all_ItemClick(object sender, ItemClickEventArgs e)
        {
            _load_all_talons = br_check_load_all.Checked;
        }

        private void tab_content_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Name=="xtraTabPage2")
            {
                textTemplateControl1.InitTextControl(_anamnezPatternCode, _operator.Id);
            }

            if (e.Page.Name == "xtraTabPage3")
            {
                textTemplateControl2.InitTextControl(_objStatusPatternCode, _operator.Id);
            }

            if (e.Page.Name == "xtraTabPage4")
            {
                if (IsVisitSelected)
                {
                    var visit = GetSelectedVisit();
                    visitRecipes1.Visit = visit;
                }


            }
        }

        private void textTemplateControl1_PatternDoubleClicked(object sender, PatternEventArgs e)
        {
            if (e.PatternCode.Equals(_anamnezPatternCode))
            {
                te_anamnez.MaskBox.AppendText(string.IsNullOrEmpty(te_anamnez.Text)
                    ? string.Format("{0}", e.Value)
                    : string.Format("\r\n{0}", e.Value));
                VisitChanged(sender, null);
            }

            if (e.PatternCode.Equals(_complaintPatternCode))
            {
                te_complains.MaskBox.AppendText(string.IsNullOrEmpty(te_complains.Text)
                    ? string.Format("{0}", e.Value)
                    : string.Format("\r\n{0}", e.Value));
                VisitChanged(sender, null);
            }
        }

        private void textTemplateControl2_PatternDoubleClicked(object sender, PatternEventArgs e)
        {
            te_objStatus.MaskBox.AppendText(string.IsNullOrEmpty(te_objStatus.Text)
                ? string.Format("\r\n{0}", e.Value)
                : string.Format("{0}", e.Value));

            VisitChanged(sender, null);
        }

        
    }

    ///// <summary>
    ///// Информация о талоне
    ///// </summary>
    //public class PreTalon
    //{
    //    public PreTalon()
    //    {
    //        CureResult = new CureResult();
    //        IsTalonLoaded = false;
    //        Visits = new List<PreVisit>();
    //    }
    //    private long _cureResultId;
    //    public long TalonId { get; private set; }
    //    public bool IsTalonLoaded { get; private set; }
    //    public string Mkb { get; set; }
    //    public long DoctorIdTo { get; set; }
    //    private long CureResultId
    //    {
    //        get { return _cureResultId; }
    //        set
    //        {
    //            _cureResultId = value;
    //            if (value != -1)
    //            {
    //                CureResult.LoadData(value);
    //            }
    //        }
    //    }
    //    public CureResult CureResult { get; private set; }
    //    public Talon Talon { get; private set; }
    //    public List<PreVisit> Visits { get; set; }
    //}

    //public class PreVisit
    //{
        
    //}
}
