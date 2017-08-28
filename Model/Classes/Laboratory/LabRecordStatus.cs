using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Laboratory
{
    public class LabRecordStatus
    {
        public long Id { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return Status;
        }
    }
}
