using Jasmine.Reflection;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Ioc
{
    public class DefaultServiceMetaDataManager : IServiceMetaDataManager
    {


        private DefaultServiceMetaDataManager()
        {

        }
        public static readonly IServiceMetaDataManager Instance = new DefaultServiceMetaDataManager();
        private ConcurrentDictionary<Type, ServiceMetaData> _typeMap = new ConcurrentDictionary<Type, ServiceMetaData>();
        private ConcurrentDictionary<Type, Type> _impls = new ConcurrentDictionary<Type, Type>();

        public void SetImplement(Type @abstract,Type impl)
        {
            if (impl.IsInterfaceOrAbstraClass())
                throw new NotImplementedException();

            if (!_impls.ContainsKey(@abstract))
            {
                _impls.TryAdd(@abstract, impl);
            }
            else
            {
                _impls[@abstract] = impl;
            }

        }
        public Type GetImplement(Type @abstract)
        {
            return _impls.TryGetValue(@abstract, out var value) ? value : null;
        }


        public ServiceMetaData this[Type key]
        {
            get => _typeMap[key];
            set
            {
                _typeMap[key] = value;
            }
        }
       

        public ICollection<Type> Keys =>_typeMap.Keys;

        public ICollection<ServiceMetaData> Values => _typeMap.Values;

        public int Count => _typeMap.Count;

        public bool IsReadOnly => false;


        public void Add(Type key, ServiceMetaData value)
        {
            if (value == null)
                return;

            _typeMap.TryAdd(key, value);
        }

        public void Add(KeyValuePair<Type, ServiceMetaData> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(string key, ServiceMetaData value)
        {
            Add(value.RelatedType, value);
        }

        public void Add(KeyValuePair<string, ServiceMetaData> item)
        {
            Add(item.Value.RelatedType, item.Value);
        }

        public void Clear()
        {
            _typeMap.Clear();
        }

        public bool Contains(KeyValuePair<Type, ServiceMetaData> item)
        {
            return _typeMap.TryGetValue(item.Key, out var value) ? value == item.Value : false;
        }

     

        public bool ContainsKey(Type key)
        {
            return _typeMap.TryGetValue(key, out var result);
        }

      

        /// <summary>
        /// ignore
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<Type, ServiceMetaData>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// ignore
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<string, ServiceMetaData>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<Type, ServiceMetaData>> GetEnumerator()
        {
            foreach (var item in _typeMap)
            {
                yield return item;
            }
        }

        public bool Remove(Type key)
        {
            if(_typeMap.TryRemove(key,out var result))
            {
                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<Type, ServiceMetaData> item)
        {
            return Remove(item.Key);
        }


        public bool TryGetValue(Type key, out ServiceMetaData value)
        {
            return _typeMap.TryGetValue(key,out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeMap.GetEnumerator();
        }
    }
}
