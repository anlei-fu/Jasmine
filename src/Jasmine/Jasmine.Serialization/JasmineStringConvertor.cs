using Jasmine.Reflection;
using System;

namespace Jasmine.Serialization
{
    public   class JasmineStringValueConvertor
    {
        public static string ToString(object obj,Type type)
        {
            return BaseTypes.Base.Contains(type) ? obj.ToString() 
                :type.IsEnum?((int)obj).ToString(): 
                    throw new NotSupportedException($"the {type} is not convertable in this convertor!");
        }


        public static object GetValue(string source,Type type)
        {
            return TryGetValue(source, type, out object value) ? value:throw new Exception($"can not convert source ({source}) to {type}");
        }
       
        public static bool TryGetValue<T>(string source,out T value)
        {
            if(TryGetValue(source,typeof(T),out var result))
            {
                value = (T)result;

                return true;
            }
            else
            {
                value = default(T);

                return false;
            }
        }
        public static bool TryGetValue(string source, Type t, out object value)
        {
            bool result = false;
            switch (t.FullName)
            {
                case BaseTypes.NSByte:
                case BaseTypes.SByte:
                    result = SByte.TryParse(source, out var nsb);
                    value = nsb;
                    break;
                case BaseTypes.NByte:
                case BaseTypes.Byte:
                    result = Byte.TryParse(source, out var bvalue);
                    value = bvalue;
                    break;
                case BaseTypes.NChar:
                case BaseTypes.Char:
                    result = SByte.TryParse(source, out var cvalue);
                    value = cvalue;
                    break;
                case BaseTypes.NShort:
                case BaseTypes.Short:
                    result = short.TryParse(source, out var svalue);
                    value = svalue;
                    break;
                case BaseTypes.NUShort:
                case BaseTypes.UShort:
                    result = ushort.TryParse(source, out var usvalue);
                    value = usvalue;
                    break;
                case BaseTypes.NInt:
                case BaseTypes.Int:
                    result = int.TryParse(source, out var ivalue);
                    value = ivalue;
                    break;
                case BaseTypes.NUInt:
                case BaseTypes.UInt:
                    result = uint.TryParse(source, out var uvalue);
                    value = uvalue;
                    break;
                case BaseTypes.NLong:
                case BaseTypes.Long:
                    result = long.TryParse(source, out var lvalue);
                    value = lvalue;
                    break;
                case BaseTypes.NULong:
                case BaseTypes.ULong:
                    result = ulong.TryParse(source, out var ulvalue);
                    value = ulvalue;
                    break;
                case BaseTypes.NBoolean:
                case BaseTypes.Boolean:
                    result = bool.TryParse(source, out var blvalue);
                    value = blvalue;
                    break;
                case BaseTypes.NFloat:
                case BaseTypes.Float:
                    result = float.TryParse(source, out var fvalue);
                    value = fvalue;
                    break;
                case BaseTypes.NDouble:
                case BaseTypes.Double:
                    result = double.TryParse(source, out var dvalue);
                    value = dvalue;
                    break;
                case BaseTypes.NDecimal:
                case BaseTypes.Decimal:
                    result = decimal.TryParse(source, out var devalue);
                    value = devalue;
                    break;
                case BaseTypes.NDateTime:
                case BaseTypes.DateTime:
                    result = DateTime.TryParse(source, out var dtvalue);
                    value = dtvalue;
                    break;
                case BaseTypes.NTimeSpan:
                case BaseTypes.TimeSpan:
                    result = TimeSpan.TryParse(source, out var tsvalue);
                    value = tsvalue;
                    break;

                case BaseTypes.NDateTimeOffset:
                case BaseTypes.DateTimeOffset:
                    result = DateTimeOffset.TryParse(source, out var dtfvalue);
                    value = dtfvalue;
                    break;
                case BaseTypes.NGuid:
                case BaseTypes.Guid:
                    result = Guid.TryParse(source, out var gvalue);
                    value = gvalue;
                    break;
                case BaseTypes.String:
                    result = true;
                    value = source;
                    break;
                default:
                    throw new NotSupportedException($"the {t} is not convertable in this convertor!");
            }

            return result;
        }






        public static object FromString(string source,Type type)
        {
            if (type.IsEnum)
                return Enum.ToObject(type, int.Parse(source));

            switch (type.Name)
            {
                case BaseTypes.NSByte:
                    return SByte.TryParse(source, out var sbvalue) ? (sbyte?)sbvalue : null;
                case BaseTypes.SByte:
                    return sbyte.Parse(source);
                case BaseTypes.NByte:
                    return Byte.TryParse(source, out var bvalue) ? (byte?)bvalue : null;
                case BaseTypes.Byte:
                    return byte.Parse(source);
                case BaseTypes.NChar:
                    return SByte.TryParse(source, out var cvalue) ? (char?)cvalue : null;
                case BaseTypes.Char:
                    return source[0];
                case BaseTypes.NShort:
                    return short.TryParse(source, out var svalue) ? (short?)svalue : null;
                case BaseTypes.Short:
                    return short.Parse(source);
                case BaseTypes.NUShort:
                    return ushort.TryParse(source, out var usvalue) ? (ushort?)usvalue : null;
                case BaseTypes.UShort:
                    return ushort.Parse(source);
                case BaseTypes.NInt:
                    return int.TryParse(source, out var ivalue) ? (int?)ivalue : null;
                case BaseTypes.Int:
                    return int.Parse(source);
                case BaseTypes.NUInt:
                    return uint.TryParse(source, out var uvalue) ? (uint?)uvalue : null;
                case BaseTypes.UInt:
                    return uint.Parse(source);
                case BaseTypes.NLong:
                    return long.TryParse(source, out var lvalue) ? (long?)lvalue : null;
                case BaseTypes.Long:
                    return long.Parse(source);
                case BaseTypes.NULong:
                    return ulong.TryParse(source, out var ulvalue) ? (ulong?)ulvalue : null;
                case BaseTypes.ULong:
                    return ulong.Parse(source);
                case BaseTypes.NBoolean:
                    return bool.TryParse(source, out var blvalue) ? (bool?)blvalue : null;
                case BaseTypes.Boolean:
                    return bool.Parse(source);
                case BaseTypes.NFloat:
                    return float.TryParse(source, out var fvalue) ? (float?)fvalue : null;
                case BaseTypes.Float:
                    return float.Parse(source);
                case BaseTypes.NDouble:
                    return double.TryParse(source, out var dvalue) ? (double?)dvalue : null;
                case BaseTypes.Double:
                    return double.Parse(source);
                case BaseTypes.NDecimal:
                    return decimal.TryParse(source, out var devalue) ? (decimal?)devalue : null;
                case BaseTypes.Decimal:
                    return decimal.Parse(source);
                case BaseTypes.NDateTime:
                    return DateTime.TryParse(source, out var dtvalue) ? (DateTime?)dtvalue : null;
                case BaseTypes.DateTime:
                    return DateTime.Parse(source);
                case BaseTypes.NTimeSpan:
                    return TimeSpan.TryParse(source, out var tsvalue) ? (TimeSpan?)tsvalue : null;
                case BaseTypes.TimeSpan:
                    return TimeSpan.Parse(source);
                case BaseTypes.NDateTimeOffset:
                    return DateTimeOffset.TryParse(source, out var dtfvalue) ? (DateTimeOffset?)dtfvalue : null;
                case BaseTypes.DateTimeOffset:
                    return DateTimeOffset.Parse(source);
                case BaseTypes.NGuid:
                    return Guid.TryParse(source, out var gvalue) ? (Guid?)gvalue : null;
                case BaseTypes.Guid:
                    return Guid.Parse(source);
                default:
                    throw new NotSupportedException($"the {type} is not convertable in this convertor!"); 
            }

        }
    }
}
