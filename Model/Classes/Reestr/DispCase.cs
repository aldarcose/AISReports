using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Classes.Reestr
{
    public class DispCase
    {
        public long? Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Основной диагноз
        /// </summary>
        public string DS1 { get; set; }
        /// <summary>
        /// Результат
        /// </summary>
        public string Result { get; set; }
        public long? ResultStage1Id { get; set; }
        public long? ResultStage2Id { get; set; }
        /// <summary>
        /// Сопособ оплаты - по умолчанию 11
        /// </summary>
        public string IDSP { get; set; }
        //Количество услуг - по умолчанию 1
        public double? ED_COL { get; set; }
        public DateTime? AcceptedDateStage1 { get;set;}
        public DateTime? AcceptedDateStage2 { get; set; }
        public long? DoctorId { get; set; }
        public DateTime? EndDateStage1 { get; set; }
        //public List<>
    }


    public class DispUsl
    {
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get;set; }
        public string CodeUsl { get; set; }
        public string Count { get; set; }
        public string Tarif { get;set; }
        public string PRVS { get; set; }
        public string CodeMD { get;set; }
        public string Comment { get; set; }
    }

    
    public class DispMrp
    {
        public long? DispCaseId { get; set; }
        public long? MrpTypeId { get; set; }
        public DateTime? MrpDate { get; set; }
        public long? DoctorId { get; set; }
        public bool? HasRefusal { get; set; }
        public bool? IsExternalLPU { get; set; } 

    }

}
