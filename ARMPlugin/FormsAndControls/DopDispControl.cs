using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using Model;
using Model.Classes.DopDisp;
using DevExpress.XtraGrid.Columns;
using SharedUtils.Classes;
using DevExpress.XtraEditors.Controls;

namespace ARMPlugin.FormsAndControls
{
    public partial class DopDispControl : DevExpress.XtraEditors.XtraUserControl
    {
        public Operator LoggedUser {get;set;}

        Patient patient;
        public Patient Patient {
            get { return patient; }
            set { patient = value; InitCardsData(); } 
        }

        public DopDispTotal SelectedCard { get; set; }
        
        DopDispRepository repo = new DopDispRepository();

        public DopDispControl()
        {
            InitializeComponent();
            InitForm();
        }


        private void InitForm()
        {
            //lueDoctor
            lueDoctor.Properties.ShowHeader = false;
            lueDoctor.Properties.Columns.Clear();
            lueDoctor.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 40) { Visible = false }
                );
            lueDoctor.Properties.Columns.Add(
                new LookUpColumnInfo("FIO", "FIO", 200) { Visible = true }
                );
            lueDoctor.Properties.DisplayMember = "FIO";
            lueDoctor.Properties.ValueMember = "Id";
            lueDoctor.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueDoctor.Properties.AutoSearchColumnIndex = 1;
            lueDoctor.Properties.DataSource = CodifiersHelper.GetDoctors();

            //lueType
            lueType.Properties.ShowHeader = false;
            lueType.Properties.Columns.Clear();
            lueType.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 40) { Visible = false }
                );
            lueType.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Name", 200) { Visible = true }
                );
            lueType.Properties.DisplayMember = "Name";
            lueType.Properties.ValueMember = "Id";
            lueType.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueType.Properties.AutoSearchColumnIndex = 1;
            lueType.Properties.DataSource = repo.GetItems<DDType>();


            //lueGroup
            lueGroup.Properties.ShowHeader = false;
            lueGroup.Properties.Columns.Clear();
            lueGroup.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 40) { Visible = false }
                );
            lueGroup.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Name", 200) { Visible = true }
                );
            lueGroup.Properties.DisplayMember = "Name";
            lueGroup.Properties.ValueMember = "Id";
            lueGroup.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueGroup.Properties.AutoSearchColumnIndex = 1;
            lueGroup.Properties.DataSource = repo.GetItems<DispGroup>();
            
            

        }

        public void InitCardsData()
        {
            if (Patient == null)
                return;
            gridCards.DataSource = repo.GetCards(Patient.Id);
            gridView1.OptionsBehavior.ReadOnly = true;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.Columns.Clear();
            gridView1.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Карты ДД",
                FieldName = "Info"
            });


            gvDiagn.OptionsBehavior.ReadOnly = true;
            gvDiagn.OptionsBehavior.Editable = false;
            gvDiagn.Columns.Clear();
            gvDiagn.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "МКБ",
                FieldName = "MKBCode"
            });

            gvDiagn.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Тип",
                FieldName = "DiseaseVid.Name"
            });

            gvDiagn.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Стадия",
                FieldName = "Stage.Name"
            });

            gvDiagn.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Доктор",
                FieldName = "Doctor"
            });

            gvDiagn.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Этап",
                FieldName = "Etap"
            });

            //var creator = new LookupCreator() { KeyColumn = "Id", ShowHeader = false, ShowKey = false, ValColumn = "Name" };
            //creator.Init(lueResult1, repo.GetResults(SelectedItem.TypeId.Value, 1));
            //creator.Init(lueResult2, repo.GetResults(SelectedItem.TypeId.Value, 2));


            //lueResult1
            lueResult1.Properties.ShowHeader = false;
            lueResult1.Properties.Columns.Clear();
            lueResult1.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 40) { Visible = false }
                );
            lueResult1.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Name", 200) { Visible = true }
                );
            lueResult1.Properties.DisplayMember = "Name";
            lueResult1.Properties.ValueMember = "Id";
            lueResult1.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueResult1.Properties.AutoSearchColumnIndex = 1;
            lueResult1.Properties.DataSource = repo.GetResults(SelectedCard.TypeId.Value, 1);

            //lueResult2
            lueResult2.Properties.ShowHeader = false;
            lueResult2.Properties.Columns.Clear();
            lueResult2.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 40) { Visible = false }
                );
            lueResult2.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Name", 200) { Visible = true }
                );
            lueResult2.Properties.DisplayMember = "Name";
            lueResult2.Properties.ValueMember = "Id";
            lueResult2.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            lueResult2.Properties.AutoSearchColumnIndex = 1;
            lueResult2.Properties.DataSource = repo.GetResults(SelectedCard.TypeId.Value, 2); ;

            meErrors.Text = SelectedCard.Errors;


        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var item = gridView1.GetFocusedRow() as DopDispTotal;
            if (item != null)
            {
                SelectedCard = item;
                InitCardData();
            }
                
        }

        private void InitCardData()
        {
            if (SelectedCard.DateBegin.HasValue)
                deStart.EditValue = SelectedCard.DateBegin;
            if (SelectedCard.DateEnd.HasValue)
                deEnd.EditValue = SelectedCard.DateEnd.Value;
            if (SelectedCard.TypeId.HasValue)
                lueType.EditValue = SelectedCard.TypeId.Value;
            if (SelectedCard.HealthGroupId.HasValue)
                lueGroup.EditValue = SelectedCard.HealthGroupId.Value;
            if (SelectedCard.IncompletionReasonId.HasValue)
                lueIncomlete.EditValue = SelectedCard.IncompletionReasonId.Value;
            if (SelectedCard.DispGroup.HasValue)
                lueDispGroup.EditValue = SelectedCard.DispGroup;
            if (SelectedCard.ResultStage1.HasValue)
                lueResult1.EditValue = SelectedCard.ResultStage1.Value;
            if (SelectedCard.ResultStage2.HasValue)
                lueResult2.EditValue = SelectedCard.ResultStage2.Value;
            if (SelectedCard.DoctorId.HasValue)
                lueDoctor.EditValue = SelectedCard.DoctorId.Value;
            if (!string.IsNullOrEmpty(SelectedCard.Errors))
                meErrors.Text = SelectedCard.Errors;

            gridDiagn.DataSource = repo.GetDignosList(SelectedCard.Id.Value);

        }

        
        private void btnAddDiagn_Click(object sender, EventArgs e)
        {
            using(var form = new DopDispDiagnosForm(LoggedUser))
            {
                form.ShowDialog();
            }
        }

        
        private void btnEditDiagn_Click(object sender, EventArgs e)
        {
            var diagn = gvDiagn.GetFocusedRow() as DopDispDiagnos;
            using (var form = new DopDispDiagnosForm(diagn, LoggedUser))
            {
                form.ShowDialog();
            }
        }

        
        private void btnDelDiagn_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            QuestinaryType qType;
            if (SelectedCard.TypeId==3)
            {
                if (patient.Age < 75)
                    qType = QuestinaryType.DD75;
                else
                    qType = QuestinaryType.DD;
            }else
            {
                qType = QuestinaryType.PMO;
            }

            using (var form = new DopDispQuestionsForm(patient, LoggedUser, qType ,SelectedCard.Id.Value))
            {
                form.ShowDialog();
            }

        }
    }
}
