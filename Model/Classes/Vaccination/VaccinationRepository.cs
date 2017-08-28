using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedDbWorker;
using Dapper;
using Model.Classes.Codifiers;

namespace Model.Classes.Vaccination
{
    public class VaccinationRepository
    {
        
        public IEnumerable<Vaccination> GetItems(long patientId)
        {
            using(var db = new DbWorker())
            {
                var sql = @"select priviv_id id, dan_id patientid, 
                             infection infectionid, i.name infectionname,
                             vid_priviv ""type"", t.name typename,
                             preparat preparatid, pr.name preparatname,
                             ed_izmeren unit, u.name unitname,
                             doza dosage, date_priviv date, 
                             seria series,
                             p.operator_id operatorid, 
                             doctor_id doctorid, 
                             reaction_id reactionid, r.name reactionname
                             from priviv_tab p 
                             left join codifiers.priv_infection_tab i on i.id=p.infection
                             left join codifiers.sp_simple_result_tab r on r.id=p.reaction_id
                             left join codifiers.priv_preparat_tab  pr on pr.id=p.preparat
                             left join codifiers.priv_vid_pr_tab t on t.id=p.vid_priviv
                             left join codifiers.priv_ed_izm_tab u on u.id=p.ed_izmeren
                             where dan_id=@PatientId
                            ";
                var result = db.Connection.Query<Vaccination>(sql, new { PatientId = patientId });
                return result;
            }
        }

        public IEnumerable<Vaccination> GetItemsByPreparat(long? preparat)
        {
            if (!preparat.HasValue)
                return null;

            using (var db = new DbWorker())
            {
                var sql = @" select d.infectionid, d.preparat preparatid, d.unit, d.dosage, d.series,
                             i.name infectionname,i.id infectionid, u.name unitname
                             from codifiers.priv_infection_tab i
                             join
                               (select infection infectionid, 
                               preparat, ed_izmeren unit, doza dosage, seria series
                               from priviv_tab
                               where char_length(seria)>3 
                               group by 1,2,3,4,5) d  on i.id=d.infectionid
                             join codifiers.priv_ed_izm_tab u on u.id=d.unit  
                               where d.preparat=@PreparatId and d.unit!=0
                               order by preparat, series
                                                        ";
                var result = db.Connection.Query<Vaccination>(sql, new { PreparatId = preparat });
                return result;
            }
        }

        public Vaccination GetItem(long? itemId)
        {
            if (!itemId.HasValue)
                return null;

            using (var db = new DbWorker())
            {
                var sql = @"select 
                            priviv_id id, 
                            dan_id patientid, 
                            infection infectionid, 
                            vid_priviv ""type"",
                            preparat preparatid, 
                            ed_izmeren unit, 
                            doza dosage, 
                            date_priviv date, 
                            seria series,
                            operator_id operatorid, 
                            doctor_id doctorid, 
                            reaction_id reactionid
                            from priviv_tab
                            where priviv_id=@Id
                            ";
                var result = db.Connection.Query<Vaccination>(sql, new { Id= itemId }).FirstOrDefault();
                return result;
            }
        }


        public bool IsExists(Vaccination vaccination)
        {
            if (vaccination.Id.HasValue)
                return true;
            else
                return false;
            
        }
        
        public void Save(Vaccination vaccination)
        {
            
            var parameters = new DynamicParameters();
            parameters.Add("PatientId", vaccination.PatientId);
            parameters.Add("InfectionId", vaccination.InfectionId);
            parameters.Add("Type",vaccination.Type);
            parameters.Add("Preparat",vaccination.PreparatId); 
            parameters.Add("Units",vaccination.Unit); 
            parameters.Add("Dosage", vaccination.Dosage);
            parameters.Add("Date", vaccination.Date);
            parameters.Add("Series",vaccination.Series);
            parameters.Add("Operatorid", vaccination.OperatorId);
            parameters.Add("Doctorid", vaccination.DoctorId);
            parameters.Add("ReactionId", vaccination.ReactionId);
            parameters.Add("ChangeDateTime", DateTime.Now);
            string sql = string.Empty;
            using (var db = new DbWorker())
            {
                if (IsExists(vaccination))
                {
                    sql = @"update priviv_tab set
                                dan_id= @Patientid, 
                                infection=@InfectionId, 
                                vid_priviv=@Type,
                                preparat=@Preparat, 
                                ed_izmeren=@Units, 
                                doza=@Dosage, 
                                date_priviv=@Date, 
                                seria=@Series,
                                operator_id=@Operatorid, 
                                doctor_id=@Doctorid, 
                                reaction_id=@ReactionId,
                                date_vnes_inf = @ChangeDateTime
                                where priviv_id=@Id
                            ";
                    parameters.Add("Id", vaccination.Id);
                    var result = db.Connection.Execute(sql, parameters);
                }
                else
                {
                    sql = @"insert into priviv_tab(dan_id, infection, vid_priviv, preparat, ed_izmeren, 
                                        doza, date_priviv, seria, operator_id,  doctor_id, reaction_id,date_vnes_inf)
                                values(@Patientid,@InfectionId,@Type,@Preparat,@Units,@Dosage,@Date,
                                        @Series,@Operatorid,@Doctorid,@ReactionId, @ChangeDateTime) returning priviv_id";
                    var result = db.Connection.Query<long?>(sql, parameters).FirstOrDefault();
                    if (result.HasValue)
                    {
                        vaccination.Id = result.Value;
                    }
                }
                
            }
        }

        public bool CanSave(Vaccination vaccination, out string message)
        {
            if (!vaccination.PatientId.HasValue)
            {
                message = "Пациент не выбран";
                return false;
            }
            if (!vaccination.DoctorId.HasValue)
            {
                message = "Не выбран врач";
                return false;
            }

            if (!vaccination.Dosage.HasValue)
            {
                message = "Не определена дозировка";
                return false;
            }

            if (!vaccination.InfectionId.HasValue)
            {
                message = "Не определена инфекция";
                return false;
            }

            if (!vaccination.PreparatId.HasValue)
            {
                message = "Не определен препарат";
                return false;
            }

            if (!vaccination.ReactionId.HasValue)
            {
                message = "Не определена реакция";
                return false;
            }

            if (string.IsNullOrEmpty(vaccination.Series))
            {
                message = "Не определена серия";
                return false;
            }

            if (!vaccination.Type.HasValue)
            {
                message = "Не определен тип";
                return false;
            }

            if (!vaccination.Unit.HasValue)
            {
                message = "Не определены ед. изм.";
                return false;
            }

            message = null;
            return true;
        }

        
        public  IEnumerable<Preparat> GetPreparatList()
        {
            using(var db=new DbWorker())
            {
                var sql = @"select id, name,code 
                            from codifiers.priv_preparat_tab
                            order by name";
                var result = db.Connection.Query<Preparat>(sql);
                return result;
            }
        }

        public  IEnumerable<VaccinationType> GetTypes()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select id, name, code 
                            from codifiers.priv_vid_pr_tab
                            order by name
                            ";
                var result = db.Connection.Query<VaccinationType>(sql);
                return result;
            }
        }


        public  IEnumerable<Reaction> GetReactions()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select id, name, code 
                            from codifiers.sp_simple_result_tab 
                            where result_type=149
                            order by name
                           ";
                var result = db.Connection.Query<Reaction>(sql);
                return result;
            }
        }

        public  IEnumerable<Doctor> GetDoctors()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select fam lastname, nam firstname, mid midname, d.id id
                            from doctor_tab d
                            join dan_tab p on d.dan_id=p.dan_id
                            where employee_type=0 and expiration_date>now() 
                            order by fam,nam,mid
                            ";
                var result = db.Connection.Query<Doctor>(sql);
                return result;
            }
        }

        public  IEnumerable<Doctor> GetNurses()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select fam lastname, nam firstname, mid midname, d.id id
                            from doctor_tab d
                            join dan_tab p on d.dan_id=p.dan_id
                            where employee_type=1 and expiration_date>now() 
                            order by fam,nam,mid
                            ";
                var result = db.Connection.Query<Doctor>(sql);
                return result;
            }
        }

        public  IEnumerable<MO> GetMO()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select id, name 
                            from codifiers.lpu_tab
                            where id!=0
                            order by name
                            ";
                var result = db.Connection.Query<MO>(sql);
                return result;
            }
        }

        public IEnumerable<Unit> GetUnits()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select id, name, code
                            from codifiers.priv_ed_izm_tab
                            order by name
                           ";
                var result = db.Connection.Query<Unit>(sql);
                return result;
            }
        }

        public IEnumerable<Infection> GetInfections()
        {
            using (var db = new DbWorker())
            {
                var sql = @"select id, name, code
                            from codifiers.priv_infection_tab
                            order by name
                           ";
                var result = db.Connection.Query<Infection>(sql);
                return result;
            }
        }

        public void RemoveItem(long itemId)
        {
            using(var db = new DbWorker())
            {
                var result = db.Connection.Execute("Delete from priviv_tab where priviv_id=@Id",
                    new { Id = itemId });
            }
        }

    }
}
