using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class ConcurrentDictionaryIDictonaryAdapter<Tkey, TValue> : IDictionary<Tkey, TValue>
    {
        private ConcurrentDictionary<Tkey, TValue> _innerDic = new ConcurrentDictionary<Tkey, TValue>();
        public TValue this[Tkey key] { get => _innerDic[key]; set => _innerDic[key]=value; }

        public ICollection<Tkey> Keys => _innerDic.Keys;

        public ICollection<TValue> Values => _innerDic.Values;

        public int Count => _innerDic.Count;

        public bool IsReadOnly => false;

        public void Add(Tkey key, TValue value)
        {
            _innerDic.TryAdd(key, value);
        }

        public void Add(KeyValuePair<Tkey, TValue> item)
        {
            _innerDic.TryAdd(item.Key, item.Value);
        }

        public void Clear()
        {
            _innerDic.Clear();
        }

        public bool Contains(KeyValuePair<Tkey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(Tkey key)
        {
            return _innerDic.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Tkey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<Tkey, TValue>> GetEnumerator()
        {
            foreach (var item in _innerDic)
            {
                yield return item;
            }
        }

        public bool Remove(Tkey key)
        {
           return  _innerDic.TryRemove(key, out var _);
        }

        public bool Remove(KeyValuePair<Tkey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(Tkey key, out TValue value)
        {
            return _innerDic.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerDic.GetEnumerator();
        }
    }
}
