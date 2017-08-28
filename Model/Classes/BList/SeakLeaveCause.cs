using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Classes.SeakLeave
{
    public class SeakLeaveCause
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }


    public class SeakLeaveCauseAdditional
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class SeakLeaveViolation
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class SeakLeaveInvalid
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }

    public class SeakLeaveOtherCauseItem
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }


    public class SeakLeavePrevNumber
    {
        public string Number { get; set; }
        
    }


}
