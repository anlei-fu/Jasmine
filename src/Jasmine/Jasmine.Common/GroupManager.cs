using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractGroupManager 
    {
        private ConcurrentDictionary<string, IServiceGroup> _innerMap;

        public int Count => _innerMap.Count;

        public bool AddGroup(string name, IServiceGroup group)
        {
            return _innerMap.TryAdd(name, group);
        }

        public void Clear()
        {
            _innerMap.Clear();
        }

        public bool ContainsGroup(string name)
        {
            return _innerMap.ContainsKey(name);
        }

        public IEnumerator<IServiceGroup> GetEnumerator()
        {
            foreach (var item in _innerMap.Values)
            {
                yield return item;
            }
        }

        public IServiceGroup GetGroup(string name)
        {
            return _innerMap.TryGetValue(name, out var result) ? result : null;
        }

        public void RemoveGroup(string name)
        {
            _innerMap.TryRemove(name, out var _);
        }

       
    }
}
