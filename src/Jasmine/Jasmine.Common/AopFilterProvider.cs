using Jasmine.Common.Exceptions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public abstract class AbstractAopFilterProvider<TContext> : IAopFilterProvider<TContext>
        where TContext:IFilterContext
    {
      
        protected ConcurrentDictionary<Type, IFilter<TContext>> _map = new ConcurrentDictionary<Type, IFilter<TContext>>();
        
        public int Count => _map.Count;

        public void AddFilter(IFilter<TContext> filter)
        {
            _map.TryAdd(filter.GetType(), filter);
            
        }
        

        public void Clear()
        {
           
            _map.Clear();
        }

       
        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }
        public bool Contains(Type type)
        {
            return _map.ContainsKey(type);
        }

   

        public IEnumerator<IFilter<TContext>> GetEnumerator()
        {
            foreach (var item in _map.Values)
            {
                yield return item;
            }
        }


        public abstract IFilter<TContext> GetFilter<T>();


        public abstract IFilter<TContext> GetFilter(Type type);
       

       

        public void Remove<T>()
        {
            Remove(typeof(T));
        }

        public void Remove(Type type)
        {
            _map.TryRemove(type, out var filter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }
    }
}
