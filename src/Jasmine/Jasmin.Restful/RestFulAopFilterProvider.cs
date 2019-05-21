using Jasmine.Common;
using Jasmine.Ioc;
using Jasmine.Restful.Exceptions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class RestfulAopFilterProvider : IAopFilterProvider<HttpFilterContext>
    {
        private RestfulAopFilterProvider()
        {

        }
        public static IAopFilterProvider<HttpFilterContext> Instance = new RestfulAopFilterProvider();

        private IServiceProvider _serviceProvider => IocServiceProvider.Instance;

        private ConcurrentDictionary<string, IFilter<HttpFilterContext>> _map = new ConcurrentDictionary<string, IFilter<HttpFilterContext>>();
        public int Count => _map.Count;

        public void AddFilter(IFilter<HttpFilterContext> filter)
        {
            if (!_map.TryAdd(filter.Name, filter))
            {

            }
        }

        public void Clear()
        {
            _map.Clear();
        }

        public bool Contains(string name)
        {
            return _map.ContainsKey(name);
        }

        public IEnumerator<IFilter<HttpFilterContext>> GetEnumerator()
        {
            foreach (var item in _map.Values)
            {
                yield return item;
            }
        }

        public IFilter<HttpFilterContext> GetFilter(string name)
        {
            if(!_map.ContainsKey(name))
            {
                var instance = _serviceProvider.GetService(Type.GetType(name));

                if(instance==null)
                {
                    throw new AopFilterCanNotBeCreatedException($"{name} can not be created!");
                }

                _map.TryAdd(name, (IFilter<HttpFilterContext>)instance);
            }


            return _map.TryGetValue(name, out var result) ? result : null;
        }


        public void Remove(string name)
        {
            _map.TryRemove(name, out var _);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }
    }
}
