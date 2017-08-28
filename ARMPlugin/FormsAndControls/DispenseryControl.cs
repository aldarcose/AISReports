using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model;
using Model.Classes;
using Model.Classes.Dispensery;
using DevExpress.XtraGrid.Columns;
using Model.Classes.Benefits;

namespace ARMPlugin.FormsAndControls
{
    public partial class DispenseryControl : DevExpress.XtraEditors.XtraUserControl
    {

        public Operator LoggedUser { get; set; }

        public Patient Patient
        {
            get { return patient; }
            set { patient = value; InitForm(); }
        }

        Patient patient;
        DispenseryRepository repo =new DispenseryRepository();

        public DispenseryControl()
        {
            InitializeComponent();
            InitForm();
        }

        private void InitForm()
        {
            if (patient == null) return;

            gridView.OptionsBehavior.Editable = false;
            gridView.Columns.Clear();
            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Льгота",
                FieldName = "BenefitName"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Диагноз",
                FieldName = "Diagnosis"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Доктор",
                FieldName = "Doctor"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Дата взятия",
                FieldName = "EntryDate"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Дата снятия",
                FieldName = "RemovalDate"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Причина снятия",
                FieldName = "RemovalReason"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Форма взятия",
                FieldName = "EntryReason"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Документ",
                FieldName = "Document"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Серия",
                FieldName = "Series"
            });

            gridView.Columns.Add(new GridColumn()
            {
                Visible = true,
                Caption = "Номер",
                FieldName = "Number"
            });

            ReloadData();
            
        }

        private void ReloadData()
        {
            grid.DataSource = repo.GetPatientBenefits(patient.Id);
            grid.RefreshDataSource();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new DispenseryEditForm(patient.Id, LoggedUser))
            {
                form.ShowDialog();
            }
            
            ReloadData();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            var rowHandle = gridView.GetSelectedRows().FirstOrDefault();
            var item = gridView.GetRow(rowHandle) as PatientBenefit;
            
            var patientBenefit = repo.Load(item.Id.Value);

            using (var form = new DispenseryEditForm(patientBenefit, LoggedUser))
            {
                form.ShowDialog();
            }

            ReloadData();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (!LoggedUser.Permissions.Any(c => c.Id == (long)Permissions.Admin))
            {
                XtraMessageBox.Show("Удаление недоступно!");
                return;
            }

            var rowHandle = gridView.GetSelectedRows().FirstOrDefault();
            var item = gridView.GetRow(rowHandle) as PatientBenefit;
            if (item.BenefitId!=null && item.BenefitId.Length != 2)
            {
                XtraMessageBox.Show("Выберите региональную льготу");
                return;
            }
            else
            {
                var patientBenefit = repo.Load(item.Id.Value);
                repo.Delete(patientBenefit);
            }

            ReloadData();

        }
    }
}
