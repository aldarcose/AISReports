using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Standart
{
    /// <summary>
    /// Настройки заполнения формы визита
    /// standart.diagn_forms_type_tab
    /// </summary>
    public class VisitFormFillSettings
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long FormAnamnezId { get; set; }
        public long FormComplaintId { get; set; }
        public long FormObjStatusId { get; set; }

        public long FormConstParamsId { get; set; }
        public long FormHospId { get; set; }
        public long FormDiagnoseId { get; set; }

        public long FormMedicamentsId { get; set; }
        /// <summary>
        /// Идентификатор шаблона анамнеза
        /// </summary>
        public long PatternAnamnezId { get; set; }
        /// <summary>
        /// Идентификатор шаблона жалоб
        /// </summary>
        public long PatternComplaintId { get; set; }
        /// <summary>
        /// Идентификатор шаблона объективного статуса
        /// </summary>
        public long PatternObjStatusId { get; set; }
    }
}
