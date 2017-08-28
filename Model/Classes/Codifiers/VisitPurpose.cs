using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Цель посещения (codifiers.visit_purpose_common_tab)
    /// </summary>
    public class VisitPurpose
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public bool IsForCure { get; set; }
        public bool IsForInspection { get; set; }

        public int Reestr { get; set; }
        public int Idsp { get; set; }

        public long LpuTypeId { get; set; }
    }
}
