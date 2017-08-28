using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SharedDbWorker.Classes
{
    public class DbResult
    {
        public DbResult()
        {
            Fields = new List<object>();
            FieldNames = new List<string>();
        }

        public List<object> Fields { get; private set; }
        public List<string> FieldNames { get; private set; }

        public object GetByName(string name)
        {
            if (FieldNames.Count == Fields.Count)
            {
                var ind = FieldNames.IndexOf(name);
                if (ind == -1)
                {
                    throw new InvalidExpressionException(string.Format("Поле {0} не найдено!", name));
                }
                return Fields[ind];
            }

            return null;
        }

        public static bool GetBoolean(object value, bool defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is bool))
                return defaultValue;
            
            Boolean.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static char GetChar(object value, char defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is Char))
                return defaultValue;

            var v = value.ToString();
            if (v.Length == 0)
                return defaultValue;

            return value.ToString()[0];
        }

        public static string GetString(object value, string defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is String))
                return defaultValue;

            return value.ToString();
        }

        public static long GetNumeric(object value, long defaultValue)
        {
            if (value == null)
                return defaultValue;

            var type = value.GetType();
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                    byte byteValue = (byte)defaultValue;
                    byteValue = GetByte(value, byteValue);
                    defaultValue = byteValue;
                    break;
                case TypeCode.SByte:
                    sbyte sbyteValue = (sbyte)defaultValue;
                    sbyteValue = GetSByte(value, sbyteValue);
                    defaultValue = sbyteValue;
                    break;
                case TypeCode.UInt16:
                    ushort ushortValue = (ushort)defaultValue;
                    ushortValue = GetUShort(value, ushortValue);
                    defaultValue = ushortValue;
                    break;
                case TypeCode.UInt32:
                    uint uintValue = (uint)defaultValue;
                    uintValue = GetUInt(value, uintValue);
                    defaultValue = uintValue;
                    break;
                case TypeCode.UInt64:
                    ulong ulongValue = (ulong)defaultValue;
                    ulongValue = GetULong(value, ulongValue);
                    defaultValue = (long)ulongValue;
                    break;
                case TypeCode.Int16:
                    short shortValue = (short)defaultValue;
                    shortValue = GetShort(value, shortValue);
                    defaultValue = shortValue;
                    break;
                case TypeCode.Int32:
                    int intValue = (int)defaultValue;
                    intValue = GetInt(value, intValue);
                    defaultValue = intValue;
                    break;
                case TypeCode.Int64:
                    long longValue = (long)defaultValue;
                    longValue = GetLong(value, longValue);
                    defaultValue = longValue;
                    break;
                case TypeCode.Decimal:
                    break;
                case TypeCode.Double:
                    break;
                case TypeCode.Single:
                    break;
                default:
                    
                    break;
            }
            return defaultValue;
        }

        public static byte GetByte(object value, byte defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is byte))
                return defaultValue;

            byte.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static sbyte GetSByte(object value, sbyte defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is sbyte))
                return defaultValue;

            sbyte.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static ushort GetUShort(object value, ushort defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is ushort))
                return defaultValue;

            ushort.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static short GetShort(object value, short defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is short))
                return defaultValue;

            short.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static uint GetUInt(object value, uint defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is uint))
                return defaultValue;

            uint.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static int GetInt(object value, int defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is int))
                return defaultValue;

            int.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static ulong GetULong(object value, ulong defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is ulong))
                return defaultValue;

            ulong.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static long GetLong(object value, long defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is long))
                return defaultValue;

            long.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static decimal GetDecimal(object value, decimal defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is decimal))
                return defaultValue;

            defaultValue = (decimal)value;

            return defaultValue;
        }

        public static double GetDouble(object value, double defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is double))
                return defaultValue;

            defaultValue = (double) value;

            return defaultValue;
        }

        public static DateTime GetDateTime(object value, DateTime defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (!(value is DateTime))
                return defaultValue;

            DateTime.TryParse(value.ToString(), out defaultValue);

            return defaultValue;
        }

        public static DateTime? GetNullableDateTime(object value)
        {
            if (value == null)
                return null;

            if (!(value is DateTime))
                return null;

            var dateTime = DateTime.MinValue;
            if (DateTime.TryParse(value.ToString(), out dateTime))
                return dateTime;

            return null;
        }
    }
}
