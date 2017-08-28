using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Laboratory
{
    public class LabParameterResult
    {
        [DisplayName("Результат получен")]
        public bool Success { get; set; }
        
        [DisplayName("Значение")]
        public string Value { get; set; }
        
        [DisplayName("Причина отсутствия результата")]
        public string FailReason { get; set; }

        [DisplayName("Дата исследования")]
        public DateTime ExamDate { get; set; }

        [DisplayName("Норм. знач.")]
        public string NormalValues { get; set; }

        [DisplayName("Ед. изм.")]
        public string Units { get; set; }

        [DisplayName("Ненорм.")]
        public string AbnormalFlag { get; set; }

        public string Code { get; set; }

        public long? OrderId { get; set; }

        public long? ExamId { get; set; }

        public long? ExamParamId { get; set; }


        public override string ToString()
        {
            if (Success)
            {
                return Value;
            }
            else
            {
                return FailReason;
            }
        }
    }
}
