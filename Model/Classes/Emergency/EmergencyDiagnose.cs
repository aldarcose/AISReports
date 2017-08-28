using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Emergency
{
    public class EmergencyDiagnose : ILoadData, ICreateable, IDeleteable, ISaveable
    {
        /// <summary>
        /// Ид в базе
        /// </summary>
        [Browsable(false)]
        public long Id { get; set; }
        /// <summary>
        /// Ид карты
        /// </summary>
        [Browsable(false)]
        public long CallCardId { get; set; }

        /// <summary>
        /// Код диагноза
        /// </summary>
        [DisplayName("Код по МКБ")]
        public string MkbCode { get; set; }

        /// <summary>
        /// Диагноз
        /// </summary>
        [DisplayName("МКБ")]
        public string MkbName { get; set; }
        /// <summary>
        /// Вид диагноза (основной, сопутствующий и т.п.)
        /// </summary>
        [Browsable(false)]
        public long DiagnoseTypeId { get; set; }

        /// <summary>
        /// Вид диагноза (основной, сопутствующий и т.п.)
        /// </summary>
        [DisplayName("Тип диагноза")]
        public string DiagnoseTypeName { get; set; }
        /// <summary>
        /// Приоритет по виду диагноза (основной, сопутствующий и т.п.)
        /// </summary>
        [Browsable(false)]
        public long DiagnoseTypeSort { get; set; }
        /// <summary>
        /// Ид доктора
        /// </summary>
        [Browsable(false)]
        public long DoctorId { get; set; }
        /// <summary>
        /// Доктор поставивший диагноз
        /// </summary>
        [DisplayName("Доктор")]
        public Doctor Doctor { get; set; }
        /// <summary>
        /// Дата постановки диагноза
        /// </summary>
        [DisplayName("Дата постановки")]
        public DateTime SetDate { get; set; }

        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            if (Loading != null)
                Loading(this, null);
        }

        public void OnLoaded()
        {
            IsLoading = false;
            IsLoaded = true;
            if (Loaded != null)
                Loaded(this, null);
        }
        [Browsable(false)]
        public bool IsLoading { get; private set; }
        [Browsable(false)]
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            throw new NotImplementedException();
        }

        public bool LoadData(DbResult result)
        {
            this.Id = DbResult.GetNumeric(result.GetByName("id"), -1);
            if (this.Id  == -1)
                return false;

            this.MkbCode = DbResult.GetString(result.GetByName("mkb_code"), "");
            this.MkbName = DbResult.GetString(result.GetByName("mkb_name"), "");
            this.DiagnoseTypeId = DbResult.GetNumeric(result.GetByName("diagnose_type_id"), -1);
            this.DoctorId = DbResult.GetNumeric(result.GetByName("doctor_id"), -1);
            this.Doctor = new Doctor();
            this.Doctor.LoadData(this.DoctorId);
            this.SetDate = DbResult.GetDateTime(result.GetByName("set_date"), DateTime.MinValue);

            using (var dbWorker = new DbWorker())
            {
                var q = new DbQuery("GetDiagnoseType");
                q.Sql = "select * from codifiers.sp_disease_vid_tab where id = @id limit 1;";
                q.AddParamWithValue("@id", this.DiagnoseTypeId);

                var r = dbWorker.GetResult(q);
                if (r != null)
                {
                    this.DiagnoseTypeName = DbResult.GetString(r.GetByName("name"), "");
                    this.DiagnoseTypeSort = (int)DbResult.GetNumeric(r.GetByName("sort"), -1);
                }
            }

            return true;
        }

        public event EventHandler Saved;
        public event EventHandler Saving;
        [Browsable(false)]
        public bool IsSaved { get; set; }
        public void OnSaving()
        {
            if (Saving != null)
            {
                Saving(this, null);
            }
        }

        public void OnSaved()
        {
            if (Saved != null)
            {
                Saved(this, null);
            }
        }

        public bool CanSave(Operator @operator, out string message)
        {
            message = "";
            return true;
        }

        public void Save(Operator @operator)
        {
            if (Id > 0)
            {
                using (var db = new DbWorker())
                {
                    var q = new DbQuery("InsertDiagnose");
                    q.Sql = @"update 
                      emergency.call_card_diagnoses_tab set
                      call_card_id = @call_card_id, mkb_code = @mkb_code, mkb_name = @mkb_name, diagnose_type_id = @diagnose_type_id, doctor_id = @doctor_id, set_date = @set_date 
                      where id = @id;";

                    q.AddParamWithValue("@id", Id);
                    q.AddParamWithValue("@call_card_id", CallCardId);
                    q.AddParamWithValue("@mkb_code", MkbCode);
                    q.AddParamWithValue("@mkb_name", MkbName);
                    q.AddParamWithValue("@diagnose_type_id", DiagnoseTypeId);
                    q.AddParamWithValue("@doctor_id", DoctorId);
                    q.AddParamWithValue("@set_date", SetDate);

                    var r = db.Execute(q);
                    if (r > 0)
                    {
                        OnCreated();
                    }
                }
            }
            else
            {
                CreateInDb(@operator);
            }
        }

        public event EventHandler<CancelableEventArgs> Deleting;
        public event EventHandler Deleted;
        [Browsable(false)]
        public bool ToDelete { get; set; }
        private CancelableEventArgs _cancelable;
        public void OnDeleting()
        {
            if (Deleting != null)
            {
                _cancelable = new CancelableEventArgs();
                Deleting(this, _cancelable);
            }
        }

        public void OnDeleted()
        {
            if (Deleted != null)
            {
                Deleted(this, null);
            }
        }

        public bool CanDeleteFromDb(Operator @operator, out string message)
        {
            message = string.Empty;
            return true;
        }

        public void DeleteFromDb(Operator @operator)
        {
            if (Id == -1)
                return;

            OnDeleting();
            if (_cancelable != null && _cancelable.Cancel)
                return;

            using (var db = new DbWorker())
            {
                var q = new DbQuery("DeleteDiagnose");
                q.Sql = "DELETE FROM emergency.call_card_diagnoses_tab WHERE id = @id;";
                q.AddParamWithValue("@id", Id);

                var r = db.Execute(q);
                if (r > 0)
                {
                    OnDeleted();
                }
            }
        }

        public event EventHandler Created;
        public void OnCreated()
        {
            if (Created != null)
                Created(this, null);
        }

        public bool CanCreateInDb(Operator @operator, out string message)
        {
            message = "";
            return true;
        }

        public void CreateInDb(Operator @operator)
        {
            using (var db = new DbWorker())
            {
                var idQ = new DbQuery("GetNewEmergencyDiagnoseId");
                idQ.Sql = "select nextval('emergency.call_card_diagnoses_tab_id_seq');";
                var newId = DbResult.GetNumeric(db.GetScalarResult(idQ), -1);
                if (newId == -1)
                    return;

                Id = newId;
                var q = new DbQuery("InsertDiagnose");
                q.Sql = @"INSERT INTO 
                      emergency.call_card_diagnoses_tab
                      (id, call_card_id, mkb_code, mkb_name, diagnose_type_id, doctor_id, set_date) 
                      VALUES (
                      @id,
                      @call_card_id,
                      @mkb_code,
                      @mkb_name,
                      @diagnose_type_id,
                      @doctor_id,
                      @set_date);";

                q.AddParamWithValue("@id", Id);
                q.AddParamWithValue("@call_card_id", CallCardId);
                q.AddParamWithValue("@mkb_code", MkbCode);
                q.AddParamWithValue("@mkb_name", MkbName);
                q.AddParamWithValue("@diagnose_type_id", DiagnoseTypeId);
                q.AddParamWithValue("@doctor_id", DoctorId);
                q.AddParamWithValue("@set_date", SetDate);

                var r = db.Execute(q);
                if (r > 0)
                {
                    OnCreated();
                }
            }
        }
    }
}
