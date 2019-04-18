using Jasmine.Reflection;
using System;

namespace Jasmine.Common
{
    public class JasmineDefaultValueProvider
    {
        public static object GetDefaultValue(Type type)
        {
            switch (type.FullName)
            {
                case BaseTypes.NSByte:
                case BaseTypes.SByte:
                case BaseTypes.NByte:
                case BaseTypes.Byte:
                case BaseTypes.NShort:
                case BaseTypes.Short:
                case BaseTypes.NUShort:
                case BaseTypes.UShort:
                case BaseTypes.NInt:
                case BaseTypes.Int:
                case BaseTypes.NUInt:
                case BaseTypes.UInt:
                case BaseTypes.NLong:
                case BaseTypes.Long:
                case BaseTypes.NULong:
                case BaseTypes.ULong:
                    return 0;
                case BaseTypes.NChar:
                case BaseTypes.Char:
                    return '\0';
                case BaseTypes.NBoolean:
                case BaseTypes.Boolean:
                    return true;
                case BaseTypes.NFloat:
                case BaseTypes.Float:
                case BaseTypes.NDouble:
                case BaseTypes.Double:
                case BaseTypes.NDecimal:
                case BaseTypes.Decimal:
                    return 0f;
                case BaseTypes.NDateTime:
                case BaseTypes.DateTime:
                    return default(DateTime);
                case BaseTypes.NTimeSpan:
                case BaseTypes.TimeSpan:
                    return default(TimeSpan);
                case BaseTypes.NDateTimeOffset:
                case BaseTypes.DateTimeOffset:
                    return default(DateTimeOffset);
                case BaseTypes.NGuid:
                case BaseTypes.Guid:
                    return default(Guid);
                default:
                    return null;
            }
        }
    }
}
