using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Codifiers
{
    public class MkbClass
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string CodeFrom { get; set; }
        public string CodeTo { get; set; }
    }
}
