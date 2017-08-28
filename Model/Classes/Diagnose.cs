using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;

namespace Model.Classes
{
    public class Diagnose : ILoadData, IDeleteable, ICreateable, ISaveable
    {
        /// <summary>
        /// Ид в базе (diagn_tab)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Код диагноза
        /// </summary>
        public string MkbCode { get; set; }

        /// <summary>
        /// Диагноз
        /// </summary>
        public string MkbName { get; set; }

        /// <summary>
        /// Диагноз травмы
        /// </summary>
        public bool IsTrauma
        {
            get { return MkbCode.StartsWith("T") || MkbCode.StartsWith("S"); }
        }
        
        /// <summary>
        /// Выявлен впервые
        /// </summary>
        public bool IsFirst { get; set; }
        /// <summary>
        /// Подтвержден лабораторно
        /// </summary>
        public bool LabConfirmed { get; set; }
        /// <summary>
        /// Флаг Зарегистрированное заболевание (учитывается в Ф.12)
        /// </summary>
        public bool IsRegistrered { get; set; }

        /// <summary>
        /// Вид диагноза (основной, сопутствующий и т.п.)
        /// </summary>
        public long DiagnoseTypeId { get; set; }
        /// <summary>
        /// Приоритет по виду диагноза (основной, сопутствующий и т.п.)
        /// </summary>
        public long DiagnoseTypeSort { get; set; }
        /// <summary>
        /// Тип диагноза
        /// </summary>
        public string DiagnoseType { get; set; }
        /// <summary>
        /// Id стадии
        /// </summary>
        public long DiseaseStadiaId { get; set; }

        /// <summary>
        /// Стадия
        /// </summary>
        public string DiseaseStadia { get; set; }
        /// <summary>
        /// Характер заболевания
        /// </summary>
        public long DiseaseCharacterId { get; set; }

        /// <summary>
        /// Характер диагноза
        /// </summary>
        public string DiseaseCharacter { get; set; }


        /// <summary>
        /// Дата постановки диагноза
        /// </summary>
        public DateTime SetDate { get; set; }
        /// <summary>
        /// Дата внесения записи
        /// </summary>
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// Ид доктора, поставившего диагноз
        /// </summary>
        public long DoctorId { get; set; }
        /// <summary>
        /// Ид пациента, которому поставили диагноз
        /// </summary>
        public long PatientId { get; set; }
        /// <summary>
        /// Ид талона, в рамках которого поставлен диагноз
        /// </summary>
        public long TalonId { get; set; }
        /// <summary>
        /// описание
        /// </summary>
        public string Description { get; set; }

        public event EventHandler Loading;
        public event EventHandler Loaded;
        public void OnLoading()
        {
            IsLoading = true;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public void OnLoaded()
        {
            IsLoading = false;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            OnLoading();
            var loadResult = false;
            Id = id;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetDiagnose");
                q.Sql = "SELECT d.*," +
                        "c.name as character_name, v.name AS type_name, m.mkb as mkb_name, s.name_stadiya as stadia_name, v.sort as diagn_type_sort " +
                        "FROM diagn_tab d " +
                        "LEFT JOIN public.sp_stadiya_diagn_tab s ON d.stage_of_disease = s.sp_stadiya_diagn_id " +
                        "LEFT JOIN codifiers.sp_disease_vid_tab v ON v.id = d.sp_disease_vid_id " +
                        "LEFT JOIN codifiers.diagn_type_tab c ON d.diagn_type_id = c.id " +
                        "LEFT JOIN mkb_tab m ON m.kod_d = d.diagn_osn " +
                        "WHERE d.id = @id;";
                q.AddParamWithValue("@id", id);

                var result = db.GetResult(q);
                if (result != null && result.Fields.Count > 0)
                {
                    LoadData(result);

                    loadResult = true;
                }
            }

            IsLoaded = loadResult;
            OnLoaded();
            return loadResult;
        }

        public bool LoadData(DbResult result)
        {
            if (result != null)
            {
                Id = DbResult.GetNumeric(result.GetByName("id"), -1);

                MkbCode = DbResult.GetString(result.GetByName("diagn_osn"), "");
                DoctorId = DbResult.GetNumeric(result.GetByName("doctor_id"), -1);
                IsFirst = DbResult.GetNumeric(result.GetByName("first_time"), 0) == 1;
                IsRegistrered = DbResult.GetNumeric(result.GetByName("count"), 0) == 1;
                PatientId = DbResult.GetNumeric(result.GetByName("dan_id"), -1);
                TalonId = DbResult.GetNumeric(result.GetByName("talon_id"), -1);
                SetDate = DbResult.GetDateTime(result.GetByName("data"), DateTime.MinValue);
                RecordDate = DbResult.GetDateTime(result.GetByName("data_vnes_inf"), DateTime.MinValue);
                DiagnoseTypeId = DbResult.GetNumeric(result.GetByName("sp_disease_vid_id"), -1);
                DiagnoseTypeSort = DbResult.GetNumeric(result.GetByName("diagn_type_sort"), -1);

                DiseaseCharacterId = DbResult.GetNumeric(result.GetByName("diagn_type_id"), -1);
                DiseaseStadiaId = DbResult.GetNumeric(result.GetByName("stage_of_disease"), -1);
                LabConfirmed = DbResult.GetBoolean(result.GetByName("lab_confirmation"), false);

                DiagnoseType = DbResult.GetString(result.GetByName("type_name"), "");
                DiseaseCharacter = DbResult.GetString(result.GetByName("character_name"), "");
                MkbName = DbResult.GetString(result.GetByName("mkb_name"), "");
                DiseaseStadia = DbResult.GetString(result.GetByName("stadia_name"), "");

                Description = DbResult.GetString(result.GetByName("opis_diagn"), "");
                return true;
            }
            return false;
        }

        public event EventHandler<CancelableEventArgs> Deleting;
        public event EventHandler Deleted;
        private CancelableEventArgs _cancelEventArgs = null;
        public void OnDeleting()
        {
            _cancelEventArgs = new CancelableEventArgs();
            if (Deleting != null)
            {
                Deleting(this, _cancelEventArgs);
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
            /*int daysToEdit = 7;
            if (RecordDate < DateTime.Now.AddDays(-daysToEdit))
            {
                message = "Для данного диагноза превышен срок редактирования.";
                return false;
            }*/

            if (DoctorId != @operator.DoctorId)
            {
                message = "Данный диагноз поставлен другим доктором.";
                
                return false;
            }
            message = string.Empty;
            return true;
        }

        public void DeleteFromDb(Operator @operator)
        {
            OnDeleting();
            if (_cancelEventArgs != null && _cancelEventArgs.Cancel)
                return;

            using (var db = new DbWorker())
            {
                var q = new DbQuery("DeleteDiagnose");
                q.Sql = "DELETE FROM diagn_tab WHERE id = @id;";
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
            {
                Created(this, null);
            }
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
                var qId = new DbQuery("GettingNewDiagnoseId");
                qId.Sql = "Select gen_tab_id('public', 'diagn_tab');";
                var id = DbResult.GetNumeric(db.GetScalarResult(qId), -1);
                if (id == -1)
                {
                    throw new DbWorkerException("Ошибка получения нового ID") { Sql = qId.Sql };
                }
                else
                {
                    Id = id;
                    var q = new DbQuery("Inserting Diagnose");
                    q.Sql = "Insert into public.diagn_tab " +
                            "(id, diagn_osn, doctor_id, dan_id, talon_id, diagn_type_id, sp_disease_vid_id, data_vnes_inf, operat_id, date_vnes_inf, operator_id, data, first_time, lab_confirmation, count, opis_diagn) " +
                            "values(@id, @mkb, @doctor, @patient, @talon, @diagn_character, @diagn_type, @create_date, @operator, @create_date, @operator, @set_date, @first, @lab, @f12,@descript);";
                    q.AddParamWithValue("@id", id);
                    q.AddParamWithValue("@mkb", MkbCode);
                    q.AddParamWithValue("@doctor", DoctorId);
                    q.AddParamWithValue("@patient", PatientId);
                    q.AddParamWithValue("@talon", TalonId);
                    q.AddParamWithValue("@diagn_character", DiseaseCharacterId);
                    q.AddParamWithValue("@diagn_type", DiagnoseTypeId);
                    q.AddParamWithValue("@create_date", RecordDate);
                    q.AddParamWithValue("@operator", @operator.Id);
                    q.AddParamWithValue("@set_date", SetDate);
                    q.AddParamWithValue("@first", IsFirst ? 1 : 0);
                    q.AddParamWithValue("@lab", LabConfirmed);
                    q.AddParamWithValue("@f12", IsRegistrered ? 1 : 0);
                    q.AddParamWithValue("@descript", Description);
                    var r = db.Execute(q);
                    if (r > 0)
                    {
                        OnCreated();
                    }
                }
            }
        }

        public event EventHandler Saved;
        public event EventHandler Saving;
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
            IsSaved = true;
            if (Saved != null)
            {
                Saved(this, null);
            }
        }

        public bool CanSave(Operator @operator, out string message)
        {
            if (Id == -1)
            {
                return CanCreateInDb(@operator, out message);
            }
            else
            {
                if (@operator.DoctorId != DoctorId)
                {
                    message = "Диагноз поставлен другим врачом!";
                    return false;
                }
            }
            message = string.Empty;
            return true;
        }

        public void Save(Operator @operator)
        {
            if (Id == -1)
            {
                this.CreateInDb(@operator);
                OnSaved();
            }
            else
            {
                using (var db = new DbWorker())
                {
                    var q = new DbQuery("UpdateDiagnose");
                    q.Sql = @"Update public.diagn_tab SET 
                                diagn_osn = @mkb,
                                doctor_id = @doctor,
                                dan_id = @patient,
                                talon_id = @talon,
                                diagn_type_id = @diagn_character, 
                                sp_disease_vid_id = @diagn_type, 
                                stage_of_disease = @diagn_stage, 
                                first_time = @first, 
                                lab_confirmation = @lab, 
                                count = @f12, 
                                data_vnes_inf = @create_date, 
                                operat_id = @operator, 
                                data = @date_pos,
                                opis_diagn = @descript 
                                where id = @id";
                    q.AddParamWithValue("@id", Id);
                    q.AddParamWithValue("@mkb", MkbCode);
                    q.AddParamWithValue("@doctor", DoctorId);
                    q.AddParamWithValue("@patient", PatientId);
                    q.AddParamWithValue("@talon", TalonId);
                    q.AddParamWithValue("@diagn_character", DiseaseCharacterId);
                    q.AddParamWithValue("@diagn_type", DiagnoseTypeId);
                    q.AddParamWithValue("@diagn_stage", DiseaseStadiaId == -1 ? null : (int?)DiseaseStadiaId);

                    q.AddParamWithValue("@first", IsFirst ? 1 : 0);
                    q.AddParamWithValue("@lab", LabConfirmed);
                    q.AddParamWithValue("@f12", IsRegistrered ? 1 : 0);

                    q.AddParamWithValue("@create_date", RecordDate);
                    q.AddParamWithValue("@operator", @operator.Id);
                    q.AddParamWithValue("@date_pos", SetDate);
                    q.AddParamWithValue("@descript", Description);

                    var r = db.Execute(q);
                    if (r > 0)
                    {
                        OnSaved();
                    }
                }
                OnSaved();
            }
        }

        public override string ToString()
        {
            return this.MkbCode;
        }
    }

    public class DiagnoseRepository
    {
        public IEnumerable<Diagnose> GetByTalon(long? talonId)
        {
            if (!talonId.HasValue)
                return null;
            
            using(var db = new DbWorker())
            {
                var sql = @"SELECT d.diagn_osn mkbcode, d.doctor_id doctorid, d.first_time isfirst, d.dan_id patientid,
                            d.talon_id talonid, d.data setdate,d.data_vnes_inf recorddate,sp_disease_vid_id diagnosetypeid,
                            d.diagn_type_id diseasecharacterid,d.stage_of_disease diseasestadiaid,
                            d.lab_confirmation labconfirmed,d.opis_diagn description,
                            c.name diseasecharacter, v.name diagnosetype, m.mkb as mkbname, 
                            s.name_stadiya diseasestadia, 
                            v.sort as diagnosetypesort
                            FROM diagn_tab d
                            LEFT JOIN public.sp_stadiya_diagn_tab s ON d.stage_of_disease = s.sp_stadiya_diagn_id
                            LEFT JOIN codifiers.sp_disease_vid_tab v ON v.id = d.sp_disease_vid_id
                            LEFT JOIN codifiers.diagn_type_tab c ON d.diagn_type_id = c.id
                            LEFT JOIN mkb_tab m ON m.kod_d = d.diagn_osn
                            WHERE d.talon_id = @Id";
                var result = db.Connection.Query<Diagnose>(sql, new { Id = talonId });
                return result;
            }
        }
    }
}
