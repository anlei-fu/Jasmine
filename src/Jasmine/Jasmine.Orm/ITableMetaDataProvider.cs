using System;

namespace Jasmine.Orm.Interfaces
{
    public interface ITableMetaDataProvider
    {
        TableMetaData GetTable(Type type);
    }
}
