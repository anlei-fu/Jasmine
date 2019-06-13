using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractGroup : IServiceGroup
    {
        private ConcurrentDictionary<string, IServiceItem> _innerMap=new ConcurrentDictionary<string, IServiceItem>();
        public int Count => _innerMap.Count;

        public bool AddItem(string name, IServiceItem item)
        {
            return _innerMap.TryAdd(name, item);
        }

        public void Clear()
        {
            _innerMap.Clear();
        }

        public bool ContainsItem(string name)
        {
            return _innerMap.ContainsKey(name);
        }

        public IEnumerator<IServiceItem> GetEnumerator()
        {
            foreach (var item in _innerMap.Values)
            {
                yield return item;
            }
        }

        public IServiceItem GetItem(string name)
        {
            return _innerMap.TryGetValue(name, out var result) ? result : null;
        }

        public void RemoveItem(string name)
        {
            _innerMap.TryRemove(name, out var _);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerMap.Values.GetEnumerator();
        }
    }

  
}
