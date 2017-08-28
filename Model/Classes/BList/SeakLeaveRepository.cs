using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using SharedDbWorker;
using Model.Classes.SeakLeave;

namespace Model.Classes.SeakLeave
{
    public class SeakLeaveRepository
    {
        public SeakLeaveItem GetItem(long id)
        {
            using(var db = new DbWorker())
            {
                var sql = @"
                            SELECT 
                              blist_id id,
                              data_end dateend,
                              data_begin datebegin,
                              diag_mkb_id diagnos,
                              dan_id patientid,
                              talon_id talonid,
                              doctor_id doctorid,
                              prichina_id causeid,
                              prichina_additional_id CauseAdditionalId,
                              prichina_change_id CauseChangeId,
                              ser serial,
                              nomer number,
                              prev_blist_num previtemid,
                              doctor_id_closed doctoridclosed,
                              type_id typeid,
                              lpu_id lpuid,
                              pregnancy_twelve_flag pregnancytwelveflag,
                              date1,
                              date2,
                              date_start_work datestartwork,
                              mest_work_id workid,
                              ""comment"",
                              date_issue   dateissue,
                              san_number sannumber,
                              san_ogrn sanogrn,
                              regiment_violation_id  regimentviolationid,
                              regiment_violation_date RegimentViolationDate,
                              mse_naprav_date msenapravdate,
                              mse_reg_date mseregdate,
                              mse_examine_date mseexaminedate,
                              inval_id invalid,
                              other_id otherid,
                              blist_other_date OtherDate,
                              num_other_next OtherNumNext,
                              jobless_flag jobless,
                              other_work_id otherworkid
                             FROM 
                              public.blist_tab 
                            WHERE blist_id=@Id
                           ";
                var result = db.Connection.Query<SeakLeaveItem>(sql, new { Id=id }).FirstOrDefault();
                result.Extends = GetExtends(result.Id.Value).ToList();
                return result;
            }
        }

        public void AddOrUpdate(SeakLeaveItem item, long operatorId)
        {

            var parameters = new DynamicParameters();
            parameters.Add("DateEnd", item.DateEnd);
            parameters.Add("DateBegin", item.DateBegin);
            parameters.Add("Diagnos", item.Diagnos);
            parameters.Add("Patientid", item.PatientId);
            parameters.Add("TalonId", item.TalonId);
            parameters.Add("DoctorId", item.DoctorId);
            parameters.Add("CauseId", item.CauseId);
            parameters.Add("Serial", item.Serial);
            parameters.Add("Number", item.Number);
            parameters.Add("PrevItemId", item.PrevItemId);
            parameters.Add("DoctorIdClosed", item.DoctorIdClosed);
            parameters.Add("TypeId", item.TypeId);
            parameters.Add("LpuId", item.LpuId);
            parameters.Add("PregnancyTwelveFlag", item.PregnancyTwelveFlag);
            parameters.Add("Date1", item.Date1);
            parameters.Add("Date2", item.Date2);
            parameters.Add("DateStartWork", item.DateStartWork);
            parameters.Add("WorkId", item.WorkId);
            parameters.Add("Comment", item.Comment);
            parameters.Add("DateIssue", item.DateIssue);
            parameters.Add("SanNumber", item.SanNumber);
            parameters.Add("SanOgrn", item.SanOgrn);
            parameters.Add("RegimentViolationId", item.RegimentViolationId);
            parameters.Add("RegimentViolationDate", item.RegimentViolationDate);
            parameters.Add("MSENapravDate", item.MSENapravDate);
            parameters.Add("MSERegDate", item.MSERegDate);
            parameters.Add("MSEExamineDate", item.MSEExamineDate);
            parameters.Add("InvalId", item.InvalId);
            parameters.Add("OtherId", item.OtherId);
            parameters.Add("OtherDate", item.OtherDate);
            parameters.Add("OtherNumNext", item.OtherNumNext);
            parameters.Add("OperatorId", operatorId);
            parameters.Add("Jobless", item.Jobless);
            parameters.Add("OtherWorkId", item.OtherWorkId);
            
            var sql = string.Empty;
            using (var db = new DbWorker())
            {
                if (item.Id!=null)
                {
                    sql = @"update public.blist_tab set
                            data_end = @DateEnd,
                            data_begin = @DateBegin,
                            diag_mkb_id = @Diagnos,
                            dan_id = @Patientid,
                            talon_id = @TalonId,
                            doctor_id = @DoctorId,
                            prichina_id = @CauseId,
                            ser = @Serial,
                            nomer = @Number,
                            prev_blist_num = @PrevItemId,
                            doctor_id_closed = @DoctorIdClosed,
                            type_id = @TypeId,
                            lpu_id = @LpuId,
                            pregnancy_twelve_flag =@PregnancyTwelveFlag,
                            date1 = @Date1,
                            date2 = @Date2,
                            date_start_work = @DateStartWork,
                            mest_work_id = @WorkId,
                            comment = @Comment,
                            date_issue = @DateIssue,
                            san_number = @SanNumber,
                            san_ogrn = @SanOgrn,
                            regiment_violation_id = @RegimentViolationId,
                            regiment_violation_date = @RegimentViolationDate,
                            mse_naprav_date = @MSENapravDate,
                            mse_reg_date = @MSERegDate,
                            mse_examine_date = @MSEExamineDate,
                            inval_id = @InvalId,
                            other_id = @OtherId,
                            blist_other_date = @OtherDate,
                            num_other_next = @OtherNumNext,
                            operator_id = @OperatorId,
                            jobless_flag=@Jobless,
                            other_work_id = @OtherWorkId
                            where blist_id=@Id
                          ";
                    parameters.Add("Id", item.Id);
                    var result = db.Connection.Execute(sql, parameters);
                }
                else
                {
                    sql = @"
                            insert into public.blist_tab(
                                data_end,
                                data_begin,
                                diag_mkb_id,
                                dan_id,
                                talon_id,
                                doctor_id,
                                prichina_id,
                                ser,
                                nomer,
                                prev_blist_num,
                                doctor_id_closed,
                                type_id,
                                lpu_id,
                                pregnancy_twelve_flag,
                                date1,
                                date2,
                                date_start_work,
                                mest_work_id,
                                comment,
                                date_issue,
                                san_number,
                                san_ogrn,
                                regiment_violation_id,
                                regiment_violation_date,
                                mse_naprav_date,
                                mse_reg_date,
                                mse_examine_date,
                                inval_id,
                                other_id,
                                blist_other_date,
                                num_other_next,
                                operator_id,
                                jobless_flag,
                                other_work_id
                            )
                            values(
                                @DateEnd,
                                @DateBegin,
                                @Diagnos,
                                @Patientid,
                                @TalonId,
                                @DoctorId,
                                @CauseId,
                                @Serial,
                                @Number,
                                @PrevItemId,
                                @DoctorIdClosed,
                                @TypeId,
                                @LpuId,
                                @PregnancyTwelveFlag,
                                @Date1,
                                @Date2,
                                @DateStartWork,
                                @WorkId,
                                @Comment,
                                @DateIssue,
                                @SanNumber,
                                @SanOgrn,
                                @RegimentViolationId,
                                @RegimentViolationDate,
                                @MSENapravDate,
                                @MSERegDate,
                                @MSEExamineDate,
                                @InvalId,
                                @OtherId,
                                @OtherDate,
                                @OtherNumNext,
                                @OperatorId,
                                @Jobless,
                                @OtherWorkId
                            ) returning blist_id
                           ";
                    var result = db.Connection.ExecuteScalar<long>(sql, parameters);
                    if (result != 0)
                    {
                        item.Id = result;
                    }
                }

            }//end using bd

            if (item.Extends.Count>0)
            {
                foreach(var ext in item.Extends)
                {
                    ext.SeakLeaveId = item.Id;
                    AddOrUpdateExtend(ext, operatorId);
                }
            }
        }

        

        public bool CanSave(SeakLeaveItem item, out string message)
        {
            message = string.Empty;
            if (!item.PatientId.HasValue)
            {
                message = "Не указан пациент";
                return false;
            }

            if (!item.LpuId.HasValue)
            {
                message = "Не указана организация";
                return false;
            }

            if(!item.DateIssue.HasValue)
            {
                message = "Не указана дата выписки";
                return false;
            }

            if (string.IsNullOrEmpty(item.Number))
            {
                message = "Не указан номер";
                return false;
            }

            if (item.Extends==null || item.Extends.Count==0)
            {
                message = "Не указаны сроки";
                return false;
            }

            if (string.IsNullOrEmpty(item.Diagnos))
            {
                message = "Не указан диагноз";
                return false;
            }

            return true;
        }

        public IEnumerable<SeakLeaveListItem> GetItems(long patientId)
        {
            using (var db = new DbWorker())
            {
                var sql = @"
                            select 
                            b.blist_id id, 
                            b.nomer number,  
                            b.data_begin datebegin, 
                            b.data_end dateend,
                            t.name typename,
                            pr.nam_prichina causename,
                            b.doctor_id id,
                            docdan.fam lastname,
                            docdan.nam firstname,
                            docdan.mid midname
                            from blist_tab b
                            left join prichina_tab pr on b.prichina_id=pr.prichina_id
                            left join doctor_tab doc on b.doctor_id = doc.id
                            left join dan_tab docdan on docdan.dan_id=doc.dan_id
                            left join codifiers.blist_type_tab t on t.id=b.type_id
                            where b.dan_id=@Id and b.type_id!=8
                            ";
                var result = db.Connection.Query<SeakLeaveListItem, Doctor, SeakLeaveListItem>(sql,
                    (sl, doc) => { sl.Doctor = doc; return sl; }, new { Id = patientId }
                    );
                return result;
            }
        }

        public void Delete(long itemId)
        {
            var sql = @"update blist_tab set
                        type_id=8
                        where blist_id=@Id
                       ";
            using(var db = new DbWorker())
            {
                var result = db.Connection.Execute(sql, new { Id = itemId });
            }
        }


        public IEnumerable<SeakLeaveCause> GetCauses()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select 
                          prichina_id id, nam_prichina ""name"", code code
                          from prichina_tab";
                var result = db.Connection.Query<SeakLeaveCause>(sql);
                return result;
            }
        }


        public SeakLeaveCause GetCause(long id)
        {
            using (var db = new DbWorker())
            {
                var sql = @"select 
                          prichina_id id, nam_prichina ""name"", code code
                          from prichina_tab 
                          where prichina_id=@Id
                          ";
                var result = db.Connection.Query<SeakLeaveCause>(sql, new { Id=id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<SeakLeaveCauseAdditional> GetCausesAdditional()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select 
                            id, ""name"", code 
                            from codifiers.disability_reasons_tab
                            where additional=true";
                var result = db.Connection.Query<SeakLeaveCauseAdditional>(sql);
                return result;
            }
        }

        public SeakLeaveCauseAdditional GetCauseAdditional(long id)
        {
            using (var db = new DbWorker())
            {
                var sql = @"select 
                            id, ""name"", code 
                            from codifiers.disability_reasons_tab
                            where id=@Id";
                var result = db.Connection.Query<SeakLeaveCauseAdditional>(sql, new { Id = id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<SeakLeaveCauseAdditional> GetTalons(long patientId)
        {
            using (var db = new DbWorker())
            {
                var sql = @"select 
                            id 
                            from talon_tab
                            where dan_id=@PatientId";
                var result = db.Connection.Query<SeakLeaveCauseAdditional>(sql, new { PatientId=patientId });
                return result;
            }
        }

        public IEnumerable<SeakLeaveInvalid> GetInvalidGroups()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select 
                            inval_id id,nam_inval ""name""
                            from inval_tab
                            where blist_show";
                var result = db.Connection.Query<SeakLeaveInvalid>(sql);
                return result;
            }
        }

        public SeakLeaveInvalid GetInvalidGroup(long id)
        {
            using (var db = new DbWorker())
            {
                var sql = @"select 
                            inval_id id,nam_inval ""name""
                            from inval_tab
                            where blist_show and inval_id=@Id";
                var result = db.Connection.Query<SeakLeaveInvalid>(sql, new { Id=id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<SeakLeaveExtend> GetExtends(long itemId)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select  
                                be.id,
                                be.lpu_id lpuid,
                                be.operator_id,
                                be.blist_id,
                                be.date_from datefrom,
                                be.date_for datefor,
                                be.doctor_id doctorid,
                                be.chief_doctor_id chiefdoctorid,
                                be.editable,
                                be.type,
                                be.printed,
                                doc1.id,
                                d1.fam lastname,
                                d1.mid midname,
                                doc1.sp_dolg_doctor_id positionid,
                                dol1.name positionname,
                                doc2.id,
                                d2.fam lastname, 
                                d2.nam firstname, 
                                d2.mid midname,
                                doc2.sp_dolg_doctor_id positionid,
                                dol2.name positionname
                                from blist_extend_tab be
                                left join doctor_tab doc1 on doc1.id = be.doctor_id
                                left join dan_tab d1 on d1.dan_id = doc1.dan_id
                                left join codifiers.doctor_dolg_tab dol1 on doc1.sp_dolg_doctor_id=dol1.id
                                left join doctor_tab doc2 on doc2.id = be.chief_doctor_id
                                left join dan_tab d2 on d2.dan_id = doc2.dan_id
                                left join codifiers.doctor_dolg_tab dol2 on doc2.sp_dolg_doctor_id=dol2.id
                            where blist_id=@ItemId";
                var result = db.Connection.Query<SeakLeaveExtend, Doctor, Doctor,SeakLeaveExtend>(sql,
                    (ex, doc, ch) => { ex.Doctor = doc; ex.ChiefDoctor = ch; return ex; },
                    new { ItemId=itemId }
                    );
                return result;
            }
        }


        public bool CanSaveExtend(SeakLeaveExtend extend, out string message)
        {
            if (!extend.DateFrom.HasValue)
            {
                message = "Нет даты начала";
                return false;
            }
            
            if (!extend.DateFor.HasValue)
            {
                message = "Нет даты окончания";
                return false;
            }

            if (!extend.DoctorId.HasValue)
            {
                message = "Не указан врач";
                return false;
            }

            message = null;
            return true;
        }



        public void AddOrUpdateExtend(SeakLeaveExtend extend, long operatorId)
        {
            var sql = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("OperatorId", operatorId);
            parameters.Add("SeakLeaveId", extend.SeakLeaveId);
            parameters.Add("DateFrom", extend.DateFrom);
            parameters.Add("DateFor", extend.DateFor);
            parameters.Add("DoctorId", extend.DoctorId);
            parameters.Add("ChiefDoctorId", extend.ChiefDoctorId);
            parameters.Add("Editable", extend.Editable);
            parameters.Add("TypeId", extend.TypeId);
            parameters.Add("Printed", extend.Printed);
            using (var db = new DbWorker())
            {
                if (extend.Id != null)
                {
                    sql = @"update blist_extend_tab set
                                  operator_id = @OperatorId,
                                  blist_id = @SeakLeaveId,
                                  date_from = @DateFrom,
                                  date_for = @DateFor,
                                  doctor_id = @DoctorId,
                                  chief_doctor_id = @ChiefDoctorId,
                                  editable = @Editable,
                                  type = @TypeId,
                                  printed = @Printed
                            where id=@Id
                           ";
                    parameters.Add("Id", extend.Id);
                    var result = db.Connection.Query<SeakLeaveExtend>(sql, parameters);
                }               
                else
                {
                    sql = @"insert into blist_extend_tab(
                                  operator_id,
                                  blist_id,
                                  date_from,
                                  date_for,
                                  doctor_id,
                                  chief_doctor_id,
                                  editable,
                                  type,
                                  printed
                            )
                            values(
                                  @OperatorId,
                                  @SeakLeaveId,
                                  @DateFrom,
                                  @DateFor,
                                  @DoctorId,
                                  @ChiefDoctorId,
                                  @Editable,
                                  @TypeId,
                                  @Printed
                            ) returning id";
                    var result = db.Connection.Query<long>(sql, parameters).FirstOrDefault();
                    extend.Id = result;
                }
            }
        }

        public void DeleteExtend(long itemId)
        {
            using(var db = new DbWorker())
            {
                var sql = "delete from blist_extend_tab where id=@Id";
                var result = db.Connection.Execute(sql, new { Id = itemId });
            }
        }


        //public bool CanSaveExtend(SeakLeaveExtend item, out string message)
        //{
        //    if (!item.DateFor.HasValue)
        //    {
        //        message = "Не указана дата окончания";
        //        return false;
        //    }

        //    if (!item.DateFrom.HasValue)
        //    {
        //        message = "Не указана дата начала";
        //        return false;
        //    }

        //    if (!item.DoctorId.HasValue)
        //    {
        //        message = "Не указан врач";
        //        return false;
        //    }
        //    message = string.Empty;
        //    return true;
        //}

        public IEnumerable<WorkPlaceItem> GetWorkPlaceItems()
        {
            using(var db = new DbWorker())
            {
                var sql = @"
                            select id, ""name"" ""name""
                            from codifiers.mest_work_tab
                            order by ""name""
                           ";
                var result = db.Connection.Query<WorkPlaceItem>(sql);
                return result;
            }
        }

        public WorkPlaceItem GetWorkPlaceItem(long itemId)
        {
            using (var db = new DbWorker())
            {
                var sql = @"
                            select id, short_name ""name""
                            from codifiers.mest_work_tab
                            where id=@ItemId
                           ";
                var result = db.Connection.Query<WorkPlaceItem>(sql, new { ItemId=itemId }).FirstOrDefault();
                return result;
            }
        }

        public void AddWorkPlaceItem(WorkPlaceItem item, long operatorId)
        {
            using(var db = new DbWorker())
            {
                var sql = @"insert into codifiers.mest_work_tab(name, short_name, operator_id)
                               values(@FullName, @ShortName, @OperatorId) returning id
                              ";
                var result = db.Connection.ExecuteScalar<long>(sql, new { FullName=item.FullName, ShortName=item.Name, OperatorId=operatorId });
                if (result!=0)
                {
                    item.Id = result;
                }
            }
        }

        public IEnumerable<SeakLeaveOtherCauseItem> GetOtherCauses()
        {
            using(var db = new DbWorker())
            {
                var sql = @"select id, code || ' - ' || ""name"" ""name""
                            from codifiers.blist_other_tab o";
                var result = db.Connection.Query<SeakLeaveOtherCauseItem>(sql);
                return result;
            }
        }

        public SeakLeaveOtherCauseItem GetOtherCause(long id)
        {
            using (var db = new DbWorker())
            {
                var sql = @"select id, code || ' - ' || ""name"" ""name"", code
                            from codifiers.blist_other_tab o
                            where o.id=@Id
                            ";
                var result = db.Connection.Query<SeakLeaveOtherCauseItem>(sql, new { Id=id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<SeakLeaveViolation> GetViolations()
        {
            using(var db = new DbWorker())
            {
                var sql = @"select id, code || ' - ' || ""name"" ""name"", code
                            from codifiers.blist_violence_tab";
                var result = db.Connection.Query<SeakLeaveViolation>(sql);
                return result;
            }
        }


        public SeakLeaveViolation GetViolation(long id)
        {
            using (var db = new DbWorker())
            {
                var sql = @"select id, code || ' - ' || ""name"" ""name"", code
                            from codifiers.blist_violence_tab
                            where id=@Id
                           ";
                var result = db.Connection.Query<SeakLeaveViolation>(sql, new { Id=id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<SeakLeavePrevNumber> GetPrevSeakLeaveItems(long patientId)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select nomer number from blist_tab where dan_id=@PatientId";

                var result = db.Connection.Query<SeakLeavePrevNumber>(sql, new { PatientId = patientId });
                return result;
            }
        }


    }
}
