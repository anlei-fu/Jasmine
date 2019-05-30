using Jasmine.Orm.Attributes;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using Jasmine.Reflection;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Orm.Implements
{
    public class DefaultTableMetaDataProvide : ITableMetaDataProvider
    {
        private DefaultTableMetaDataProvide()
        {

        }

        private readonly ConcurrentDictionary<Type, TableMetaData> _tables = new ConcurrentDictionary<Type, TableMetaData>();
        private readonly ITypeCache _reflection = JasmineReflectionCache.Instance;

        public static readonly DefaultTableMetaDataProvide Instance = new DefaultTableMetaDataProvide();
        public void Cache(Type type)
        {


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
