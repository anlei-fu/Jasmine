using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractMetadataManager<TMetaData> : IDictionary<Type, TMetaData>
    {
        private ConcurrentDictionaryIDictonaryAdapter<Type, TMetaData> _innerMap = new ConcurrentDictionaryIDictonaryAdapter<Type, TMetaData>();
        public TMetaData this[Type key]
        {
            get => _innerMap[key];
            set
            {
                _innerMap[key] = value;
            }
        }

        public ICollection<Type> Keys => _innerMap.Keys;

        public ICollection<TMetaData> Values => _innerMap.Values;

        public int Count => _innerMap.Count;

        public bool IsReadOnly => _innerMap.IsReadOnly;

        public void Add(Type key, TMetaData value)
        {
            _innerMap.Add(key, value);
        }

        public void Add(KeyValuePair<Type, TMetaData> item)
        {
            _innerMap.Add(item);
        }

        public void Clear()
        {
            _innerMap.Clear();
        }

        public bool Contains(KeyValuePair<Type, TMetaData> item)
        {
            return _innerMap.Contains(item);
        }

        public bool ContainsKey(Type key)
        {
            return _innerMap.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Type, TMetaData>[] array, int arrayIndex)
        {
            _innerMap.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Type, TMetaData>> GetEnumerator()
        {
            foreach (var item in _innerMap)
            {
                yield return item;
            }
        }

        public bool Remove(Type key)
        {
          return  _innerMap.Remove(key);
        }

        public bool Remove(KeyValuePair<Type, TMetaData> item)
        {
            return _innerMap.Remove(item);
        }

        public bool TryGetValue(Type key, out TMetaData value)
        {
            return _innerMap.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerMap.GetEnumerator();
        }
    }
}
