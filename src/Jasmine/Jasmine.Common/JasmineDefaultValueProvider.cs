using Jasmine.Reflection;
using System;

namespace Jasmine.Common
{
    public class JasmineDefaultValueProvider
    {
        public static object GetDefaultValue(Type type)
        {

            if(type.IsEnum)
            {
                return Enum.GetValues(type).GetValue(0);
            }

            switch (type.FullName)
            {
                case BaseTypes.SByte:
                    return (sbyte)0;
                case BaseTypes.Byte:
                    return (byte)0;
                case BaseTypes.Short:
                    return (short)0;
                case BaseTypes.UShort:
                    return (ushort)0;
                case BaseTypes.Int:
                    return 0;
                case BaseTypes.UInt:
                    return (uint)0;
                case BaseTypes.Long:
                    return (long)0;
                case BaseTypes.ULong:
                    return (ulong)0;
                case BaseTypes.Char:
                    return '\0';
                case BaseTypes.Boolean:
                    return false;
                case BaseTypes.Float:
                    return 0f;
                case BaseTypes.Double:
                    return 0d;
                case BaseTypes.Decimal:
                    return 0f;
                case BaseTypes.DateTime:
                    return default(DateTime);
                case BaseTypes.TimeSpan:
                    return default(TimeSpan);
                case BaseTypes.DateTimeOffset:
                    return default(DateTimeOffset);
                case BaseTypes.Guid:
                    return default(Guid);
                case BaseTypes.NFloat:
                case BaseTypes.NInt:
                case BaseTypes.NLong:
                case BaseTypes.NUInt:
                case BaseTypes.NUShort:
                case BaseTypes.NShort:
                case BaseTypes.NByte:
                case BaseTypes.NSByte:
                case BaseTypes.NULong:
                case BaseTypes.NChar:
                case BaseTypes.NBoolean:
                case BaseTypes.NDouble:
                case BaseTypes.NDecimal:
                case BaseTypes.NDateTime:
                case BaseTypes.NTimeSpan:
                case BaseTypes.NDateTimeOffset:
                case BaseTypes.NGuid:
                default:
                    return null;
            }
        }
    }
}
