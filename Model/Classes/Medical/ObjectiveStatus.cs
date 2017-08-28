using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Medical
{
    public class ObjectiveStatus
    {
        public long Id { get; set; }
        public long VisitId { get; set; }

        public string Description { get; set; }
    }
}
