using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Documents;
using Model.Classes.Research;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;

namespace Model.Classes
{
    public class Person : ILoadData
    {
        public Person()
        {
            Phones = new List<PhoneNumber>();
        }
        public long Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }

        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public Gender Gender { get; set; }

        public Address RegAddress { get; set; }
        public Address FactAddress { get; set; }

        public PersonDocument Document { get; set; }

        public Policy Policy { get; set; }

        public string Snils { get; set; }

        public int SocStatusId { get; set; }
        public int CategoryId { get; set; }
        /// <summary>
        /// Ид группы риска по ТВС
        /// </summary>
        public int RiskId { get; set; }
        public string WorkPlace { get; set; }
        public long? WorkPlaceId { get; set; }
        public bool Attached { get; set; }

        public List<PhoneNumber> Phones { get; set; }

        public int Age {
            get
            {
                int age = DateTime.Today.Year - BirthDate.Year;
                if (BirthDate > DateTime.Today.AddYears(-age))
                    age--;
                return age;
            }
        }

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
            IsLoaded = true;
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

            Id = id;

            var loadResult = false;
            using (var dbWorker = new DbWorker())
            {
                DbQuery query = new DbQuery("GetPersonData");
                query.Sql =
                    "SELECT p.fam, p.nam, p.mid, " + // фио
                           "p.date_born, p.sex, " + // др, пол
                           "adr0.text_address as reg_address, adr1.text_address as fact_address, p.mest_born, " + // адреса
                           "p.pens, " + // снилс
                           "p.sozstatus_id, " + // соц статус
                           "p.kat, " + // категория
                           "p.policy_type, p.data_begin, p.data_end, p.ser_pol, p.num_pol, p.strcom_id, p.strcom_region_id, " + // полис
                           "p.vid_doc_id, p.date_output, p.kem_output, p.serdoc, p.numdoc, " + // документ
                           "p.telephon, p.phone_mobile, p.work_phone, p.risk_id " + // документ
                           "FROM dan_tab p " +
                           "LEFT JOIN address_tab adr0 ON adr0.dan_id = p.dan_id and adr0.sp_addr_type_id = 1 " +
                           "LEFT JOIN address_tab adr1 ON adr1.dan_id = p.dan_id and adr1.sp_addr_type_id = 0 " +
                           "WHERE p.id = @id;";
                query.AddParamWithValue("@id", id);

                var result = dbWorker.GetResult(query);
                if (result != null)
                {
                    // загружаем
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
            if (result == null || result.Fields.Count == 0)
                return false;

            LastName = DbResult.GetString(result.GetByName("fam"), "");
            FirstName = DbResult.GetString(result.GetByName("nam"), "");
            MidName = DbResult.GetString(result.GetByName("mid"), "");

            BirthDate = DbResult.GetDateTime(result.GetByName("date_born"), DateTime.MinValue);

            var gender = DbResult.GetString(result.GetByName("sex"), "");
            if (gender.Equals(""))
                Gender = Gender.Unknown;
            else
                Gender = gender.Equals("ж") ? Gender.Female : Gender.Male;

            RegAddress = new Address() { Text = DbResult.GetString(result.GetByName("reg_address"), "") };
            FactAddress = new Address() { Text = DbResult.GetString(result.GetByName("fact_address"), "") };
            BirthPlace = DbResult.GetString(result.GetByName("mest_born"), "");

            Snils = DbResult.GetString(result.GetByName("pens"), "");

            SocStatusId = DbResult.GetInt(result.GetByName("sozstatus_id"), -1);
            CategoryId = DbResult.GetInt(result.GetByName("kat"), -1);

            if (result.GetByName("policy_type") != null)
            {
                // Идентификатор Единого номера полиса
                var defaultCode = 3;
                var policyType = new PolicyType()
                {
                    Id = DbResult.GetInt(result.GetByName("policy_type"), defaultCode)
                };

                var dateBeg = result.GetByName("data_begin") == null
                    ? null
                    : (DateTime?)DbResult.GetDateTime(result.GetByName("data_begin"), DateTime.MinValue);

                var dateEnd = result.GetByName("data_end") == null
                    ? null
                    : (DateTime?)DbResult.GetDateTime(result.GetByName("data_end"), DateTime.MinValue);

                var serial = DbResult.GetString(result.GetByName("ser_pol"), "");
                var number = DbResult.GetString(result.GetByName("num_pol"), "");

                var smoCode = DbResult.GetInt(result.GetByName("strcom_id"), -1);
                var smoRegionCode = DbResult.GetString(result.GetByName("strcom_region_id"), "");

                Policy = new Policy()
                {
                    Type = policyType,
                    DateBeg = dateBeg,
                    DateEnd = dateEnd,
                    Serial = serial,
                    Number = number,
                    SmoId = smoCode
                    //SmoRegionCode = smoRegionCode
                };
            }

            if (result.GetByName("vid_doc_id") != null)
            {
                // Идентификатор паспорта
                var defaultCode = 14;
                var docType = new PersonDocumentType()
                {
                    Id = DbResult.GetInt(result.GetByName("vid_doc_id"), defaultCode)
                };

                var dateBeg = result.GetByName("date_output") == null
                    ? null
                    : (DateTime?)DbResult.GetDateTime(result.GetByName("date_output"), DateTime.MinValue);


                var org = DbResult.GetString(result.GetByName("kem_output"), "");
                var serial = DbResult.GetString(result.GetByName("serdoc"), "");
                var number = DbResult.GetString(result.GetByName("numdoc"), "");

                Document = new PersonDocument()
                {
                    Type = docType,
                    DateBeg = dateBeg,
                    Serial = serial,
                    Number = number,
                    Organization = org
                };
            }

            if (result.GetByName("telephon") != null)
            {
                var homePhones = DbResult.GetString(result.GetByName("telephon"), "");

                foreach (var homePhone in homePhones.Split(new char[] { ';' }))
                {
                    if (!string.IsNullOrEmpty(homePhone))
                        Phones.Add(new PhoneNumber() { Number = homePhone, Type = PhoneType.Home });
                }
            }

            if (result.GetByName("phone_mobile") != null)
            {
                var mobPhones = DbResult.GetString(result.GetByName("phone_mobile"), "");

                foreach (var mobPhone in mobPhones.Split(new char[] { ';' }))
                {
                    if (!string.IsNullOrEmpty(mobPhone))
                        Phones.Add(new PhoneNumber() { Number = mobPhone, Type = PhoneType.Mobile });
                }
            }

            if (result.GetByName("work_phone") != null)
            {
                var workPhones = DbResult.GetString(result.GetByName("work_phone"), "");

                foreach (var workPhone in workPhones.Split(new char[] { ';' }))
                {
                    if (!string.IsNullOrEmpty(workPhone))
                        Phones.Add(new PhoneNumber() { Number = workPhone, Type = PhoneType.Work });
                }
            }

            if (result.GetByName("risk_id") != null)
            {
                RiskId = DbResult.GetInt(result.GetByName("risk_id"), -1);
            }
            return true;
        }

        public string GetRiskGroupName()
        {
            var risk = string.Empty;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetRiskName");
                q.Sql = "select name from codifiers.gr_risk_tab where id = @id;";
                q.AddParamWithValue("id", RiskId);

                risk = DbResult.GetString(db.GetScalarResult(q), string.Empty);
            }
            return risk;
        }

        public bool HasDuplicates()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select dan_id from dan_tab
                            where upper(fam)=upper(@Fam) and upper(nam)=upper(@Name) 
                            and date_born=@DateBorn";
                var dbResult = db.Connection.Query<long?>(sql, new { Fam = LastName, Name = FirstName, DateBorn = BirthDate }).FirstOrDefault();
                if (dbResult.HasValue && dbResult.Value == Id)
                    return false;
                if (dbResult.HasValue && dbResult.Value != Id)
                    return true;
            }
            return false;
        }

        public long? GetId()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select dan_id from dan_tab
                            where upper(fam)=upper(@Fam) and upper(nam)=upper(@Name) 
                            and date_born=@DateBorn";
                var dbResult = db.Connection.Query<long?>(sql, new { Fam = LastName, Name = FirstName, DateBorn = BirthDate }).FirstOrDefault();
                return dbResult;
            }
        }


    }

    public enum Gender
    {
        Unknown = -1,
        Male = 1,
        Female = 2
    }
}
