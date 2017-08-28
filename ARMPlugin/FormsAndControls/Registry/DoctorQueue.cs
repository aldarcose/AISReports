using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NetrikaServices;
using Netrika=Model.Netrika;
using Model.Classes;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;


namespace ARMPlugin.FormsAndControls
{
    public partial class DoctorQueue : DevExpress.XtraEditors.XtraForm
    {
        public Patient Patient { get; set; }

        private int? idDistrict;
        private int? idLpu;
        private string idSpec;
        private string idDoc;
        private Netrika.Appointment[] apts;
 

        public DoctorQueue(Patient patient)
        {
            InitializeComponent();
            Patient = patient;
            InitForm();
        }

        private void InitForm()
        {

            using(var client = new Registry())
            {
                var districts=client.GetDistricts();
                lue_districts.Properties.DataSource = districts;
                lue_districts.Properties.DisplayMember = "DistrictName";
                lue_districts.Properties.ValueMember = "Id";
                lue_districts.Properties.ShowHeader = false;
            };

            lue_lpu.Properties.ValueMember = "Id";
            lue_lpu.Properties.DisplayMember = "ShortName";

            lue_lpu.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", 20, "ИД"));
            lue_lpu.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ShortName", 150, "ЛПУ"));

            lue_spec.Properties.ValueMember = "IdSpeсiality";
            lue_spec.Properties.DisplayMember = "NameSpeciality";
            lue_spec.Properties.Columns.Add(
                new LookUpColumnInfo("IdSpeсiality", 20, "ИД")
                );
            lue_spec.Properties.Columns.Add(
                new LookUpColumnInfo("NameSpeciality", 150, "Специальность")
                );
            lue_spec.Properties.Columns.Add(
                new LookUpColumnInfo("CountFreeParticipantIE", 20, "Талоны")
                );

            lue_doc.Properties.DisplayMember = "Name";
            lue_doc.Properties.ValueMember = "IdDoc";
            lue_doc.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IdDoc", 20, "ИД"));
            lue_doc.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", 200, "Имя"));
            lue_doc.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CountFreeParticipantIE", 20, "Талоны"));

            calendar.MaxSelectionCount = 1;

            gvSchedule.Columns.Clear();
            GridColumn idCol = gvSchedule.Columns.AddField("IdAppointment");
            idCol.Visible = false;
            GridColumn timeCol = gvSchedule.Columns.AddField("VisitStart");
            timeCol.Visible = true;
            timeCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeCol.DisplayFormat.FormatString = "t";
            timeCol.OptionsColumn.AllowEdit = false;
            timeCol.Caption = String.Empty;
            

        }

        private void lue_districts_EditValueChanged(object sender, EventArgs e)
        {
            idDistrict = Convert.ToInt32(lue_districts.EditValue);
            using(var client = new Registry())
            {
                var lpus = client.GetLPUList(idDistrict);
                lue_lpu.Properties.ShowHeader = false;
                lue_lpu.Properties.DataSource = lpus;
                
            }
        }

        private void lue_lpu_EditValueChanged(object sender, EventArgs e)
        {
            idLpu = Convert.ToInt32(lue_lpu.EditValue);
            using (var client = new Registry())
            {
                var result = client.GetSpecialities(idLpu);
                lue_spec.Properties.ShowHeader = true;
                lue_spec.Properties.DataSource = result;
                
            }
        }


        private void lue_spec_EditValueChanged(object sender, EventArgs e)
        {
            idSpec = lue_spec.EditValue.ToString();
            using (var client = new Registry())
            {
                var result = client.GetDoctors(idLpu,idSpec);
                lue_doc.Properties.ShowHeader = false;
                lue_doc.Properties.DataSource = result;
                
            }
        }
        
        private void lue_doc_EditValueChanged(object sender, EventArgs e)
        {
            ReloadAppts();
        }

        private void calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            var date = e.Start;
            //gridSchedule.DataSource = apts.Where(a => a.VisitStart.Date == date.Date).ToList();
            ReloadSchedule(date);
            
        }

        
        private void gvSchedule_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;
                e.Menu.Items.Clear();
                
                var itemWrite = new DXMenuItem("Записать", new EventHandler(OnWrite));
                itemWrite.Tag = view.GetRow(e.HitInfo.RowHandle) as Netrika.Appointment;
                e.Menu.Items.Add(itemWrite);

                //var itemCancel = new DXMenuItem("Отменить запись", new EventHandler(OnCancel));
                //itemWrite.Tag = view.GetRow(e.HitInfo.RowHandle) as Netrika.Appointment;
                //e.Menu.Items.Add(itemCancel);
                //e.Menu.Show(e.Point);

                var itemReload = new DXMenuItem("Обновить", new EventHandler(OnReload));
                itemWrite.Tag = view.GetRow(e.HitInfo.RowHandle) as Netrika.Appointment;
                e.Menu.Items.Add(itemReload);
                e.Menu.Show(e.Point);

            }
        }

        private void OnWrite(object sender, EventArgs e)
        {
            var item = sender as DXMenuItem;
            string apptId = (item.Tag as Netrika.Appointment).IdAppointment;
            using(var client = new Registry())
            {
                var patient = Netrika.Utils.ConvertPatient(Patient);
                var result = client.CheckPatient(patient,idLpu);
                
                if (result == null || result.IdPat == null)
                {
                    var newPatient=client.AddNewPatient(patient, idLpu);
                    if (newPatient==null || newPatient.IdPat==null)
                    {
                        XtraMessageBox.Show("Пациента не удалось записать");
                        return;
                    }
                }

                if (result!=null && result.IdPat!=null)
                {
                    var apptResult = client.SetAppointment(apptId, result.IdPat, idLpu, result.IdHistory);
                    if (apptResult!=null && apptResult.Success.HasValue && apptResult.Success.Value)
                    {
                        XtraMessageBox.Show("Пациент успешно записан");
                        ReloadAppts();
                        return;
                    }
                }

            }
        }

        /// <summary>
        /// Получаем расписание для конкретной даты
        /// </summary>
        /// <param name="date">Дата</param>
        private void ReloadAppts(DateTime? date=null)
        {
            var startDate = DateTime.Today.AddDays(1);
            var endDate = startDate.AddMonths(2);
            //определяем начало текущей недели
            if(date.HasValue)
            {
                 startDate = date.Value;
                 endDate = date.Value;
            }

            var dayOfWeek = startDate;
            idDoc = lue_doc.EditValue.ToString();

            using (var client = new Registry())
            {
                var result = client.GetAppointments(idDoc, idLpu, startDate, endDate);
                if (result != null)
                {
                    var dates = result.Select(d => d.VisitStart).ToArray();
                    calendar.BoldedDates = dates;
                    apts = result.ToArray();
                    var selectedDate = calendar.SelectionStart;
                    ReloadSchedule(selectedDate);
                }
            }
        }

        private void ReloadSchedule(DateTime? date)
        {
            if (!date.HasValue)
                return;
            var timeCol = gvSchedule.VisibleColumns.FirstOrDefault();
            timeCol.Caption = date.Value.ToShortDateString();
            var dateAppts = apts.Where(a => a.VisitStart.Date == date.Value.Date);
            gridSchedule.DataSource = dateAppts;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            var item = sender as DXMenuItem;
            string apptId = (item.Tag as Netrika.Appointment).IdAppointment;
            //TODO: отменить запись пациента на прием
        }

        private void OnReload(object sender, EventArgs e)
        {
            //var item = sender as DXMenuItem;
            //string apptId = (item.Tag as Netrika.Appointment).IdAppointment;
            //TODO: Обновить время приема
            ReloadAppts();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            using(var client = new Registry())
            {
                var patient = Netrika.Utils.ConvertPatient(Patient);
                var result = client.CheckPatient(patient, idLpu);
                if (result!=null && result.IdPat!=null)
                {
                    using (var form = new RegistryHistoryForm(idLpu, result.IdPat))
                    {
                        form.ShowDialog();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Пациент не найден!");
                }
            }
        }
        
    }


    public class DocAppointment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public int Label { get; set; }
        public string Location { get; set; }
        public bool AllDay { get; set; }
        public int EventType { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ReminderInfo { get; set; }
        public object OwnerId { get; set; }
    }

    public class DocQueueResource
    {
        public string Name { get; set; }
        public int ResID { get; set; }
        public Color ResColor { get; set; }
    }
}