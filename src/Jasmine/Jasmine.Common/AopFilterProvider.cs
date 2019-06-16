using Jasmine.Common.Exceptions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public abstract class AbstractAopFilterProvider<TContext> : IAopFilterProvider<TContext>
    {
        protected ConcurrentDictionary<string, Type> _nameMap = new ConcurrentDictionary<string, Type>();
        protected ConcurrentDictionary<Type, IFilter<TContext>> _map = new ConcurrentDictionary<Type, IFilter<TContext>>();
        
        public int Count => _map.Count;

        public void AddFilter(IFilter<TContext> filter)
        {
            if(!_nameMap.TryAdd(filter.Name, filter.GetType())&&_nameMap[filter.Name]!=filter.GetType())
            {
                throw new FilterAlreadyExistException($" giving filter ({filter}) 's name alreadey exists in {_nameMap[filter.Name]} ");
            }

            _map.TryAdd(filter.GetType(), filter);
            
        }

        public abstract void AddFilter(string name, Type type);


        public abstract void AddFilter<T>() where T : IFilter<TContext>;
        

        public void Clear()
        {
            _nameMap.Clear();
            _map.Clear();
        }

        public bool Contains(string name)
        {
            return _nameMap.TryGetValue(name,out var type)?_map.ContainsKey(type):false;
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

        public abstract IFilter<TContext> GetFilter(string name);

        public abstract IFilter<TContext> GetFilter<T>();


        public abstract IFilter<TContext> GetFilter(Type type);
       

        public void Remove(string name)
        {
           if( _nameMap.TryRemove(name, out var type))
            {
                _map.TryRemove(type, out var _);
            }
        }

        public void Remove<T>()
        {
            Remove(typeof(T));
        }

        public void Remove(Type type)
        {
           if(_map.TryRemove(type, out var filter))
            {
               _nameMap.TryRemove(filter.Name, out var _);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }
    }
}
