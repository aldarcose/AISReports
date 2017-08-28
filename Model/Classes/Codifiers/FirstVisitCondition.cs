using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interface;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// Состоянии при обращении (codifiers.first_visit_condition_tab)
    /// </summary>
    public class FirstVisitCondition
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
