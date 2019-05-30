using Jasmine.Orm.Interfaces;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Orm.Implements
{
    public   class DefaultSqlConvertorProvider:IOrmConvertorProvider
    {

        private DefaultSqlConvertorProvider()
        {

        }
        public static readonly IOrmConvertorProvider Instance = new DefaultSqlConvertorProvider();

        private ConcurrentDictionary<Type, IOrmConvertor> _convertors = new ConcurrentDictionary<Type, IOrmConvertor>();

        public IOrmConvertor GetConvertor<T>()
        {
            return GetConvertor(typeof(T));
        }

        public IOrmConvertor GetConvertor(Type type)
        {
            return _convertors.TryGetValue(type, out var result) ?
                                                               result : DefaultSqlConvertor.Instance;
        }

        public void RemoveConvertor<T>()
        {
            RemoveConvertor(typeof(T));
        }
        public void RemoveConvertor(Type type)
        {
            _convertors.TryRemove(type, out var result);
        }

        public void AddConvertor<T>(IOrmConvertor convertor)
        {
            AddConvertor(typeof(T),convertor);
        }
        public void AddConvertor(Type type, IOrmConvertor convertor)
        {
            if (_convertors.ContainsKey(type))
                _convertors[type] = convertor;
            else
                _convertors.TryAdd(type, convertor);
        }
    }
}
