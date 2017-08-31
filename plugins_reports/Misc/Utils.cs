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

        public static bool IsNumericType(object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
