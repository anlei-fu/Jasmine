using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Interfaces;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Jasmine.Orm.Implements
{
    public class DefaultBaseTypeConvertor : ISqlBaseTypeConvertor
    {
        private DefaultBaseTypeConvertor()
        {

        }

        private static readonly HashSet<Type> _baseTypes = new HashSet<Type>()
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(float),
            typeof(double),
            typeof(long),
            typeof(ulong),
            typeof(decimal),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(DateTimeOffset),
            typeof(Guid),

            typeof(byte?),
            typeof(sbyte?),
            typeof(short?),
            typeof(ushort?),
            typeof(int?),
            typeof(uint?),
            typeof(float?),
            typeof(double?),
            typeof(long?),
            typeof(ulong?),
            typeof(decimal?),
            typeof(DateTime?),
            typeof(TimeSpan?),
            typeof(DateTimeOffset?),
            typeof(Guid?)
        };

        private static readonly HashSet<Type> _times = new HashSet<Type>()
        {
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(DateTimeOffset),
        };

        public static readonly ISqlBaseTypeConvertor Instance = new DefaultBaseTypeConvertor();

        public object FromSql(DbDataReader reader, int index, Type type)
        {
            var value = reader.GetValue(index);

            if (type.IsEnum)
            {
               

            }

            switch (type.FullName)
            {

                case BaseTypes.SByte:
                   return (sbyte)value;
                case BaseTypes.NSByte:
                    return (sbyte?)value;
                case BaseTypes.Char:
                    return (char)value;
                case BaseTypes.NChar:
                    return (char?)value;
                case BaseTypes.Byte:
                    return (byte)value;
                case BaseTypes.NByte:
                    return (byte?)value;
                case BaseTypes.Boolean:
                    return (bool)value;
                case BaseTypes.NBoolean:
                    return (bool?)value;
                case BaseTypes.UShort:
                    return (ushort)value;
                case BaseTypes.NUShort:
                    return (ushort?)value;
                case BaseTypes.Short:
                    return (short)value;
                case BaseTypes.NShort:
                    return (short?)value;
                case BaseTypes.Int:
                    return (int)value;

                case BaseTypes.NInt:
                    return (int?)value;

                case BaseTypes.UInt:
                    return (uint)value;

                case BaseTypes.NUInt:
                    return (uint?)value;

                case BaseTypes.Long:
                    return (long)value;
                case BaseTypes.NLong:
                    return (long?)value;
                case BaseTypes.ULong:
                    return (ulong)value;
                case BaseTypes.NULong:
                    return (ulong?)value;
                case BaseTypes.Float:
                    return (float)value;
                case BaseTypes.NFloat:
                    return (float?)value;
                case BaseTypes.Double:
                    return (double)value;
                case BaseTypes.NDouble:
                    return (double?)value;
                case BaseTypes.Decimal:
                    return (decimal)value;
                case BaseTypes.NDecimal:
                    return (decimal?)value;
                case BaseTypes.Guid:
                    return (Guid)value;
                case BaseTypes.NGuid:
                    return (Guid?)value;
                case BaseTypes.DateTime:
                    return (DateTime)value;
                case BaseTypes.NDateTime:
                    return (DateTime?)value;
                case BaseTypes.TimeSpan:
                    return (TimeSpan)value;
                case BaseTypes.NTimeSpan:
                    return (TimeSpan?)value;
                case BaseTypes.DateTimeOffset:
                    return (DateTimeOffset)value;
                case BaseTypes.NDateTimeOffset:
                    return (DateTimeOffset?)value;
                case BaseTypes.String:
                    return (string)value;
                default:
                    throw new NotConvertableTypeException(type,GetType());
            }
        }

        public string ToSQL(Type type, object obj, bool nullable = true)
        {
            if (obj == null)
            {
                if (nullable)
                    return "null";
                else
                {
                    if (type == BaseTypes.TString)
                        return string.Empty;
                    else
                        return "0";
                }
            }
            else if (type.IsEnum)
                return ((int)obj).ToString();
            else if (type == typeof(char))
                return (char)obj == '\'' ? "''''" : $"'{obj.ToString()}'";
            else if (type == typeof(string))
                return formatSingleQuota((string)obj);
            else if (_baseTypes.Contains(type))
                return obj.ToString();
            else
                throw new NotConvertableTypeException(type, GetType());
        }


        private string formatSingleQuota(string source)
        {
            var sb = new StringBuilder();

            foreach (var item in source)
            {
                if (item == '\'')
                    sb.Append("''");
                else
                    sb.Append(item);
            }

            return $"'{sb.ToString()}'";
        }

    }
}
