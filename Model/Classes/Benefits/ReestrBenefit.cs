using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Classes.Benefits
{
    public class ReestrPatientBenefit
    {
        public long? PatientId {get;set;}
        public DateTime? ReestrDate { get; set; }
        public int? InvType {get;set;}

    }
}
