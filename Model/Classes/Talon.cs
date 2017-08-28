using System.Linq;
using Model.Classes.Codifiers;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using System;
using System.Collections.Generic;
using NLog;
using Dapper;

namespace Model.Classes
{
    public class Talon : ILoadData, ICreateable, ISaveable
    {
        private long _cureResultId;
        private long _blistId;
        private bool _isTrauma;

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public Talon()
        {
            IsLoading = false;
            IsLoaded = false;

            IsTrauma = false;
            Visits = new List<Visit>();

            CureResult = new CureResult();
            Blist = new BList();

            FirstVisitConditionId = -1;
        }

        public Talon(long id) : this()
        {
            LoadData(id);
        }
        /// <summary>
        /// Id талона
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Ид результата лечения
        /// </summary>
        public long CureResultId
        {
            get { return _cureResultId; }
            set
            {
                _cureResultId = value;
                if (value != -1)
                {
                    CureResult.LoadData(value);
                }
            }
        }
        /// <summary>
        /// Результат лечения
        /// </summary>
        public CureResult CureResult { get; private set; }

        /// <summary>
        /// Флаг, указывающий на то, что талон уже закрыт
        /// </summary>
        public bool IsTalonClosed
        {
            get
            {
                var closedId = new List<long>();
                closedId.Add(0);
                closedId.Add(1);
                closedId.Add(2);
                closedId.Add(3);
                closedId.Add(5);
                closedId.Add(6);
                closedId.Add(7);
                closedId.Add(8);
                closedId.Add(9);
                closedId.Add(13);
                closedId.Add(14);

                return closedId.Contains(CureResultId);
            }
        }


        /// <summary>
        /// Ид пациента
        /// </summary>
        public long PatientId { get; set; }
        /// <summary>
        /// Дата посещения
        /// </summary>
        public DateTime? VisitDate { get; set; }
        /// <summary>
        /// Дата след. визита
        /// </summary>
        public DateTime? NextVisitDate { get; set; }

        /// <summary>
        /// Основной диагноз
        /// </summary>
        public string MainDiagnose { get; set; }
        /// <summary>
        /// Ид состояния при создании
        /// </summary>
        public long FirstVisitConditionId { get; set; }
        /// <summary>
        /// Ид доктора
        /// </summary>
        public long DoctorIdTo { get; set; }
        /// <summary>
        /// Ид типа оплаты
        /// </summary>
        public long PaymentTypeId { get; set; }

        /// <summary>
        /// ИД больничный лист
        /// </summary>
        public long BlistId
        {
            get { return _blistId; }
            set
            {
                _blistId = value;
                if (value != -1)
                    Blist.LoadData(value);
            }
        }

        /// <summary>
        /// Код направившего ЛПУ
        /// </summary>
        public string LpuNaprCode { get; set; }

        /// <summary>
        /// Больничный лист
        /// </summary>
        public BList Blist { get; private set; }

        /// <summary>
        /// Дата и время внесения/изменения записи
        /// </summary>
        public DateTime CreateRecordDate { get; set; }

        /// <summary>
        /// Содержит диагноз с травмой
        /// </summary>
        public bool IsTrauma
        {
            get { return _isTrauma; }
            set
            {
                _isTrauma = value;
                if (!value)
                {
                    TraumaTypeId = -1;
                }
            }
        }

        /// <summary>
        /// Вид травмы
        /// </summary>
        public long TraumaTypeId { get; set; }

        /// <summary>
        /// Посещения в рамках талона
        /// </summary>
        public List<Visit> Visits { get; set; }

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
            Visits.Clear();
            var loadresult = false;
            Id = id;
            using (var db = new DbWorker())
            {
                var visitIdsQuery = new DbQuery("GetTalonData");
                visitIdsQuery.Sql = "Select visit_id from public.visit_tab where talon_id = @talonId;";
                visitIdsQuery.AddParamWithValue("@talonId", id);
                var results = db.GetResults(visitIdsQuery);
                foreach (var result in results)
                {
                    var visitId = DbResult.GetNumeric(result.Fields[0], -1);
                    if (visitId != -1)
                    {
                        var item = new Visit();
                        item.TalonId = id;
                        item.LoadData(visitId);
                        item.IsSaved = true;
                        Visits.Add(item);
                    }
                }

                var talonDataQuery = new DbQuery("GetTalonData");
                talonDataQuery.Sql = @"select t.*, b.blist_id 
                                       from public.talon_tab t 
                                       left join public.blist_tab b on b.talon_id = t.talon_id 
                                       where t.talon_id = @talonId;";
                talonDataQuery.AddParamWithValue("@talonId", id);
                var dataResult = db.GetResult(talonDataQuery);
                if (dataResult != null && dataResult.Fields.Count > 0)
                {
                    CureResultId = DbResult.GetNumeric(dataResult.GetByName("result_id"), -1);
                    PatientId = DbResult.GetNumeric(dataResult.GetByName("dan_id"), -1);
                    DoctorIdTo = DbResult.GetNumeric(dataResult.GetByName("doctor_id"), -1);
                    PaymentTypeId = DbResult.GetNumeric(dataResult.GetByName("vid_oplat_id"), -1);
                    FirstVisitConditionId = DbResult.GetNumeric(dataResult.GetByName("first_visit_condition"), -1);
                    MainDiagnose = DbResult.GetString(dataResult.GetByName("diag_osn"), string.Empty);
                    VisitDate = DbResult.GetNullableDateTime(dataResult.GetByName("date_visit"));
                    NextVisitDate = DbResult.GetNullableDateTime(dataResult.GetByName("date_next"));
                    CreateRecordDate = DbResult.GetDateTime(dataResult.GetByName("date_vnes_inf"), DateTime.MinValue);
                    BlistId = DbResult.GetNumeric(dataResult.GetByName("blist_id"), -1);
                    TraumaTypeId = DbResult.GetNumeric(dataResult.GetByName("vid_travma"), -1);
                    LpuNaprCode = DbResult.GetString(dataResult.GetByName("lpu_napr_code"), null);
                    loadresult = true;
                }
            }

            IsLoaded = loadresult;
            OnLoaded();

            return loadresult;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }

        public void AddVisit(Visit visit)
        {
            if (visit != null)
            {
                if (Visits.Count == 0)
                {
                    //visit.IsFirst = true;
                    VisitDate = visit.VisitDate;
                }
                Visits.Add(visit);
            }
        }

        public void DeleteVisit(Visit visit, Operator @operator)
        {
            if (visit != null)
            {
                // удаляем из списка
                Visits.Remove(visit);

                // удаляем из базы
                visit.DeleteFromDb(@operator);
            }
        }

        public bool CanSetNextVisitDate(Operator @operator)
        {
            return false;
        }
        public void SetNextVisitDate(DateTime nextVisitDate, Operator @operator)
        {
            NextVisitDate = nextVisitDate;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("SetNextVisit");
                q.Sql = "UPDATE talon_tab SET date_next = @date WHERE talon_id = @talondId;";
                q.AddParamWithValue("@date", nextVisitDate);
                q.AddParamWithValue("@talonId", Id);
                var r = db.Execute(q);
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
                var qId = new DbQuery("GettingNewTalonId");
                qId.Sql = "Select gen_tab_id('talon_tab');";
                var id = DbResult.GetNumeric(db.GetScalarResult(qId), -1);
                if (id == -1)
                {
                    throw new DbWorkerException("Ошибка получения нового ID талона") { Sql = qId.Sql };
                }
                else
                {
                    Id = id;
                    CreateRecordDate = DateTime.Now;
                    //var qCreate = new DbQuery("CreateTalon");

                    //qCreate.Sql =
                      var query =  "INSERT INTO talon_tab (id,talon_id,dan_id,doctor_id,vid_oplat_id) " +
                        "VALUES (@id, @id, @danId, @docId, @paymentType);";
                      var sqlParams = new Dictionary<string, object>();

                      sqlParams.Add("id", id);
                      sqlParams.Add("danId", PatientId);
                      sqlParams.Add("docId", DoctorIdTo);
                      sqlParams.Add("paymentType", PaymentTypeId);

                    _logger.Debug("SQL: {0}", query);
                    _logger.Debug("Params: id={0} danid={1} docid={2} paymentType={3} ", id, PatientId,DoctorIdTo,PaymentTypeId);
                    var result=db.Connection.Execute(query, sqlParams);
                    _logger.Debug("Result={0}", result);
                    //var r = db.Execute(qCreate);
                    if (!(result > 0))
                    {
                        new DbWorkerException("Запрос не создал строку в базе!") { Sql = query };
                    }
                    else
                    {
                        //AddNewTalonToQueue(@operator);
                        OnCreated();
                    }
                }
            }
        }

        private void AddNewTalonToQueue(Operator @operator)
        {
            using (var db = new DbWorker())
            {
                var qId = new DbQuery("GettingNewTalonDoctorId");
                qId.Sql = "Select gen_tab_id('public', 'talon_doctor_tab', '');";
                var id = DbResult.GetNumeric(db.GetScalarResult(qId), -1);
                if (id == -1)
                {
                    throw new DbWorkerException("Ошибка получения нового ID") { Sql = qId.Sql };
                }
                else
                {
                    
                    var q = "INSERT INTO talon_doctor_tab(doctor_id, talon_id, date_nazn, flag, dan_id, operator_id, " +
                        "sp_kind_paid_id, programm_name) " +
                        "VALUES ( @docId, @talonId, @date, @flag, @danId, @operatorId, @paymentType, @programm);";

                    var sqlParams = new Dictionary<string,object>();

                    sqlParams.Add("@docId", DoctorIdTo);
                    sqlParams.Add("@talonId", Id);

                    sqlParams.Add("@date", VisitDate);
                    sqlParams.Add("@flag", 'Р');

                    sqlParams.Add("@danId", PatientId);
                    sqlParams.Add("@operatorId", @operator.Id);
                    sqlParams.Add("@paymentType", PaymentTypeId);
                    sqlParams.Add("@programm", "Plugins");


                    //var r = db.Execute(qCreate);
                    var r= db.Connection.Execute(q,sqlParams);
                    if (!(r > 0))
                    {
                        throw new DbWorkerException("Запрос не создал строку в базе!") { Sql = q };
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
            message = "";

            if (IsTrauma && TraumaTypeId == -1)
            {
                message = "Укажите тип травмы!";
                return false;
            }

            if (CureResultId==-1)
            {
                message = "Укажите результат лечения!";
                return false;
            }

            if (string.IsNullOrEmpty(MainDiagnose))
            {
                message = "Укажите основной диагноз!";
                return false;
            }

            return true;
        }

        public void Save(Operator @operator)
        {
            if (Id == -1)
            {
                CreateInDb(@operator);
                return;
            }

            OnSaving();

            using (var db = new DbWorker())
            {
                //var q = new DbQuery("UpdateTalon");
                var q = "Update talon_tab SET "+ 
                    "result_id = @result_id, " + 
                    "dan_id = @patient_id, " + 
                    "date_visit = @date_visit, " +
                    "diag_osn = @main_diagn, " +
                    "date_next = @next_date_visit, " +
                    "vid_oplat_id = @payment_id, " +
                    "doctor_id = @doctor_id, " +
                    "operator_id = @operator_id, " +
                    "date_vnes_inf = @record_date, " +
                    "first_visit_condition = @firstvisit, " +
                    "vid_travma = @trauma_type, " +
                    "lpu_napr_code = @lpu_napr_code " +
                    "where id = @id";

                var sqlParams = new Dictionary<string, object>();

                sqlParams.Add("@id", Id);
                sqlParams.Add("@result_id", CureResultId == -1 ? null : (long?)CureResultId);
                sqlParams.Add("@patient_id", PatientId);
                sqlParams.Add("@date_visit", VisitDate.HasValue ? (DateTime?)VisitDate.Value.Date : null);
                sqlParams.Add("@main_diagn", string.IsNullOrEmpty(MainDiagnose)? null : MainDiagnose);
                sqlParams.Add("@next_date_visit", NextVisitDate.HasValue ? (DateTime?)NextVisitDate.Value.Date : null);
                sqlParams.Add("@payment_id", PaymentTypeId == -1 ? null : (long?)PaymentTypeId);
                sqlParams.Add("@doctor_id", DoctorIdTo == -1 ? null : (long?)DoctorIdTo);
                sqlParams.Add("@operator_id", @operator.Id);
                sqlParams.Add("@record_date", CreateRecordDate);
                sqlParams.Add("@firstvisit", FirstVisitConditionId == -1 ? null : (long?)FirstVisitConditionId);
                sqlParams.Add("@trauma_type", TraumaTypeId == -1 ? null : (long?)TraumaTypeId);
                sqlParams.Add("@lpu_napr_code", string.IsNullOrEmpty(LpuNaprCode) ? null : LpuNaprCode);

                var r= db.Connection.Execute(q, sqlParams);
                //var r = db.Execute(q);
                if (r > 0)
                {
                    OnSaved();
                }
            }
        }

        // быстрая загрузка талонов
        public static List<Talon> PreLoad(Patient p)
        {
            var talons = new List<Talon>();
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GettingPreTalons");
                q.Sql = @"SELECT t.talon_id, t.result_id, t.diag_osn, t.doctor_id, t.date_vnes_inf,
                        v.visit_id, v.doctor_id as vdoctor_id, v.data_vnes_inf as vdate_vnes_inf, v.data_pos 
                        FROM public.talon_tab t left join public.visit_tab v on v.talon_id = t.talon_id
                        where t.dan_id = @patient_id;";
                q.AddParamWithValue("patient_id", p.PatientId);

                var results = db.GetResults(q);

                foreach (var dbResult in results)
                {
                    var talonId = DbResult.GetNumeric(dbResult.GetByName("talon_id"), 0);
                    var talon = talons.FirstOrDefault(t => t.Id == talonId);
                    

                    var resultId = DbResult.GetNumeric(dbResult.GetByName("result_id"), -1);
                    var mkb = DbResult.GetString(dbResult.GetByName("diag_osn"), "");
                    var doctorId = DbResult.GetNumeric(dbResult.GetByName("doctor_id"), 0);
                    var createRecordDate = DbResult.GetDateTime(dbResult.GetByName("date_vnes_inf"), DateTime.MinValue);

                    if (talon == null)
                    {
                        talon = new Talon();
                        talon.Id = talonId;
                        talon.CureResultId = resultId;
                        talon.MainDiagnose = mkb;
                        talon.DoctorIdTo = doctorId;
                        talon.CreateRecordDate = createRecordDate;
                        talons.Add(talon);
                    }

                    var visitId = DbResult.GetNumeric(dbResult.GetByName("visit_id"), 0);
                    if (visitId != 0)
                    {
                        var visit = new Visit();
                        var vdoctorId = DbResult.GetNumeric(dbResult.GetByName("vdoctor_id"), 0);
                        var vcreateRecordDate = DbResult.GetDateTime(dbResult.GetByName("vdate_vnes_inf"),
                            DateTime.MinValue);
                        var visitDate = DbResult.GetNullableDateTime(dbResult.GetByName("data_pos"));

                        visit.Id = visitId;
                        visit.TalonId = talonId;
                        visit.DoctorId = vdoctorId;
                        visit.VisitDate = visitDate;
                        visit.CreateRecordDate = vcreateRecordDate;
                        talon.Visits.Add(visit);
                    }
                }
            }
            return talons;
        }
    }
}
