using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedDbWorker;
using Dapper;

namespace Model.Classes.Registry
{
    public class WaitingListItem
    {
        public long? Id { get; set; }
        public long? PatientId { get; set; }
        public Patient Patient { get; set; }
        public long? SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public long? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime? AppointmentDateTime { get; set; }
        public long? TalonDoctorId { get; set; }
        public long? TalonId { get; set; }
        public bool? IsGroup { get; set; }
    }

    public class ChildrenScheduleItem
    {
        public long? Id { get; set; }
        public int? MonthId { get; set; }
        public int? SpecialityId { get; set; }
    }

    public class WaitingListRepository
    {
        public IEnumerable<WaitingListItem>  GetItems(long patientId)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select t.id, t.dan_id patientid,  t.spec_id specialityid, spec.name specialityname,
                            t.date_begin datebegin, t.date_end dateend, t.talon_id talonid,t.talon_doctor_id talondoctorid,
                            t.isgroup isgroup,
                            dtal.date_nazn+dtal.time_nazn appointmentdatetime,
                            doc.id, doc.inner_doctor_code innercode, ddan.fam lastname, ddan.nam firstname, ddan.mid midname
                            from integration.task  t
                            left join codifiers.doctor_spec_tab spec on t.spec_id = spec.id
                            left join talon_doctor_tab dtal on dtal.talon_doctor_id=t.talon_doctor_id
                            left join doctor_tab doc on doc.id=dtal.doctor_id
                            left join dan_tab ddan on ddan.dan_id=doc.dan_id
                            where t.dan_id=@PatientId
                            order by t.date_begin
                           ";
                var result = db.Connection.Query<WaitingListItem, Doctor, WaitingListItem>(sql, (wlItem, doc) =>
                    {
                        wlItem.Doctor = doc; 
                        return wlItem;
                    },new { PatientId = patientId });
                return result;
            }
        }

        public WaitingListItem GetItem(long itemId)
        {
            using (var db = new DbWorker())
            {
                var sql = @"select t.id, t.dan_id patientid,  t.spec_id specialityid, spec.name specialityname,
                            t.date_begin datebegin, t.date_end dateend, t.talon_id talonid,t.talon_doctor_id talondoctorid,
                            t.isgroup isgroup,
                            dtal.date_nazn+dtal.time_nazn appointmentdatetime,
                            doc.id, doc.inner_doctor_code innercode, ddan.fam lastname, ddan.nam firstname, ddan.mid midname
                            from integration.task  t
                            left join codifiers.doctor_spec_tab spec on t.spec_id = spec.id
                            left join talon_doctor_tab dtal on dtal.talon_doctor_id=t.talon_doctor_id
                            left join doctor_tab doc on doc.id=dtal.doctor_id
                            left join dan_tab ddan on ddan.dan_id=doc.dan_id
                            where t.id=@Id
                            order by t.date_begin
                           ";
                var result = db.Connection.Query<WaitingListItem, Doctor, WaitingListItem>(sql, (wlItem, doc) =>
                {
                    wlItem.Doctor = doc;
                    return wlItem;
                }, new { Id = itemId }).FirstOrDefault();
                return result;
            }
        }

        public bool AddOrUpdateItem(WaitingListItem item)
        {
            if (item!=null && item.Id.HasValue)
            {
                //обновляем данные
                var sql = @"update integration.task set 
                            dan_id=@PatientId,
                            spec_id=@SpecId,
                            date_begin = @DateBegin,
                            date_end = @DateEnd,
                            doctor_id = @DoctorId,
                            isgroup=@IsGroup
                            where id=@Id
                            ";
                using(var db = new DbWorker())
                {
                    var result = db.Connection.Execute(sql, 
                                 new {
                                     PatientId=item.PatientId,
                                     SpecId=item.SpecialityId,
                                     DateBegin=item.DateBegin,
                                     DateEnd=item.DateEnd,
                                     DoctorId=item.DoctorId,
                                     IsGroup=item.IsGroup,
                                     Id = item.Id
                                 });
                    
                    return result>0;
                }
            }else if(item!=null && !item.Id.HasValue)
            {
                //обновляем данные
                var sql = @"insert into integration.task(dan_id,spec_id,date_begin,date_end,doctor_id,isgroup) 
                            values(@PatientId,@SpecId,@DateBegin,@DateEnd,@DoctorId,@IsGroup) returning id                           
                            ";
                using (var db = new DbWorker())
                {
                    var result = db.Connection.ExecuteScalar<long>(sql,
                                 new
                                 {
                                     PatientId = item.PatientId,
                                     SpecId = item.SpecialityId,
                                     DateBegin = item.DateBegin,
                                     DateEnd = item.DateEnd,
                                     DoctorId = item.DoctorId,
                                     IsGroup = item.IsGroup
                                 });
                    item.Id = result;
                    return result>0;
                }
            }
            return false;
        }

        public int RemoveItem(long? itemId)
        {
            var wlItem = this.GetItem(itemId.Value);
            using(var db = new DbWorker())
            {
                if (wlItem.TalonDoctorId.HasValue)
                {
                    var talonDelResult = db.Connection.Execute("delete from public.talon_doctor_tab where talon_doctor_id=@Id", new { Id=wlItem.TalonDoctorId.Value});
                }
                
                var result = db.Connection.Execute("delete from integration.task where id=@Id", new { Id=itemId});
                return result;
            }
        }


        public bool Validate(WaitingListItem wlItem, out string error)
        {
            if (wlItem == null){
                error = "Пустой элемент";
                return false;
            }
                
            if ((!wlItem.IsGroup.HasValue  || !wlItem.IsGroup.Value) && !wlItem.SpecialityId.HasValue)
            {
                error = "Не указана специальность";
                return false;
            }
                
            if (!wlItem.DateBegin.HasValue || !wlItem.DateEnd.HasValue)
            {
                error = "Не указаны даты";
                return false;
            }

            //if (wlItem.IsGroup.HasValue && wlItem.IsGroup.value && wlItem.Patient != null)
            //{
            //    if (wlItem.Patient.Age >= 1)
            //    {
            //        error = "групповая запись доступна для детей до 1 года";
            //        return false;
            //    }
            //}

            error = string.Empty;
            return true;
        }

        public IEnumerable<ChildrenScheduleItem> GetChildrenSchedule()
        {
            using (var db = new DbWorker())
            {
                var result = db.Connection.Query<ChildrenScheduleItem>("select id,month_id monthid, spec_id specialityid from integration.task_children");
                return result;
            }
        }

        public bool AddGroupItems(Patient patient, WaitingListItem wlItem)
        {
            //if (patient.Age >= 1) return false;
            var birthdate = patient.BirthDate;
            var dateBegin = wlItem.DateBegin;
            var schedule = GetChildrenSchedule();
            var wlList = new List<WaitingListItem>();
            foreach (var item in schedule)
            {
                var newwlItem = new WaitingListItem()
                {
                    DateBegin = birthdate.AddMonths(item.MonthId.Value),
                    SpecialityId = item.SpecialityId,
                    PatientId = patient.Id,
                    IsGroup= true
                };

                newwlItem.DateEnd = newwlItem.DateBegin.Value.AddDays(14);
                if (newwlItem.DateBegin >= dateBegin)
                {
                    wlList.Add(newwlItem);
                }
            }
            bool result=false;
            foreach(var item in wlList)
            {
                result = AddOrUpdateItem(item);
                
            }
            return result;
        }
        
    }
}
