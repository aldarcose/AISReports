using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Характер заболевания (codifiers.diagn_type_tab) (острый, хронический и т.п.)
    /// </summary>
    public class DiseaseCharacter
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
