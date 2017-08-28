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
    public class Doctor : Person
    {
        public Doctor()
        {}

        public new long Id { get; set; }
        /// <summary>
        /// Ид Данных о докторе как человеке
        /// </summary>
        public long PatientId { get; set; }
        /// <summary>
        /// ид врача-медсестры, закрепленной за доктором
        /// </summary>
        public long NurseId { get; set; }
        /// <summary>
        /// Ид отделения
        /// </summary>
        public long DivisionId { get; set; }
        public string DivisionName { get;set;}
        /// <summary>
        /// Ид специальности
        /// </summary>
        public long SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        /// <summary>
        /// Ид должности
        /// </summary>
        public long PositionId { get; set; }
        public string PositionName { get { return GetPositionName(); } }
        /// <summary>
        /// Ид участка
        /// </summary>
        public long MedAreId { get; set; }
        /// <summary>
        /// Фед. код врача
        /// </summary>
        public string FedCode { get; set; }
        /// <summary>
        /// Внутренний код врача
        /// </summary>
        public int? InnerCode { get; set; }
        public DateTime? Norma { get; set; }
        public Decimal? Stavka { get; set; }
        public long? OperatorId { get; set; }
        public string Login { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CertificationDate { get; set; }
        public int? SameTimePatientCount { get; set; }
        public string Description { get;set;}
        public decimal? PaymentPercent { get;set; }
        public int? UET { get; set; }
        
        /// <summary>
        /// Показывать ли  врача в регистратуре
        /// </summary>
        public bool? ShowInReg { get; set; }
        /// <summary>
        /// Может ли иметь расписание в регистратуре
        /// can_have_schedule
        /// </summary>
        public bool? CanHaveSchedule { get; set; }
        /// <summary>
        /// макс кол-во записей в день.
        /// </summary>
        public int? MaxRecords { get; set; }
        
        public EmployeeType EmpType { get; set; }

        public long LpuId { get; set; }


        public new bool LoadData(long id)
        {
            OnLoading();
            
            var loadResult = false;
            Id = id;

            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetDoctorData");
                q.Sql = @"Select t.*, spec.xprvs_new 
                          from public.doctor_tab t 
                          inner join codifiers.doctor_spec_tab spec on spec.id = t.sp_spec_doctor_id 
                          where t.id=@id;";
                q.AddParamWithValue("@id", id);
                var r = db.GetResult(q);

                if (r != null && r.Fields.Count > 0)
                {
                    this.PatientId = DbResult.GetNumeric(r.GetByName("dan_id"), -1);
                    this.NurseId = DbResult.GetNumeric(r.GetByName("nurse_id"), -1);

                    this.DivisionId = DbResult.GetNumeric(r.GetByName("sp_otdel_id"), -1);
                    this.SpecialityId = DbResult.GetNumeric(r.GetByName("sp_spec_doctor_id"), -1);
                    this.PositionId = DbResult.GetNumeric(r.GetByName("sp_dolg_doctor_id"), -1);
                    this.MedAreId = DbResult.GetNumeric(r.GetByName("uchastok_id"), -1);
                    this.InnerCode = DbResult.GetInt(r.GetByName("inner_doctor_code"), -1);
                    //this.SpecialityId = DbResult.GetNumeric(r.GetByName("xprvs_new"), -1);
                    this.FedCode = DbResult.GetString(r.GetByName("federal_code"), "");
                    this.LpuId = DbResult.GetLong(r.GetByName("sp_lpu_id"), -1);
                    loadResult = true;
                }
            }

            base.LoadData(PatientId);

            OnLoaded();
            return loadResult;
        }


        public string GetPositionName()
        {
            using(var db = new DbWorker())
            {
                var result = db.Connection.Query<string>("select name from  codifiers.doctor_dolg_tab where id=@Id", new { Id=PositionId}).FirstOrDefault();
                return result;
            }
        }

        public override string ToString()
        {
            return string.Format("{3} ({0} {1}{2})", this.LastName, string.IsNullOrEmpty(this.FirstName) ? string.Empty : this.FirstName[0] + ".",
                string.IsNullOrEmpty(this.MidName) ? string.Empty : this.MidName[0] + ".", this.InnerCode);
        }

        public string FIO
        {
            get{
                return string.Format("{0} {1}{2}", this.LastName, string.IsNullOrEmpty(this.FirstName) ? string.Empty : this.FirstName[0] + ".",
            string.IsNullOrEmpty(this.MidName) ? string.Empty : this.MidName[0] + ".", this.InnerCode);
            }
        }

        public string RegistryInfo
        {
            get
            {
                return string.Format("{0} {1}{2} ({3}) код {4}", this.LastName, string.IsNullOrEmpty(this.FirstName) ? string.Empty : this.FirstName[0] + ".",
            string.IsNullOrEmpty(this.MidName) ? string.Empty : this.MidName[0] + ".", 
            (string.IsNullOrEmpty(SpecialityName) ? GetSpecialityName() : SpecialityName), 
                this.InnerCode);
            }
        }

        public string GetSpecialityName()
        {
            var name = string.Empty;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetSpecialityName");
                q.Sql = "SELECT name FROM codifiers.doctor_spec_tab where id = @id;";
                q.AddParamWithValue("@id", SpecialityId);
                var r = db.GetScalarResult(q);

                name = DbResult.GetString(r, string.Empty);
            }
            return name;
        }

        public string GetDivisionName()
        {
            var name = string.Empty;
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetDoctorDivisionName");
                q.Sql = "SELECT name FROM codifiers.otdel_tab o inner join public.doctor_tab d on o.id = d.sp_otdel_id where d.doctor_id = @id;";
                q.AddParamWithValue("@id", Id);
                var r = db.GetScalarResult(q);

                name = DbResult.GetString(r, string.Empty);
            }
            return name;
        }

        /// <summary>
        /// Сохрание в БД 
        /// </summary>
        public void Save(long operatorId)
        {
            string sql = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("StartDate", StartDate);
            parameters.Add("CertDate", CertificationDate);
            parameters.Add("FedCode", FedCode);
            if (PositionId != -1 && PositionId != 0)
                parameters.Add("PositionId", PositionId);
            else
                parameters.Add("PositionId", null);
            parameters.Add("SpecialityId", SpecialityId);
            parameters.Add("Stavka", Stavka);
            parameters.Add("Norma", Norma);
            parameters.Add("DivisionId", DivisionId);
            parameters.Add("ShowInReg", ShowInReg.HasValue ? Convert.ToInt32(ShowInReg.Value) : 0);
            parameters.Add("Description", Description);
            parameters.Add("EndDate", EndDate);
            parameters.Add("UET", UET);
            parameters.Add("Payment", PaymentPercent);
            if (NurseId == -1 || NurseId == 0)
                parameters.Add("Nurseid", null);
            else
                parameters.Add("Nurseid", NurseId);
            
            if (MedAreId == -1 || MedAreId==0)
                parameters.Add("UchastokId", null);
            else
                parameters.Add("UchastokId", MedAreId);
                
            parameters.Add("SameTimeCount", SameTimePatientCount);
            parameters.Add("CanHaveSch", CanHaveSchedule.HasValue ? Convert.ToInt32(CanHaveSchedule.Value) : 0);
            parameters.Add("MaxCount", MaxRecords);
            parameters.Add("EmpType", (int)EmpType);
            parameters.Add("OperatorId", operatorId);

            if (Id!=default(long))
            {
                sql = @"update doctor_tab set
                            date_insert_doctor=@StartDate,
                            date_attestat_doctor=@CertDate,
                            federal_code=@FedCode,
                            sp_dolg_doctor_id=@PositionId,
                            sp_spec_doctor_id=@SpecialityId,
                            stavka=@Stavka,
                            norma=@Norma,
                            sp_otdel_id=@DivisionId,
                            show_in_reg=@ShowInReg,
                            description=@Description,
                            expiration_date=@EndDate,
                            uet=@UET,
                            payment=@Payment,
                            nurse_id=@Nurseid,
                            uchastok_id=@UchastokId,
                            sim_pat_count=@SameTimeCount,
                            can_have_schedule=@CanHaveSch,
                            max_records_per_day=@MaxCount,
                            employee_type=@EmpType,
                            operator_id=@OperatorId
                            where id=@Id
                           ";
                using(var db = new DbWorker())
                {
                    parameters.Add("Id", Id);
                    var result = db.Connection.Execute(sql, parameters);
                }
            }
            else //создаем нового врача
            {
                long? personId;
                if (HasDuplicates())
                {
                    personId = GetId();
                }
                else //создаем Person
                {
                    using(var db = new DbWorker())
                    {
                        sql = @"insert into dan_tab(fam,nam,mid,date_born,pens,operator_id)
                                    values(@Fam, @Nam, @Mid, @BirthDate, @Pens,@OperatorId) returning dan_id
                                   ";
                        var result = db.Connection.ExecuteScalar<long>(sql, new { 
                            Fam=LastName, Nam=FirstName, Mid=MidName,
                            BirthDate = BirthDate,
                            Pens = Snils,
                            OperatorId = operatorId
                        });
                        personId = result;
                        PatientId = personId.Value;
                    }
                }
                
                using(var db= new DbWorker())
                {
                    parameters.Add("PersonId", personId);
                    sql = "select gen_tab_id('public.doctor_tab')";
                    var id = db.Connection.ExecuteScalar<long>(sql, parameters);
                    Id = id;
                    InnerCode = (int?)id;

                    
                    if (LpuId==default(long))
                    {
                        sql = "select get_lpu_id()";
                        var idLpu = db.Connection.ExecuteScalar<long>(sql);
                        parameters.Add("IdLPU", idLpu);
                    }
                    else
                    {
                        parameters.Add("IdLPU", LpuId);
                    }
                    
                    sql = @"insert into doctor_tab(id,date_insert_doctor,            
                            date_attestat_doctor,federal_code,sp_dolg_doctor_id,
                            sp_spec_doctor_id,stavka,norma,sp_otdel_id,
                            inner_doctor_code,show_in_reg,description,
                            expiration_date,uet,payment,
                            nurse_id,uchastok_id,sim_pat_count,
                            can_have_schedule,max_records_per_day,dan_id,employee_type,operator_id,sp_lpu_id) 
                            values(@Id,@StartDate,@CertDate,@FedCode,@PositionId,@SpecialityId,@Stavka,
                            @Norma,@DivisionId,@InnerCode,@ShowInReg,@Description,@EndDate,@UET,@Payment,@Nurseid,
                            @UchastokId,@SameTimeCount,@CanHaveSch,@MaxCount,@PersonId,@EmpType,@OperatorId,@IdLPU)";
                    parameters.Add("Id",id);
                    parameters.Add("InnerCode", InnerCode);
                    var result = db.Connection.Execute(sql, parameters);
                }
            }
        }

        public bool CanSave(out string errorMsg)
        {
            if (string.IsNullOrEmpty(LastName))
            {
                errorMsg = "Нет фамилии";
                return false;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                errorMsg = "Нет имени";
                return false;
            }

            if (BirthDate==default(DateTime))
            {
                errorMsg = "Нет даты рождения";
                return false;
            }

            if (Gender==Classes.Gender.Unknown)
            {
                errorMsg = "Не указан пол";
                return false;
            }

            if(SpecialityId==0 || SpecialityId==-1)
            {
                errorMsg = "Не указана специальность";
                return false;
            }

            if (!Stavka.HasValue)
            {
                errorMsg = "Не указана ставка";
                return false;
            }

            if (!InnerCode.HasValue)
            {
                errorMsg = "Не указан внутренний код";
                return false;
            }

            if (!EndDate.HasValue)
            {
                errorMsg = "Не указана дата окончания";
                return false;
            }
            
            errorMsg = string.Empty;
            return true;
        }

        public long GetLPUId()
        {
            using(var db = new DbWorker())
            {
                var result = db.Connection.Query<long>("select sp_lpu_id from doctor_tab where id=@Id", new { Id = Id }).FirstOrDefault();
                return result;
            }
        }

        public void SetExpirationDate(DateTime? expDate)
        {
            using(var db = new DbWorker())
            {
                var result = db.Connection.Execute("update doctor_tab set expiration_date=@ExpDate where id=@Id",
                                new { ExpDate  = expDate, Id=Id}
                    );

            }
        }
    }

    public enum EmployeeType
    {
        Doctor=0,
        Nurse=1
    }
}
