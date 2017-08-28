using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedDbWorker;
using Dapper;
using System.Reflection;

namespace Model.Classes.DopDisp
{
    public class Questionary
    {
        public IEnumerable<Question> GetQuestions(QuestinaryType qType)
        {
            using (var db = new DbWorker())
            {
                var sql = @"select 
	                            number, 
                                question ""text"",
                                answers,
                                additional_info additionalinfo
                            from codifiers.questionary_questions_tab qq
                            where qq.questionary_id=@QType
                            order by number";
                var result = db.Connection.Query<Question>(sql, new { QType = (long)qType });
                return result;
            }

        }

        public QuestionaryAnswer GetAnswer(long totalId)
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            using (var db = new DbWorker())
            {
                var result = db.Connection.GetList<QuestionaryAnswer>(new { DopDispTotalId = totalId }).FirstOrDefault();
                return result;
            }
        }

        public void AddOrUpdate(QuestionaryAnswer item)
        {
            var repo = new GenericRepository<QuestionaryAnswer>();
            repo.AddOrUpdate(item);
            if (item.Id.HasValue && !item.Id.ToString().EndsWith("03001"))
            {
                item.Id = Convert.ToInt64(item.Id.Value.ToString() + "03001");
            }
        }

        public IEnumerable<PropertyInfo> GetItems()
        {
            Type t = typeof(QuestionaryAnswer);
            PropertyInfo[] props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return props.Where(p => p.Name.StartsWith("ans") || p.Name.StartsWith("add")).ToList();
        }

    }


    public class Question
    {
        public long? TypeId { get; set; }
        public int? Number { get; set; }
        public string Text { get; set; }
        public string Answers { get; set; }
        public string[] AnswersArray {
            get
            {

                if (!string.IsNullOrEmpty(Answers))
                {
                    return Answers.Split(new char[]{ ','});
                }else
                {
                    return null;
                }

            }
        }

        public string AdditionalInfo { get; set; }

    }


    public enum QuestinaryType:long
    {
        PMO  = 100000,
        DD   = 300000,
        DD75 = 310000
    }

    [Table("dop_disp_questionary_tab")]
    public class QuestionaryAnswer:DbObject
    {
        [Key]
        [Column("id")]
        public long? Id { get; set; }
        [Column("operator_id")]
        public long? OperatorId { get; set; }
        [Column("dop_disp_main_id")]
        public long? DopDispTotalId { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public string answer5 { get; set; }
        public string answer6 { get; set; }
        public string answer7 { get; set; }
        public string answer8  { get; set; }
        public string answer9 { get; set; }
        public string answer10 { get; set; }
        public string answer11 { get; set; }
        public string answer12 { get; set; }
        public string additional12 { get; set; }
        public string answer13 { get; set; }
        public string answer14 { get; set; }
        public string answer15 { get; set; }
        public string answer16 { get; set; }
        public string additional6 { get; set; }
        public string answer17 { get; set; }
        public string answer18 { get; set; }
        public string additional8 { get; set; }
        public string answer19 { get; set; }
        public string answer20 { get; set; }
        public string answer21 { get; set; }
        public string answer22 { get; set; }
        public string answer23 { get; set; }
        public string answer24 { get; set; }
        public string answer25 { get; set; }
        public string answer26 { get; set; }
        public string additional26 { get; set; }
        public string answer27 { get; set; }
        public string answer28 { get; set; }
        public string answer29 { get; set; }
        public string answer30 { get; set; }
        public string answer31 { get; set; }
        public string answer32 { get; set; }
        public string answer33 { get; set; }
        public string answer34 { get; set; }
        public string answer35 { get; set; }
        public string answer36 { get; set; }
        public string answer37 { get; set; }
        public string answer38 { get; set; }
        public string answer39 { get; set; }
        public string answer40 { get; set; }
        public string answer41 { get; set; }
        public string answer42 { get; set; }
        public string answer43 { get; set; }
        public string additional43 { get; set; }
        public string answer44 { get; set; }
        public string answer45 { get; set; }


    }

}
