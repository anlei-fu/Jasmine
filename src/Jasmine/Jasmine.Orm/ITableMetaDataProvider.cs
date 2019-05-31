using System;
namespace Jasmine.Orm.Interfaces
{
    public interface ITableMetaDataProvider
    {
        /// <summary>
        /// return  a <see cref="TableMetaData"/> by give type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        TableMetaData GetTable(Type type);
    }
}
