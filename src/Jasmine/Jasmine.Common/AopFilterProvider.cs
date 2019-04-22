using Jasmine.Common.Exceptions;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AopFilterProvider<TContext> : IAopFilterProvider<TContext>
    {
        private ConcurrentDictionary<string, IFilter<TContext>> _map = new ConcurrentDictionary<string, IFilter<TContext>>();
        public int Count => _map.Count;

        public void AddFilter(IFilter<TContext> filter)
        {
            if(!_map.TryAdd(filter.Name,filter))
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

        public IFilter<TContext> GetFilter(string name)
        {
            return _map.TryGetValue(name, out var value) ? value : null;
        }

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
