using System;
namespace Jasmine.Orm
{
    public interface ITableMetaDataProvider
    {
        /// <summary>
        /// return  a <see cref="TableMetaData"/> by given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        TableMetaData GetTable(Type type);
    }
}
