using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Model.Classes.AddressPart;
using Model.Classes.Codifiers;
using Model.Classes.Documents;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Emergency
{
    /// <summary>
    /// Данные пациента для неотложной помощи
    /// </summary>
    public class EmergencyPatient : ILoadData, ISaveable, ICreateable, IValidatable
    {
        public EmergencyPatient()
        {
            Id = -1;
            PatientId = -1;
            BirthDate = null;
            RegAddress = new Address();
            Policy = new Policy();
            Document = new PersonDocument();
            Gender = Gender.Unknown;
        }

        /// <summary>
        /// ИД пациента из основной базы (если он был найден).
        /// </summary>
        public long PatientId { get; set; }

        public long Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Адрес прописки
        /// </summary>
        public Address RegAddress { get; set; }

        /// <summary>
        /// Место работы/учебы
        /// </summary>
        public string WorkStudyPlace { get; set; }

        /// <summary>
        /// Код поликлиники прикрепления
        /// </summary>
        public string LpuAttachCode { get; set; }

        /// <summary>
        /// Полис пациента
        /// Нужны тип, серия, номер, ид страховой
        /// </summary>
        public Policy Policy { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность (паспорт, свидетельство о рождении)
        /// Нужны только тип, серия и номер
        /// </summary>
        public PersonDocument Document { get; set; }

        /// <summary>
        /// Представление
        /// </summary>
        public string Text
        {
            get
            {
                var fio = string.Format("{0}{1}{2}",
                    string.IsNullOrEmpty(LastName) ? "" : LastName + " ",
                    string.IsNullOrEmpty(FirstName) ? "" : FirstName + " ",
                    string.IsNullOrEmpty(MiddleName) ? "" : MiddleName);
                
                var result =
                    string.Format("{0}, {1}{2}", 
                    string.IsNullOrEmpty(fio) ? "ФИО НЕ УКАЗАНО" : fio,
                    BirthDate.HasValue && BirthDate.Value!=DateTime.MinValue ? BirthDate.Value.ToShortDateString() + " д.р., " : "",
                    string.IsNullOrEmpty(Policy.Number) ? "" : string.Format("Полис {0} {1}", Policy.Serial, Policy.Number)
                    );
                return result;
            }
        }

        public void GetFromDan(Patient patient)
        {
            this.LastName = patient.LastName;
            this.FirstName = patient.FirstName;
            this.MiddleName = patient.MidName;
            this.BirthDate = patient.BirthDate;
            this.Gender = patient.Gender;
            this.LpuAttachCode = patient.LpuAttachCode;
            this.RegAddress = patient.RegAddress;
            this.Policy = patient.Policy;
            this.Document = patient.Document;
            this.PatientId = patient.PatientId;
            this.WorkStudyPlace = patient.WorkPlace;
        }

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

        public bool IsLoading { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool LoadData(long id)
        {
            OnLoading();

            using (var dbWorker = new DbWorker())
            {
                var q = new DbQuery("GetEmergencyPatient");
                q.Sql = "select * from emergency.patient_tab where id = @id;";
                q.AddParamWithValue("@id", id);

                var result = dbWorker.GetResult(q);
                if (result != null)
                {
                    this.Id = id;
                    this.PatientId = DbResult.GetNumeric(result.GetByName("dan_id"), -1);
                    this.LastName = DbResult.GetString(result.GetByName("fam"), string.Empty);
                    this.FirstName = DbResult.GetString(result.GetByName("nam"), string.Empty);
                    this.MiddleName = DbResult.GetString(result.GetByName("mid"), string.Empty);
                    this.BirthDate = DbResult.GetNullableDateTime(result.GetByName("birth_date"));
                    var gender = DbResult.GetString(result.GetByName("gender"), "");
                    this.Gender = gender == "" ? Gender.Unknown :
                        (gender.Equals("м") ? Gender.Male : Gender.Female);
                    this.WorkStudyPlace = DbResult.GetString(result.GetByName("work_study_place"), string.Empty);
                    this.LpuAttachCode = DbResult.GetString(result.GetByName("lpu_code"), string.Empty);
                    
                    this.Policy.Type.LoadData(DbResult.GetNumeric(result.GetByName("policy_type_id"), -1));
                    this.Policy.SmoId = DbResult.GetNumeric(result.GetByName("policy_smo_id"), -1);
                    this.Policy.Serial = DbResult.GetString(result.GetByName("policy_serial"), string.Empty);
                    this.Policy.Number = DbResult.GetString(result.GetByName("policy_number"), string.Empty);

                    this.Document.Type.LoadData(DbResult.GetNumeric(result.GetByName("document_type_id"), -1));
                    this.Document.Serial = DbResult.GetString(result.GetByName("document_serial"), string.Empty);
                    this.Document.Number = DbResult.GetString(result.GetByName("document_number"), string.Empty);

                    var regionCode = DbResult.GetString(result.GetByName("address_region_code"), string.Empty);
                    var areaCode = DbResult.GetString(result.GetByName("address_area_code"), string.Empty);
                    var cityCode = DbResult.GetString(result.GetByName("address_city_code"), string.Empty);
                    var villageCode = DbResult.GetString(result.GetByName("address_township_code"), string.Empty);
                    var streetCode = DbResult.GetString(result.GetByName("address_street_code"), string.Empty);

                    if (!string.IsNullOrEmpty(regionCode))
                    {
                        if (RegAddress.Region == null)
                            RegAddress.Region = new Region();
                        RegAddress.Region.LoadByCode(regionCode);
                    }
                    if (!string.IsNullOrEmpty(areaCode))
                    {
                        if (RegAddress.Area == null)
                            RegAddress.Area = new Area();
                        RegAddress.Area.LoadByCode(areaCode);
                    }
                    if (!string.IsNullOrEmpty(cityCode))
                    {
                        if (RegAddress.City == null)
                            RegAddress.City = new City();
                        RegAddress.City.LoadByCode(cityCode);
                    }
                    if (!string.IsNullOrEmpty(villageCode))
                    {
                        if (RegAddress.Village == null)
                            RegAddress.Village = new Village();
                        RegAddress.Village.LoadByCode(villageCode);
                    }
                    if (!string.IsNullOrEmpty(streetCode))
                    {
                        if (RegAddress.Street == null)
                            RegAddress.Street = new Street();
                        RegAddress.Street.LoadByCode(streetCode);
                    }

                    this.RegAddress.Text = DbResult.GetString(result.GetByName("address_text"), string.Empty);
                    this.RegAddress.House = DbResult.GetInt(result.GetByName("address_house"), 0);
                    this.RegAddress.Building = DbResult.GetString(result.GetByName("address_building"), string.Empty);
                    this.RegAddress.Flat = DbResult.GetInt(result.GetByName("address_flat"), 0);
                    this.RegAddress.Additional = DbResult.GetString(result.GetByName("address_additional"), string.Empty);
                }
            }

            OnLoaded();
            return true;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }

        public event EventHandler Saved;
        public event EventHandler Saving;
        public bool IsSaved { get; set; }
        public void OnSaving()
        {
            if (Saving != null)
                Saving(this, null);
        }

        public void OnSaved()
        {
            IsSaved = true;
            if (Saved != null)
                Saved(this, null);
        }

        public bool CanSave(Operator @operator, out string message)
        {
            message = "";
            return true;
        }

        public void Save(Operator @operator)
        {
            OnSaving();

            if (Id < 1)
            {
                CreateInDb(@operator);
                OnSaved();
                return;
            }

            using (var db = new DbWorker())
            {
                var q = new DbQuery("UpdateEmergencyPatient");
                q.Sql = "Update emergency.patient_tab set " +
                        "dan_id = @dan_id, " +
                        "fam = @fam, " +
                        "nam = @nam, " +
                        "mid = @mid, " +
                        "birth_date = @birth_date, " +
                        "gender = @gender, " +
                        "work_study_place = @work_study_place, " +
                        "lpu_code = @lpu_code, " +
                        "policy_type_id = @policy_type_id, " +
                        "policy_smo_id = @policy_smo_id, " +
                        "policy_serial = @policy_serial, " +
                        "policy_number = @policy_number, " +
                        "document_type_id = @document_type_id, " +
                        "document_serial = @document_serial, " +
                        "document_number = @document_number, " +
                        "address_region_code = @address_region_code, " +
                        "address_area_code = @address_area_code, " +
                        "address_city_code = @address_city_code, " +
                        "address_township_code = @address_township_code, " +
                        "address_street_code = @address_street_code, " +
                        "address_text = @address_text, " +
                        "address_house = @address_house, " +
                        "address_building = @address_building, " +
                        "address_flat = @address_flat, " +
                        "address_additional = @address_additional " +
                        "where id=@id;";

                q.AddParamWithValue("@id", Id);
                q.AddParamWithValue("@dan_id", PatientId == -1 ? null : (long?)PatientId);
                q.AddParamWithValue("@fam", LastName);
                q.AddParamWithValue("@nam", FirstName);
                q.AddParamWithValue("@mid", MiddleName);
                q.AddParamWithValue("@birth_date", BirthDate);
                q.AddParamWithValue("@gender", Gender == Gender.Unknown
                    ? null
                    : (char?)(Gender == Gender.Male ? 'м' : 'ж'));
                q.AddParamWithValue("@work_study_place", WorkStudyPlace);
                q.AddParamWithValue("@lpu_code", LpuAttachCode);
                if (Policy.Type != null && Policy.Type.Id > 0)
                {
                    q.AddParamWithValue("@policy_type_id", Policy.Type.Id);
                    q.AddParamWithValue("@policy_smo_id", Policy.SmoId);
                    q.AddParamWithValue("@policy_serial", Policy.Serial);
                    q.AddParamWithValue("@policy_number", Policy.Number);
                }
                else
                {
                    q.AddParamWithValue("@policy_type_id", null);
                    q.AddParamWithValue("@policy_smo_id", null);
                    q.AddParamWithValue("@policy_serial", null);
                    q.AddParamWithValue("@policy_number", null);
                }

                if (Document.Type != null && Document.Type.Id > 0)
                {
                    q.AddParamWithValue("@document_type_id", Document.Type.Id);
                    q.AddParamWithValue("@document_serial", Document.Serial);
                    q.AddParamWithValue("@document_number", Document.Number);
                }
                else
                {
                    q.AddParamWithValue("@document_type_id", null);
                    q.AddParamWithValue("@document_serial", null);
                    q.AddParamWithValue("@document_number", null);
                }

                q.AddParamWithValue("@address_region_code", (RegAddress.Region != null && RegAddress.Region.Id > 0)
                                                            ? RegAddress.Region.Code
                                                            : null);

                q.AddParamWithValue("@address_area_code", (RegAddress.Area != null && RegAddress.Area.Id > 0)
                                                            ? RegAddress.Area.Code
                                                            : null);

                q.AddParamWithValue("@address_city_code", (RegAddress.City != null && RegAddress.City.Id > 0)
                                                            ? RegAddress.City.Code
                                                            : null);

                q.AddParamWithValue("@address_township_code", (RegAddress.Village != null && RegAddress.Village.Id > 0)
                                                            ? RegAddress.Village.Code
                                                            : null);

                q.AddParamWithValue("@address_street_code", (RegAddress.Street != null && RegAddress.Street.Id > 0)
                                                            ? RegAddress.Street.Code
                                                            : null);

                q.AddParamWithValue("@address_house", (RegAddress.House > 0)
                                                            ? (int?)RegAddress.House
                                                            : null);

                q.AddParamWithValue("@address_building", RegAddress.Building);

                q.AddParamWithValue("@address_flat", (RegAddress.Flat > 0)
                                                            ? (int?)RegAddress.Flat
                                                            : null);

                q.AddParamWithValue("@address_text", RegAddress.Text);
                q.AddParamWithValue("@address_additional", RegAddress.Additional);

                var r = db.Execute(q);
                if (r > 0)
                {
                    OnSaved();
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
            message = string.Empty;
            return true;
        }

        public void CreateInDb(Operator @operator)
        {
            using (var db = new DbWorker())
            {
                var idQ = new DbQuery("GetNewEmergencyPatientId");
                idQ.Sql = "select nextval('emergency.patient_tab_id_seq');";

                var newId = DbResult.GetNumeric(db.GetScalarResult(idQ), -1);
                if (newId == -1)
                    return;
                Id = newId;

                var q = new DbQuery("InsertEmergencyPatient");
                q.Sql = "insert into emergency.patient_tab " +
                        "(id, dan_id, fam, nam, mid, birth_date, gender, work_study_place, lpu_code, " +
                        "policy_type_id, policy_smo_id, policy_serial, policy_number, " +
                        "document_type_id, document_serial, document_number, " +
                        "address_region_code, address_area_code, address_city_code, address_township_code, " +
                        "address_street_code, address_text, address_house, address_building, address_flat, address_additional) " +
                        "values " +
                        "(@id, @dan_id, @fam, @nam, @mid, @birth_date, @gender, @work_study_place, @lpu_code, " +
                        "@policy_type_id, @policy_smo_id, @policy_serial, @policy_number, " +
                        "@document_type_id, @document_serial, @document_number, " +
                        "@address_region_code, @address_area_code, @address_city_code, @address_township_code, " +
                        "@address_street_code, @address_text, @address_house, @address_building, @address_flat, @address_additional);";

                q.AddParamWithValue("@id", Id);
                q.AddParamWithValue("@dan_id", PatientId == -1 ? null : (long?)PatientId);
                q.AddParamWithValue("@fam", LastName);
                q.AddParamWithValue("@nam", FirstName);
                q.AddParamWithValue("@mid", MiddleName);
                q.AddParamWithValue("@birth_date", BirthDate);
                q.AddParamWithValue("@gender", Gender == Gender.Unknown 
                    ? null
                    : (char?)(Gender == Gender.Male ? 'м' : 'ж'));
                q.AddParamWithValue("@work_study_place", WorkStudyPlace);
                q.AddParamWithValue("@lpu_code", LpuAttachCode);

                if (Policy.Type != null && Policy.Type.Id > 0)
                {
                    q.AddParamWithValue("@policy_type_id", Policy.Type.Id);
                    q.AddParamWithValue("@policy_smo_id", Policy.SmoId);
                    q.AddParamWithValue("@policy_serial", Policy.Serial);
                    q.AddParamWithValue("@policy_number", Policy.Number);
                }
                else
                {
                    q.AddParamWithValue("@policy_type_id", null);
                    q.AddParamWithValue("@policy_smo_id", null);
                    q.AddParamWithValue("@policy_serial", null);
                    q.AddParamWithValue("@policy_number", null);
                }

                if (Document.Type != null && Document.Type.Id > 0)
                {
                    q.AddParamWithValue("@document_type_id", Document.Type.Id);
                    q.AddParamWithValue("@document_serial", Document.Serial);
                    q.AddParamWithValue("@document_number", Document.Number);
                }
                else
                {
                    q.AddParamWithValue("@document_type_id", null);
                    q.AddParamWithValue("@document_serial", null);
                    q.AddParamWithValue("@document_number", null);
                }

                q.AddParamWithValue("@address_region_code", (RegAddress.Region != null && RegAddress.Region.Id > 0)
                                                            ? RegAddress.Region.Code
                                                            : null);

                q.AddParamWithValue("@address_area_code", (RegAddress.Area != null && RegAddress.Area.Id > 0)
                                                            ? RegAddress.Area.Code
                                                            : null);

                q.AddParamWithValue("@address_city_code", (RegAddress.City != null && RegAddress.City.Id > 0)
                                                            ? RegAddress.City.Code
                                                            : null);

                q.AddParamWithValue("@address_township_code", (RegAddress.Village != null && RegAddress.Village.Id > 0)
                                                            ? RegAddress.Region.Code
                                                            : null);

                q.AddParamWithValue("@address_street_code", (RegAddress.Street != null && RegAddress.Street.Id > 0)
                                                            ? RegAddress.Street.Code
                                                            : null);

                q.AddParamWithValue("@address_house", (RegAddress.House > 0)
                                                            ? (int?) RegAddress.House
                                                            : null);

                q.AddParamWithValue("@address_building", RegAddress.Building);

                q.AddParamWithValue("@address_flat", (RegAddress.Flat > 0)
                                                            ? (int?)RegAddress.Flat
                                                            : null);

                q.AddParamWithValue("@address_text", RegAddress.Text);
                q.AddParamWithValue("@address_additional", RegAddress.Additional);

                var r = db.Execute(q);
                if (r > 0)
                {
                    
                    OnCreated();
                }
                else
                {
                    Id = -1;
                }
            }
        }

        public bool Validate(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (Gender == Gender.Unknown)
            {
                errorMessage = "Укажите пол пациента";
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return Text;
        }

        private Guid? PatientGuid { get; set; }

        public bool IsPatientChild()
        {
            if (BirthDate.HasValue)
            {
                var now = DateTime.Now;
                var y = now.Year - BirthDate.Value.Year;
                if (y < 18)
                    return true;
                if (y == 18)
                {
                    var m = now.Month - BirthDate.Value.Month;
                    if (m < 0)
                        return true;
                    if (m == 0)
                    {
                        var d = now.Day - BirthDate.Value.Day;
                        return d <= 0;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool CanExportPatient()
        {
            if (this.Gender == Gender.Unknown)
                return false;

            if (!BirthDate.HasValue)
                return false;

            if (string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(FirstName))
                return false;

            if (Policy == null)
                return false;
            
            if (Policy.Type == null)
                return false;

            if (string.IsNullOrEmpty(Policy.Number))
                return false;

            return true;
        }

        public XElement GetHMElement()
        {
            if (!PatientGuid.HasValue)
            {
                PatientGuid = Guid.NewGuid();
            }
            var x = new XElement("PACIENT");
            x.Add(new XElement("ID_PAC", PatientGuid.Value.ToString()));
            x.Add(new XElement("VPOLIS", Policy.Type.Code));
            if (string.IsNullOrEmpty(Policy.Serial))
            {
                x.Add(new XElement("SPOLIS"));
            }
            else
            {
                x.Add(new XElement("SPOLIS", Policy.Serial));
            }
            
            x.Add(new XElement("NPOLIS", Policy.Number));

            var smo = new SMO();
            smo.LoadData(Policy.SmoId);
            var code = smo.GetTFomsCode();
            if (!string.IsNullOrEmpty(code))
                x.Add(new XElement("SMO", code));

            x.Add(new XElement("NOVOR", 0));
            return x;
        }

        public XElement GetLMElement()
        {
            if (!PatientGuid.HasValue)
            {
                PatientGuid = Guid.NewGuid();
            }

            var x = new XElement("PERS");
            x.Add(new XElement("ID_PAC", PatientGuid.Value.ToString()));
            x.Add(new XElement("FAM", LastName));
            x.Add(new XElement("IM", FirstName));
            if (!string.IsNullOrEmpty(MiddleName))
            {
                x.Add(new XElement("OT", MiddleName));
            }
            var g = Gender == Gender.Male ? 1 : 2;
            if (Gender != Gender.Unknown)
            {
                x.Add(new XElement("W", g));
            }

            if (BirthDate.HasValue)
            {
                x.Add(new XElement("DR", BirthDate.Value.ToString("yyyy-MM-dd")));
            }
            return x;
        }
    }
}
