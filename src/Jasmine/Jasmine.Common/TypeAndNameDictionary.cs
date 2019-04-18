using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class TypeAndNameDictionary<T> : IDictionary<Type, T>, IDictionary<string, T>
        where T: INameFearture, ITypeFearture 
    {

        private IDictionary<string, T> _strMap;
        private IDictionary<Type, T> _typeMap;
        public TypeAndNameDictionary(IDictionary<string,T> strMap,IDictionary<Type,T>typeMap)
        {
            _strMap = strMap??throw new ArgumentNullException();
            _typeMap = typeMap??throw new ArgumentNullException();
        }
        public T this[Type key]
        {
            get =>_typeMap[key];
            set
            {
                _typeMap[key] = value;
            }
        }
        public T this[string key]
        {

            get => _strMap[key];
            set
            {
                _strMap[key] = value;
            }
        }

        public ICollection<Type> Keys =>_typeMap.Keys;

        public ICollection<T> Values => _typeMap.Values;

        public int Count => _typeMap.Count;

        public bool IsReadOnly => false;

        ICollection<string> IDictionary<string, T>.Keys => _strMap.Keys;

        public void Add(Type key, T value)
        {
            if (!_typeMap.ContainsKey(key))
                _typeMap.Add(key, value);


            if (!_strMap.ContainsKey(value.Name))
                _strMap.Add(value.Name, value);
        }

        public void Add(KeyValuePair<Type, T> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(string key, T value)
        {
            Add(value.RelatedType, value);
        }

        public void Add(KeyValuePair<string, T> item)
        {
            Add(item.Value.RelatedType, item.Value);
        }

        public void Clear()
        {
            _strMap.Clear();
            _typeMap.Clear();
        }

        public bool Contains(KeyValuePair<Type, T> item)
        {
            return _typeMap.Contains(item);
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            return _strMap.Contains(item);
        }

        public bool ContainsKey(Type key)
        {
            return _typeMap.ContainsKey(key);
        }

        public bool ContainsKey(string key)
        {
            return _strMap.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Type, T>[] array, int arrayIndex)
        {
            _typeMap.CopyTo(array, arrayIndex);
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            _strMap.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Type, T>> GetEnumerator()
        {
            foreach (var item in _typeMap)
            {
                yield return item;
            }
        }

        public bool Remove(Type key)
        {
            if(_typeMap.ContainsKey(key))
            {
                var value = _typeMap[key];

                _strMap.Remove(value.Name);
                _typeMap.Remove(key);

                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<Type, T> item)
        {
            return Remove(item.Key);
        }

        public bool Remove(string key)
        {

            return _strMap.ContainsKey(key)? Remove(_strMap[key].RelatedType):false;
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(Type key, out T value)
        {
            return _typeMap.TryGetValue(key, out value);
        }

        public bool TryGetValue(string key, out T value)
        {
            return _strMap.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeMap.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator()
        {
            foreach (var item in _strMap)
            {
                yield return item;
            }
        }
    }
}
