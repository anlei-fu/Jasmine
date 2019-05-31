using Jasmine.Orm.Model;
using System;

namespace Jasmine.Orm
{
    public interface IDataTypeMapper
    {
        /// <summary>
        /// get sql type by given <see cref="System.Type"/> ,<see cref="DataSourceType"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        string GetSqlType(Type type, DataSourceType dataSource);
        /// <summary>
        /// get <see cref="System.Type"/> by given sql type,<see cref="DataSourceType"/>
        /// </summary>
        /// <param name="sqlType"></param>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        Type GetCSharpType(string sqlType, DataSourceType dataSource);

    }
}
