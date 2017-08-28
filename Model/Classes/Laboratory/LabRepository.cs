using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using SharedDbWorker;
using System.Xml.Linq;
using DbCaching;



namespace Model.Classes.Laboratory
{
    public class LabRepository
    {
        public IEnumerable<LabOrder> GetOrders(long patientId)
        {
            using(var db = new DbWorker())
            {
                var sql = @"SELECT o.id,o.create_date createtime,o.talon_id talonid, o.response_datetime responsetime,
                        case when o.voenkomat is null then false else o.voenkomat end voenkomat, o.trimestr,
                        s.id,s.name status, doc.id,doc.inner_doctor_code innercode, dan.fam lastname, dan.nam firstname, dan.mid midname,
                        pdan.dan_id id, pdan.fam lastname, pdan.nam firstname, pdan.mid midname, pdan.date_born birthdate
                        FROM laboratory.order o
                        left join laboratory.sp_order_status s on s.id = o.status_id
                        left join public.doctor_tab doc on doc.id=o.doctor_id
                        left join public.dan_tab dan on dan.dan_id=doc.dan_id
                        left join public.dan_tab pdan on pdan.dan_id=o.dan_id
                        where o.dan_id = @PatientId
                        order by o.id desc";
                var orders = db.Connection.Query<LabOrder, LabRecordStatus, Doctor, Patient, LabOrder>(sql,
                    (order, status, doctor, patient) => { order.Doctor = doctor; order.Status = status; order.Patient = patient; return order; },
                    new {PatientId=patientId} );
                return orders;
            }
        }

        public List<Exam> GetLabResults(long id)
        {
            
            List<Exam> examsAll = null;
            List<Exam> exams = new List<Exam>();            
            using(var db = new DbWorker())
            {
                var xmls=db.Connection.Query<string>("select file_content from laboratory.lis_response_files where order_id=@OrderId", new { OrderId = id});
                foreach(var content in xmls)
                {
                    var resultData = XDocument.Parse(content);

                    var examResults = resultData.Descendants("exam_result");
                    foreach(var xmlExam in examResults)
                    {
                        var exam = new Exam();
                        
                        if (DbCache.GetItem("LabExams")==null)
                        {
                            examsAll = (List<Exam>)Exam.GetAll();
                            DbCache.SetItem("LabExams",examsAll , 3600);
                        }
                        else
                        {
                            examsAll = (List<Exam>)DbCache.GetItem("LabExams");
                        };

                        exam.Parameters = new List<ExamParameter>();
                        exam.ExchangeId = Convert.ToInt64(xmlExam.Attribute("id").Value.ToString());
                        if (examsAll.Any(e => e.ExchangeId == exam.ExchangeId))
                        {
                            exam.Name = examsAll.First(e => e.ExchangeId == exam.ExchangeId).Name;
                        }
                        
                        ExamParameter examParam = null;
                        
                        foreach(var node in  xmlExam.Elements())
                        {
                            
                            switch(node.Name.ToString())
                            {
                                case "parameter": 
                                    examParam = InitExamParameter(node);
                                    exam.Parameters.Add(examParam);
                                    break;
                                case "normalvalues": examParam.Result.NormalValues = node.Value.ToString();break;
                                case "units": examParam.Result.Units = node.Value.ToString(); break;
                                case "abnormalflag": examParam.Result.AbnormalFlag = node.Value.ToString(); break;
                                default: break;
                            }

                        }
                        exams.Add(exam);
                    }

                }
                

            }
            return exams;
        }

        private ExamParameter InitExamParameter(XElement node)
        {
            List<ExamParameter> paramsAll = null;
            ExamParameter examParam = new ExamParameter();

            if (DbCache.GetItem("LabParameters") == null)
            {
                paramsAll = (List<ExamParameter>)ExamParameter.GetAll();
                DbCache.SetItem("LabParameters", paramsAll, 3600);
            }
            else
            {
                paramsAll = (List<ExamParameter>)DbCache.GetItem("LabParameters");
            }
            
            LabParameterResult result = new LabParameterResult();
            var examParamCode = node.Attribute("name").Value;

            if (paramsAll.Any(p => p.Code == examParamCode))
            {
                examParam = paramsAll.Where(p => p.Code == examParamCode).FirstOrDefault();
            }

            result.Success = Convert.ToBoolean(node.Attribute("success").Value.ToString());
            if (node.Attribute("fail_reason") != null)
            {
                result.FailReason = node.Attribute("fail_reason").Value;
            }

            result.Value = node.Value.ToString();

            examParam.Result = result;
            return examParam;
        }

    }
}
