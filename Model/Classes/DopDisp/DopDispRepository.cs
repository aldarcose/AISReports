using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedDbWorker;
using Dapper;

namespace Model.Classes.DopDisp
{
    public class DopDispRepository
    {
        public IEnumerable<DopDispTotal> GetTotalItems(long patientId)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using(var db = new DbWorker())
            {
                var result = db.Connection.GetList<DopDispTotal>(new { PatientId = patientId });
                return result;
            }
        }


        public void AddDopDispTotal(DopDispTotal item)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using (var db = new DbWorker())
            {
                var result = db.Connection.Insert(item);
                if (result.HasValue)
                    item.Id = result.Value;
                
            }
        }

        public void UpdateDopDispTotal(DopDispTotal item)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using (var db = new DbWorker())
            {
                var result = db.Connection.Update(item);
            }
        }

        public bool ValidateTotal(DopDispTotal item, out string message)
        {
            if (item.DiagnosItems==null || item.DiagnosItems.Count==0)
            {
                message = "Нет диагноза";
                return false;
            }

            if (!item.DateBegin.HasValue)
            {
                message = "Нет даты начала";
                return false;
            }

            if (!item.TypeId.HasValue)
            {
                message = "Не указан тип";
                return false;
            }

            if (!item.DoctorId.HasValue)
            {
                message = "Не указан врач";
                return false;
            }

            //if(!string.IsNullOrEmpty(item.Errors))
            //{
            //    message = "Устраните оошибки проверки";

            //}
            message = null;
            return true;
        }


        public IEnumerable<DopDispDiagnos> GetDignosList(long totalItemId)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using(var db = new DbWorker())
            {
                var result = db.Connection.GetList<DopDispDiagnos>(new { DopDispTotalId = totalItemId });
                return result;
            }
        }

        public IEnumerable<DopDispDiagnos> GetAllDiagnosList(long patientId)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using (var db = new DbWorker())
            {
                var result = db.Connection.GetList<DopDispDiagnos>(
                    @"where dop_disp_mmain_id in (select id
                      from dop_disp_main_tab dd
                      where dd.dan_id = @PatientId )", new { PatientId= patientId});
                return result;
            }
        }


        public DopDispDiagnos GetDignos(long id)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using (var db = new DbWorker())
            {
                var result = db.Connection.Get<DopDispDiagnos>(id);
                return result;
            }
        }





        public IEnumerable<DopDispTotal> GetCards(long patientId)
        {

            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using(var db = new DbWorker())
            {
                var result = db.Connection.GetList<DopDispTotal>(new { PatientId = patientId });
                return result.OrderByDescending(c=>c.DateBegin);
            }

        }

        public IEnumerable<T> GetItems<T>()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using(var db = new DbWorker())
            {
                var result = db.Connection.GetList<T>();
                return result;
            }
        }

        public IEnumerable<DDStageResult> GetResults(long ddTypeId, long stage)
        {
            string sql = "";
            using(var db = new DbWorker())
            {
                
                if (stage==1) 
                    sql = @"
                            select 
                            r.id, r.name 
                            from codifiers.dop_disp_type_tab t
                            left join codifiers.med_help_result_tab r on r.id = any(t.result_codes_first_stage)
                            where t.id=@DDType
                            order by r.id
                          ";
                else
                    sql = @"
                            select 
                            r.id, r.name 
                            from codifiers.dop_disp_type_tab t
                            left join codifiers.med_help_result_tab r on r.id = any(t.result_codes_second_stage)
                            where t.id=@DDType
                            order by r.id
                          ";
                var result = db.Connection.Query<DDStageResult>(sql, new { DDType = ddTypeId});
                return result;
            }

        }



    }

}
