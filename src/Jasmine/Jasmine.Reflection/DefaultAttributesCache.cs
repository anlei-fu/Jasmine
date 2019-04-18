using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Reflection.Implements
{
    public class DefaultAttributesCache : IAttributeCache
    {
        public ConcurrentDictionary<Type, Attribute> _keyMap = new ConcurrentDictionary<Type, Attribute>();
        public ConcurrentDictionary<string, Attribute> _nameMap = new ConcurrentDictionary<string, Attribute>();

        public void Cache(Attribute attr)
        {
            if(_keyMap.TryAdd(attr.GetType(),attr))
            {
                _nameMap.TryAdd(attr.GetType().Name, attr);
            }
        }

        public bool Contains(string name)
        {
            return _nameMap.ContainsKey(name);
        }

        public bool Contains(Type attrType)
        {
            return _keyMap.ContainsKey(attrType);
        }

        public Attribute GetAttribute(string name)
        {
            return _nameMap.TryGetValue(name, out var result) ? result : null;
        }

        public Attribute GetAttribute(Type attrType)
        {
            return _keyMap.TryGetValue(attrType, out var result) ? result : null;
        }

        public T GetAttribute<T>() where T : Attribute
        {
            return (T)GetAttribute(typeof(T));
        }

        public IEnumerator<Attribute> GetEnumerator()
        {
            foreach (var item in _keyMap.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in _keyMap.Values)
            {
                yield return item;
            }
        }
    }
}
