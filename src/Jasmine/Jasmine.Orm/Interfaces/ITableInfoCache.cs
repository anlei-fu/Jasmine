using Jasmine.Orm.Model;
using System;

namespace Jasmine.Orm.Interfaces
{
    public  interface ITableInfoCache
    {
        void Cache<T>();
        void Cache(Type type);
        Table GetTable(Type type);
        Table GetTable<T>();
        bool ContainsTable<T>();
        bool ContainsTable(Type table);
    }
}
