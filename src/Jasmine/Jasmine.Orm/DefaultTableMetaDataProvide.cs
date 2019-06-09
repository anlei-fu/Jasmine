using System;
using System.Collections.Concurrent;

namespace Jasmine.Orm
{
    public class DefaultTableMetaDataProvider : ITableMetaDataProvider
    {
        private DefaultTableMetaDataProvider()
        {

        }
        private readonly ConcurrentDictionary<Type, TableMetaData> _tables = new ConcurrentDictionary<Type, TableMetaData>();
     
        public static readonly DefaultTableMetaDataProvider Instance = new DefaultTableMetaDataProvider();

        public TableMetaData GetTable<T>() => GetTable(typeof(T));
        public TableMetaData GetTable(Type type)
        {
            if (!Contains(type))
                Cache(type);

            return _tables.TryGetValue(type, out var result) ? result : null;
        }
        public void Cache<T>()
        {
            Cache(typeof(T));
        }
        public void Cache(Type type)
        {
            _tables.TryAdd(type, TableMetaDataReflectResolver.Instace.Resolve(type));
        }
        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }
        public bool Contains(Type type)
        {
            return _tables.ContainsKey(type);
        }
    }
}
