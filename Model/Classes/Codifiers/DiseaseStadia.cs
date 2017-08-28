using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Стадия заболевания (public.sp_stadiya_diagn_tab) (ранее известное хроническое, выявленное во время дд и т.п.)
    /// </summary>
    public class DiseaseStadia
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
