using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes.Laboratory
{
    public class RequestFilter
    {
        public FilterType Type { get; set; }
        public string Value { get; set; }
    }

    public enum FilterType
    {
        Exam,
        DateFrom,
        DateTo,
        ExactDate
    }
}
