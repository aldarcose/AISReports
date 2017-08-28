using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Model.Classes.AddressPart;
using Model.Classes.Codifiers;
using Model.Classes.Codifiers.Emergency;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Emergency
{
    /// <summary>
    /// Карта вызова неотложной помощи
    /// </summary>
    public class EmergencyCallCard : ISaveable, IValidatable, ILoadData, ICreateable
    {
        public EmergencyCallCard()
        {
            Id = -1;
            Diagnoses = new List<EmergencyDiagnose>();
            Patient = new EmergencyPatient();
            MedHanding = new EmergencyHanding() {To = HandingTo.Med_Org};
            PolHanding = new EmergencyHanding() {To = HandingTo.Pol_Org};
            SanHanding = new EmergencyHanding() {To = HandingTo.San_Org};
            DispRegistration = new DispRegistration();

            CallReason = new CallReason();
            CallResult = new CallResult();
            AidResult = new AidResult();
            ActiveVisitMedArea = new MedArea();

            Doctor = new Doctor();

            var defaultRegion = "Бурятия";
            var defaultCity = "Улан-Удэ";
            CallAddress = new Address()
            {
                Region = new Region(),
                City = new City()
            };
            CallAddress.Region.LoadByName(defaultRegion);
            CallAddress.City.LoadByName(defaultCity);

        }

        /// <summary>
        /// Ид в базе
        /// </summary>
        [Browsable(false)]

        public long Id { get; set; }

        /// <summary>
        /// Номер карты
        /// </summary>
        [DisplayName("Номер карты")]
        public string CardNum { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [DisplayName("Дата вызова")]
        public DateTime? CallDate { get; set; }

        /// <summary>
        /// Кто вызвал
        /// </summary>
        [DisplayName("Кто вызвал")]
        public string WhoCalls { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [DisplayName("Кто вызвал (телефон)")]
        public string WhoCallsPhone { get; set; }

        /// <summary>
        /// Передал с "03" (код)
        /// </summary>
        [DisplayName("Передал с \"03\" (код)")]
        public string From03Code { get; set; }

        /// <summary>
        /// Номер бригады
        /// </summary>
        [DisplayName("Номер бригады")]
        public string BrigadeNum { get; set; }

        /// <summary>
        /// Повод вызова
        /// </summary>
        [DisplayName("Повод вызова")]
        public string CallMotive { get; set; }

        /// <summary>
        /// Адрес вызова
        /// </summary>
        [DisplayName("Адрес вызова")]
        public Address CallAddress { get; set; }

        /// <summary>
        /// Время принятия вызова
        /// </summary>
        [DisplayName("Время принятия вызова")]
        public TimeSpan AcceptTime { get; set; }

        /// <summary>
        /// Время передачи вызова бригаде НМП
        /// </summary>
        [DisplayName("Время принятия вызова")]
        public TimeSpan BrigadeAcceptTime { get; set; }

        /// <summary>
        /// Время выезда на вызов
        /// </summary>
        [DisplayName("Время выезда на вызов")]
        public TimeSpan BrigadeGoTime { get; set; }

        /// <summary>
        /// Время прибытия на место вызова
        /// </summary>
        [DisplayName("Время прибытия на место вызова")]
        public TimeSpan BrigadeArriveTime { get; set; }

        /// <summary>
        /// Время начала транспортировки
        /// </summary>
        [DisplayName("Время начала транспортировки")]
        public TimeSpan StartTransportTime { get; set; }

        /// <summary>
        /// Время прибытия в медучреждение
        /// </summary>
        [DisplayName("Время прибытия в медучреждение")]
        public TimeSpan ArriveTransportTime { get; set; }

        /// <summary>
        /// Время освобождения бригады
        /// </summary>
        [DisplayName("Время освобождения бригады")]
        public TimeSpan BrigadeReleaseTime { get; set; }

        /// <summary>
        /// Следующий вызов или возвращение в пункт
        /// </summary>
        [DisplayName("Следующий вызов или возвращение в пункт")]
        public TimeSpan NextCallOrReturnTime { get; set; }

        /// <summary>
        /// Время, затр. на выполнение вызова
        /// </summary>
        [DisplayName("Время, затр. на выполнение вызова")]
        public TimeSpan SpentTime
        {
            get { return BrigadeReleaseTime - AcceptTime; }
        }

        /// <summary>
        /// Причина вызова
        /// </summary>
        [DisplayName("Причина вызова")]
        public CallReason CallReason { get; set; }

        /// <summary>
        /// Результат выезда
        /// </summary>
        [DisplayName("Результат выезда")]
        public CallResult CallResult { get; set; }

        /// <summary>
        /// Результат оказания неотложной мед. помощи
        /// </summary>
        [DisplayName("Результат оказания неотложной мед. помощи")]
        public AidResult AidResult { get; set; }

        /// <summary>
        /// Диагнозы
        /// </summary>
        [DisplayName("Диагнозы")]
        public List<EmergencyDiagnose> Diagnoses { get; set; }

        /// <summary>
        /// Данные пациента
        /// </summary>
        [DisplayName("Пациент")]
        public EmergencyPatient Patient { get; set; }

        /// <summary>
        /// Присутствие алкоголя
        /// </summary>
        [DisplayName("Присутствие алкоголя")]
        public bool IsAlcoholPresent { get; set; }

        /// <summary>
        /// Дисп. учет по данному заболеванию
        /// </summary>
        [DisplayName("Состои на дисп. учете")]
        public DispRegistration DispRegistration { get; set; }

        /// <summary>
        /// передан в мед. учреждение
        /// </summary>
        [DisplayName("Передан в мед. учреждение")]
        public EmergencyHanding MedHanding { get; set; }

        /// <summary>
        /// передан в ЦГиЭ
        /// </summary>
        [DisplayName("Передан в ЦГиЭ")]
        public EmergencyHanding SanHanding { get; set; }

        /// <summary>
        /// передан в силовые структуры (ОВД, ГИБДД, УФКСН)
        /// </summary>
        [DisplayName("Передан в ОВД, ГИБДД, УФКСН")]
        public EmergencyHanding PolHanding { get; set; }

        /// <summary>
        /// Подлежит активному посещению
        /// </summary>
        [DisplayName("Подлежит активному посещению")]
        public bool IsNeedActiveVisit { get; set; }

        /// <summary>
        /// Адрес, которое нужно "активно" посетить
        /// </summary>
        [DisplayName("Адрес актива")]
        public string ActiveVisitAddress { get; set; }

        /// <summary>
        /// Участок адреса активного посещения
        /// </summary>
        [DisplayName("Участок актива")]
        public MedArea ActiveVisitMedArea { get; set; }

        /// <summary>
        /// Актив принят врачом
        /// </summary>
        [DisplayName("Актив принят врачом")]
        public bool IsActiveAccepted { get; set; }

        /// <summary>
        /// Ид доктора, взявшего актив
        /// </summary>
        public long AcceptDoctorId { get; set; }

        /// <summary>
        /// Доктор
        /// </summary>
        [DisplayName("Доктор, создавший карту")]
        public Doctor Doctor { get; set; }

        public int PaymentStatus { get; private set; }

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
            IsSaved = true;
            if (Saved != null)
            {
                Saved(this, null);
            }
        }

        public bool CanSave(Operator @operator, out string message)
        {
            message = "";

            if (PaymentStatus != 0)
            {
                message = "Карта оплачена. Изменение данных невозможно!";
                return false;
            }

            return true;
        }

        public void Save(Operator @operator)
        {
            OnSaving();
            if (Id == -1)
            {
                CreateInDb(@operator);
                OnSaved();
                return;
            }
            using (var db = new DbWorker())
            {
                var q = new DbQuery("InsertCallCardData");
                q.Sql = @"UPDATE 
                          emergency.call_card_tab  
                        SET 
                          call_date = @call_date,
                          who_calls = @who_calls,
                          who_calls_phone = @who_calls_phone,
                          handed_03_code = @handed_03_code,
                          brigade_num = @brigade_num,
                          call_reason_id = @call_reason_id,
                          call_reason_additional = @call_reason_additional,
                          call_accept_time = @call_accept_time,
                          call_handed_brigade_time = @call_handed_brigade_time,
                          call_brigade_out_time = @call_brigade_out_time,
                          call_brigade_arrive_time = @call_brigade_arrive_time,
                          briage_free_time = @briage_free_time,
                          start_transport_time = @start_transport_time,
                          arrive_transport_time = @arrive_transport_time,
                          spent_time = @spent_time,
                          call_motive = @call_motive,
                          call_address_region_code = @call_address_region_code,
                          call_address_area_code = @call_address_area_code,
                          call_address_city_code = @call_address_city_code,
                          call_address_township = @call_address_township,
                          call_address_street_code = @call_address_street_code,
                          call_address_house = @call_address_house,
                          call_address_build = @call_address_build,
                          call_address_flat = @call_address_flat,
                          call_address_additional = @call_address_additional,
                          call_address_text = @call_address_text,
                          emergency_patient_id = @emergency_patient_id,
                          disp_registration_id = @disp_registration_id,
                          is_alcohol = @is_alcohol,
                          call_result_id = @call_result_id,
                          call_no_result_id = @call_no_result_id,
                          med_handing_code = @med_handing_code,
                          med_handing_time = @med_handing_time,
                          med_handing_fio = @med_handing_fio,
                          san_handing_num = @san_handing_num,
                          san_handing_time = @san_handing_time,
                          san_handing_fio = @san_handing_fio,
                          police_handing_num = @police_handing_num,
                          police_handing_time = @police_handing_time,
                          police_handing_fio = @police_handing_fio,
                          doctor_id = @doctor_id,  
                          is_need_active_visit = @is_need_active_visit,
                          active_visit_address = @active_visit_address,
                          active_visit_med_area_id = @active_visit_med_area_id,
                            aid_result_id = @aid_result_id
                        WHERE id = @id;";
                q.AddParamWithValue("@id", Id);
                q.AddParamWithValue("@call_date", CallDate);
                q.AddParamWithValue("@who_calls", WhoCalls);
                q.AddParamWithValue("@who_calls_phone", WhoCallsPhone);
                q.AddParamWithValue("@handed_03_code", From03Code);
                q.AddParamWithValue("@brigade_num", BrigadeNum);
                q.AddParamWithValue("@call_reason_id", CallReason.Type.Id);
                q.AddParamWithValue("@call_reason_additional", CallReason.Additional);
                q.AddParamWithValue("@call_accept_time", AcceptTime);
                q.AddParamWithValue("@call_handed_brigade_time", BrigadeAcceptTime);
                q.AddParamWithValue("@call_brigade_out_time", BrigadeGoTime);
                q.AddParamWithValue("@call_brigade_arrive_time", BrigadeArriveTime);
                q.AddParamWithValue("@briage_free_time", BrigadeReleaseTime);
                q.AddParamWithValue("@start_transport_time", StartTransportTime);
                q.AddParamWithValue("@arrive_transport_time", ArriveTransportTime);
                q.AddParamWithValue("@spent_time", SpentTime);
                q.AddParamWithValue("@call_motive", CallMotive);
                q.AddParamWithValue("@call_address_region_code",
                    CallAddress.Region == null ? null : CallAddress.Region.Code);
                q.AddParamWithValue("@call_address_area_code", CallAddress.Area == null ? null : CallAddress.Area.Code);
                q.AddParamWithValue("@call_address_city_code", CallAddress.City == null ? null : CallAddress.City.Code);
                q.AddParamWithValue("@call_address_township",
                    CallAddress.Village == null ? null : CallAddress.Village.Code);
                q.AddParamWithValue("@call_address_street_code",
                    CallAddress.Street == null ? null : CallAddress.Street.Code);
                q.AddParamWithValue("@call_address_house", CallAddress.House);
                q.AddParamWithValue("@call_address_build", CallAddress.Building);
                q.AddParamWithValue("@call_address_flat", CallAddress.Flat);
                q.AddParamWithValue("@call_address_additional", CallAddress.Additional);
                q.AddParamWithValue("@call_address_text", CallAddress.Text);
                q.AddParamWithValue("@emergency_patient_id", Patient.Id);
                q.AddParamWithValue("@disp_registration_id", DispRegistration.Id);
                q.AddParamWithValue("@is_alcohol", IsAlcoholPresent);
                q.AddParamWithValue("@call_result_id", CallResult.Type.Id);
                q.AddParamWithValue("@call_no_result_id", CallResult.IsNoResult ? CallResult.GetNoResults() : null);
                q.AddParamWithValue("@med_handing_code", MedHanding.CodeNumber);
                q.AddParamWithValue("@med_handing_time", MedHanding.Time);
                q.AddParamWithValue("@med_handing_fio", MedHanding.FIO);
                q.AddParamWithValue("@san_handing_num", SanHanding.CodeNumber);
                q.AddParamWithValue("@san_handing_time", SanHanding.Time);
                q.AddParamWithValue("@san_handing_fio", SanHanding.FIO);
                q.AddParamWithValue("@police_handing_num", PolHanding.CodeNumber);
                q.AddParamWithValue("@police_handing_time", PolHanding.Time);
                q.AddParamWithValue("@police_handing_fio", PolHanding.FIO);
                q.AddParamWithValue("@doctor_id", Doctor == null ? null : (long?) Doctor.Id);
                q.AddParamWithValue("@aid_result_id", AidResult.Id);
                q.AddParamWithValue("@is_need_active_visit", IsNeedActiveVisit);

                q.AddParamWithValue("@active_visit_address", IsNeedActiveVisit ? ActiveVisitAddress : null);
                q.AddParamWithValue("@active_visit_med_area_id",
                    IsNeedActiveVisit ? (long?) ActiveVisitMedArea.Id : null);

                var r = db.Execute(q);
                if (r > 0)
                {
                    foreach (var emergencyDiagnosis in Diagnoses)
                    {
                        if (emergencyDiagnosis.ToDelete)
                        {
                            emergencyDiagnosis.DeleteFromDb(@operator);
                            Diagnoses.Remove(emergencyDiagnosis);
                        }
                        else
                        {
                            emergencyDiagnosis.CallCardId = Id;
                            emergencyDiagnosis.Save(@operator);
                        }
                    }

                    var toDelete = Diagnoses.Where(t => t.ToDelete).ToList();
                    foreach (var emergencyDiagnosis in toDelete)
                    {
                        Diagnoses.Remove(emergencyDiagnosis);
                    }

                    OnSaved();
                }
            }

            // устанавливаем данные актива
            SetActiveInMunkoProgram();
        }

        public bool Validate(out string errorMessage)
        {
            if (Patient.Id == -1)
            {
                errorMessage = "Укажите пациента.";
                return false;
            }
            if (CallDate == null)
            {
                errorMessage = "Укажите дату вызова.";
                return false;
            }
            if (WhoCalls == null && WhoCallsPhone == null)
            {
                errorMessage = "Укажите того, кто вызвал.";
                return false;
            }
            if (string.IsNullOrEmpty(BrigadeNum))
            {
                errorMessage = "Укажите номер бригады.";
                return false;
            }
            if (CallReason.Type == null || CallReason.Type.Id == -1)
            {
                errorMessage = "Укажите причину вызова.";
                return false;
            }

            if (string.IsNullOrEmpty(CallReason.Additional) && CallReason.IsOtherReason)
            {
                errorMessage = "Укажите ДРУГУЮ причину вызова.";
                return false;
            }

            if (string.IsNullOrEmpty(CallMotive))
            {
                errorMessage = "Укажите повод вызова.";
                return false;
            }
            var nullTimeSpan = new TimeSpan(0);
            if (AcceptTime.Equals(nullTimeSpan))
            {
                errorMessage = "Укажите время принятия вызова.";
                return false;
            }
            if (BrigadeAcceptTime.Equals(nullTimeSpan))
            {
                errorMessage = "Укажите время передачи вызова бригаде.";
                return false;
            }
            if (BrigadeGoTime.Equals(nullTimeSpan))
            {
                errorMessage = "Укажите время выезда бригады на вызов.";
                return false;
            }
            if (BrigadeArriveTime.Equals(nullTimeSpan))
            {
                errorMessage = "Укажите время прибытия на место вызова.";
                return false;
            }
            if (BrigadeReleaseTime.Equals(nullTimeSpan))
            {
                errorMessage = "Укажите время особождения бригады.";
                return false;
            }

            if (SpentTime.Equals(nullTimeSpan))
            {
                errorMessage = "Время, затраченное на выполнение вызова не может быть равным 0.";
                return false;
            }

            if (DispRegistration.Id == -1)
            {
                errorMessage = "Укажите состоит ли пациент на дисп. учете по данному заболеванию.";
                return false;
            }
            if (!CallResult.IsNoResult)
            {
                if (Diagnoses == null || Diagnoses.All(t => t.ToDelete))
                {
                    errorMessage = "Укажите хотя бы один диагноз.";
                    return false;
                }

                var mainTypeDiagnId = 200001; // ид основного диагноза
                if (Diagnoses.Count(t => t.DiagnoseTypeId == mainTypeDiagnId) != 1)
                {
                    errorMessage = "Не указан основной диагноз.";
                    return false;
                }
            }

            errorMessage = "";
            return true;
        }

        public event EventHandler Loading;
        public event EventHandler Loaded;

        public void OnLoading()
        {
            IsLoading = true;
            IsLoaded = false;
            if (Loading != null)
                Loading(this, null);
        }

        public void OnLoaded()
        {
            IsLoading = false;
            IsLoaded = true;
            IsSaved = true;
            if (Loaded != null)
                Loaded(this, null);
        }

        [Browsable(false)]
        public bool IsLoading { get; private set; }

        [Browsable(false)]
        public bool IsLoaded { get; private set; }

        public bool LoadData(long id)
        {
            OnLoading();
            Id = id;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetCallCardData");
                q.Sql = @"SELECT *
                        FROM emergency.call_card_tab 
                        where id = @id;";
                q.AddParamWithValue("@id", id);
                var r = db.GetResult(q);
                if (r != null && r.Fields.Count > 0)
                {
                    Id = DbResult.GetNumeric(r.GetByName("id"), -1);
                    CallDate = DbResult.GetDateTime(r.GetByName("call_date"), DateTime.MinValue);
                    WhoCalls = DbResult.GetString(r.GetByName("who_calls"), string.Empty);
                    WhoCallsPhone = DbResult.GetString(r.GetByName("who_calls_phone"), string.Empty);
                    From03Code = DbResult.GetString(r.GetByName("handed_03_code"), string.Empty);
                    BrigadeNum = DbResult.GetString(r.GetByName("brigade_num"), string.Empty);
                    CallReason = new CallReason();
                    CallReason.Type.Id = DbResult.GetNumeric(r.GetByName("call_reason_id"), -1);

                    CallReason.Additional = DbResult.GetString(r.GetByName("call_reason_additional"), string.Empty);
                    AcceptTime = DbResult.GetDateTime(r.GetByName("call_accept_time"), DateTime.MinValue).TimeOfDay;
                    BrigadeAcceptTime =
                        DbResult.GetDateTime(r.GetByName("call_handed_brigade_time"), DateTime.MinValue).TimeOfDay;
                    BrigadeGoTime =
                        DbResult.GetDateTime(r.GetByName("call_brigade_out_time"), DateTime.MinValue).TimeOfDay;
                    BrigadeArriveTime =
                        DbResult.GetDateTime(r.GetByName("call_brigade_arrive_time"), DateTime.MinValue).TimeOfDay;
                    BrigadeReleaseTime =
                        DbResult.GetDateTime(r.GetByName("briage_free_time"), DateTime.MinValue).TimeOfDay;
                    StartTransportTime =
                        DbResult.GetDateTime(r.GetByName("start_transport_time"), DateTime.MinValue).TimeOfDay;
                    ArriveTransportTime =
                        DbResult.GetDateTime(r.GetByName("arrive_transport_time"), DateTime.MinValue).TimeOfDay;
                    CallMotive = DbResult.GetString(r.GetByName("call_motive"), string.Empty);

                    var regionCode = DbResult.GetString(r.GetByName("call_address_region_code"), string.Empty);
                    var areaCode = DbResult.GetString(r.GetByName("call_address_area_code"), string.Empty);
                    var cityCode = DbResult.GetString(r.GetByName("call_address_city_code"), string.Empty);
                    var villageCode = DbResult.GetString(r.GetByName("call_address_township"), string.Empty);
                    var streetCode = DbResult.GetString(r.GetByName("call_address_street_code"), string.Empty);
                    if (!string.IsNullOrEmpty(regionCode))
                    {
                        if (CallAddress.Region == null)
                            CallAddress.Region = new Region();
                        CallAddress.Region.LoadByCode(regionCode);
                    }
                    if (!string.IsNullOrEmpty(areaCode))
                    {
                        if (CallAddress.Area == null)
                            CallAddress.Area = new Area();
                        CallAddress.Area.LoadByCode(areaCode);
                    }
                    if (!string.IsNullOrEmpty(cityCode))
                    {
                        if (CallAddress.City == null)
                            CallAddress.City = new City();
                        CallAddress.City.LoadByCode(cityCode);
                    }
                    if (!string.IsNullOrEmpty(villageCode))
                    {
                        if (CallAddress.Village == null)
                            CallAddress.Village = new Village();
                        CallAddress.Village.LoadByCode(villageCode);
                    }
                    if (!string.IsNullOrEmpty(streetCode))
                    {
                        if (CallAddress.Street == null)
                            CallAddress.Street = new Street();
                        CallAddress.Street.LoadByCode(streetCode);
                    }
                    var houseStr = DbResult.GetString(r.GetByName("call_address_house"), string.Empty);
                    CallAddress.House = string.IsNullOrEmpty(houseStr) ? -1 : int.Parse(houseStr);
                    CallAddress.Building = DbResult.GetString(r.GetByName("call_address_build"), string.Empty);
                    var flatStr = DbResult.GetString(r.GetByName("call_address_flat"), string.Empty);
                    CallAddress.Flat = string.IsNullOrEmpty(flatStr) ? -1 : int.Parse(flatStr);
                    CallAddress.Additional = DbResult.GetString(r.GetByName("call_address_additional"), string.Empty);
                    CallAddress.Text = DbResult.GetString(r.GetByName("call_address_text"), string.Empty);
                    Patient.LoadData(DbResult.GetNumeric(r.GetByName("emergency_patient_id"), -1));
                    Doctor.LoadData(DbResult.GetNumeric(r.GetByName("doctor_id"), -1));

                    DispRegistration.Id = DbResult.GetNumeric(r.GetByName("disp_registration_id"), -1);
                    IsAlcoholPresent = DbResult.GetBoolean(r.GetByName("is_alcohol"), false);
                    var callResult = DbResult.GetNumeric(r.GetByName("call_result_id"), -1);
                    if (callResult != -1)
                    {
                        var cc = CodifiersHelper.GetEmergencyCallResults().FirstOrDefault(t => t.Id == callResult);
                        if (cc != null)
                            CallResult.Type = cc;
                    }

                    var noResults = DbResult.GetString(r.GetByName("call_no_result_id"), string.Empty).Split(new char[]{';'}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var noResult in noResults)
                    {
                        var nr_id = int.Parse(noResult);
                        var nr = CodifiersHelper.GetEmergencyNoResultReasons().FirstOrDefault(t => t.Id == nr_id);
                        if (nr != null)
                            CallResult.NoResultReasons.Add(nr);
                    }

                    MedHanding.CodeNumber = DbResult.GetString(r.GetByName("med_handing_code"), string.Empty);
                    MedHanding.Time = DbResult.GetDateTime(r.GetByName("med_handing_time"), DateTime.MinValue);
                    MedHanding.FIO = DbResult.GetString(r.GetByName("med_handing_fio"), string.Empty);

                    SanHanding.CodeNumber = DbResult.GetString(r.GetByName("san_handing_num"), string.Empty);
                    SanHanding.Time = DbResult.GetDateTime(r.GetByName("san_handing_time"), DateTime.MinValue);
                    SanHanding.FIO = DbResult.GetString(r.GetByName("san_handing_fio"), string.Empty);

                    PolHanding.CodeNumber = DbResult.GetString(r.GetByName("police_handing_num"), string.Empty);
                    PolHanding.Time = DbResult.GetDateTime(r.GetByName("police_handing_time"), DateTime.MinValue);
                    PolHanding.FIO = DbResult.GetString(r.GetByName("police_handing_fio"), string.Empty);

                    IsNeedActiveVisit = DbResult.GetBoolean(r.GetByName("is_need_active_visit"), false);

                    ActiveVisitAddress = DbResult.GetString(r.GetByName("active_visit_address"), string.Empty);
                    //IsNeedActiveVisit = DbResult(r.GetByName("active_visit_med_area_id"));

                    var aid_res = DbResult.GetNumeric(r.GetByName("aid_result_id"), -1);
                    var aid = CodifiersHelper.GetEmergencyAidResults().FirstOrDefault(t => t.Id == aid_res);
                    if (aid != null)
                        AidResult = aid;

                    PaymentStatus = DbResult.GetInt(r.GetByName("payment_status"), 0);

                    LoadDiagnoses();

                    OnLoaded();
                }
            }
            return IsLoaded;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }

        private void LoadDiagnoses()
        {
            Diagnoses.Clear();
            using (var dbWorker = new DbWorker())
            {
                var q = new DbQuery("GetCardDiagnoses");
                q.Sql = "select * from emergency.call_card_diagnoses_tab where call_card_id = @card_id;";
                q.AddParamWithValue("@card_id", Id);

                var r = dbWorker.GetResults(q);

                if (r != null && r.Count > 0)
                {
                    foreach (var result in r)
                    {
                        var item = new EmergencyDiagnose();
                        item.LoadData(result);
                        if (item.Id != -1)
                            Diagnoses.Add(item);
                    }

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
            message = string.Empty;
            return true;
        }

        public void CreateInDb(Operator @operator)
        {
            Patient.Save(@operator);

            using (var db = new DbWorker())
            {
                var idQ = new DbQuery("GetNewCallCardId");
                idQ.Sql = "Select nextval('emergency.call_card_tab_id_seq');";
                var newId = DbResult.GetNumeric(db.GetScalarResult(idQ), -1);
                if (newId == -1)
                    return;

                Id = newId;

                var q = new DbQuery("InsertCallCardData");
                q.Sql = @"INSERT INTO emergency.call_card_tab
                    (id, call_date, who_calls, who_calls_phone, handed_03_code,
                    brigade_num, call_reason_id, call_reason_additional,
                    call_accept_time, call_handed_brigade_time, call_brigade_out_time, call_brigade_arrive_time,
                    briage_free_time, start_transport_time, arrive_transport_time, spent_time, call_motive, 
                    call_address_region_code, call_address_area_code, call_address_city_code, call_address_township, 
                    call_address_street_code, call_address_house, call_address_build, call_address_flat, call_address_additional, call_address_text, 
                    emergency_patient_id, disp_registration_id, is_alcohol,
                    call_result_id, call_no_result_id, 
                    med_handing_code, med_handing_time, med_handing_fio,
                    san_handing_num, san_handing_time, san_handing_fio,
                    police_handing_num, police_handing_time, police_handing_fio,
                    is_need_active_visit, active_visit_address, active_visit_med_area_id, aid_result_id, doctor_id) 
                    VALUES (
                      @id,
                      @call_date,
                      @who_calls,
                      @who_calls_phone,
                      @handed_03_code,
                      @brigade_num,
                      @call_reason_id,
                      @call_reason_additional,
                      @call_accept_time,
                      @call_handed_brigade_time,
                      @call_brigade_out_time,
                      @call_brigade_arrive_time,
                      @briage_free_time,
                      @start_transport_time,
                      @arrive_transport_time,
                      @spent_time,
                      @call_motive,
                      @call_address_region_code,
                      @call_address_area_code,
                      @call_address_city_code,
                      @call_address_township,
                      @call_address_street_code,
                      @call_address_house,
                      @call_address_build,
                      @call_address_flat,
                      @call_address_additional,
                      @call_address_text,
                      @emergency_patient_id,
                      @disp_registration_id,
                      @is_alcohol,
                      @call_result_id,
                      @call_no_result_id,
                      @med_handing_code,
                      @med_handing_time,
                      @med_handing_fio,
                      @san_handing_num,
                      @san_handing_time,
                      @san_handing_fio,
                      @police_handing_num,
                      @police_handing_time,
                      @police_handing_fio,
                      @is_need_active_visit,
                      @active_visit_address,
                      @active_visit_med_area_id,
                      @aid_result_id,
                      @doctor_id);";
                q.AddParamWithValue("@id", Id);
                q.AddParamWithValue("@call_date", CallDate);
                q.AddParamWithValue("@who_calls", WhoCalls);
                q.AddParamWithValue("@who_calls_phone", WhoCallsPhone);
                q.AddParamWithValue("@handed_03_code", From03Code);
                q.AddParamWithValue("@brigade_num", BrigadeNum);
                q.AddParamWithValue("@call_reason_id", CallReason.Type.Id);
                q.AddParamWithValue("@call_reason_additional", CallReason.Additional);
                q.AddParamWithValue("@call_accept_time", AcceptTime);
                q.AddParamWithValue("@call_handed_brigade_time", BrigadeAcceptTime);
                q.AddParamWithValue("@call_brigade_out_time", BrigadeGoTime);
                q.AddParamWithValue("@call_brigade_arrive_time", BrigadeArriveTime);
                q.AddParamWithValue("@briage_free_time", BrigadeReleaseTime);
                q.AddParamWithValue("@start_transport_time", StartTransportTime);
                q.AddParamWithValue("@arrive_transport_time", ArriveTransportTime);
                q.AddParamWithValue("@spent_time", SpentTime);
                q.AddParamWithValue("@call_motive", CallMotive);
                q.AddParamWithValue("@call_address_region_code",
                    CallAddress.Region == null ? null : CallAddress.Region.Code);
                q.AddParamWithValue("@call_address_area_code", CallAddress.Area == null ? null : CallAddress.Area.Code);
                q.AddParamWithValue("@call_address_city_code", CallAddress.City == null ? null : CallAddress.City.Code);
                q.AddParamWithValue("@call_address_township",
                    CallAddress.Village == null ? null : CallAddress.Village.Code);
                q.AddParamWithValue("@call_address_street_code",
                    CallAddress.Street == null ? null : CallAddress.Street.Code);
                q.AddParamWithValue("@call_address_house", CallAddress.House);
                q.AddParamWithValue("@call_address_build", CallAddress.Building);
                q.AddParamWithValue("@call_address_flat", CallAddress.Flat);
                q.AddParamWithValue("@call_address_additional", CallAddress.Additional);
                q.AddParamWithValue("@call_address_text", CallAddress.Text);
                q.AddParamWithValue("@emergency_patient_id", Patient.Id);
                q.AddParamWithValue("@disp_registration_id", DispRegistration.Id);
                q.AddParamWithValue("@is_alcohol", IsAlcoholPresent);
                q.AddParamWithValue("@call_result_id", CallResult.Type.Id);
                q.AddParamWithValue("@call_no_result_id", CallResult.IsNoResult ? CallResult.GetNoResults() : null);
                q.AddParamWithValue("@med_handing_code", MedHanding.CodeNumber);
                q.AddParamWithValue("@med_handing_time", MedHanding.Time);
                q.AddParamWithValue("@med_handing_fio", MedHanding.FIO);
                q.AddParamWithValue("@san_handing_num", SanHanding.CodeNumber);
                q.AddParamWithValue("@san_handing_time", SanHanding.Time);
                q.AddParamWithValue("@san_handing_fio", SanHanding.FIO);
                q.AddParamWithValue("@police_handing_num", PolHanding.CodeNumber);
                q.AddParamWithValue("@police_handing_time", PolHanding.Time);
                q.AddParamWithValue("@police_handing_fio", PolHanding.FIO);
                q.AddParamWithValue("@is_need_active_visit", IsNeedActiveVisit);
                q.AddParamWithValue("@aid_result_id", AidResult.Id);
                q.AddParamWithValue("@doctor_id", Doctor == null ? null : (long?) Doctor.Id);
                q.AddParamWithValue("@active_visit_address", IsNeedActiveVisit ? ActiveVisitAddress : null);
                q.AddParamWithValue("@active_visit_med_area_id",
                    IsNeedActiveVisit ? (long?) ActiveVisitMedArea.Id : null);

                var r = db.Execute(q);
                if (r > 0)
                {
                    OnCreated();

                    foreach (var emergencyDiagnosis in Diagnoses)
                    {
                        if (emergencyDiagnosis.ToDelete)
                        {
                            emergencyDiagnosis.DeleteFromDb(@operator);
                        }
                        else
                        {
                            emergencyDiagnosis.CallCardId = Id;
                            emergencyDiagnosis.Save(@operator);
                        }
                    }

                    var toDelete = Diagnoses.Where(t => t.ToDelete).ToList();
                    foreach (var emergencyDiagnosis in toDelete)
                    {
                        Diagnoses.Remove(emergencyDiagnosis);
                    }
                }
                else
                {
                    Id = -1;
                }
            }
        }

        private void SetActiveInMunkoProgram()
        {
            long vizov_id = -1;
            using (var maindDb = new DbWorker())
            {
                var q = new DbQuery("GetVizovId");
                q.Sql = "Select vizov_id from emergency.vizov_mapping_tab where call_card_id = @id;";
                q.AddParamWithValue("@id", Id);

                var result = maindDb.GetScalarResult(q);
                if (result != null)
                {
                    vizov_id = DbResult.GetNumeric(result, -1);
                }

                const string connectionString = "Server=192.168.16.253;Port=5432;User Id=munkokizh;Password=15101986;Database=gp1rb;";
                using (var vizovDb = new DbWorker(connectionString))
                {
                    if (vizov_id == -1)
                    {
                        if (IsNeedActiveVisit){
                            // вставка
                            var insertQ = new DbQuery("InsertVizov");
                            insertQ.Sql =
                                @"INSERT INTO vizov.vizov (comment, data_vnesenia, adress, telefon, fio_dr, time_vnesenia, users_id, dan_id, doctor_id, uchactok_id, talon_doctor_id, otdel) 
            	                    VALUES (@comment, current_date, @address, null, @fio, current_time, @users_id, @dan_id, @doctor_id, @area_id, null, @otdel_id) returning id;";

                            var mainTypeDiagnId = 200001; // ид основного диагноза
                            var mainDiagnose = Diagnoses.First(t => t.DiagnoseTypeId == mainTypeDiagnId);

                            insertQ.AddParamWithValue("@comment", string.Format("АКТИВ неотложка, {0}-{1}, {2}", mainDiagnose.MkbCode, mainDiagnose.MkbName, this.CallMotive));
                            insertQ.AddParamWithValue("@address", ActiveVisitAddress);
                            var patient = string.Format("{0}{1}{2} {3}",
                                string.IsNullOrEmpty(Patient.LastName) ? "" : Patient.LastName + " ",
                                string.IsNullOrEmpty(Patient.FirstName) ? "" : Patient.FirstName + " ",
                                string.IsNullOrEmpty(Patient.MiddleName) ? "" : Patient.MiddleName,
                                Patient.BirthDate.HasValue ? Patient.BirthDate.Value.ToString("dd.MM.yyyy") : "");

                            insertQ.AddParamWithValue("@fio", patient);
                            insertQ.AddParamWithValue("@users_id", null);
                            insertQ.AddParamWithValue("@dan_id", Patient.PatientId);
                            insertQ.AddParamWithValue("@doctor_id", Doctor.Id);
                            insertQ.AddParamWithValue("@area_id", (ActiveVisitMedArea == null || ActiveVisitMedArea.Id == 0) ? null : (long?)ActiveVisitMedArea.Id);
                            insertQ.AddParamWithValue("@otdel_id", Doctor.DivisionId);

                            var newId = vizovDb.GetScalarResult(insertQ);

                            var insertMapQ = new DbQuery("CreateVizovMapping");
                            insertMapQ.Sql =
                                "insert into emergency.vizov_mapping_tab (call_card_id, vizov_id, create_time) values (@call_card_id, @vizov_id, @time);";
                            insertMapQ.AddParamWithValue("@call_card_id", Id);
                            insertMapQ.AddParamWithValue("@vizov_id", newId);
                            insertMapQ.AddParamWithValue("@time", DateTime.Now);
                            maindDb.Execute(insertMapQ);
                        }
                    }
                    else
                    {
                        // обновление
                        var updateQ = new DbQuery("UpdateVizov");
                        updateQ.Sql = "update vizov.vizov set arxiv = @value where id = @id;";
                        updateQ.AddParamWithValue("@value", !IsNeedActiveVisit);
                        updateQ.AddParamWithValue("@id", vizov_id);
                        vizovDb.Execute(updateQ);
                    }
                }
            }

            
        }

        public void Delete(string deleteReason, DateTime date)
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery("DeleteCard");
                q.Sql =
                    "UPDATE emergency.call_card_tab SET is_deleted = true, delete_reason = @delete_reason, delete_date = @delete_date WHERE id = @id;";
                q.AddParamWithValue("delete_reason", deleteReason);
                q.AddParamWithValue("delete_date", date);
                q.AddParamWithValue("id", Id);
                
                db.Execute(q);
            }
        }

        public bool CanGetReestrElement()
        {
            if (Patient.CanExportPatient())
            {
                var d = Diagnoses.FirstOrDefault(t => t.DiagnoseTypeId == 200001);
                if (d == null)
                    return false;

                if (!CallDate.HasValue)
                    return false;

                if (AidResult.GetIshodCode() == 0)
                    return false;

                if (CallResult.GetRsltCode() > 400)
                    return false;

                return true;
            }
            return false;
        }

        public XElement GetReestrElement()
        {
            var min_Zap = 400000;
            var min_Case = 4000000;
            var min_History = 100000000000000;
            
            var zap = new XElement("ZAP");

            zap.Add(new XElement("N_ZAP", min_Zap + Id));
            zap.Add(new XElement("PR_NOV", 0));

            zap.Add(Patient.GetHMElement());

            var sluch = new XElement("SLUCH");
            sluch.Add(new XElement("IDCASE", min_Case + Id));
            sluch.Add(new XElement("USL_OK", 3));
            sluch.Add(new XElement("VIDPOM", 11));
            sluch.Add(new XElement("FOR_POM", 2));
            sluch.Add(new XElement("EXTR", 2));
            sluch.Add(new XElement("LPU", "032021"));
            sluch.Add(new XElement("LPU_1", "032021"));
            sluch.Add(new XElement("PODR", 147));
            sluch.Add(new XElement("PROFIL", 160));
            
            var isChild = Patient.IsPatientChild();

            sluch.Add(new XElement("DET", isChild ? 1 : 0));

            sluch.Add(new XElement("NHISTORY", min_History + Id));
            sluch.Add(new XElement("DATE_1", this.CallDate.Value.ToString("yyyy-MM-dd")));
            sluch.Add(new XElement("DATE_2", this.CallDate.Value.ToString("yyyy-MM-dd")));
            var d = Diagnoses.FirstOrDefault(t => t.DiagnoseTypeId == 200001);

            var diagn = d != null ? d.MkbCode : string.Empty;

            sluch.Add(new XElement("DS1", diagn));
            sluch.Add(new XElement("RSLT", CallResult.GetRsltCode()));
            sluch.Add(new XElement("ISHOD", AidResult.GetIshodCode()));
            sluch.Add(new XElement("PRVS", "204"));
            sluch.Add(new XElement("VERS_SPEC", "V015"));

            var doctorData = Doctor.Snils;
            if (string.IsNullOrEmpty(doctorData) || Regex.IsMatch(Doctor.Snils, "^\\D"))
                doctorData = Doctor.Id.ToString(CultureInfo.InvariantCulture);

            sluch.Add(new XElement("IDDOKT", doctorData));
            sluch.Add(new XElement("IDSP", "41"));
            sluch.Add(new XElement("ED_COL", "1"));
            sluch.Add(new XElement("TARIF", "613.59"));
            sluch.Add(new XElement("SUMV", "613.59"));
            sluch.Add(new XElement("OPLATA", "0"));

            var usl = new XElement("USL");
            usl.Add(new XElement("IDSERV", 1));
            usl.Add(new XElement("LPU", 032021));
            usl.Add(new XElement("LPU_1", 032021));
            usl.Add(new XElement("PODR", 147));
            usl.Add(new XElement("PROFIL", 160));
            usl.Add(new XElement("DET", isChild ? 1 : 0));
            usl.Add(new XElement("DATE_IN", this.CallDate.Value.ToString("yyyy-MM-dd")));
            usl.Add(new XElement("DATE_OUT", this.CallDate.Value.ToString("yyyy-MM-dd")));
            usl.Add(new XElement("DS", diagn));
            usl.Add(new XElement("CODE_USL", isChild ? "161141" : "061141"));
            usl.Add(new XElement("KOL_USL", 1));
            usl.Add(new XElement("TARIF", 613.59));
            usl.Add(new XElement("SUMV_USL", 613.59));
            usl.Add(new XElement("PRVS", 204));
            usl.Add(new XElement("CODE_MD", doctorData));
            usl.Add(new XElement("COMENTU"));
            sluch.Add(usl);
            sluch.Add(new XElement("COMENTSL", "F62:8;"));
            zap.Add(sluch);
            return zap;
        }

        public static void SetPaymentStatus(long id, int status)
        {
            using (var db = new DbWorker())
            {
                var q = new DbQuery("SetPayment");
                q.Sql = "UPDATE emergency.call_card_tab SET payment_status = @payment_status WHERE id = @id;";
                q.AddParamWithValue("payment_status", status);
                q.AddParamWithValue("id", id);
                db.Execute(q);
            }
        }
    }
}
