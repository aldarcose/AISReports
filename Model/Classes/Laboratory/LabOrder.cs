using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Model.Classes.Codifiers;
using SharedDbWorker;
using SharedDbWorker.Classes;
using NLog;
using Dapper;

namespace Model.Classes.Laboratory
{
    public class LabOrder
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Browsable(false)]
        public long Id { get; set; }

        public List<Exam> Exams { get; set; }

        public string MKBCode { get; set; }
        
        [Browsable(false)]
        public Patient Patient { get; set; }

        [Browsable(false)]
        public long? PatientId { get; set; }
        
        
        private string _patientData;

        [DisplayName("Пациент")]
        public string PatientData
        {
            get
            {
                if (Patient != null)
                {
                    if (string.IsNullOrEmpty(_patientData))
                    {
                        var fio = string.Format("{0}{1}{2}",
                        string.IsNullOrEmpty(Patient.LastName) ? "" : Patient.LastName + " ",
                        string.IsNullOrEmpty(Patient.FirstName) ? "" : Patient.FirstName + " ",
                        string.IsNullOrEmpty(Patient.MidName) ? "" : Patient.MidName);
                        _patientData = string.Format("{0}, {1}", fio, Patient.BirthDate.ToString("dd.MM.yyyy"));
                        return _patientData;
                    }
                    else
                    {
                        return _patientData;
                    }
                    
                }
                return null;
            }
            set 
            {
                _patientData = value;
            }
        }
        
        [DisplayName("Дата создания")]
        public DateTime CreateTime { get; set; }

        [DisplayName("Дата ответа")]
        public DateTime ResponseTime { get; set; }

        [DisplayName("Статус")]
        public LabRecordStatus Status { get; set; }

        [Browsable(false)]
        public long? StatusId { get; set; }

        [Browsable(false)]
        public Doctor Doctor { get; set; }

        [Browsable(false)]
        public long? DoctorId { get; set; }

        [Browsable(false)]
        public string DoctorDivision
        {
            get
            {
                if (Doctor != null)
                {
                    return Doctor.GetDivisionName();
                }
                else
                {
                    return "Не указан";
                }
            }
        }

        [DisplayName("Мед. работник")]
        public string DoctorFIO
        {
            get
            {
                if (Doctor != null)
                {
                    return Doctor.ToString();
                }
                else
                {
                    return "Не указан";
                }

            }
        }

        [Browsable(false)]
        public long? TalonId { get; set; }

        [DisplayName("Случай")]
        public string TalonData
        {
            get
            {
                if (TalonId != null)
                {
                    return TalonId.ToString();
                }
                else
                {
                    return "Не указан";
                }

            }
        }

        [DisplayName("Военкомат")]
        public bool? Voenkomat{get; set;}

        [DisplayName("Триместр")]
        public string Trimestr { get; set; }


        [Browsable(false)]
        public bool HasResults
        {
            get
            {
                var hasResultsStatus = 2;
                if (Status != null)
                    return Status.Id == hasResultsStatus;
                return false;
            }
        }

        [DisplayName("ЛПУ")]
        public string SendedLpuCode
        {
            get;
            set;
        }

        public void Save()
        {
            using (var db = new DbWorker())
            {

                var q= @"INSERT INTO laboratory.""order"" (dan_id, doctor_id, talon_id,mkbcode,voenkomat, trimestr, sended_lpu) 
                            VALUES (@dan_id, @doctor_id, @talon_id, @MkbCode,@Voenkomat,@Trimestr, @SendedLpu) returning id;
                        ";
                long? id = db.Connection.ExecuteScalar<long?>(q,
                    new
                    {
                        dan_id = Patient.PatientId,
                        doctor_id = Doctor == null ? null : (long?)Doctor.Id,
                        @talon_id = TalonId == null ? null : (long?)TalonId,
                        MkbCode = MKBCode,
                        Voenkomat = Voenkomat,
                        Trimestr = Trimestr,
                        SendedLpu=SendedLpuCode
                    });
                
                
                //var q = new DbQuery("InsertData");
                //q.Sql = "INSERT INTO laboratory.\"order\" (dan_id, doctor_id, talon_id) VALUES (@dan_id, @doctor_id, @talon_id) returning id;";
                //q.AddParamWithValue("@dan_id", Patient.PatientId);
                //q.AddParamWithValue("@doctor_id", Doctor == null ? null : (long?)Doctor.Id);
                //q.AddParamWithValue("@talon_id", TalonId == null ? null : (long?)TalonId);

                logger.Debug("SQL: {0}", q);
                logger.Debug("Params dan_id={0} doctor_id={1} talon_id={2}", Patient.PatientId, Doctor == null ? null : (long?)Doctor.Id, TalonId == null ? null : (long?)TalonId);

                //var id = db.GetScalarResult(q);
                //if (id != null)
                //{
                //    this.Id = DbResult.GetNumeric(id, -1);
                //}
                

                logger.Debug("Result id={0}", id);
                
                //var sql = string.Format("select id,exam_id examid,code,name,measure from laboratory.sp_exam_param p where exam_id in ({0})", string.Join(",", Exams.Select(e=>e.Id)));
                //var parameters = db.Connection.Query<ExamParameter>(sql);

                //foreach(var exam in Exams)
                //{
                //    exam.Parameters = parameters.Where(p => p.ExamId == exam.Id).ToList();
                //}

                if (id.HasValue)
                {
                    Id = id.Value;
                    foreach (var exam in Exams)
                    {
                        //foreach(var param in exam.Parameters)
                        //{
                            var qq = new DbQuery("InsertExamData");
                            qq.Sql =
                                @"INSERT INTO laboratory.order_exam_data(order_id, exam_id) 
                              VALUES (@order_id, @exam_id);";
                            qq.AddParamWithValue("@order_id", Id);
                            qq.AddParamWithValue("@exam_id", exam.Id);
                            //qq.AddParamWithValue("@exam_parameter_id", param.Id);

                            logger.Debug("SQL: {0}", qq.Sql);
                            logger.Debug("Params order_id={0} exam_id={1} ", Id, exam.Id);

                            var res = db.Execute(qq);

                            logger.Debug("result={0}", res);
                        //}
                        
                    }
                    //отправка уведомлений в БД по каналу
                    //var sqlNotify = "select pg_notify('laboratory_tab_notify_lis','insert order:@Id')";
                    //db.Connection.Execute(sqlNotify, new { Id=this.Id });
                    //logger.Debug("SQL= {0}, Id={1}", sqlNotify, this.Id );
                    //PERFORM pg_notify('laboratory_tab_notify_lis', 'insert order:' || CAST(NEW.id AS text));
                }
            }

        }

        public void SetStatus(int status)
        {
            using (var db = new DbWorker())
            {
                var qqq = new DbQuery("UpdateStatus");
                qqq.Sql = "UPDATE laboratory.\"order\" set status_id = @status where id = @id;";
                qqq.AddParamWithValue("@id", Id);
                qqq.AddParamWithValue("@status", status); // меняем с 0 (создание) на 1 (в очередь). другая программа отправляет данные при изменении этого поля
                db.Execute(qqq);
            }
        }
        public void Load()
        {
            
        }

        public void Load(long id)
        {
            this.Id = id;
            using (var db = new DbWorker())
            {
                
                var sql = @"select o.dan_id patientid,o.doctor_id doctorid,o.talon_id talonid,
                        o.status_id status_id,o.create_date createtime, o.response_datetime responsetime, 
                        o.trimestr, o.sended_lpu sendedlpucode, o.mkbcode mkbcode,
                        s.id,s.name as status 
                        from laboratory.order o 
                        inner join laboratory.sp_order_status s on s.id = o.status_id 
                        where o.id = @Id;";
                LabOrder order = db.Connection.Query<LabOrder, LabRecordStatus,LabOrder>(sql, (ord, status) => { ord.Status = status; return ord; }, new { Id = id }).FirstOrDefault();
                logger.Debug("Params: id={0}", id);
                
                Exams = new List<Exam>();
                //var _exams = Exam.GetAll();
                //var _exam_params = ExamParameter.GetAll();

                var danId = order.PatientId;
                this.Patient = new Patient();
                this.Patient.LoadData(danId.Value);

                var doctorId = order.DoctorId;
                if (doctorId.HasValue)
                {
                    this.Doctor = new Doctor();
                    this.Doctor.LoadData(doctorId.Value);
                }

                var talonId = order.TalonId;
                if (talonId.HasValue)
                {
                    this.TalonId = talonId;
                }

                //Status = new LabRecordStatus()
                //{
                //    Id = DbResult.GetNumeric(results[0].GetByName("status_id"), -1),
                //    Status = DbResult.GetString(results[0].GetByName("status"), string.Empty)
                //};
                this.Status = order.Status;
                //var date = order.CreateTime;
                //this.CreateTime = date;
                this.CreateTime = order.CreateTime;

                //date = DbResult.GetDateTime(results[0].GetByName("response_datetime"), DateTime.MinValue);
                this.ResponseTime = order.ResponseTime;


                //var results = db.GetResults(q);   
                if (!HasResults)
                {
                    sql = @"select distinct e.id,e.name,e.exchange_id exchangeid from laboratory.order_exam_data oed
                        inner join laboratory.sp_exam e on e.id=oed.exam_id
                        where oed.order_id=@OrderId";

                    var exams = db.Connection.Query<Exam>(sql, new { OrderId = id }).ToList();
                    this.Exams = exams;
                }

                if (!string.IsNullOrEmpty(order.MKBCode))
                {
                    this.MKBCode = order.MKBCode;
                }


                if (!string.IsNullOrEmpty(order.SendedLpuCode))
                {
                    this.SendedLpuCode = order.SendedLpuCode;
                }

                //if (results!=null)
                //    {
                //        foreach (var result in results)
                //        {
                //            var exam_id = DbResult.GetNumeric(result.GetByName("exam_id"), -1);
                //            var exam_param_id = DbResult.GetNumeric(result.GetByName("exam_parameter_id"), -1);

                //            if (this.Exams.Any(t => t.Id == exam_id))
                //            {
                //                var exam = Exams.First(t => t.Id == exam_id);
                //                var param = _exam_params.FirstOrDefault(t => t.Id == exam_param_id);
                //                exam.Parameters.Add(param);
                //            }
                //            else
                //            {
                //                var exam = _exams.FirstOrDefault(t => t.Id == exam_id);
                //                if (exam != null)
                //                {
                //                    this.Exams.Add(exam);
                //                    exam.Parameters = new List<ExamParameter>();
                //                    var param = _exam_params.FirstOrDefault(t => t.Id == exam_param_id);
                //                    exam.Parameters.Add(param);
                //                }

                //            }
                //        }
                //    }

                
            }
        }

        public void GetLabResults()
        {

            var repo = new LabRepository();
            Exams=repo.GetLabResults(Id);
//            using (var db = new DbWorker())
//            {




////                var sql = @"SELECT p.id, p.name, p.code, p.measure measure, 
////                              r.order_id orderid, r.exam_id examid, r.exam_param_id examparamid, r.value, 
////                              r.success, r.error_reason failreason, r.exam_date examdate
////                              FROM  laboratory.order_result r
////                              inner join laboratory.sp_exam_param p on p.id=r.exam_param_id
////                              where order_id = @ID";
////                logger.Debug("sql: {0}", sql);
////                logger.Debug("params: id={0}", Id);

////                IEnumerable<ExamParameter> parameters = db.Connection.Query<ExamParameter, LabParameterResult, ExamParameter>(sql,
////                    (param, result) => { param.Result = result; return param; }, new { ID = Id }, splitOn: "orderid");
                
////                if ((parameters == null) || (parameters.Count() == 0)) return;
                
////                List<long?> examIds = parameters.Select(p => p.Result.ExamId).Distinct().ToList();
////                sql=string.Format("select id,name,exchange_id exchangeid from laboratory.sp_exam where id in ({0})", string.Join(",",examIds.Where(p=>p!=null)));
////                this.Exams = db.Connection.Query<Exam>(sql).ToList();
////                if (Exams!=null)
////                {
////                    foreach(var exam in Exams)
////                    {
////                        exam.Parameters = parameters.Where(p => p.Result.ExamId==exam.Id).ToList();
////                    }
////                }


//                //foreach (var result in results)
//                //{
//                //    var paramId = DbResult.GetNumeric(result.GetByName("exam_param_id"), -1);
//                //    logger.Debug("paramId: {0}", paramId);
//                //    if (Exams.Any(t => t.Parameters.Any(p => p.Id == paramId)))
//                //    {
//                //        var exam = Exams.First(e => e.Parameters.Any(t => t.Id == paramId));
//                //        var param = exam.Parameters.First(t => t.Id == paramId);
//                //        param.Result = new LabParameterResult()
//                //        {
//                //            Success = DbResult.GetBoolean(result.GetByName("success"), false),
//                //            Value = DbResult.GetDouble(result.GetByName("value"), 0),
//                //            FailReason = DbResult.GetString(result.GetByName("error_reason"), string.Empty),
//                //            ExamDate = DbResult.GetDateTime(result.GetByName("exam_date"), DateTime.MinValue)
//                //        };
//                //    }
//                //}
//            }
        }

        public XElement GetXmlElement()
        {
            var root = new XElement("order");
            root.Add(new XAttribute("id", Id));

            var patient = new XElement("patient");
            patient.Add(new XElement("dan_id", Patient.PatientId));
            patient.Add(new XElement("last_name", Patient.LastName));
            patient.Add(new XElement("first_name", Patient.FirstName));
            if (!string.IsNullOrEmpty(Patient.MidName))
                patient.Add(new XElement("mid_name", Patient.MidName));
            patient.Add(new XElement("birthdate", Patient.BirthDate.ToString("yyyy-MM-dd")));
            patient.Add(new XElement("gender", Patient.Gender == Gender.Male ? "male" : "female"));
            
            root.Add(patient);

            foreach (var exam in Exams)
            {
                var examElement = new XElement("exam");
                examElement.Add(new XAttribute("id", exam.ExchangeId));
                foreach (var param in exam.Parameters)
                {
                    var paramElement = new XElement("parameter");
                    paramElement.Add(new XAttribute("name", param.Code));
                    examElement.Add(paramElement);
                }
                root.Add(examElement);
            }

            return root;
        }
    }
}
