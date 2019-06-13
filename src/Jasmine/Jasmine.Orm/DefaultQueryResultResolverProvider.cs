using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Orm.Implements
{
    public class DefaultQueryResultResolverProvider : IQueryResultResolverProvider, IReadOnlyCollection<KeyValuePair<Type, IQueryResultResolver>>
    {
        private DefaultQueryResultResolverProvider()
        {

        }
        public static readonly IQueryResultResolverProvider Instance = new DefaultQueryResultResolverProvider();

        private ConcurrentDictionary<Type, IQueryResultResolver> _resolvers = new ConcurrentDictionary<Type, IQueryResultResolver>();

        public int Count => _resolvers.Count;

        public IQueryResultResolver GetResolver<T>()
        {
            return GetResolver(typeof(T));
        }

        public IQueryResultResolver GetResolver(Type type)
        {
            return _resolvers.TryGetValue(type, out var result) ?
                                                               result : DefaultQueryResultResolver.Instance;
        }

        public void Remove<T>()
        {
            Remove(typeof(T));
        }
        public void Remove(Type type)
        {
            _resolvers.TryRemove(type, out var result);
        }

        public void Add<T>(IQueryResultResolver convertor)
        {
            Add(typeof(T), convertor);
        }
        public void Add(Type type, IQueryResultResolver convertor)
        {
            if (_resolvers.ContainsKey(type))
                _resolvers[type] = convertor;
            else
                _resolvers.TryAdd(type, convertor);
        }

        public IEnumerator<KeyValuePair<Type, IQueryResultResolver>> GetEnumerator()
        {
            foreach (var item in _resolvers)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _resolvers.GetEnumerator();
        }
    }
}
