using Jasmine.Orm.Implements;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public class MySqlDataTypeMapper : IDataTypeMapper
    {
        private static readonly Dictionary<string, Type> _mysqlSc = new Dictionary<string, Type>()
        {
          {"tinyint",typeof(byte) },
          {"samllint",typeof(short) },
          {"mediumint",typeof(int) },
          {"int",typeof(int) },
          {"interger",typeof(int) },
          {"bigint",typeof(long) },
          {"boolean",typeof(bool) },
          {"decimal",typeof(decimal) },
          {"float",typeof(float) },
          {"double",typeof(double) },
          {"date",typeof(DateTime) },
          {"datetime",typeof(DateTime) },
          {"time",typeof(TimeSpan) },
          {"year",typeof(DateTime) },
          {"timestamp",typeof(DateTime) },
          {"tinyblob",typeof(string) },
          { "mediumblob",typeof(string)},
          { "longblob",typeof(string)},
          {"blob",typeof(string) },
          {"char",typeof(string) },
          {"vachar",typeof(string) },
          {"tinytext",typeof(string) },
          { "mediumtext",typeof(string)},
          {"text",typeof(string) },
          {"longtext",typeof(string) },
        };
        private static readonly Dictionary<Type, string> _mysqlCs = new Dictionary<Type, string>()
        {
          {typeof(sbyte),"tinyint" },
          {typeof(sbyte?),"tinyint" },
          {typeof(byte),"tinyint" },
          {typeof(byte?),"tinyint" },
          {typeof(ushort) ,"samllint"},
          {typeof(ushort?) ,"samllint"},
          {typeof(short) ,"samllint"},
          {typeof(short?) ,"samllint"},
          {typeof(uint) ,"int"},
          {typeof(uint?) ,"int"},
          {typeof(int) ,"int"},
          {typeof(int?) ,"int"},
          {typeof(long),"bigint"},
          {typeof(long?),"bigint"},
          {typeof(bool) ,"bit"},
          {typeof(bool?) ,"bit"},
          {typeof(float),"float"},
          {typeof(float?),"float"},
          {typeof(double),"double" },
          {typeof(double?),"double" },
          {typeof(decimal),"decimal"},
          {typeof(decimal?),"decimal"},
          {typeof(DateTime),"datetime" },
          {typeof(DateTime?),"datetime" },
          {typeof(DateTimeOffset),"timestamp" },
          {typeof(DateTimeOffset?),"timestamp" },
          {typeof(TimeSpan),"time" },
          {typeof(TimeSpan?),"time" },
          {typeof(string),"text" },

        };
        public object DoExplictConvert(Type destination, object source)
        {
            throw new NotImplementedException();
        }

        public Type GetCSharpType(string sqlType, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string GetSqlType(Type type, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string ToSqlString(Type type, object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class OracleDataTypeMapper : IDataTypeMapper
    {
        private static readonly Dictionary<string, Type> _oracleqlSc = new Dictionary<string, Type>()
        {    
            //CHAR(size)：固定长度字符串，最大长度2000 bytes
            {"char",typeof(string) },
            //VARCHAR2(size)：可变长度的字符串，最大长度4000 bytes，可做索引的最大
            {"varchar2",typeof(string) },
            //NCHAR(size)：根据字符集而定的固定长度字符串，最大长度2000 bytes
            { "nchar",typeof(string)},
            //NVARCHAR2(size)：根据字符集而定的可变长度字符串，最大长度4000 byte
            {"nvarchar",typeof(string) },
            //LONG：变长的字符串，最大长度限制是2GB
            {"long",typeof(string) },
            //CLOB：最大长度4G
            {"clob",typeof(string) },
            //NCLOB：根据字符集而定的字符数据，最大长度4G
            {"nclob",typeof(string) },
            //
            {"number",typeof(double) },
            //
            {"date",typeof(DateTime) },
            { "timestamp",typeof(DateTime)},
            //BLOB：最大长度4G
            {"blob",typeof(byte[]) },
            //RAW ：可变长度的二进制数据，最大长度2000 字节
            {"raw",typeof(byte[]) },
            //LONG RAW：可变长度的二进制数据，最大长度2G
            { "longraw",typeof(byte[])},
            //BFILE：存放在数据库外的二进制数据，最大长度4G
            { "bfile",typeof(byte[])}


        };
        private static readonly Dictionary<Type, string> _orackeCs = new Dictionary<Type, string>()
        {
          {typeof(sbyte),"number(1)" },
          {typeof(sbyte?),"number(1)" },
          {typeof(byte),"number(1)" },
          {typeof(byte?),"number(1)" },
          {typeof(ushort) ,"number(5)"},
          {typeof(ushort?) ,"number(5)"},
          {typeof(short) ,"number(5)"},
          {typeof(short?) ,"number(5)"},
          {typeof(uint) ,"number(10)"},
          {typeof(uint?) ,"number(10)"},
          {typeof(int) ,"number(10)"},
          {typeof(int?) ,"number(10)"},
          {typeof(long),"number"},
          {typeof(long?),"number"},
          {typeof(bool) ,"number(1)"},
          {typeof(bool?) ,"number(1)"},
          {typeof(float),"number(7,3)"},
          {typeof(float?),"number(7,3)"},
          {typeof(double),"number(15,5)" },
          {typeof(double?),"number(15,5)" },
          {typeof(decimal),"number"},
          {typeof(decimal?),"number"},
          {typeof(DateTime),"timestamp" },
          {typeof(DateTime?),"timestamp" },
          {typeof(DateTimeOffset),"timestamp" },
          {typeof(DateTimeOffset?),"timestamp" },
          {typeof(TimeSpan),"tiemstamp" },
          {typeof(TimeSpan?),"timestamp" },
          {typeof(string),"text" },
          {typeof(byte[]),"bfile" }
        };
        public object DoExplictConvert(Type destination, object source)
        {
            throw new NotImplementedException();
        }

        public Type GetCSharpType(string sqlType, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string GetSqlType(Type type, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string ToSqlString(Type type, object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class Db2DataTypeMapper : IDataTypeMapper
    {
        private static readonly Dictionary<string, Type> _db2Sc = new Dictionary<string, Type>()
        {
            //n bytes定长字符串. n 大于0 不大于255. 默认 1.
            {"character",typeof(char)},
            //变长字符串，最大 n bytes. n大于 0 小于表空间的 page size. 最大32704.
            {"varchar",typeof(string) },
            //变长字符串，最大2 147 483 647.默认1.
            {"clob",typeof(string) },
            //定长图形字符串， n 个双字节字符. n 大于 0 小于128. 默认 1.
            {"graphic",typeof(string) },
            //双字节变长字符串， n不能超过 1 073 741 824.默认1.
            {"dbclob",typeof(string) },
            //定长或变长二进制字符串. n 大于 0 不大于 255. 默认1.
            {"binary",typeof(string) },
            //变长二进制字符串，n大于 0小于表空间的 page size. 最大 32704.
            {"varbinary",typeof(string) },
            //变长二进制字符串，n 不大于 2 147 483 647. 默认 1.
            {"blob",typeof(string) },
            //小整数，精度（即通常说的长度） 15 bits. 范围 -32768 到 +32767.
            {"samllint",typeof(short) },
            //整数，精度 31 bits的二进制整数，范围 -2147483648 到 +2147483647.
            {"int",typeof(int) },
            {"integer",typeof(int) },
            //大整数，精度 63 bits二进制整数，范围 -9223372036854775808 到 +9223372036854775807.
            {"bigint",typeof(long) },
            //压缩十进制数，小数点位置由precision和scale决定，scale非负且小于精度.最大精度 31 digits.decimal 列中的值有同样的precision 和 scale.范围 1 - 10³¹ 到 10³¹ - 1.
            {"decimal",typeof(decimal) },
            {"numeric",typeof(decimal) },
            //十进制浮点数，最大精度 34 位.（早期DB2版本不支持）
            {"decfloat",typeof(float) },
            //单精度浮点数，32 bits.范围大约为 -7.2E+75 到 7.2E+75.最大负值约为 -5.4E-79, 最小正值约为 5.4E-079.
            {"real",typeof(float) },
            //双精度浮点数，64-bits. 范围大约为 -7.2E+75 到 7.2E+75.最大负值约为 -5.4E-79, 最小正值约为 5.4E-079.
            { "double",typeof(double)},
            //年月日组成的日期，范围 0001-01-01 到 9999-12-31.
            { "date",typeof(DateTime)},
            //时分秒组成的时间，范围 00.00.00 到 24.00.00.
            {"time",typeof(DateTime) },
            //年月日时分秒微妙组成的时间，范围 0001-01-01-00.00.00.000000000 到 9999-12-31-24.00.00.000000000 精确到纳秒. 可保存时区信息
            {"tiemspan",typeof(DateTime) }
        };
        private static readonly Dictionary<Type, string> _db2Cs = new Dictionary<Type, string>()
        {
            {typeof(sbyte),"character" },
          {typeof(sbyte?),"character" },
          {typeof(byte),"character" },
          {typeof(byte?),"character" },
          {typeof(ushort) ,"smallint"},
          {typeof(ushort?) ,"samllint"},
          {typeof(short) ,"samllint"},
          {typeof(short?) ,"samllint"},
          {typeof(uint) ,"int"},
          {typeof(uint?) ,"int"},
          {typeof(int) ,"int"},
          {typeof(int?) ,"int"},
          {typeof(long),"bigint"},
          {typeof(long?),"bigint"},
          {typeof(bool) ,"character"},
          {typeof(bool?) ,"character"},
          {typeof(float),"float"},
          {typeof(float?),"float"},
          {typeof(double),"double" },
          {typeof(double?),"double" },
          {typeof(decimal),"decimal"},
          {typeof(decimal?),"decimal"},
          {typeof(DateTime),"timestamp" },
          {typeof(DateTime?),"timestamp" },
          {typeof(DateTimeOffset),"timestamp" },
          {typeof(DateTimeOffset?),"timestamp" },
          {typeof(TimeSpan),"time" },
          {typeof(TimeSpan?),"time" },
          {typeof(string),"text" },
        };
        public object DoExplictConvert(Type destination, object source)
        {
            throw new NotImplementedException();
        }

        public Type GetCSharpType(string sqlType, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string GetSqlType(Type type, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string ToSqlString(Type type, object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class SqliteDataTypeMapper : IDataTypeMapper
    {
        private static readonly Dictionary<Type, string> _sqlLiteqlSc = new Dictionary<Type, string>()
        {

        };
        private static readonly Dictionary<Type, string> _sqlLiteCs = new Dictionary<Type, string>()
        {

        };
        public object DoExplictConvert(Type destination, object source)
        {
            throw new NotImplementedException();
        }

        public Type GetCSharpType(string sqlType, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string GetSqlType(Type type, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string ToSqlString(Type type, object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class PostgreDataTypeMapper : IDataTypeMapper
    {
        private static readonly Dictionary<string, Type> _postgre2Sc = new Dictionary<string, Type>()
        {
            //有符号 8 字节整数
            {"bigint",typeof(long)},
              //有符号 8 字节整数
            {"char",typeof(char)},
            //自增八字节整数
            {"bigserial",typeof(long)},
            //定长位串
            {"bit",typeof(byte) },
            //逻辑布尔量 （真/假）
            {"bool",typeof(bool) },
            //
            {"int2",typeof(short) },
            {"int4",typeof(int) },
            {"int8",typeof(long) },
            //二进制数据（"字节数组"）
            {"bytea",typeof(byte[]) },
            //变长字符串
            {"character varying",typeof(string) },
            //定长字符串
            {"character",typeof(string)},
             //定长字符串
            {"varchar",typeof(string)},
            //日历日期（年，月，日）
            {"date",typeof(DateTime) },
            //双精度浮点数字
            { "float8",typeof(double)},
            //可选精度的准确数字 p,s as oracle
            {"numeric",typeof(decimal) },
            //单精度浮点数
            {"float4",typeof(float) },
            //有符号两字节整数
            { "samllint",typeof(short)},
            //自增四字节整数
            { "serial",typeof(int)},
            //变长字符串
            {"text",typeof(string) },
            //日期和时间
            { "timestamp",typeof(DateTime)},
            //日期和时间
            { "time",typeof(TimeSpan)},
             //日期和时间
            { "timetz",typeof(DateTimeOffset)},
            //通用唯一标识符
            {"uuid",typeof(Guid) },

        };
        private static readonly Dictionary<Type, string> _postgre2Cs = new Dictionary<Type, string>()
        {
          {typeof(sbyte),"int2" },
          {typeof(sbyte?),"int2" },
          {typeof(byte),"int2" },
          {typeof(byte?),"int2" },
          {typeof(ushort) ,"int2"},
          {typeof(ushort?) ,"int2"},
          {typeof(short) ,"int2"},
          {typeof(short?) ,"int2"},
          {typeof(uint) ,"int4"},
          {typeof(uint?) ,"int4"},
          {typeof(int) ,"int4"},
          {typeof(int?) ,"int4"},
          {typeof(long),"int8"},
          {typeof(long?),"int8"},
          {typeof(bool) ,"bool"},
          {typeof(bool?) ,"bool"},
          {typeof(float),"float4"},
          {typeof(float?),"float4"},
          {typeof(double),"float8" },
          {typeof(double?),"float8" },
          {typeof(decimal),"numeric"},
          {typeof(decimal?),"numeric"},
          {typeof(DateTime),"timestamp" },
          {typeof(DateTime?),"timestamp" },
          {typeof(DateTimeOffset),"timetz" },
          {typeof(DateTimeOffset?),"timetz" },
          {typeof(TimeSpan),"time" },
          {typeof(TimeSpan?),"time" },
          {typeof(byte[]) ,"bytea"},
          {typeof(string),"text" },

        };
        public object DoExplictConvert(Type destination, object source)
        {
            throw new NotImplementedException();
        }

        public Type GetCSharpType(string sqlType, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string GetSqlType(Type type, DataSource dataSource)
        {
            throw new NotImplementedException();
        }

        public string ToSqlString(Type type, object obj)
        {
            throw new NotImplementedException();
        }
    }

    public class SqlServerDataTypeMapper : IDataTypeMapper
    {
        private static readonly Dictionary<string, Type> _sqlServerSc = new Dictionary<string, Type>()
        {
          {"tinyint",typeof(byte) },
          {"samllint",typeof(short) },
          {"int",typeof(int) },
          {"bigint",typeof(long) },
          {"bit",typeof(bool) },
          {"numeric",typeof(float) },
          {"float",typeof(double) },
          {"decimal",typeof(decimal) },
          {"money",typeof(double) },
          {"datetime",typeof(DateTime) },
          {"smalldatetime",typeof(TimeSpan) },
          {"image",typeof(byte[]) },
          {"char",typeof(string) },
          {"nchar",typeof(string) },
          {"vachar",typeof(string) },
          {"text",typeof(string) },
          {"ntext",typeof(string) },

        };
        private static readonly Dictionary<Type, string> _sqlServerCs = new Dictionary<Type, string>()
        {
          {typeof(sbyte),"tinyint" },
          {typeof(sbyte?),"tinyint" },
          {typeof(byte),"tinyint" },
          {typeof(byte?),"tinyint" },
          {typeof(ushort) ,"samllint"},
          {typeof(ushort?) ,"samllint"},
          {typeof(short) ,"samllint"},
          {typeof(short?) ,"samllint"},
          {typeof(uint) ,"int"},
          {typeof(uint?) ,"int"},
          {typeof(int) ,"int"},
          {typeof(int?) ,"int"},
          {typeof(long),"bigint"},
          {typeof(long?),"bigint"},
          {typeof(bool) ,"bit"},
          {typeof(bool?) ,"bit"},
          {typeof(float),"numeric"},
          {typeof(float?),"numeric"},
          {typeof(double),"float" },
          {typeof(double?),"float" },
          {typeof(decimal),"decimal"},
          {typeof(decimal?),"decimal"},
          {typeof(DateTime),"datetime" },
          {typeof(DateTime?),"datetime" },
          {typeof(DateTimeOffset),"datetime" },
          {typeof(DateTimeOffset?),"datetime" },
          {typeof(TimeSpan),"smalldatetime" },
          {typeof(TimeSpan?),"smalldatetime" },
          {typeof(byte[]) ,"image"},
          {typeof(string),"text" },


        };

       

        public static readonly IDataTypeMapper Instace = new SqlServerDataTypeMapper();


       

        public Type GetCSharpType(string sqlType, DataSource dataSource)
        {
            if (sqlType == null)
                throw new ArgumentNullException(nameof(sqlType));

            sqlType=  sqlType.Trim();

            var index = sqlType.IndexOf("(");

            if (index != -1)
                sqlType = sqlType.Substring(0, index);

           

            return _sqlServerSc[sqlType];
        }

        public string GetSqlType(Type type, DataSource dataSource)
        {
            

            return _sqlServerCs[type];
        }

        public string ToSqlString(Type type, object obj)
        {
            return DefaultBaseTypeConvertor.Instance.ConvertToSqlString(type, obj);
        }

        public object DoExplictConvert(Type destination, object source)
        {
            return DefaultBaseTypeConvertor.Instance.FromSqlFiledValue(source, destination);
        }
    }
}
