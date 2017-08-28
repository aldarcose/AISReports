using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedDbWorker;
using Dapper;
using NLog;

namespace Model.Classes.Registry
{
    
    public class DocSpeciality
    {
        public long? Id {get;set;}
        public string Name {get;set;} 
    }

    public class AppointmentInfo
    {
        public DateTime? TalonTime { get; set; }
        public long? TalonId { get; set; }
        public long? PatientId { get; set; }
        public string PatientFIO { get; set; }
        public string Address { get; set; }
        public string ActAddress { get; set; }
        public string Telephone { get; set; }
        public string CardCode { get; set; }
        public long? DoctorId { get; set; }
        public string DoctorFIO { get; set; }
        public string Cabinet { get; set; }
        public string DocCommnet { get; set; }
        public long? OperatorId { get; set; }
        public string OperatorFIO { get; set; }
        public string Program { get; set; }
        public DateTime? EditDate { get; set; }
        public long? OperatorIdPrinted { get; set; }
        public string OperatorFIOPrinted { get; set; }
        public string ProgramPrinted { get; set; }
        public DateTime? EditDatePrinted { get; set; }
        public long? OperatorIdMarked { get; set; }
        public string OperatorFIOMarked { get; set; }
        public string ProgramMarked { get; set; }
        public DateTime? EditDateMarked { get; set; }
    

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Пациент: " + PatientFIO);
            sb.AppendLine("Амб.карта: " + CardCode);
            sb.AppendLine("Телефон: "+ Telephone);
            sb.AppendLine("Адрес: "+Address);
            sb.AppendLine("Факт.адрес: " + ActAddress);
            sb.AppendLine("Врач: "+DoctorFIO);
            sb.AppendLine("Кабинет: " + Cabinet);
            sb.AppendLine("Доп.инф. " + DocCommnet);
            sb.AppendLine("Оператор: " + OperatorFIO);
            sb.AppendLine("Программа: " + Program);
            sb.AppendLine("Распечатал: " + OperatorFIOPrinted+ " "+EditDatePrinted);
            sb.AppendLine("Программа: " + ProgramPrinted);

            return sb.ToString();
        }

        public string ToShortString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Пациент: " + PatientFIO + " Телефон: " + Telephone);
            sb.AppendLine("Адрес: " + Address + "Факт.адрес: " + ActAddress);
            sb.AppendLine("Врач: " + DoctorFIO);
            sb.AppendLine("Оператор: " + OperatorFIO + " Программа: " + Program);
            return sb.ToString();
        }
    }


    public class AppointmentHistoryItem
    {
        public DateTime? AppointmentDateTime { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public long? Id { get; set; }
        public string Action { get; set; }
        public long? PatientId { get; set; }
        public Patient Patient { get; set; }
        public long? DoctorId { get; set; }
        public Doctor Doctor { get;set; }
        public string Cabinet { get; set; }
        public string Program { get; set; }
        public string Operator { get; set; }
        public long? OperatorId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public long? TalonId { get; set; }
    }

    public class AppointmentSearchCriterion
    {
        public DateTime? ChangeTimeStart { get; set; }
        public DateTime? ChangeTimeEnd { get; set; }
        public DateTime? VisitTimeStart { get; set; }
        public DateTime? VisitTimeEnd { get; set; }
        public long? DoctorId { get;set; }
        public long? PatientId { get; set; }
    }

    public class Appointment
    {
        public DateTime? TalonTime { get; set; }
        public long? PatientId { get; set; }
        public long? DoctorId { get; set; }
        public long? TalonType { get; set; }
        public string TalonTypeColor { get; set; }
        public string TalonTypeName { get; set; }
        public int ReservedCount { get; set; }
        public long? CabinetId { get;set;}
        public Cabinet Cabinet { get; set; }
        public string CabinetName { get; set; }
        public string Text { get; set; }
        public bool HasTime { 
            get {
                if (!TalonTime.HasValue) 
                    return false;
                if (TalonTime.HasValue && TalonTime.Value.TimeOfDay.TotalSeconds == 0)
                    return false;
                if (TalonTime.HasValue && TalonTime.Value.TimeOfDay.TotalSeconds != 0)
                    return true;
                return false;
            } 
        }

        public Appointment Copy()
        {
            var appt = new Appointment();
            appt.TalonTime=this.TalonTime;
            appt.PatientId=this.PatientId;
            appt.DoctorId=this.DoctorId;
            appt.TalonType=this.TalonType;
            appt.TalonTypeColor=this.TalonTypeColor;
            appt.TalonTypeName=this.TalonTypeName;
            appt.ReservedCount=this.ReservedCount;
            appt.CabinetId=this.CabinetId;
            if (Cabinet!=null)
            {
                appt.Cabinet.Id= this.Cabinet.Id;
                appt.Cabinet.Name = this.Cabinet.Name;
                appt.Cabinet.Number = this.Cabinet.Number;
            }
            appt.CabinetName = this.CabinetName;
            return appt;
        }

    }

    public class VidTalon
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        //public int[] RGB { }

    }

    public class SetAppointmentResult
    {
        //f.talon_id,f.out_flag,f.error_msg,f.talon_doctor_id
        public long? TalonId { get; set; }
        public string OutFlag { get; set; }
        public string ErrMesg { get; set; }
        public long? TalonDoctorId { get; set; }
    }

    public class Building
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }

    public class PaymentType
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }
    
    public class LocalRegistry
    {

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        public IEnumerable<DateTime?> GetWeekDays(DateTime? date)
        {
            if (!date.HasValue)
                yield return null;
            var delta = DayOfWeek.Monday - date.Value.DayOfWeek;
            var monday = date.Value.AddDays(delta);
            var wdate = monday;
            yield return wdate;
            for (int i=0;i<6;i++)
            {
                wdate = wdate.AddDays(1);
                yield return wdate;
                
            }
        }

        public IEnumerable<Building> GetBuildings()
        {
            var buildings = new List<Building>();
            buildings.Add(new Building() { Id = 0, Name = "Все" });
            using(var db = new DbWorker())
            {
                var sql = "select id,name from codifiers.lpu_building_tab";
                var result = db.Connection.Query<Building>(sql);
                logger.Debug("Запрос: {0}",sql);
                buildings.AddRange(result);
                return buildings;
            }
        }

        public IEnumerable<Cabinet> GetCabinets(long buildingId)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select id,name,number,lpu_building_id buildingid 
                            from codifiers.cabinet_tab
                            where lpu_building_id=@BuildingId";
                var result = db.Connection.Query<Cabinet>(sql, new { BuildingId=buildingId });
                logger.Debug("Запрос: {0}", sql);
                return result;
            }
        }

        public IEnumerable<PaymentType> GetPaymentTypes()
        {
            using(var db = new DbWorker())
            {
                var sql = "select id, name from codifiers.sp_kind_paid_tab order by 1";
                var result = db.Connection.Query<PaymentType>(sql);
                logger.Debug("Запрос: {0}", sql);
                return result;
            }
        }

        public SetAppointmentResult SetAppointment(long? doctorId, long? patientId, DateTime dateTime, bool withTime=true, long userId=0, long talonId=0)
        {

            using(var db = new DbWorker())
            {
                var qCreateTalonParams = new DynamicParameters();
                
                var sql = @"SELECT f.talon_id talonid,f.out_flag outflag,
                            f.error_msg errormsg,f.talon_doctor_id talondoctorid 
                            FROM queue_save_data_2(@p1,@p2,@p3,@p4,@p5,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12) as f(talon_id, out_flag, error_msg, talon_doctor_id)";
                qCreateTalonParams.Add("@p1", 1); // тип программы = 1 (арм врача) см. ф. queue_privileges_sql
                qCreateTalonParams.Add("@p2", doctorId.Value); // идентификатор доктора
                qCreateTalonParams.Add("@p3", null); // кабинет
                qCreateTalonParams.Add("@p4", dateTime.Date, System.Data.DbType.Date); // дата записи
                if (withTime)
                    qCreateTalonParams.Add("@p5", dateTime.TimeOfDay.ToString(), System.Data.DbType.String); // время записи
                else
                    qCreateTalonParams.Add("@p5", null);
                qCreateTalonParams.Add("@p6", 0); // идентификатор типа оплаты = 0 (ОМС)
                qCreateTalonParams.Add("@p7", patientId.Value); // идентификатор пациента
                qCreateTalonParams.Add("@p8", 0); // идентификатор места обслуживания = 0 (Поликлиника)
                qCreateTalonParams.Add("@p9", "ARM Plugin"); // имя рабочей программы
                if (talonId==0)
                {
                    qCreateTalonParams.Add("@p10", true); // флаг: создавать ли талон
                    qCreateTalonParams.Add("@p12", null); // идентификатор талона
                }else
                {
                    qCreateTalonParams.Add("@p10", false); // флаг: создавать ли талон
                    qCreateTalonParams.Add("@p12", talonId); // идентификатор талона
                }
                //qCreateTalonParams.Add("@p10", true); // флаг: создавать ли талон
                qCreateTalonParams.Add("@p11", doctorId.Value); // идентификатор врача
                //qCreateTalonParams.Add("@p12", null); // идентификатор талона

                var result = db.Connection.Query<SetAppointmentResult>(sql, qCreateTalonParams).FirstOrDefault();
                logger.Debug("Запрос: {0}", sql);
                
                if (result!=null && result.TalonId.HasValue && userId!=0)
                {
                    var userInfoSql = @"update talon_doctor_tab set operator_id=@UserId where talon_id=@TalonId";
                    var userInfoResult = db.Connection.Execute(userInfoSql, new { TalonId=result.TalonId, UserId=userId });
                }

                return result;
            }
        }

        public int CancelAppointment(DateTime dateTime, long doctorId, long patientId)
        {
            using (var db = new DbWorker())
            {

                var param = new DynamicParameters();
                string sql =
                    "Delete from public.talon_doctor_tab where doctor_id = @doc_id and date_nazn = @date and time_nazn = @time;";
                param.Add("@doc_id", doctorId);
                param.Add("@date", dateTime.Date);
                param.Add("@time", dateTime.TimeOfDay.ToString());

                var result = db.Connection.Execute(sql, param);
                logger.Debug("Запрос: {0}", sql);
                return result;
            }
        }

        public IEnumerable<AppointmentHistoryItem> GetHistory(DateTime talonTime)
        {
            using(var db = new DbWorker())
            {
                //var sql = @"select * from public.";
                //var result = db.Connection.Query<AppointmentHistoryItem>();

            }
            return null;
        }

        public IEnumerable<VidTalon> GetVidTalon()
        {
            using(var db = new DbWorker())
            {
                var sql = @"select id,name,color from codifiers.vid_talon_tab order by id";
                var result = db.Connection.Query<VidTalon>(sql);
                logger.Debug("Запрос: {0} ", sql);
                return result;
            }
        }
        
        public  IEnumerable<AppointmentInfo> GetReservedAppointment(long? docId, DateTime?  apptTime)
        {
            if (!apptTime.HasValue) return null;
            

            using(var db = new DbWorker())
            {
                if (apptTime.Value.TimeOfDay.TotalSeconds!=0)
                {
                    var sql = @"select td.date_nazn+td.time_nazn talontime,
                            patdan.dan_id patientid,
                            coalesce(patdan.fam,'') || ' ' || coalesce(patdan.nam,'') || ' ' || coalesce(patdan.mid,'') patientfio,
                            patdan.address,
                            patdan.fakt_adr actaddress,
                            patdan.telephon,
                            patdan.med_karta_kod cardcode,
                            doc.id doctorid,
                            docdan.fam || ' ' || docdan.nam || ' ' || docdan.mid doctorfio,
                            td.cabinet_nam cabinet,
                            doc.doc_comment doccomment,
                            td.operator_id operatorid,
                            userdan.fam || ' ' || userdan.nam || ' ' || userdan.mid operatorfio,
                            td.programm_name ""program"",
                            td.date_vnes_inf editdate,
                            td.talon_operator_id operatoridprinted,
                            puserdan.fam || ' ' || puserdan.nam || ' ' || puserdan.mid operatorfioprinted,
                            td.talon_date_vnes_inf editdateprinted,
                            td.talon_programm_name programprinted,
                            td.mark_operator_id operatoridmarked,
                            muserdan.fam || ' ' || muserdan.nam || ' ' || muserdan.mid operatorfiomarked,
                            td.mark_date_vnes_inf editdatemarked,
                            td.mark_programm_name programmarked
                            from talon_doctor_tab td
                            left join doctor_tab doc on td.doctor_id=doc.id
                            left join dan_tab docdan on doc.dan_id = docdan.dan_id  
                            left join dan_tab patdan on td.dan_id = patdan.dan_id
                            left join users_tab users on td.operator_id = users.id
                            left join dan_tab userdan on users.dan_id = userdan.dan_id
                            left join users_tab pusers on td.talon_operator_id = pusers.id
                            left join dan_tab puserdan on pusers.dan_id = puserdan.dan_id
                            left join users_tab musers on td.mark_operator_id = musers.id
                            left join dan_tab muserdan on musers.dan_id = muserdan.dan_id
                            where td.doctor_id=@DocId and td.date_nazn=@Date and td.time_nazn=@Time";
                    logger.Debug("Запрос:  {0}", sql);
                    var apptInfo = db.Connection.Query<AppointmentInfo>(sql, new { DocId = docId, Date = apptTime.Value.Date, Time = apptTime.Value.TimeOfDay });
                    return apptInfo;
                }
                if (apptTime.Value.TimeOfDay.TotalSeconds == 0) //запись без времени
                {
                    var sql = @"select td.date_nazn talontime,
                            patdan.dan_id patientid,
                            coalesce(patdan.fam,'') || ' ' || coalesce(patdan.nam,'') || ' ' || coalesce(patdan.mid,'') patientfio,
                            patdan.address,
                            patdan.fakt_adr actaddress,
                            patdan.telephon,
                            patdan.med_karta_kod cardcode,
                            doc.id doctorid,
                            docdan.fam || ' ' || docdan.nam || ' ' || docdan.mid doctorfio,
                            td.cabinet_nam cabinet,
                            doc.doc_comment doccomment,
                            td.operator_id operatorid,
                            userdan.fam || ' ' || userdan.nam || ' ' || userdan.mid operatorfio,
                            td.programm_name ""program"",
                            td.date_vnes_inf editdate,
                            td.talon_operator_id operatoridprinted,
                            puserdan.fam || ' ' || puserdan.nam || ' ' || puserdan.mid operatorfioprinted,
                            td.talon_date_vnes_inf editdateprinted,
                            td.talon_programm_name programprinted,
                            td.mark_operator_id operatoridmarked,
                            muserdan.fam || ' ' || muserdan.nam || ' ' || muserdan.mid operatorfiomarked,
                            td.mark_date_vnes_inf editdatemarked,
                            td.mark_programm_name programmarked
                            from talon_doctor_tab td
                            left join doctor_tab doc on td.doctor_id=doc.id
                            left join dan_tab docdan on doc.dan_id = docdan.dan_id  
                            left join dan_tab patdan on td.dan_id = patdan.dan_id
                            left join users_tab users on td.operator_id = users.id
                            left join dan_tab userdan on users.dan_id = userdan.dan_id
                            left join users_tab pusers on td.talon_operator_id = pusers.id
                            left join dan_tab puserdan on pusers.dan_id = puserdan.dan_id
                            left join users_tab musers on td.mark_operator_id = musers.id
                            left join dan_tab muserdan on musers.dan_id = muserdan.dan_id
                            where td.doctor_id=@DocId and td.date_nazn=@Date and td.time_nazn is null";
                    logger.Debug("Запрос:  {0}", sql);
                    var apptInfo = db.Connection.Query<AppointmentInfo>(sql, 
                        new { 
                            DocId = docId, 
                            Date = apptTime.Value.Date, 
                        });
                    return apptInfo;
                }
                return null;
            }
        }

        public IEnumerable<DocSpeciality> GetSpecialities(long lpuId=0)
        {
            using(var db = new DbWorker())
            {
                var query = "";
                IEnumerable < DocSpeciality > result = null;
                if (lpuId==0)
                {
                    query = @"select 
                                d.sp_spec_doctor_id as id, spec.name as name
                                from public.free_talon_tab ft 
                                inner join public.doctor_tab d on ft.doctor_id = d.doctor_id
                                inner join codifiers.doctor_spec_tab spec on d.sp_spec_doctor_id = spec.id
                                where d.show_in_reg=1 and spec.id!=900
                                group by d.sp_spec_doctor_id, spec.name
                                order by spec.name";
                    result = db.Connection.Query<DocSpeciality>(query);
                    logger.Debug("Запрос:  {0}", query);
                    return result;
                }
                //фильтр по LPU
                query = @"select 
                                d.sp_spec_doctor_id as id, spec.name as name
                                from public.free_talon_tab ft 
                                inner join public.doctor_tab d on ft.doctor_id = d.doctor_id
                                inner join codifiers.doctor_spec_tab spec on d.sp_spec_doctor_id = spec.id
                                where d.show_in_reg=1 and spec.id!=900 and d.sp_lpu_id=@LPUId
                                group by d.sp_spec_doctor_id, spec.name
                                
                                order by spec.name";
                result = db.Connection.Query<DocSpeciality>(query, new { LPUId = lpuId } );
                logger.Debug("Запрос:  {0}", query);
                return result;
                
            }
        }


        public IEnumerable<Doctor> GetDoctors(long? specId, DateTime date, long lpuId=0)
        {
            var query = "";
            IEnumerable<Doctor> result = null;
            using (var db = new DbWorker())
            {
                if (lpuId==0)
                {
                    query = @"SELECT doc.id id, d.fam lastname, d.nam firstname, d.mid midname, 
                            doc.inner_doctor_code innercode,
                            spec.name specialityname, spec.id specialityid
                            FROM public.dan_tab d 
                            left join doctor_tab doc on doc.dan_id = d.dan_id
                            inner join codifiers.doctor_spec_tab spec on doc.sp_spec_doctor_id = spec.id
                            where doc.sp_spec_doctor_id=@SpecId and doc.show_in_reg=1 and expiration_date>@Date
                            order by 2";
                    logger.Debug("Запрос:  {0}", query);
                    result = db.Connection.Query<Doctor>(query, new { SpecId = specId, Date = date });
                    return result;
                }
                //фильтр по ЛПУ
                query = @"SELECT doc.id id, d.fam lastname, d.nam firstname, d.mid midname, 
                            doc.inner_doctor_code innercode,
                            spec.name specialityname, spec.id specialityid
                            FROM public.dan_tab d 
                            left join doctor_tab doc on doc.dan_id = d.dan_id
                            inner join codifiers.doctor_spec_tab spec on doc.sp_spec_doctor_id = spec.id
                            where doc.sp_spec_doctor_id=@SpecId and doc.show_in_reg=1 and expiration_date>@Date
                                and doc.sp_lpu_id=@LPUId
                            order by 2";
                logger.Debug("Запрос:  {0}", query);
                result = db.Connection.Query<Doctor>(query, new { SpecId = specId, Date = date, LPUId=lpuId });
                return result;

            }
        }

        public IEnumerable<Doctor> GetDoctors(bool showLaidOff, DateTime date, long LPUId=0)
        {
            var parameters = new DynamicParameters();
            var filterList = new List<string>();
            using (var db = new DbWorker())
            {
                var query = @"select dan.fam lastname, dan.nam firstname, dan.mid midname, 
                            doc.norma, doc.stavka, doc.dan_id patientid,doc.id id,doc.operator_id operatorid,
                            otd.name divisionname,
                            spec.name specialityname, dolg.name positionname, 
                            users.login login
                            from doctor_tab doc
                            left join codifiers.otdel_tab otd on otd.id=doc.sp_otdel_id
                            left join codifiers.doctor_spec_tab spec on spec.id=doc.sp_spec_doctor_id
                            left join codifiers.doctor_dolg_tab dolg on dolg.id=doc.sp_dolg_doctor_id
                            left join dan_tab dan on doc.dan_id = dan.dan_id
                            left join users_tab users on users.dan_id = dan.dan_id
                            ";
                if (!showLaidOff)
                {
                    filterList.Add( "doc.expiration_date>@ExpDate ");
                    parameters.Add("ExpDate", date);
                    //logger.Debug("Запрос: ", query);
                    //var result = db.Connection.Query<Doctor>(query, new { Date = date });
                    //return result;
                }

                if (LPUId!=0)
                {
                    filterList.Add("doc.sp_lpu_id=@LPUId");
                    parameters.Add("LPUId", LPUId);
                }

                if (filterList.Count > 0)
                {
                    var filterString = " where " + string.Join("and", filterList);
                    query += filterString;
                }
                
                query += @" order by 2";
                logger.Debug("Запрос:  {0}", query);
                var result = db.Connection.Query<Doctor>(query, parameters);
                return result;
                

            }
        }


        /// <summary>
        /// Получает всех врачей
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Doctor> GetDoctors()
        {
            using (var db = new DbWorker())
            {
                var query = @"select dan.fam lastname, dan.nam firstname, dan.mid midname, 
                            doc.norma, doc.stavka, doc.dan_id patientid,doc.id id,doc.operator_id operatorid,
                            otd.name divisionname,
                            spec.name specialityname, dolg.name positionname, 
                            users.login login
                            from doctor_tab doc
                            left join codifiers.otdel_tab otd on otd.id=doc.sp_otdel_id
                            left join codifiers.doctor_spec_tab spec on spec.id=doc.sp_spec_doctor_id
                            left join codifiers.doctor_dolg_tab dolg on dolg.id=doc.sp_dolg_doctor_id
                            left join dan_tab dan on doc.dan_id = dan.dan_id
                            left join users_tab users on users.id = doc.users_id
                            ";
                query += @" order by 2";
                logger.Debug("Запрос:  {0}", query);
                var result = db.Connection.Query<Doctor>(query);
                return result;
            }
        }

        public Doctor GetDoctor(long id)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select dan.fam lastname, dan.nam firstname, dan.mid midname,
                            dan.date_born birthdate, 
                            case when dan.sex='м' then 1 else 2 end gender,
                            doc.id id,
                            doc.dan_id patientid,
                            doc.date_insert_doctor startdate,
                            doc.date_attestat_doctor certificationdate,
                            doc.federal_code fedcode,
                            doc.sp_dolg_doctor_id  positionid,
                            doc.sp_spec_doctor_id specialityid,
                            doc.stavka,
                            doc.norma,
                            doc.users_id login,
                            doc.sp_otdel_id divisionid,
                            doc.inner_doctor_code innercode,
                            doc.show_in_reg showinreg,
                            doc.description,
                            doc.expiration_date enddate,
                            doc.uet,
                            doc.payment paymentpercent,
                            doc.employee_type emptype,
                            doc.nurse_id nurseid,
                            doc.uchastok_id medareid,
                            doc.sim_pat_count sametimepatientcount,
                            doc.can_have_schedule canhaveschedule,
                            doc.max_records_per_day maxrecords
                            from doctor_tab doc
                            left join dan_tab dan on doc.dan_id = dan.dan_id 
                            where doc.id=@Id
                            ";
                logger.Debug("Запрос:  {0}", sql);
                var result = db.Connection.Query<Doctor>(sql, new { Id=id }).FirstOrDefault();
                return result;
                    
            }
        }

        public static IEnumerable<Doctor> GetNurses(DateTime date)
        {
            using (var db = new DbWorker())
            {
                var query = @"select dan.fam lastname, dan.nam firstname, dan.mid midname, 
                            doc.norma, doc.stavka, doc.dan_id patientid,doc.id id,doc.operator_id operatorid,
                            otd.name divisionname,
                            spec.name specialityname, dolg.name positionname, 
                            users.login login
                            from doctor_tab doc
                            left join codifiers.otdel_tab otd on otd.id=doc.sp_otdel_id
                            left join codifiers.doctor_spec_tab spec on spec.id=doc.sp_spec_doctor_id
                            left join codifiers.doctor_dolg_tab dolg on dolg.id=doc.sp_dolg_doctor_id
                            left join dan_tab dan on doc.dan_id = dan.dan_id
                            left join users_tab users on users.id = doc.users_id
                            where employee_type=1 and doc.expiration_date>@Date
                            order by 2                            
                            ";
                logger.Debug("Запрос:  {0}", query);
                var result = db.Connection.Query<Doctor>(query, new { Date = date });
                return result;
            }
        }


        //public IEnumerable<>

        public IEnumerable<Appointment> GetAppointments(long? docId, DateTime date)
        {
            using(var db = new DbWorker())
            {
                var query = @"select date_talon+time_talon talontime, 
                            ft.doctor_id doctorid, 
                            case when ft.reserve_patient_count>0 then 7 else ft.vid_talon_id end talontype, 
                            ft.cabinet_nam cabinetname,
                            ft.cabinet_id cabinetid,
                            vt.name talontypename,vt.color talontypecolor, 
                            ft.reserve_patient_count reservedcount ,
                            tdt.dan_id patientid
                            from free_talon_tab ft
                            left join codifiers.vid_talon_tab vt on (case when ft.reserve_patient_count>0 then 7 else ft.vid_talon_id end)=vt.id
                            left join talon_doctor_tab tdt on tdt.doctor_id=ft.doctor_id and tdt.date_nazn=ft.date_talon and tdt.time_nazn=ft.time_talon
                            where ft.doctor_id=@DocId and date_talon=@Date
                            order by date_talon,time_talon";
                logger.Debug("Запрос:  {0}", query);
                var result = db.Connection.Query<Appointment>(query, new { DocId = docId, Date=date.Date });
                return result;
            }
        }

        public IEnumerable<Appointment> GetAppointmentsWithoutTime(long? docId, DateTime date)
        {
            using (var db = new DbWorker())
            {
                var query = @"select td.date_nazn talontime, 
                                td.doctor_id doctorid, 
                                td.cabinet_nam cabinetname,
                                td.dan_id patientid
                                from talon_doctor_tab td
                                where td.doctor_id=@DocId 
                                and td.date_nazn=@Date 
                                and td.time_nazn is null";
                logger.Debug("Запрос:  {0}", query);
                var result = db.Connection.Query<Appointment>(query, new { DocId = docId, Date = date.Date });
                return result;
            }
        }

        public IEnumerable<AppointmentHistoryItem> SearchAppointmentHistoryItems(AppointmentSearchCriterion criterion) 
        {
            using(var db = new DbWorker())
            {
                var sql = @" select * from(
                             select
                             'Создан' as action,  
                             talon_doctor_id id, 
                             doctor_id doctorid, 
                             date_nazn appointmentdate, 
                             time_nazn appointmenttime,
                             operator_id operatorid,
                             date_vnes_inf createdatetime,
                             dan_id patientid,
                             programm_name ""program"", 
                             talon_id talonid
                             from talon_doctor_tab 
                             union all
                             select 
                             'Удален' as action,
                             (t.value).talon_doctor_id id,                             
                             (t.value).doctor_id doctorid,
                             (t.value).date_nazn,
                             (t.value).time_nazn,
                             (t.value).operator_id,
                             date_time_mod createdatetime,                             
                             (t.value).dan_id patientid,
                             (t.value).programm_name,
                             (t.value).talon_id
                             from logs.talon_doctor_tab t
                             where flag_mod=1
                            ) sub
                            ";
                var where = new List<string>();
                var parameters = new DynamicParameters();
                if (criterion.ChangeTimeEnd.HasValue && criterion.ChangeTimeStart.HasValue)
                {
                    where.Add("createdatetime between @ChangeStartDate and @ChangeEndDate");
                    parameters.Add("ChangeStartDate", criterion.ChangeTimeStart);
                    parameters.Add("ChangeEndDate", criterion.ChangeTimeEnd);
                }
                if (criterion.VisitTimeStart.HasValue && criterion.VisitTimeEnd.HasValue)
                {
                    where.Add("appointmentdate between @VisitStartDate and @VisitEndDate");
                    parameters.Add("VisitStartDate", criterion.VisitTimeStart);
                    parameters.Add("VisitEndDate", criterion.VisitTimeEnd);
                }
                if (criterion.DoctorId.HasValue)
                {
                    where.Add("doctorid=@DoctorId");
                    parameters.Add("DoctorId", criterion.DoctorId);
                }

                if (criterion.PatientId.HasValue)
                {
                    where.Add("patientid=@PatientId");
                    parameters.Add("PatientId", criterion.PatientId);
                }
                
                if (where.Count > 1)
                    sql += " where " + string.Join(" and ", where);
                else if (where.Count==1)
                    sql = sql + " where " + where.First();

                sql = sql + " " + "order by createdatetime desc";
                var result = db.Connection.Query<AppointmentHistoryItem>(sql, parameters);
                foreach(var appt in result)
                {
                    appt.Patient = new Patient(appt.PatientId.Value);
                    var doc = new Doctor();
                    doc.LoadData(appt.DoctorId.Value);
                    appt.Doctor = doc;
                    if (appt.OperatorId.HasValue)
                    {
                        var oper = new Operator((int)appt.OperatorId.Value);
                        appt.Operator = oper.ToString();
                    }
                }
                return result;
            }
        }

        public AppointmentHistoryItem GetAppointmentHistoryItem(long id)
        {
            using(var db = new DbWorker())
            {
                var sql = @"Select talon_doctor_id id,
                              date_nazn+time_nazn appointmentdatetime,
                              cabinet_nam cabinet,
                              date_vnes_inf createdatetime,
                              doctor_id doctorid,
                              operator_id operatorid,
                              dan_id patientid,
                              talon_id talonid
                              from talon_doctor_tab
                              where talon_doctor_id=@Id";
                logger.Debug("Запрос:  {0}", sql);
                var result = db.Connection.Query<AppointmentHistoryItem>(sql, new { Id = id }).FirstOrDefault();
                return result;

            }
        }

        public AppointmentHistoryItem GetAppointmentHistoryItem(long doctorId, long danId, DateTime apptDateTime)
        {
            using (var db = new DbWorker())
            {
                var sql = @"Select talon_doctor_id id,
                              date_nazn+time_nazn appointmentdatetime,
                              cabinet_nam cabinet,
                              date_vnes_inf createdatetime,
                              doctor_id doctorid,
                              operator_id operatorid,
                              dan_id patientid,
                              talon_id talonid
                              from talon_doctor_tab
                              where doctor_id=@DoctorId and dan_id=@DanId and date_nazn=@Date and time_nazn=@Time";
                logger.Debug("Запрос:  {0}", sql);
                var result = db.Connection.Query<AppointmentHistoryItem>(sql,
                                            new { DoctorId = doctorId, DanId = danId, Date=apptDateTime.Date, Time=apptDateTime.TimeOfDay }
                                            ).FirstOrDefault();
                return result;

            }
        }

        public IEnumerable<Appointment> CreateAppointments(DateTime fromTime, DateTime toTime, DateTime norma, long doctorId)
        {
            var normSpan = norma-DateTime.MinValue;
            DateTime curTime = fromTime;
            var result = new List<Appointment>();
            
            while(curTime<toTime)
            {
                var appt = new Appointment(){ DoctorId=doctorId,  TalonTime = curTime, TalonType = 0 }; //fromTime
                curTime=curTime.Add(normSpan);
                if (!IsExistsAppointment(appt))
                    result.Add(appt);
            }
            return result;
        }

        public void DeleteAppointment(DateTime dateTime, long doctorId, bool deleteAllDay=true)
        {
            using(var db = new DbWorker())
            {
                if (deleteAllDay)
                {
                    //удаляем талоны на дату
                    var sql = @"delete from free_talon_tab where date_talon=@Date and doctor_id=@DoctorId";
                    logger.Debug("Запрос:  {0}", sql);
                    var result = db.Connection.Execute(sql, new { Date=dateTime.Date, DoctorId=doctorId });
                }else
                {
                    var sql = @"delete from free_talon_tab where date_talon=@Date and doctor_id=@DoctorId and time_talon=@Time";
                    logger.Debug("Запрос:  {0}", sql);
                    var result = db.Connection.Execute(sql, new { Date = dateTime.Date, DoctorId = doctorId, Time=dateTime.TimeOfDay });
                }
            }
        }

        
        public IEnumerable<Appointment> CopyAppointments(IEnumerable<Appointment> weekAppts, int weekCount, int weekAfter)
        {
            //var weekSchedule = new Dictionary<int, IEnumerable<Appointment>>();
            var result = new List<Appointment>();
            for (int i=0; i<7;i++)
            {
                var appts = weekAppts.Where(c => c.TalonTime.HasValue && (int)c.TalonTime.Value.DayOfWeek==i);
                if (appts == null || !appts.Any())
                    continue;
                foreach(var appt in appts)
                {
                    var newAppt = appt.Copy();
                    for (int wk = 1; wk < weekCount; wk++)
                    {
                        var date = newAppt.TalonTime.Value.Date.AddDays((weekAfter + 1) * 7);
                        date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                        newAppt = newAppt.Copy();
                        newAppt.TalonTime = date.AddHours(appt.TalonTime.Value.Hour).AddMinutes(appt.TalonTime.Value.Minute);
                        result.Add(newAppt);
                    }
                }
            }
            return result;
        }

        public bool IsExistsAppointment(Appointment appt)
        {
            using(var db =new DbWorker())
            {
                var sql = @"select exists(
                    select 1  
                    from free_talon_tab
                    where doctor_id=@DocId and time_talon=@TalonTime and date_talon=@DateTalon
                    )";
                logger.Debug("Запрос:  {0}", sql);
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(new { DocId = appt.DoctorId, TalonTime = appt.TalonTime.Value.TimeOfDay, DateTalon = appt.TalonTime.Value.Date });
                var isExist = db.Connection.Query<bool>(sql, parameters).First();
                return isExist;
            }
        }

        public bool SaveAppointment(Appointment appt)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select exists(
                    select 1  
                    from free_talon_tab
                    where doctor_id=@DocId and time_talon=@TalonTime and date_talon=@DateTalon
                    )";
                var parameters = new DynamicParameters();
                parameters.AddDynamicParams(new { DocId = appt.DoctorId, TalonTime = appt.TalonTime.Value.TimeOfDay, DateTalon = appt.TalonTime.Value.Date });
                logger.Debug("Запрос:  {0}", sql);
                var isExist = db.Connection.Query<bool>(sql,parameters).First();
                if (isExist)
                {
                    sql = @"update free_talon_tab 
                            set vid_talon_id=@TalonType,
                            cabinet_id=@CabinetId,
                            cabinet_nam=@CabinetNam
                            where doctor_id=@DocId and time_talon=@TalonTime and date_talon=@DateTalon";

                    parameters.AddDynamicParams(new { TalonType=appt.TalonType, CabinetId= appt.CabinetId,CabinetNam=appt.CabinetName});
                    logger.Debug("Запрос:  {0}", sql);
                    var result = db.Connection.Execute(sql,parameters);
                    return result == 1;
                }else
                {
                    sql = @"insert into free_talon_tab(vid_talon_id,cabinet_id,cabinet_nam,doctor_id,time_talon,date_talon) 
                            values(@TalonType,@CabinetId,@CabinetNam, @DocId, @TalonTime, @DateTalon)";
                    parameters.AddDynamicParams(new { TalonType = appt.TalonType, CabinetId = appt.CabinetId, CabinetNam = appt.CabinetName });
                    logger.Debug("Запрос:  {0}", sql);
                    var result = db.Connection.Execute(sql,parameters);
                    return result == 1;
                }
            }
        }

        public Cabinet GetCabinet(long id)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select id,name,number,lpu_building_id buildingid 
                           from codifiers.cabinet_tab
                           where id=@Id";
                logger.Debug("Запрос:  {0}", sql);
                var result = db.Connection.Query<Cabinet>(sql, new { Id=id }).FirstOrDefault();
                return result;
            }
        }

    
    }

    
}
