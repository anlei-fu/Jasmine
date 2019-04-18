using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractMetadataManager<TMetaData> : IDictionary<Type, TMetaData>
    {
        private ConcurrentDictionaryIDictonaryAdapter<Type, TMetaData> _innerDic = new ConcurrentDictionaryIDictonaryAdapter<Type, TMetaData>();
        public TMetaData this[Type key]
        {
            get => _innerDic[key];
            set
            {
                _innerDic[key] = value;
            }
        }

        public ICollection<Type> Keys => _innerDic.Keys;

        public ICollection<TMetaData> Values => _innerDic.Values;

        public int Count => _innerDic.Count;

        public bool IsReadOnly => _innerDic.IsReadOnly;

        public void Add(Type key, TMetaData value)
        {
            _innerDic.Add(key, value);
        }

        public void Add(KeyValuePair<Type, TMetaData> item)
        {
            _innerDic.Add(item);
        }

        public void Clear()
        {
            _innerDic.Clear();
        }

        public bool Contains(KeyValuePair<Type, TMetaData> item)
        {
            return _innerDic.Contains(item);
        }

        public bool ContainsKey(Type key)
        {
            return _innerDic.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Type, TMetaData>[] array, int arrayIndex)
        {
            _innerDic.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Type, TMetaData>> GetEnumerator()
        {
            foreach (var item in _innerDic)
            {
                yield return item;
            }
        }

        public bool Remove(Type key)
        {
          return  _innerDic.Remove(key);
        }

        public bool Remove(KeyValuePair<Type, TMetaData> item)
        {
            return _innerDic.Remove(item);
        }

        public bool TryGetValue(Type key, out TMetaData value)
        {
            return _innerDic.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerDic.GetEnumerator();
        }
    }
}
