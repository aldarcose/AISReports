using System.Collections.Generic;
using System.ComponentModel;
using SharedDbWorker;
using SharedDbWorker.Classes;

namespace Model.Classes.Laboratory
{
    public class ExamParameter
    {
        /// <summary>
        /// Идентификатор параметра
        /// </summary>
        [Browsable(false)]
        public long Id { get; set; }
        /// <summary>
        /// Идентификатор теста, к которому относится параметр
        /// </summary>
        [Browsable(false)]
        public long ExamId { get; set; }
        /// <summary>
        /// Название параметра
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Код параметра
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Measure { get; set; }

        public LabParameterResult Result { get; set; }

        public static List<ExamParameter> GetAll()
        {
            var list = new List<ExamParameter>();
            using (var db = new DbWorker())
            {
                var q = new DbQuery("GetAllExamParams");
                q.Sql = "select * from laboratory.sp_exam_param order by sort;";

                var results = db.GetResults(q);
                if (results != null && results.Count > 0)
                {
                    foreach (var dbResult in results)
                    {
                        var e = new ExamParameter();
                        e.Id = DbResult.GetNumeric(dbResult.GetByName("id"), -1);
                        e.Name = DbResult.GetString(dbResult.GetByName("name"), string.Empty);
                        e.ExamId = DbResult.GetNumeric(dbResult.GetByName("exam_id"), -1);
                        e.Code = DbResult.GetString(dbResult.GetByName("code"), string.Empty);
                        e.Measure = DbResult.GetString(dbResult.GetByName("measure"), string.Empty);

                        list.Add(e);
                    }
                }
            }
            return list;
        }
    }
}
