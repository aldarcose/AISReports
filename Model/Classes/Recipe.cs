using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Classes.Codifiers;
using Model.Interface;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;
using Model.Classes;


namespace Model.Classes
{
    public class Recipe : ISaveable, ILoadData
    {
        public long Id { get; set; }

        /// <summary>
        /// Серия рецепта
        /// </summary>
        public string Serial { get; set; }
        /// <summary>
        /// Номер рецепта
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Код льготы
        /// </summary>
        public string BenefitCode { get; set; }

        /// <summary>
        /// Идентификатор пациента
        /// </summary>
        public long PatientId { get; set; }
        /// <summary>
        /// идентификатор случая, в рамках которого выписывается рецепт
        /// </summary>
        public long TalonId { get; set; }

        /// <summary>
        /// Код диагноза по МКБ
        /// </summary>
        public string MkbCode { get; set; }

        /// <summary>
        /// Дата внесения информации
        /// </summary>
        public DateTime InsertInfoDate { get; set; }

        /// <summary>
        /// Доктор, который выписал рецепт
        /// </summary>
        public long DoctorId { get; set; }

        /// <summary>
        /// Способ применения
        /// </summary>
        public string Signa { get; set; }

        /// <summary>
        /// Дата выписки
        /// </summary>
        public DateTime DischargeDate { get; set; }

        /// <summary>
        /// Источник финансирования
        /// </summary>
        public RevenueType RevenueSource { get; set; }

        /// <summary>
        /// Процент оплаты
        /// </summary>
        public PayPercent PayPercent { get; set; }

        /// <summary>
        /// Срок действия рецепта (в днях: 5, 10, 30, 90)
        /// </summary>
        public int Validity { get; set; }

        /// <summary>
        /// Флаг МНН/Торг. название
        /// </summary>
        public bool IsTradeName { get; set; }
        /// <summary>
        /// Идентификатор ЛС
        /// </summary>
        public long MedicamentId { get; set; }
        /// <summary>
        /// Название препарата (либо по мнн, либо торговое)
        /// </summary>
        public string MedicamentName { get; set; }
        /// <summary>
        /// Форма Л.С.
        /// </summary>
        public string MedicamentDtd { get; set; }
        /// <summary>
        /// Код препарата
        /// </summary>
        public long MedicamentFedCode { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Дозировка
        /// </summary>
        public string Doze { get; set; }

        /// <summary>
        /// Выписан через врачебную комиссию
        /// </summary>
        public bool IsVK { get; set; }

        /// <summary>
        /// Дата отпуска
        /// </summary>
        public DateTime ReleaseDate { get; set; }

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

        // period = 0 - за день. period = 1 - за месяц
        public int GetPatientRecipeCount(int period)
        {
            var result = 0;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetRecipeCount");
                q.Sql = "SELECT count(dan_id) FROM rezept_tab WHERE dan_id = @patientId ";
                q.AddParamWithValue("patientId", PatientId);
                switch (period)
                {
                    case 0:
                        q.Sql += "and date_vipiski = @date;";
                        q.AddParamWithValue("date", DischargeDate);
                        break;
                    case 1:
                        q.Sql += "and date_part('month', date_vipiski) = @month;";
                        q.AddParamWithValue("month", DischargeDate.Month);
                        break;
                }
                var results = db.GetResults(q);
                if (results!=null)
                    result = results.Count;
            }
            return result;
        }

        private bool checkNosology()
        {
            var result = false;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("CheckNosology");
                q.Sql = "SELECT r.id FROM codifiers.recept_nosology_tab r WHERE r.code = @code;";
                q.AddParamWithValue("code", MkbCode.Length < 5 ? MkbCode.Substring(0, 3) : MkbCode);

                var results = db.GetResults(q);
                if (results != null && results.Count > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool CanSave(Operator @operator, out string message)
        {
            if (MedicamentId == -1)
            {
                message = "Не выбрано лекарственное средство";
                return false;
            }

            if (string.IsNullOrEmpty(MkbCode))
            {
                message = "Не указан диагноз";
                return false;
            }

            if (string.IsNullOrEmpty(BenefitCode) && !checkNosology())
            {
                message = "У данного пациента нет льготы.";
                return false;
            }

            if (string.IsNullOrEmpty(Signa))
            {
                message = "Не указан способ применения";
                return false;
            }

            if (Count < 1)
            {
                message = "Не указано количество ЛС";
                return false;
            }

            if (Validity==default(int))
            {
                message = "Не указан период действия";
                return false;
            }

            if (@operator.Permissions.Any(x=>x.Id!=(long)Permissions.Admin) &&  DoctorId != @operator.DoctorId)
            {
                message = "Рецепт создан другим врачом";
                return false;
            }

            using(var db = new DbWorker())
            {
                var snils = db.Connection.Query<string>("select pens from dan_tab where dan_id=@PatientId", new { PatientId=PatientId }).FirstOrDefault();
                var result=SnilsValidator.Validate(snils);
                if (!result)
                {
                    message = "Неверный СНИЛС пациента";
                    return false;
                }
            }
            

            message = string.Empty;
            return true;
        }

        public string GetNewRecipeNumber()
        {
            var number = string.Empty;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("getNumber");
                q.Sql = "SELECT nextval('recept_seq') as num;";
                var result = db.GetScalarResult(q);
                if (result != null)
                {
                    number = result.ToString();
                }
            }
            return number;
        }
        public void Save(Operator @operator)
        {
            OnSaving();

            var medicament = new Medicament();
            if (!medicament.LoadData(MedicamentId))
            {
                return;
            }

            if (string.IsNullOrEmpty(Number))
            {
                Number = GetNewRecipeNumber();
            }

            using (var db = new DbWorker())
            {
                var q = new DbQuery("Insert recipe");
                if (this.Id!=default(long))
                {
                    q.Sql = @"update public.rezept_tab set
                              dan_id = @dan_id, 
                              talon_id= @talon_id,
                              date_vipiski = @date_vipiski, 
                              lekarstv_id = @lekarstv_id, 
                              cena=@cena, 
                              kol=@kol,
                              summa= @summa,                                                            
                              doctor_id = @doctor_id,
                              kod_lgot = @kod_lgot,
                              doza = @doza,
                              dt_vnes_rezept = @dt_vnes_rezept,
                              kod_minzd = @kod_minzd,
                              ist_finans = @ist_finans,
                              proc_oplat = @proc_oplat,
                              vipis_po_torg = @vipis_po_torg,
                              p_kek = @p_kek,
                              diagn_id = @diagn_id,
                              signa =@signa,
                              operator_id = @operator_id,
                              d_ls = @d_ls,
                              sp_doza_id = @sp_doza_id,
                              valid_period = @valid_period,
                              ist_financing= @ist_financing,
                              percent_paid = @percent_paid
                              where rezept_id=@id
                             ";
                    q.AddParamWithValue("@id", Id);
                }else
                {
                    q.Sql = @"INSERT INTO 
                              public.rezept_tab (
                              dan_id, talon_id,
                              date_vipiski, lekarstv_id, 
                              cena, kol, summa,                              
                              doctor_id,
                              kod_lgot,
                              doza,
                              dt_vnes_rezept,
                              kod_minzd,
                              ist_finans,
                              proc_oplat,
                              vipis_po_torg,
                              p_kek,
                              diagn_id,
                              signa,
                              operator_id,
                              d_ls,
                              sp_doza_id,
                              valid_period,
                              ist_financing,
                              percent_paid,
                              rezept_ser,
                              rezept_num) 
                            VALUES (
                              @dan_id,
                              @talon_id,
                              @date_vipiski,
                              @lekarstv_id,
                              @cena,
                              @kol,
                              @summa,                              
                              @doctor_id,
                              @kod_lgot,
                              @doza,
                              @dt_vnes_rezept,
                              @kod_minzd,
                              @ist_finans,
                              @proc_oplat,
                              @vipis_po_torg,
                              @p_kek,
                              @diagn_id,
                              @signa,
                              @operator_id,
                              @d_ls,
                              @sp_doza_id,
                              @valid_period,
                              @ist_financing,
                              @percent_paid,
                              @rezept_ser,
                              @rezept_num
                            );";
                }
                
                q.AddParamWithValue("dan_id", PatientId);
                q.AddParamWithValue("talon_id", TalonId);
                q.AddParamWithValue("date_vipiski", DischargeDate);
                q.AddParamWithValue("lekarstv_id", medicament.Mnn);
                q.AddParamWithValue("cena", medicament.Price);
                q.AddParamWithValue("kol", Count);
                q.AddParamWithValue("summa", medicament.Price * Count);
                q.AddParamWithValue("doctor_id", DoctorId);
                q.AddParamWithValue("kod_lgot", this.BenefitCode);
                q.AddParamWithValue("doza", Doze);
                q.AddParamWithValue("dt_vnes_rezept", InsertInfoDate);
                q.AddParamWithValue("kod_minzd", MedicamentFedCode);
                if (this.Id==default(long))
                {
                    q.AddParamWithValue("rezept_ser", Serial);
                    q.AddParamWithValue("rezept_num", Number);
                }
                
                switch (RevenueSource)
                {
                    case RevenueType.Federal:
                        q.AddParamWithValue("ist_finans", "Федеральный");
                        q.AddParamWithValue("ist_financing", (int)RevenueSource - 1);
                        break;
                    case RevenueType.Region:
                        q.AddParamWithValue("ist_finans", "Субъект РФ");
                        q.AddParamWithValue("ist_financing", (int)RevenueSource - 1);
                        break;
                    case RevenueType.Municipal:
                        q.AddParamWithValue("ist_finans", "Муниципальный");
                        q.AddParamWithValue("ist_financing", (int)RevenueSource - 1);
                        break;
                    default:
                        break;
                }

                switch (PayPercent)
                {
                    case PayPercent.Free:
                        q.AddParamWithValue("proc_oplat", "Бесплатно");
                        q.AddParamWithValue("percent_paid", (int)PayPercent);
                        break;
                    case PayPercent.P50:
                        q.AddParamWithValue("proc_oplat", "50%");
                        q.AddParamWithValue("percent_paid", (int)PayPercent);
                        break;
                    default:
                        break;
                }
                q.AddParamWithValue("vipis_po_torg", IsTradeName ? 1 : 0);
                q.AddParamWithValue("p_kek", IsVK ? 1 : 0);
                q.AddParamWithValue("diagn_id", MkbCode);
                q.AddParamWithValue("signa", Signa);
                q.AddParamWithValue("operator_id", @operator.Id);
                q.AddParamWithValue("prog_mode", 1);
                q.AddParamWithValue("d_ls", medicament.VDoze);
                q.AddParamWithValue("sp_doza_id", medicament.DozeId);

                switch (Validity)
                {
                    case 5:
                        q.AddParamWithValue("valid_period", 300000);
                        break;
                    case 10:
                        q.AddParamWithValue("valid_period", 400000);
                        break;
                    case 15:
                        q.AddParamWithValue("valid_period", 0);
                        break;
                    case 30:
                        q.AddParamWithValue("valid_period", 500000);
                        break;
                    case 90:
                        q.AddParamWithValue("valid_period", 600000);
                        break;
                }
                var insertResult = db.Execute(q);
                var id = DbResult.GetNumeric(insertResult, -1);
                if (id > 0)
                {
                    
                }

            }

            OnSaved();
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
            var loadResult = false;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetRecipe");
                q.Sql = "SELECT r.dan_id, r.talon_id, r.date_vipiski, r.doctor_id, " +
                        "r.kod_lgot, r.doza, r.dt_vnes_rezept, r.vipis_po_torg, " +
                        "r.p_kek, r.diagn_id, r.signa, r.valid_period, r.ist_financing, " +
                        "r.percent_paid, r.rezept_ser,r.rezept_num, p.id as medicament_id, r.kol " +
                        "FROM public.rezept_tab r inner join codifiers.preparat_tab p on p.code = r.kod_minzd " +
                        "where r.rezept_id = @id;";
                q.AddParamWithValue("id", id);

                var result = db.GetResult(q);
                if (result != null)
                {
                    PatientId = DbResult.GetNumeric(result.GetByName("dan_id"), 0);
                    TalonId = DbResult.GetNumeric(result.GetByName("talon_id"), 0);
                    DischargeDate = DbResult.GetDateTime(result.GetByName("date_vipiski"), DateTime.Now);
                    DoctorId = DbResult.GetNumeric(result.GetByName("doctor_id"), 0);
                    BenefitCode = DbResult.GetString(result.GetByName("kod_lgot"), string.Empty);
                    Doze = DbResult.GetString(result.GetByName("doza"), string.Empty);
                    InsertInfoDate = DbResult.GetDateTime(result.GetByName("dt_vnes_rezept"), DateTime.Now);
                    var torg = DbResult.GetNumeric(result.GetByName("vipis_po_torg"), 0);
                    IsTradeName = torg == 1;
                    var vk = DbResult.GetNumeric(result.GetByName("p_kek"), 0);
                    IsVK = vk == 1;
                    MkbCode = DbResult.GetString(result.GetByName("diagn_id"), string.Empty);
                    Signa = DbResult.GetString(result.GetByName("signa"), string.Empty);
                    Count = DbResult.GetInt(result.GetByName("kol"), 0);
                    MedicamentId = DbResult.GetNumeric(result.GetByName("medicament_id"), 0);
                    Serial = DbResult.GetString(result.GetByName("rezept_ser"), string.Empty);
                    Number = DbResult.GetString(result.GetByName("rezept_num"), string.Empty);

                    var valid = DbResult.GetNumeric(result.GetByName("valid_period"), 0);
                    switch (valid)
                    {
                        case 300000:
                            Validity = 5;
                            break;
                        case 400000:
                            Validity = 10;
                            break;
                        case 0:
                            Validity = 15;
                            break;
                        case 500000:
                            Validity = 30;
                            break;
                        case 600000:
                            Validity = 90;
                            break;
                    }
                    var ist = DbResult.GetNumeric(result.GetByName("ist_financing"), 0);
                    switch (ist)
                    {
                        case 0:
                            RevenueSource = RevenueType.Federal;
                            break;
                        case 1:
                            RevenueSource = RevenueType.Region;
                            break;
                        case 2:
                            RevenueSource = RevenueType.Municipal;
                            break;
                        default:
                            break;
                    }
                    var paid = DbResult.GetNumeric(result.GetByName("percent_paid"), 0);
                    switch (paid)
                    {
                        case 0:
                            PayPercent = PayPercent.Free;
                            break;
                        case 1:
                            PayPercent = PayPercent.P50;
                            break;
                        default:
                            break;
                    }

                    loadResult = true;
                }
            }

            return loadResult;
        }

        public bool LoadData(DbResult result)
        {
            throw new NotImplementedException();
        }
    }

    public enum RevenueType
    {
        // федеральный
        Federal = 1,
        // Субъект РФ
        Region = 2,
        // Муниципальный
        Municipal = 3
    }

    public enum PayPercent
    {
        // Бесплатно
        Free = 0,
        // 50 %
        P50 = 1
    }

    public class RecipeRepository
    {
        public IEnumerable<Recipe> GetRecipesByVisit(Visit visit)
        {
            var recipes = new List<Recipe>();
            if (visit == null) return null;
            using(var db = new DbWorker())
            {

                var sql = "SELECT r.rezept_id id, r.rezept_ser serial, r.rezept_num number, r.doza doze, r.signa, " +
                        "case when r.valid_period=300000 then 5 when r.valid_period=400000 then 10 when r.valid_period=500000 then 30 when valid_period=600000 then 90 end validity, " +
                        "r.kod_minzd medicamentfedcode " +
                        "FROM public.rezept_tab r " +
                        "where talon_id=@TalonId and date_vipiski=@IssueDate and not bad ";
                var result = db.Connection.Query<Recipe>(sql, new { TalonId = visit.TalonId, IssueDate = visit.VisitDate });
                return result;
                
            }

        }
    }

    public class Drug
    {
        public long? Id { get; set; }
        public long? OperatorId { get; set; }
        public DateTime? DateBegin {get;set;}
        public DateTime? DateEnd { get; set; }
        public long? PatientId { get;set;}
        public long? LechTypeId { get; set; }
        public string Dose { get; set; }
        public string MedicamentCode { get; set; }
        public string MedicamentName { get; set; }
        public string MkbCode { get; set; }
        public long? InjectionTypeId { get; set; }
        public string Comments { get; set; }
        public DateTime InsertInfoDate{get;set;}
        public long? VisitId { get; set; }
        public string Signa { get; set; }

    }

    public class DrugRepository
    {
        
        private string drugInitSql = @"select id, date_vnes_inf insertinfodate, operator_id operatorid, date_begin datebegin, date_end dateend, dan_id patientid, 
                          dose, kod_minzd medicamentcode, diagn_id mkbcode, preparat_name medicamentname, injection_type injectiontypeid, comments,
                          preparat_dose signa from public.lech_preparat_tab ";
        
        public Drug Get(long? id)
        {
            if (!id.HasValue) return null;
            using(var db=new DbWorker())
            {
                var sql = drugInitSql + " where id=@Id" ;
                var result=db.Connection.Query<Drug>(sql, new { Id = id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Drug> GetByVisit(Visit visit)
        {
            if (visit.Id == default(long)) return null;
            using(var db=new DbWorker())
            {
                var sql = drugInitSql + "where visit_id=@VisitId ";
                          
                var result = db.Connection.Query<Drug>(sql, new { VisitId = visit.Id });
                return result;
            }
        }

        public IEnumerable<Drug> GetByPatient(long? patientId)
        {
            if (!patientId.HasValue) return null;
            using (var db = new DbWorker())
            {
                var sql = drugInitSql+"where dan_id=@PatientId";
                          
                var result = db.Connection.Query<Drug>(sql, new { PatientId = patientId });
                return result;
            }
        }

        public IEnumerable<Drug> GetByPatient(long? patientId, long visitId)
        {
            if (!patientId.HasValue) return null;
            using (var db = new DbWorker())
            {
                var sql = drugInitSql + "where dan_id=@PatientId and visit_id=@VisitId";

                var result = db.Connection.Query<Drug>(sql, new { PatientId = patientId, VisitId = visitId });
                return result;
            }
        }

        public bool CanSave(Drug drug, out string message)
        {
            if (!drug.PatientId.HasValue)
            {
                message = "Не определен пациент";
                return false;
            }

            if (!drug.OperatorId.HasValue)
            {
                message = "Не определен врач";
                return false;
            }
            
            if (string.IsNullOrEmpty(drug.MedicamentName))
            {
                message = "Отсутствует название препарата";
                return false;
            }

            if (!drug.DateBegin.HasValue)
            {
                message = "Отсутствует дата начала приема";
                return false;
            }

            if (!drug.DateEnd.HasValue)
            {
                message = "Отсутствует дата окончания приема";
                return false;
            }

            message = string.Empty;
            return true;
        }

        public void Save(Drug drug, long visitId)
        {
            using(var db = new DbWorker())
            {
                var sql = string.Empty;
                if (drug.Id.HasValue)
                {
                    sql = @"update public.lech_preparat_tab set 
                          date_vnes_inf=@insertinfodate, operator_id=@operatorid, date_begin=@datebegin, date_end=@dateend, dan_id=@patientid, 
                          dose=@dose, kod_minzd=@medicamentcode, diagn_id=@mkbcode, preparat_name=@medicamentname, injection_type=@injectiontypeid, comments=@comments,
                          lech_type_id=0, visit_id=@VisitId
                          where id=@Id";
                }
                else
                {
                    sql = @"insert into  
                        public.lech_preparat_tab(date_vnes_inf, operator_id, date_begin, date_end, dan_id, 
                        dose, kod_minzd, diagn_id, preparat_name, injection_type, comments, lech_type_id, visit_id)
                        values(@insertinfodate, @operatorid,@datebegin,@dateend,@patientid,@dose,@medicamentcode,@mkbcode,@medicamentname,@injectiontypeid,@comments,0, @VisitId)    
                        returning id";
                }

                var args = new DynamicParameters();
                if (drug.Id.HasValue)
                    args.Add("Id", drug.Id.Value);
                drug.InsertInfoDate = DateTime.Now;
                args.Add("insertinfodate", drug.InsertInfoDate);
                args.Add("operatorid", drug.OperatorId);
                args.Add("datebegin", drug.DateBegin);
                args.Add("dateend", drug.DateEnd);
                args.Add("patientid", drug.PatientId);
                args.Add("dose", drug.Dose);
                args.Add("medicamentcode", drug.MedicamentCode);
                args.Add("mkbcode", drug.MkbCode);
                args.Add("medicamentname", drug.MedicamentName);
                args.Add("injectiontypeid", drug.InjectionTypeId);
                args.Add("comments", drug.Comments);
                args.Add("VisitId", visitId);

                var result = db.Connection.ExecuteScalar<long?>(sql,args);
            }//end using

        }

        public int Delete(long? id)
        {
            if (!id.HasValue) return 0;
            using(var db  = new DbWorker())
            {
                var result = db.Connection.Execute("delete from public.lech_preparat_tab where id=@Id", new { Id = id });
                return result;
            }
        }

        
    }

    
}
