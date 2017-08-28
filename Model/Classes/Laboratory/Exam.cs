using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using SharedDbWorker;
using SharedDbWorker.Classes;
using Dapper;
using System.Linq;

namespace Model.Classes.Laboratory
{
    public class Exam
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        [Browsable(false)]
        public long Id { get; set; }
        /// <summary>
        /// Название теста
        /// </summary>
        [DisplayName("Тест")]
        public string Name { get; set; }
        /// <summary>
        /// Используется для связи с ЛИС
        /// </summary>
        public long ExchangeId { get; set; }
        public List<ExamParameter> Parameters { get; set; }

        public static List<Exam> GetAll()
        {
            var list = new List<Exam>();
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetAllExams");
                q.Sql = "select * from laboratory.sp_exam where not is_bad order by sort;";

                var results = db.GetResults(q);
                if (results != null && results.Count > 0)
                {
                    foreach (var dbResult in results)
                    {
                        var e = new Exam();
                        e.Id = DbResult.GetNumeric(dbResult.GetByName("id"), -1);
                        e.Name = DbResult.GetString(dbResult.GetByName("name"), string.Empty);
                        e.ExchangeId = DbResult.GetNumeric(dbResult.GetByName("exchange_id"), -1);
                        list.Add(e);
                    }
                }
            }

            return list;
        }

        public static Exam GetItem(long id)
        {
            using (var db=new DbWorker())
            {
                return db.Connection.Query<Exam>("select id,name,exchange_id exchangeid from laboratory.sp_exam where id=@Id").FirstOrDefault();
            }
        }
    }
}
