using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    public class MedBenefit
    {
        public long Id { get; set; }
        /// <summary>
        /// Наименование льготы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Код льготы
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Коды диагнозов (разделены запятой), диапазон через дефис
        /// </summary>
        public string DiagnRange { get; set; }

        /// <summary>
        /// Код для формирования списка препаратов на данную льготу.
        /// </summary>
        public long MedicamentTypeId { get; set; }
    }
}
