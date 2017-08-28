using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model;
using Model.Classes.Vaccination;
using Model.Classes;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;

namespace ARMPlugin.FormsAndControls
{
    public partial class VaccinationEditItemForm : DevExpress.XtraEditors.XtraForm
    {
        Operator loggedUser;
        Patient patient;
        VaccinationRepository repo;
        Vaccination vaccination = new Vaccination();

        public VaccinationEditItemForm(Operator loggedUser, Patient patient, long? vacId=null)
        {
            InitializeComponent();
            this.loggedUser = loggedUser;
            this.patient = patient;
            repo = new VaccinationRepository();
            if (vacId != null)
                vaccination = repo.GetItem(vacId);
            else
                vaccination = new Vaccination();
            InitForm();
        }

        private void InitForm()
        {
            
            #region init lookups
            //preparat
            luMedication.Properties.DataSource = repo.GetPreparatList();
            luMedication.Properties.ShowHeader = false;
            luMedication.Properties.Columns.Clear();
            luMedication.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible=false}
                );
            luMedication.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            luMedication.Properties.DisplayMember = "Name";
            luMedication.Properties.ValueMember = "Id";
            luMedication.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luMedication.Properties.AutoSearchColumnIndex = 1;

            //types
            luType.Properties.DataSource = repo.GetTypes();
            luType.Properties.ShowHeader = false;
            luType.Properties.Columns.Clear();
            luType.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            luType.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            luType.Properties.DisplayMember = "Name";
            luType.Properties.ValueMember = "Id";
            luType.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luType.Properties.AutoSearchColumnIndex = 1;

            //doctors
            luDoctors.Properties.DataSource = repo.GetDoctors();
            luDoctors.Properties.ShowHeader = false;
            luDoctors.Properties.Columns.Clear();
            luDoctors.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            luDoctors.Properties.Columns.Add(
                new LookUpColumnInfo("FIO", "Название", 200) { Visible = true }
                );
            luDoctors.Properties.DisplayMember = "FIO";
            luDoctors.Properties.ValueMember = "Id";
            luDoctors.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luDoctors.Properties.AutoSearchColumnIndex = 1;
            if (loggedUser.DoctorId != 0 || loggedUser.DoctorId != -1)
            {
                luDoctors.EditValue = loggedUser.DoctorId;
            }


            //nurses
            luNurse.Properties.DataSource = repo.GetNurses();
            luNurse.Properties.ShowHeader = false;
            luNurse.Properties.Columns.Clear();
            luNurse.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            luNurse.Properties.Columns.Add(
                new LookUpColumnInfo("FIO", "Название", 200) { Visible = true }
                );
            luNurse.Properties.DisplayMember = "FIO";
            luNurse.Properties.ValueMember = "Id";
            luNurse.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luNurse.Properties.AutoSearchColumnIndex = 1;


            //MO Place
            luPlace.Properties.DataSource = repo.GetMO();
            luPlace.Properties.ShowHeader = false;
            luPlace.Properties.Columns.Clear();
            luPlace.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            luPlace.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            luPlace.Properties.DisplayMember = "Name";
            luPlace.Properties.ValueMember = "Id";
            luPlace.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luPlace.Properties.AutoSearchColumnIndex = 1;
            luPlace.EditValue = 2301001;

            //payer
            luPayer.Properties.DataSource = repo.GetMO();
            luPayer.Properties.ShowHeader = false;
            luPayer.Properties.Columns.Clear();
            luPayer.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            luPayer.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            luPayer.Properties.DisplayMember = "Name";
            luPayer.Properties.ValueMember = "Id";
            luPayer.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luPayer.Properties.AutoSearchColumnIndex = 1;
            luPayer.EditValue = 2301001;

            //units
            luUnits.Properties.DataSource = repo.GetUnits();
            luUnits.Properties.ShowHeader = false;
            luUnits.Properties.Columns.Clear();
            luUnits.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            luUnits.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            luUnits.Properties.DisplayMember = "Name";
            luUnits.Properties.ValueMember = "Id";
            luUnits.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luUnits.Properties.AutoSearchColumnIndex = 1;

            //infections
            luInfections.Properties.DataSource = repo.GetInfections();
            luInfections.Properties.ShowHeader = false;
            luInfections.Properties.Columns.Clear();
            luInfections.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            luInfections.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            luInfections.Properties.DisplayMember = "Name";
            luInfections.Properties.ValueMember = "Id";
            luInfections.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luInfections.Properties.AutoSearchColumnIndex = 1;
            

            //reactions
            luReaction.Properties.DataSource = repo.GetReactions();
            luReaction.Properties.ShowHeader = false;
            luReaction.Properties.Columns.Clear();
            luReaction.Properties.Columns.Add(
                new LookUpColumnInfo("Id", "Id", 10) { Visible = false }
                );
            luReaction.Properties.Columns.Add(
                new LookUpColumnInfo("Name", "Название", 200) { Visible = true }
                );
            luReaction.Properties.DisplayMember = "Name";
            luReaction.Properties.ValueMember = "Id";
            luReaction.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
            luReaction.Properties.AutoSearchColumnIndex = 1;
            luReaction.EditValue = 7400000; //отрицательная реакция
            #endregion

            deDate.EditValue = DateTime.Now;
            lcgVacData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //на редактирование
            if (vaccination.Id.HasValue)
            {
                luMedication.EditValue = vaccination.PreparatId;
                luType.EditValue = vaccination.Type;
                deDate.EditValue = vaccination.Date;
                luReaction.EditValue = vaccination.ReactionId;
                luDoctors.EditValue = vaccination.DoctorId;
                teSeria.Text = vaccination.Series;
                teDosage.Text = vaccination.Dosage.ToString();
                luUnits.EditValue = vaccination.Unit;
                luInfections.EditValue = vaccination.InfectionId;

                ceTemplate.Checked = true;
            }

        }

        private void InitTemplates(long preparatId)
        {
            gcTemplate.DataSource = repo.GetItemsByPreparat(preparatId);

            gvTemplate.OptionsBehavior.Editable = false;

            gvTemplate.Columns.Clear();

            var colPreparat = new GridColumn() { 
                Caption="Препарат", VisibleIndex=0, Visible=true, Width=25,
                FieldName="PreparatId"
            };
            gvTemplate.Columns.Add(colPreparat);

            var colUnit = new GridColumn()
            {
                Caption = "Ед. изм.",
                VisibleIndex = 0,
                Visible = true,
                Width = 25,
                FieldName = "UnitName"
            };
            gvTemplate.Columns.Add(colUnit);

            var colDosage = new GridColumn()
            {
                Caption = "Доза",
                VisibleIndex = 0,
                Visible = true,
                Width = 25,
                FieldName = "Dosage"
            };
            gvTemplate.Columns.Add(colDosage);

            var colSeries = new GridColumn()
            {
                Caption = "Серия",
                VisibleIndex = 0,
                Visible = true,
                Width = 25,
                FieldName = "Series"
            };
            gvTemplate.Columns.Add(colSeries);

            var colInfection = new GridColumn()
            {
                Caption = "Инфекция",
                VisibleIndex = 0,
                Visible = true,
                Width = 25,
                FieldName="InfectionName"
            };
            gvTemplate.Columns.Add(colInfection);

        }

        private void GetItemData()
        {
            if (vaccination == null)
                return;

            luMedication.EditValue=vaccination.PreparatId;
            luType.EditValue=vaccination.Type;
            deDate.EditValue=vaccination.Date;
            luReaction.EditValue=vaccination.ReactionId;
            luDoctors.EditValue=vaccination.DoctorId;
            luNurse.EditValue=vaccination.NurseId;

            if (lcgVacData.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                teSeria.Text=vaccination.Series;
                teDosage.Text = vaccination.Dosage.ToString();
                luUnits.EditValue=vaccination.Unit;
                luInfections.EditValue=vaccination.InfectionId;
            }

        }

        private void InitItemData()
        {
            vaccination.PatientId = patient.Id;
            vaccination.PreparatId = (long?)luMedication.EditValue;
            vaccination.Type = (long?)luType.EditValue;
            vaccination.Date = (DateTime?)deDate.EditValue;
            vaccination.ReactionId = (long?)luReaction.EditValue;
            vaccination.DoctorId = (long?)luDoctors.EditValue;
            if (luNurse.EditValue!=null)
                vaccination.NurseId = (long)luNurse.EditValue;
            vaccination.OperatorId = loggedUser.Id;
            
            if (lcgVacData.Visibility==DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                double dosage = 0;
                vaccination.Series = (string)teSeria.Text;
                if (Double.TryParse(teDosage.Text, out dosage))
                {
                    vaccination.Dosage = dosage;
                }
                else
                {
                    vaccination.Dosage = null;
                }

                vaccination.Unit = (long?)luUnits.EditValue;
                vaccination.InfectionId = (long?)luInfections.EditValue;  
            }
            
        }
        
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            InitItemData();
            string message=string.Empty;
            
            if (!repo.CanSave(vaccination, out message))
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else
            {
                repo.Save(vaccination);
                this.Close();
            }

            
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void luMedication_EditValueChanged(object sender, EventArgs e)
        {
            if (luMedication.EditValue==null)
                return;
            InitTemplates((long)luMedication.EditValue);
        }

        private void gvTemplate_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var vacItem = gvTemplate.GetRow(e.RowHandle) as Vaccination;
            vaccination.Series = vacItem.Series;
            vaccination.Dosage = vacItem.Dosage;
            vaccination.Unit = vacItem.Unit;
            vaccination.InfectionId = vacItem.InfectionId;

        }

        private void ceTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (ceTemplate.Checked)
            {
                lcgVacData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lciTemplate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }else
            {
                lcgVacData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lciTemplate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
        }





        
    }
}