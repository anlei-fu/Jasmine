using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Reflection
{
    public abstract class AbstractReflectionCache<TContent, TRelated> : IReflectionCache<TContent, TRelated>
    {
        protected ConcurrentDictionary<string, TContent> _nameMap = new ConcurrentDictionary<string, TContent>();
        protected ConcurrentDictionary<TRelated, TContent> _keyMap = new ConcurrentDictionary<TRelated, TContent>();
        public int Count => _nameMap.Count;

       

        public abstract void Cache(TRelated info);
      

        public bool Contains(TRelated info) => _keyMap.ContainsKey(info);


        public bool Contains(string name) => _nameMap.ContainsKey(name);


        public IEnumerable<TContent> GetAll() => _nameMap.Values.ToArray();


        public string[] GetAllNames() => _nameMap.Keys.ToArray();

        public IEnumerator<TContent> GetEnumerator()
        {
            foreach (var item in _keyMap.Values)
            {
                yield return item;
            }
        }

        public virtual TContent GetItem(TRelated info) => _keyMap.TryGetValue(info, out var result) ? result : default(TContent);


        public TContent GetItemByName(string name) => _nameMap.TryGetValue(name, out var result) ? result : default(TContent);

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in _keyMap.Values)
            {
                yield return item;
            }
        }
    }
}
