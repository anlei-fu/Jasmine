using Jasmine.Orm.Interfaces;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Orm.Implements
{
    public   class DefaultSqlConvertorProvider:ISqlConvertorProvider
    {

        private DefaultSqlConvertorProvider()
        {

        }
        public static readonly ISqlConvertorProvider Instance = new DefaultSqlConvertorProvider();

        private ConcurrentDictionary<Type, ISqlConvertor> _convertors = new ConcurrentDictionary<Type, ISqlConvertor>();

        public ISqlConvertor GetConvertor<T>()
        {
            return GetConvertor(typeof(T));
        }

        public ISqlConvertor GetConvertor(Type type)
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

        public void AddConvertor<T>(ISqlConvertor convertor)
        {
            AddConvertor(typeof(T),convertor);
        }
        public void AddConvertor(Type type, ISqlConvertor convertor)
        {
            if (_convertors.ContainsKey(type))
                _convertors[type] = convertor;
            else
                _convertors.TryAdd(type, convertor);
        }
    }
}
