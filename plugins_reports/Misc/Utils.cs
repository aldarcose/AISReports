using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports
{
    public static class Utils
    {
        public static string ToString(object obj)
        {
            if (obj is string) return obj.ToString();
            if (obj is DateTime) return string.Format("{0:yyyy-MM-dd}", obj);

            throw new InvalidCastException(string.Format("Unexpected type: {0}", obj.GetType()));
        }
    }
}
