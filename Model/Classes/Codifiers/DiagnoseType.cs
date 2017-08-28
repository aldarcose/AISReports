using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Вид диагноза (codifiers.sp_disease_vid_tab) (основное, сопутствующее, фоновое и т.п.)
    /// </summary>
    public class DiagnoseType
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
    }
}
