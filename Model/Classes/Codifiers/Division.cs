using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    /// <summary>
    /// отделение
    /// </summary>
    public class Division
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? LpuId { get; set; }
    }
}
