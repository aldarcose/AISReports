using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedDbWorker;
using Dapper;
using System.Xml.Linq;
using System.Threading.Tasks;
using NLog;

namespace Model.Classes.Benefits
{
    public class BenefitRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public IEnumerable<PatientBenefit> GetPatientBenefits(long patientId)
        {
            using (var db = new DbWorker())
            {
                var sql = @"
                             select 
                                ds.id, 
                                l.name benefitname,
                                er.name entryreason,  
                                ds.entry_date entrydate,
                                ds.leave_date removaldate,
                                lr.name removalreason,
                                er.name entryform,
                                ds.diagn_id diagnosis, 
                                (dt.fam || ' ' || dt.nam || ' ' || dt.mid) doctor,
                                lpu.name
                                from dispensary_registration_tab ds
                                left join codifiers.lgota_tab l on ds.lgota_id=l.code
                                left join codifiers.dispan_leave_reason_common_tab lr on lr.id=ds.leave_reason_id
                                left join codifiers.dispan_entry_reason_common_tab er on er.id=ds.entry_reason_id
                                left join sp_lgot_doc_type_tab d on d.sp_lgot_doc_type_id=ds.sp_lgot_doc_type_id
                                left join doctor_tab doc on doc.id=ds.doctor_id
                                left join dan_tab dt on dt.dan_id=doc.dan_id
                                left join codifiers.lpu_tab lpu on lpu.code = ds.entry_lpu
                                where ds.dan_id=@PatientId
                           ";
                var result = db.Connection.Query<PatientBenefit>(sql, new { PatientId = patientId });
                return result;
            }
        }

        public PatientBenefit Load(long id)
        {
            using(var db=new DbWorker())
            {
                var sql = @"
                            select 
                            id,
                            lgota_id benefitid,    
                            entry_reason_id entryreasonid,
                            entry_date entrydate,
                            diagn_id diagnosis,
                            doctor_id doctorid,
                            leave_reason_id removalreasonid,
                            leave_date removaldate,
                            next_date nextvisit,
                            sp_lgot_doc_type_id documentid,
                            ser_lgot_doc series,
                            num_lgot_doc number,
                            lkk_date lkkdate,
                            epikriz_lkk_id epicrisid,
                            dan_id patientid
                            from dispensary_registration_tab
                            where id=@Id
                           ";
                var result = db.Connection.Query<PatientBenefit>(sql, new { Id = id }).FirstOrDefault();
                return result;
            }
        }

        public PatientBenefit Load(long patientId, string code)
        {
            using (var db = new DbWorker())
            {
                var sql = @"
                            select 
                            id,
                            lgota_id benefitid,    
                            entry_reason_id entryreasonid,
                            entry_date entrydate,
                            diagn_id diagnosis,
                            doctor_id doctorid,
                            leave_reason_id removalreasonid,
                            leave_date removaldate,
                            next_date nextvisit,
                            sp_lgot_doc_type_id documentid,
                            ser_lgot_doc series,
                            num_lgot_doc number,
                            lkk_date lkkdate,
                            epikriz_lkk_id epicrisid,
                            dan_id patientid,
                            entry_lpu entrylpuid
                            from dispensary_registration_tab
                            where lgota_id=@Code and dan_id=@PatientId
                           ";
                var result = db.Connection.Query<PatientBenefit>(sql, new { Code = code, PatientId=patientId}).FirstOrDefault();
                return result;
            }
        }

        
        public bool IsExist(PatientBenefit patientBenefit)
        {
            var sql = @"select exists(select 1 from dispensary_registration_tab
                            where lgota_id=@Code and dan_id=@PatientId)";
            using(var db = new DbWorker())
            {
                var result = db.Connection.Query<bool>(sql, new { Code=patientBenefit.BenefitId, PatientId=patientBenefit.PatientId}).FirstOrDefault();
                return result;
            }
        }


        public IEnumerable<string> Validate(PatientBenefit pBenefit, bool withLgot=true)
        {
            var errors = new List<string>();
            if (withLgot && string.IsNullOrEmpty( pBenefit.BenefitId))
            {
                errors.Add("Не указана льгота");
            }

            if (pBenefit.RemovalDate.HasValue && !pBenefit.RemovalReasonId.HasValue)
            {
                errors.Add("Не указана причина выбытия");
            }

            if (!pBenefit.RemovalDate.HasValue && pBenefit.RemovalReasonId.HasValue)
            {
                errors.Add("Не указана дата выбытия");
            }

            if(!pBenefit.Id.HasValue && IsExist(pBenefit))
            {
                errors.Add("Обнаружен дубль льготы");
            }

            if (!pBenefit.EntryDate.HasValue)
            {
                errors.Add("Не указан дата взятия на учет");
            }

            if (!pBenefit.EntryReasonId.HasValue)
            {
                errors.Add("Не указана форма взятия на учет");
            }

            if (string.IsNullOrEmpty(pBenefit.Diagnosis))
            {
                errors.Add("Не указан диагноз");
            }

            return errors;
        }

        public void Save(PatientBenefit patientBenefit)
        {
            var sqlParam = new DynamicParameters();

            sqlParam.Add("@BenefitId", patientBenefit.BenefitId);
            sqlParam.Add("@EntryReasonId",patientBenefit.EntryReasonId);
            sqlParam.Add("EntryDate",patientBenefit.EntryDate);
            sqlParam.Add("@Diagnosis", patientBenefit.Diagnosis);
            sqlParam.Add("@DoctorId", patientBenefit.DoctorId);
            sqlParam.Add("@RemovalReasonId", patientBenefit.RemovalReasonId);
            sqlParam.Add("@RemovalDate", patientBenefit.RemovalDate);
            sqlParam.Add("@NextVisit", patientBenefit.NextVisit);
            sqlParam.Add("@DocumentId", patientBenefit.DocumentId);
            sqlParam.Add("@Series", patientBenefit.Series);
            sqlParam.Add("@Number", patientBenefit.Number);
            sqlParam.Add("@LKKDate", patientBenefit.LKKDate);
            sqlParam.Add("@EpicrisId", patientBenefit.EpicrisId);
            sqlParam.Add("@PatientId", patientBenefit.PatientId);
            sqlParam.Add("@OperatorId", patientBenefit.OperatorId);
            sqlParam.Add("@EntryLPUId", patientBenefit.EntryLPUId);

            using(var db = new DbWorker())
            {
                string sql = null;
                if (patientBenefit.Id.HasValue)
                {
                    sqlParam.Add("@Id", patientBenefit.Id);
                    sql = @"update dispensary_registration_tab set
                            lgota_id=@BenefitId,    
                            entry_reason_id=@EntryReasonId,
                            entry_date=@EntryDate,
                            diagn_id=@Diagnosis,
                            doctor_id=@DoctorId,
                            leave_reason_id=@RemovalReasonId,
                            leave_date=@RemovalDate,
                            next_date=@NextVisit,
                            sp_lgot_doc_type_id=@DocumentId,
                            ser_lgot_doc=@Series,
                            num_lgot_doc=@Number,
                            lkk_date=@LKKDate,
                            epikriz_lkk_id=@EpicrisId,
                            dan_id=@PatientId,
                            operator_id=@OperatorId,
                            entry_lpu=@EntryLPUId  
                            where id=@Id
                           ";
                    var result = db.Connection.Execute(sql, sqlParam);
                }
                else
                {
                    sql = @"insert into 
                            dispensary_registration_tab(lgota_id, entry_reason_id,entry_date,diagn_id,doctor_id,leave_reason_id, leave_date, next_date,sp_lgot_doc_type_id,ser_lgot_doc,num_lgot_doc,lkk_date,epikriz_lkk_id, dan_id, operator_id, entry_lpu)
                            values(@BenefitId, @EntryReasonId,@EntryDate,@Diagnosis,@DoctorId,@RemovalReasonId, @RemovalDate,@NextVisit,@DocumentId,@Series,@Number,@LKKDate,@EpicrisId,@PatientId, @OperatorId, @EntryLPUId) 
                            returning id; 
                           ";
                    var result = db.Connection.ExecuteScalar<long?>(sql, sqlParam);
                    if (result > 0)
                        patientBenefit.Id = result;
                }
            }
        }

        
        public void Delete(PatientBenefit patientBenefit)
        {
            using(var db = new DbWorker())
            {
                var result = db.Connection.Execute("delete from dispensary_registration_tab where id=@Id", 
                    new  { Id=patientBenefit.Id });
            }
        }

        public IEnumerable<Benefit> GetBenefits()
        {
            using(var db=new DbWorker())
            {
                var result = db.Connection.Query<Benefit>("select id, code, name from codifiers.lgota_tab where length(code)=2 order by name");
                return result;
            }
        }

        public IEnumerable<RemovalReason> GetRemovalReasons()
        {
            using (var db = new DbWorker())
            {
                var result = db.Connection.Query<RemovalReason>("select id,code, name from codifiers.dispan_leave_reason_common_tab where old_prichin_sn_id is not null order by name");
                return result;
            }
        }


        public IEnumerable<EntryReason> GetEntryReasons()
        {
            using (var db = new DbWorker())
            {
                var result = db.Connection.Query<EntryReason>("select id,code, name from codifiers.dispan_entry_reason_common_tab where id in ('1','2') order by code ");
                return result;
            }
        }

        public IEnumerable<BenefitDocument> GetDocuments()
        {
            using (var db = new DbWorker())
            {
                var result = db.Connection.Query<BenefitDocument>("select sp_lgot_doc_type_id id, sp_lgot_doc_type as name from sp_lgot_doc_type_tab");
                return result;
            }
        }

        public IEnumerable<LKKEpicris> GetEpicrisItems()
        {
            using (var db = new DbWorker())
            {
                var result = db.Connection.Query<LKKEpicris>("select id, name,code from codifiers.onco_epikriz_tab order by name");
                return result;
            }
        }

        public void SetReestrDispatchDate(IEnumerable<ReestrPatientBenefit> reestrPatients )
        {

            Parallel.ForEach(reestrPatients, (item) => 
            {
                using (var db = new DbWorker())
                {
                    var sqlNew = @"insert into fed_lgota_reestr_tab(dan_id, reestr_date, inv, changedate) values(@PatientId, @ReestrDate, @Inv, @ChangeDate)";
                    var sqlChange = @"update fed_lgota_reestr_tab set reestr_date=@ReestrDate, inv=@Inv, changedate=@ChangeDate where dan_id=@PatientId";
                    var isExists = db.Connection.ExecuteScalar<bool>("select exists(select 1 from fed_lgota_reestr_tab where dan_id=@PatientId)", new { PatientId = item.PatientId });
                    if (isExists)
                    {
                        db.Connection.Execute(sqlChange, new { PatientId = item.PatientId, ReestrDate = item.ReestrDate, Inv = item.InvType, ChangeDate = DateTime.Now });
                    }
                    else
                    {
                        db.Connection.Execute(sqlNew, new { PatientId = item.PatientId, ReestrDate = item.ReestrDate, Inv = item.InvType, ChangeDate = DateTime.Now });
                    }
                }
            }); 
            
        }


        public IEnumerable<ReestrPatientBenefit> GetPatientReestrBenefits(string pathToXML, DateTime reestrDate)
        {
            XDocument doc = XDocument.Load(pathToXML);
            var patients = doc.Descendants("PACIENT").Where(x =>x.Element("INV")!=null).ToList();
            var result = new List<ReestrPatientBenefit>();
            var uniq = 0;
            foreach(var pat in patients)
            {
                var patBenefit = new ReestrPatientBenefit();
                patBenefit.PatientId = Convert.ToInt64(pat.Element("ID_PAC").Value);
                patBenefit.InvType = Convert.ToInt32(pat.Element("INV").Value);
                patBenefit.ReestrDate = reestrDate;
                
                if (result.Exists(x=>x.PatientId==patBenefit.PatientId))
                {
                    logger.Debug("Дубль пациента {0}", patBenefit.PatientId);
                }
                else
                {
                    uniq++;
                }
                result.Add(patBenefit);
            }
            logger.Debug("Уникальных {0}", uniq);
            return result;
        }


    }
}
