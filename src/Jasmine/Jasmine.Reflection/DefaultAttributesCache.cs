using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Reflection.Implements
{
    public class DefaultAttributesCache : IAttributeCache
    {
        private ConcurrentDictionary<Type, List<Attribute>> _keyMap = new ConcurrentDictionary<Type, List<Attribute>>();
        private ConcurrentDictionary<string, List<Attribute>> _nameMap = new ConcurrentDictionary<string, List<Attribute>>();
        public void Cache(Attribute attr)
        {
            var type = attr.GetType();

            if (!_keyMap.ContainsKey(type))
            {
                _keyMap.TryAdd(type, new List<Attribute>());
                _nameMap.TryAdd(type.Name,_keyMap[type]);
            }

            _keyMap[type].Add(attr);
        }

        public bool Contains(string name)
        {
            return _nameMap.ContainsKey(name);
        }

        public bool Contains(Type attrType)
        {
            return _keyMap.ContainsKey(attrType);
        }

        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }
        public Attribute[] GetAttribute(string name)
        {
            return _nameMap.TryGetValue(name, out var result) ? result.ToArray() : null;
        }

        public Attribute[] GetAttribute(Type attrType)
        {
            return _keyMap.TryGetValue(attrType, out var result) ? result.ToArray() : null;
        }
        public T[] GetAttribute<T>() where T : Attribute
        {
            // need a cast

            var ls = new List<T>();

            foreach (var item in GetAttribute(typeof(T)))
                ls.Add((T)item);

            return ls.ToArray();
        }

        public IEnumerator<Attribute[]> GetEnumerator()
        {
            foreach (var item in _keyMap.Values)
            {
                yield return item.ToArray();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _keyMap.Values.GetEnumerator();
        }
    }
}
