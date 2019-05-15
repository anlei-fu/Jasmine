using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractGroup : IGroup
    {
        private ConcurrentDictionary<string, IGroupItem> _innerMap;
        public int Count => _innerMap.Count;

        public bool AddItem(string name, IGroupItem item)
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

        public IEnumerator<IGroupItem> GetEnumerator()
        {
            foreach (var item in _innerMap.Values)
            {
                yield return item;
            }
        }

        public IGroupItem GetItem(string name)
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
