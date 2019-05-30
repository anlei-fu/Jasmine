using Jasmine.Orm.Model;
using System;

namespace Jasmine.Orm
{
    public interface IDataTypeMapper
    {
        string GetSqlType(Type type, DataSourceType dataSource);
        Type GetCSharpType(string sqlType, DataSourceType dataSource);

    }
}
