using Jasmine.Common.Exceptions;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public abstract class AbstractAopFilterProvider<TContext> : IAopFilterProvider<TContext>
    {
        protected ConcurrentDictionary<string, IFilter<TContext>> _map = new ConcurrentDictionary<string, IFilter<TContext>>();
        public int Count => _map.Count;

        public void AddFilter(IFilter<TContext> filter)
        {
            if (!_map.TryAdd(filter.Name, filter))
            {
                throw new FilterAlreadyExistException($" filter ({filter.Name}) already exists! ");
            }
        }

        public void Clear()
        {
            _map.Clear();
        }

        public bool Contains(string name)
        {
            return _map.ContainsKey(name);
        }

        public IEnumerator<IFilter<TContext>> GetEnumerator()
        {
            foreach (var item in _map.Values)
            {
                yield return item;
            }
        }

        public abstract IFilter<TContext> GetFilter(string name);
       

        public void Remove(string name)
        {
            _map.TryRemove(name, out var _);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }
    }
}
