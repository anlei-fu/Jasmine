using System;

namespace Jasmine.Orm
{
    public interface IDataTypeMapper
    {
        /// <summary>
        /// get sql type by given <see cref="System.Type"/> ,<see cref="DataSource"/>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        string GetSqlType(Type type, DataSource dataSource);
        /// <summary>
        /// get <see cref="System.Type"/> by given sql type,<see cref="DataSource"/>
        /// </summary>
        /// <param name="sqlType"></param>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        Type GetCSharpType(string sqlType, DataSource dataSource);

        string ToSqlString(Type type,object obj);
        object DoExplictConvert(Type destination, object source);

    }
}
