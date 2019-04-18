using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Interfaces;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public object FromSql(SqlDataReader reader, int index, Type type)
        {

            if (type.IsEnum)
            {
                var e = reader.GetSqlInt32(index);

            }

            switch (type.FullName)
            {

                case BaseTypes.SByte:
                    var sb = reader.GetSqlByte(index);
                    return sb.IsNull ? default(sbyte) : (sbyte)(byte)sb;
                case BaseTypes.NSByte:
                    var nsb = reader.GetSqlByte(index);
                    return nsb.IsNull ? default(sbyte?) : (sbyte?)(byte)nsb;

                case BaseTypes.Char:
                    var c = reader.GetSqlByte(index);
                    return c.IsNull ? default(char) : (char)(byte)c;
                case BaseTypes.NChar:
                    var nc = reader.GetSqlByte(index);
                    return nc.IsNull ? null : (char?)(byte)nc;

                case BaseTypes.Byte:
                    var b = reader.GetSqlByte(index);
                    return b.IsNull ? default(byte) : (byte)b;
                case BaseTypes.NByte:
                    var nb = reader.GetSqlByte(index);
                    return nb.IsNull ? null : (byte?)nb;

                case BaseTypes.Boolean:
                    var bl = reader.GetSqlBoolean(index);
                    return bl.IsNull ? default(bool) : (bool)bl;
                case BaseTypes.NBoolean:
                    var nbl = reader.GetSqlBoolean(index);
                    return nbl.IsNull ? null : (bool?)nbl;

                case BaseTypes.UShort:
                    var ui16 = reader.GetSqlInt16(index);
                    return ui16.IsNull ? default(ushort) : (ushort)(short)ui16;

                case BaseTypes.NUShort:
                    var nui16 = reader.GetSqlInt16(index);
                    return nui16.IsNull ? null : (ushort?)(short)nui16;

                case BaseTypes.Short:
                    var i16 = reader.GetSqlInt16(index);
                    return i16.IsNull ? default(short) : (short)i16;

                case BaseTypes.NShort:
                    var ni16 = reader.GetSqlInt16(index);
                    return ni16.IsNull ? null : (short?)ni16;

                case BaseTypes.Int:
                    var i = reader.GetSqlInt32(index);
                    return i.IsNull ? default(int) : (int)i;

                case BaseTypes.NInt:
                    var ni = reader.GetSqlInt32(index);
                    return ni.IsNull ? null : (int?)ni;

                case BaseTypes.UInt:
                    var ui = reader.GetSqlInt32(index);
                    return ui.IsNull ? default(uint) : (uint)(int)ui;

                case BaseTypes.NUInt:
                    var nui = reader.GetSqlInt32(index);
                    return nui.IsNull ? null : (uint?)(int)nui;

                case BaseTypes.Long:
                    var l = reader.GetSqlInt64(index);
                    return l.IsNull ? default(long) : (long)l;
                case BaseTypes.NLong:
                    var nl = reader.GetSqlInt64(index);
                    return nl.IsNull ? null : (long?)nl;
                case BaseTypes.ULong:
                    var ul = reader.GetSqlInt64(index);
                    return ul.IsNull ? default(ulong) : (ulong)(long)ul;
                case BaseTypes.NULong:
                    var nul = reader.GetSqlInt64(index);
                    return nul.IsNull ? null : (ulong?)(long)nul;

                case BaseTypes.Float:
                    var f = reader.GetSqlSingle(index);
                    return f.IsNull ? default(float) : (float)f;

                case BaseTypes.NFloat:
                    var nf = reader.GetSqlSingle(index);
                    return nf.IsNull ? null : (float?)nf;

                case BaseTypes.Double:
                    var d = reader.GetSqlDouble(index);
                    return d.IsNull ? default(double) : (double)d;

                case BaseTypes.NDouble:
                    var nd = reader.GetSqlDouble(index);
                    return nd.IsNull ? null : (double?)nd;

                case BaseTypes.Decimal:
                    var de = reader.GetSqlDecimal(index);
                    return de.IsNull ? default(decimal) : (decimal)de;
                case BaseTypes.NDecimal:
                    var nde = reader.GetSqlDecimal(index);
                    return nde.IsNull ? null : (decimal?)nde;

                case BaseTypes.Guid:
                    var g = reader.GetSqlGuid(index);
                    return g.IsNull ? default(Guid) : (Guid)g;
                case BaseTypes.NGuid:
                    var ng = reader.GetSqlGuid(index);
                    return ng.IsNull ? null : (Guid?)ng;

                case BaseTypes.DateTime:
                    var dt = reader.GetSqlDateTime(index);
                    return dt.IsNull ? default(DateTime) : (DateTime)dt;
                case BaseTypes.NDateTime:
                    var ndt = reader.GetSqlDateTime(index);
                    return ndt.IsNull ? null : (DateTime?)ndt;

                case BaseTypes.TimeSpan:
                    var ts = reader.GetSqlDateTime(index);
                    return ts.IsNull ? default(TimeSpan) : TimeSpan.FromTicks(((DateTime)ts).Ticks);

                case BaseTypes.NTimeSpan:
                    var nts = reader.GetSqlDateTime(index);
                    return nts.IsNull ? null : (TimeSpan?)TimeSpan.FromTicks(((DateTime)nts).Ticks);

                case BaseTypes.DateTimeOffset:
                    var dto = reader.GetSqlDateTime(index);
                    return dto.IsNull ? default(DateTimeOffset) : DateTimeOffset.FromUnixTimeMilliseconds(((DateTime)dto).Ticks);
                case BaseTypes.NDateTimeOffset:
                    var ndto = reader.GetSqlDateTime(index);
                    return ndto.IsNull ? null : (DateTimeOffset?)DateTimeOffset.FromUnixTimeMilliseconds(((DateTime)ndto).Ticks);

                case BaseTypes.String:
                    return reader.GetString(index);

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
