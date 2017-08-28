using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model.Classes.SeakLeave
{
    public class SeakLeaveListItem
    {
        public long? Id { get; set; }
        public string Number { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public string TypeName { get; set; }
        public Doctor Doctor { get; set; }
        public long? CauseId { get; set; }
        public string CauseName { get; set; }
        
    }
}
