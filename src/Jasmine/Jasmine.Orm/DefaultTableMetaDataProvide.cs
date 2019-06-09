using Jasmine.Orm.Interfaces;
using Jasmine.Reflection;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Orm.Implements
{
    public class DefaultTableMetaDataProvider : ITableMetaDataProvider
    {
        private DefaultTableMetaDataProvider()
        {

        }
        private readonly ConcurrentDictionary<Type, TableMetaData> _tables = new ConcurrentDictionary<Type, TableMetaData>();
        private readonly ITypeCache _reflection = JasmineReflectionCache.Instance;

        public static readonly DefaultTableMetaDataProvider Instance = new DefaultTableMetaDataProvider();
        public void Cache(Type type)
        {
            _tables.TryAdd(type, TableMetaDataReflectResolver.Instace.Resolve(type));

        }
        public bool Contains(Type type)
        {
            return _tables.ContainsKey(type);
        }

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
        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }
    }
}
