using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Documents
{
    public abstract class BaseDocument
    {
        public string Serial { get; set; }
        public string Number { get; set; }
        public DateTime? DateBeg { get; set; }
    }
}
