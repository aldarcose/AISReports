using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model.Annotations;
using Model.Classes.Documents;
using Model.Classes.Research;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using System;
using Dapper;


namespace Model.Classes
{
    // dan_tab
    public class Patient : Person, ILoadData, ISaveable
    {
        public Patient()
        {
            IsLoading = false;
            IsLoaded = false;
        }
        public Patient(long patientId) : this()
        {
            LoadData(patientId);
        }

        private List<Lgota> _lgotas = null;
        private FluorographyResearch _fluorography;

        /// <summary>
        /// Льготы пациента
        /// </summary>
        public List<Lgota> Lgotas {
            get
            {
                //if (_lgotas == null || _lgotas.Count == 0)
                //{
                    LoadLgotas();
                //}
                return _lgotas;
            } 
        }

        /// <summary>
        /// Исследования
        /// </summary>
        public List<BaseResearch> Researches
        {
            get
            {
                throw new NotImplementedException();
                return null;
            }
        }

        private void LoadLgotas()
        {
            _lgotas = new List<Lgota>();

            using (var db = new DbWorker())
            {
                var federal = new DbQuery("GetFedLgotas");
                federal.Sql = "select * from fed_lgota_tab where pens like @snils;";
                federal.AddParamWithValue("snils", Snils);

                var fedResults = db.GetResults(federal);
                foreach (var result in fedResults)
                {
                    var fedLgota = new FederalLgota();
                    if (fedLgota.LoadData(result))
                    {
                        _lgotas.Add(fedLgota);
                    }
                }

                var local = new DbQuery("GetLocalLgotas");
                local.Sql = @"select 
                              lgota_id lgotacode, 
                              cl.name lgotaname, 
                              d.entry_date datestart, 
                              d.leave_date dateend
                              from dispensary_registration_tab d
                              left join codifiers.lgota_tab cl on cl.code=d.lgota_id
                              where length(cl.code)=2 and dan_id = @Id
                              union 
                                SELECT 
	                                '999'::character varying AS lgota,
                                    'Беременные'::varchar lgotaname,
                                    null datestart,
                                    null dateend
                                FROM diagn_tab diag
                                WHERE diag.data >= (now() - '1 mon'::interval) AND diag.data <= now() AND
                                    (diag.diagn_osn::text ~~* '%O%'::text OR diag.diagn_osn::text ~~*
                                    '%Z34%'::text OR diag.diagn_osn::text ~~* '%Z35%'::text OR
                                    diag.diagn_osn::text ~~* '%Z32%'::text) and dan_id=@Id
                                UNION
                                SELECT 
                                    '003'::character varying AS lgotacode,
                                    'Дети до 3-х лет'::varchar lgotaname,
                                    null datestart,
                                    null dateend
	                                FROM dan_tab d
	                                WHERE date_part('year'::text, age(now(), d.date_born::timestamp with time
                                    zone)) < 3::double precision and dan_id=@Id
                             ";
                var localBenefits = db.Connection.Query<LocalLgota>(local.Sql, new { Id=PatientId});
                _lgotas.AddRange(localBenefits);
                //local.AddParamWithValue("id", PatientId);

                //var locResults = db.GetResults(local);
                //foreach (var result in locResults)
                //{
                //    var lgota = DbResult.GetString(result.GetByName("lgota"), "");
                //    if (lgota.Contains(","))
                //    {
                //        var lgotas = lgota.Split(',').ToArray();
                //        foreach (var lg in lgotas)
                //        {
                //            var localLgota = new LocalLgota();
                //            localLgota.PatientId = DbResult.GetNumeric(result.GetByName("dan_id"), -1);
                //            localLgota.LgotaCode = lg;
                //            _lgotas.Add(localLgota);
                //        }
                //    }
                //    else
                //    {
                //        var localLgota = new LocalLgota();

                //        if (localLgota.LoadData(result))
                //        {
                //            _lgotas.Add(localLgota);
                //        }
                //    }
                    
                //}


            }
        }
        
        public long PatientId { get; private set; }

        public long MedCardStateId { get; set; }
        public string MedCardNum { get; set; }
        public int MedCardType { get; set; }

        public FluorographyResearch Fluorography
        {
            get
            {
                if (_fluorography == null)
                {
                    _fluorography = GetLastFluorography();
                }
                return _fluorography;
            }
        }

        public long UchastokId { get; set; }
        public long UchastokDopId { get; set; }
        
        /// <summary>
        /// Код ЛПУ прикрепления
        /// </summary>
        public string LpuAttachCode { get; set; }

        public new event EventHandler Loading;
        public new event EventHandler Loaded;
        public new void OnLoading()
        {
            IsLoading = true;
            if (Loading != null)
            {
                Loading(this, null);
            }
        }

        public new void OnLoaded()
        {
            IsLoading = false;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }
        public new bool IsLoading { get; private set; }
        public new bool IsLoaded { get; private set; }

        public string Email { get; set; }

        public new bool LoadData(long id)
        {
            OnLoading();

            Id = id;
            PatientId = id;

            var loadResult = false;
            using (var dbWorker = new DbWorker())
            {
                DbQuery query = new DbQuery("GetPatientData");
                query.Sql =
                    "SELECT p.fam, p.nam, p.mid, " + // фио
                           "p.date_born, p.sex, " + // др, пол
                           "adr0.text_address, adr1.text_address, p.mest_born, " + // адреса
                           "p.card_type, p.med_karta_kod, p.med_card_state_id, " + // мед. карта
                           "p.uchactok_id, p.uchactok_dop_id, " + // участки
                           "p.date_fluog, p.lpu_fluog, p.num_fluog, " + // флюорография
                           "p.pens, " + // снилс
                           "p.sozstatus_id, " + // соц статус
                           "p.spolref, " + //  лпу прикрепления
                           "p.policy_type, p.data_begin, p.data_end, p.ser_pol, p.num_pol, p.strcom_id, p.strcom_region_id, " + // полис
                           "p.vid_doc_id, p.date_output, p.kem_output, p.serdoc, p.numdoc, " + // документ
                           "p.telephon, p.phone_mobile, p.work_phone, " + // документ
                           "p.mest_work, " + // место рождения
                           "p.email, " + // email
                           "case when p.spolref='2301001' then true else false end attached, " + //прикреплен
                           "p.mesto_work_id " +
                           "FROM dan_tab p " +
                           "LEFT JOIN address_tab adr0 ON adr0.dan_id = p.dan_id and adr0.sp_addr_type_id = 1 " +
                           "LEFT JOIN address_tab adr1 ON adr1.dan_id = p.dan_id and adr1.sp_addr_type_id = 0 " +
                           "WHERE p.dan_id = @patientid";

                query.AddParamWithValue("@patientid", PatientId);

                

                var result = dbWorker.GetResult(query);
                if (result != null)
                {
                    LastName = DbResult.GetString(result.Fields[0], "");
                    FirstName = DbResult.GetString(result.Fields[1], "");
                    MidName = DbResult.GetString(result.Fields[2], "");

                    BirthDate = DbResult.GetDateTime(result.Fields[3], DateTime.MinValue);

                    var gender = DbResult.GetString(result.Fields[4], "");
                    if (gender.Equals(""))
                        Gender = Gender.Unknown;
                    else
                        Gender = gender.Equals("ж") ? Gender.Female : Gender.Male;

                    RegAddress = new Address();
                    RegAddress.LoadAddressByPatientId(PatientId, AddressType.Reg);
                    FactAddress = new Address();
                    FactAddress.LoadAddressByPatientId(PatientId, AddressType.Fact);

                    BirthPlace = DbResult.GetString(result.Fields[7], "");

                    MedCardType = DbResult.GetInt(result.Fields[8], 0);
                    MedCardNum = DbResult.GetString(result.Fields[9], "");
                    MedCardStateId = DbResult.GetLong(result.Fields[10], -1);

                    UchastokId = DbResult.GetInt(result.Fields[11], -1);
                    UchastokDopId = DbResult.GetLong(result.Fields[12], -1);

                    if (result.Fields[13] != null)
                    {
                        /*var fluoDate = DbResult.GetNullableDateTime(result.Fields[13]);
                        var fluoLpu = DbResult.GetString(result.Fields[14], "");
                        var fluoNum = DbResult.GetString(result.Fields[15], "");

                        Fluorography = new FluorographyResearch()
                        {
                            Date = fluoDate,
                            Num =  fluoNum
                        };*/
                    }

                    Snils = DbResult.GetString(result.Fields[16], "");

                    SocStatusId = DbResult.GetInt(result.Fields[17], -1);

                    LpuAttachCode = DbResult.GetString(result.Fields[18], "");

                    if (result.Fields[19] != null)
                    {
                        // Идентификатор Единого номера полиса
                        var defaultCode = 3;
                        var policyType = new PolicyType()
                        {
                            Id = DbResult.GetInt(result.Fields[19], defaultCode)
                        };
                        policyType.LoadData(policyType.Id);

                        var dateBeg = result.Fields[20] == null
                            ? null
                            : (DateTime?) DbResult.GetDateTime(result.Fields[20], DateTime.MinValue);

                        var dateEnd = result.Fields[21] == null
                            ? null
                            : (DateTime?)DbResult.GetDateTime(result.Fields[21], DateTime.MinValue);

                        var serial = DbResult.GetString(result.Fields[22], "");
                        var number = DbResult.GetString(result.Fields[23], "");

                        var smoId = DbResult.GetInt(result.Fields[24], -1);
                        var smoRegionCode = DbResult.GetString(result.Fields[25], "");

                        Policy = new Policy()
                        {
                            Type = policyType,
                            DateBeg =  dateBeg,
                            DateEnd = dateEnd,
                            Serial = serial,
                            Number = number,
                            SmoId = smoId
                            //SmoRegionCode = smoRegionCode
                        };
                    }

                    if (result.Fields[26] != null)
                    {
                        // Идентификатор паспорта
                        var defaultCode = 14;
                        var docType = new PersonDocumentType()
                        {
                            Id = DbResult.GetInt(result.Fields[26], defaultCode)
                        };

                        docType.LoadData(docType.Id);

                        var dateBeg = result.Fields[27] == null
                            ? null
                            : (DateTime?)DbResult.GetDateTime(result.Fields[27], DateTime.MinValue);


                        var org = DbResult.GetString(result.Fields[28], "");
                        var serial = DbResult.GetString(result.Fields[29], "");
                        var number = DbResult.GetString(result.Fields[30], "");

                        Document = new PersonDocument()
                        {
                            Type = docType,
                            DateBeg = dateBeg,
                            Serial = serial,
                            Number = number,
                            Organization = org
                        };
                    }

                    if (result.Fields[31] != null)
                    {
                        var homePhones = DbResult.GetString(result.Fields[31], "");

                        foreach (var homePhone in homePhones.Split(new char[]{';'}))
                        {
                            if (!string.IsNullOrEmpty(homePhone))
                            Phones.Add(new PhoneNumber(){Number = homePhone, Type = PhoneType.Home});
                        }
                    }

                    if (result.Fields[32] != null)
                    {
                        var mobPhones = DbResult.GetString(result.Fields[32], "");

                        foreach (var mobPhone in mobPhones.Split(new char[] { ';' }))
                        {
                            if (!string.IsNullOrEmpty(mobPhone))
                                Phones.Add(new PhoneNumber() { Number = mobPhone, Type = PhoneType.Mobile });
                        }
                    }

                    if (result.Fields[33] != null)
                    {
                        var workPhones = DbResult.GetString(result.Fields[33], "");

                        foreach (var workPhone in workPhones.Split(new char[] { ';' }))
                        {
                            if (!string.IsNullOrEmpty(workPhone))
                                Phones.Add(new PhoneNumber() { Number = workPhone, Type = PhoneType.Work });
                        }
                    }

                    this.WorkPlace = DbResult.GetString(result.Fields[34], "");
                    this.Email = DbResult.GetString(result.Fields[35], "");
                    this.Attached = DbResult.GetBoolean(result.Fields[36], false);
                    this.WorkPlaceId = DbResult.GetInt(result.Fields[37], -1);

                    loadResult = true;
                }
            }

            IsLoaded = loadResult;
            OnLoaded();

            return loadResult;
        }

        public event EventHandler Saving;
        
        public void OnSaving()
        {
            if (Saving != null)
            {
                Saving(this, null);
            }
        }

        public event EventHandler Saved;

        public void OnSaved()
        {
            if (Saved != null)
            {
                Saved(this, null);
            }
        }

        public bool CanSave(Operator @operator, out string message)
        {
            if (string.IsNullOrEmpty(LastName))
            {
                message = "Нет фамилии";
                return false;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                message = "Нет Имени";
                return false;
            }

            if(BirthDate==DateTime.MinValue)
            {
                message = "Не указана дата рождения";
                return false;
            }

            if (HasDuplicates())
            {
                message = "Существуют дубли пациента";
                return false;
            }
            
            message = string.Empty;
            return true;
        }

        public void Save(Operator @operator)
        {
            throw new NotImplementedException();
        }

        public bool IsSaved { get; set; }

        
        
        public void Save()
        {
            OnSaving();

            string sql =string.Empty;
            bool isExist = (Id!=default(long));
            
            if (!isExist)
            {
                sql = @"insert into dan_tab(fam, nam, mid, date_born, sex) 
                        values(@Fam, @Im, @Ot, @DateBorn, @Sex) returning dan_id";
            }
                            
            string updateSql=
                    @"update dan_tab set
                    fam=@Fam, nam=@Im, mid=@Ot, 
                    date_born=@DateBorn, 
                    sex=@Sex,
                    mest_born=@BornPlace,
                    uchactok_id=@UchastokId,
                    uchactok_dop_id= @UchastokDopId,
                    pens=@SNILS,
                    sozstatus_id=@SocStatusId,
                    policy_type=@PolicyType,
                    data_begin=@PolicyDateBegin,
                    data_end=@PolicyDateEnd,
                    ser_pol=@PolicySerial,
                    num_pol=@PolicyNum,
                    strcom_id=@PolicyStrCom,
                    strcom_region_id=@PolicyStrComRegionId,
                    vid_doc_id=@DocVid,
                    date_output=@DocDateOut,
                    kem_output=@DocWhoOut,
                    serdoc=@DocSerial,
                    numdoc=@DocNum,
                    telephon=@Phone,
                    phone_mobile=@PhoneMobile,
                    work_phone=@PhoneWork,
                    mest_work=@WorkPlace,
                    email=@Email
                    where dan_id=@PatientId";

                           
            using(var db=new DbWorker())
            {
                var parameters = new DynamicParameters();
                parameters.Add("Fam",LastName);
                        parameters.Add("Im",FirstName);
                        parameters.Add("Ot", MidName);
                        parameters.Add("DateBorn",BirthDate);
                        parameters.Add("Sex", (Gender == Gender.Male ? 'м' : 'ж'));
                if (!isExist)
                {
                    var id = db.Connection.Query<long>(sql, parameters).FirstOrDefault();
                    this.Id = id;
                }

                if (UchastokId == default(long))
                    parameters.Add("UchastokId", null);
                else
                    parameters.Add("UchastokId", UchastokId);

                if (UchastokDopId == default(long))
                    parameters.Add("UchastokDopId", null);
                else
                    parameters.Add("UchastokDopId", UchastokDopId);
                
                parameters.AddDynamicParams(new { SNILS=Snils });
                parameters.AddDynamicParams(new { SocStatusId = SocStatusId });
                
                if (Policy!=null)
                {
                    int code = 0;
                    if (Int32.TryParse(Policy.Type.Code, out code))
                    {
                        parameters.Add("PolicyType",code);
                    }
                    else
                    {
                        parameters.Add("PolicyType", null);
                    };
                    
                    parameters.AddDynamicParams(new
                    {
                        PolicyDateBegin = Policy.DateBeg,
                        PolicyDateEnd = Policy.DateBeg,
                        PolicySerial = Policy.Serial,
                        PolicyNum = Policy.Number,
                        PolicyStrCom = Policy.SmoId,
                        PolicyStrComRegionId = Policy.SmoRegionCode,
                    });
                }
                    
                if (Document!=null)
                {
                    parameters.AddDynamicParams(new {
                        DocVid=Document.Type.FomsCode,
                        DocDateOut=Document.DateBeg,
                        DocWhoOut = Document.Organization,
                        DocSerial = Document.Serial,
                        DocNum= Document.Number, 
                    });
                }

                if (Phones != null)
                {
                    var home=Phones.FirstOrDefault(t => t.Type == PhoneType.Home);
                    if (home!=null)
                        parameters.Add("Phone", home.Number!=null ? home.Number:null);
                    else
                        parameters.Add("Phone", null);
                    var mobile = Phones.FirstOrDefault(t => t.Type == PhoneType.Mobile);
                    if (mobile!=null)
                        parameters.Add("PhoneMobile", mobile.Number == null ? null : mobile.Number);
                    else
                        parameters.Add("PhoneMobile", null);
                    var work = Phones.FirstOrDefault(t => t.Type == PhoneType.Work);
                    if (work!=null)
                        parameters.Add("PhoneWork", work.Number == null ? null : work.Number);
                    else
                        parameters.Add("PhoneWork", null);
                }
                else
                {
                    parameters.Add("Phone", null);
                    parameters.Add("PhoneMobile", null);
                    parameters.Add("PhoneWork", null);
                }

                parameters.Add("BornPlace", BirthPlace);
                parameters.AddDynamicParams(new { WorkPlace = WorkPlace });
                if (!string.IsNullOrEmpty(Email))
                {
                    parameters.Add("Email", Email);
                }
                else
                {
                    parameters.Add("Email", null);
                }

                parameters.Add("PatientId", Id);
                
                var result = db.Connection.Execute(updateSql, parameters);
            }

            OnSaved();
            
        }

        private FluorographyResearch GetLastFluorography()
        {
            FluorographyResearch fluo = null;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("SelectLastFluorography");
                q.Sql =
                    "SELECT * FROM researches.fluorography_tab where dan_id = @id order by date_analisis desc limit 1;";
                q.AddParamWithValue("id", PatientId);

                var result = db.GetResult(q);

                if (result != null)
                {
                    fluo = new FluorographyResearch();
                    fluo.Date = DbResult.GetNullableDateTime(result.GetByName("date_analisis"));
                    fluo.LpuId = DbResult.GetNumeric(result.GetByName("sp_lpu_id"), 0);
                    fluo.Num = DbResult.GetNumeric(result.GetByName("analisis_number"), 0);
                    fluo.PatientId = PatientId;
                    fluo.RecordOwnerLpuId = DbResult.GetString(result.GetByName("lpu_id"), string.Empty);
                }
            }

            return fluo;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", LastName, FirstName, MidName, BirthDate.ToShortDateString());
        }
    }
}
