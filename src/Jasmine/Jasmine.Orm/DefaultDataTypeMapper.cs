using System;
using System.Collections.Generic;
using Jasmine.Orm.Model;

namespace Jasmine.Orm
{
    public class DefaultDataTypeMapper : IDataTypeMapper
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

        private static readonly Dictionary<Type, string> _oracleqlSc = new Dictionary<Type, string>()
        {

        };
        private static readonly Dictionary<Type, string> _orackeCs = new Dictionary<Type, string>()
        {

        };

        private static readonly Dictionary<Type, string> _sqlLiteqlSc = new Dictionary<Type, string>()
        {

        };
        private static readonly Dictionary<Type, string> _sqlLiteCs = new Dictionary<Type, string>()
        {

        };

        public static readonly IDataTypeMapper Instace = new DefaultDataTypeMapper();
        public Type GetCSharpType(string sqlType, DataSource dataSource)
        {
            if (sqlType == null)
                throw new ArgumentNullException(nameof(sqlType));

            sqlType=  sqlType.Trim();

            var index = sqlType.IndexOf("(");

            if (index != -1)
                sqlType = sqlType.Substring(0, index);

            switch (dataSource)
            {
                case DataSource.SqlServer:

                    return _sqlServerSc.TryGetValue(sqlType,out var sqlServerV)?sqlServerV:null;

                case DataSource.Oracle:
                    break;
                case DataSource.MySql:

                    return _mysqlSc.TryGetValue(sqlType, out var mysqlV) ? mysqlV : null;

                case DataSource.Db2:
                    break;
                case DataSource.Sqlite:
                    break;
                case DataSource.Access:
                    break;
                default:
                    break;
            }

            return null;
        }

        public string GetSqlType(Type type, DataSource dataSource)
        {
            switch (dataSource)
            {
                case DataSource.SqlServer:

                    return _sqlServerCs.TryGetValue(type, out var sqlServerV) ? sqlServerV : null;

                case DataSource.Oracle:

                    break;

                case DataSource.MySql:

                    return _mysqlCs.TryGetValue(type, out var mysqlV) ? mysqlV : null;

                case DataSource.Db2:
                    break;

                case DataSource.Sqlite:
                    break;

                case DataSource.Access:
                    break;

                default:
                    break;
            }

            return null;
        }
    }
}
