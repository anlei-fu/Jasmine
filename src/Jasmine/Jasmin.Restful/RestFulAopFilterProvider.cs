using Jasmine.Common;
using Jasmine.Ioc;
using Jasmine.Restful.Exceptions;
using System;

namespace Jasmine.Restful
{
    public class RestfulAopFilterProvider : AbstractAopFilterProvider<HttpFilterContext>
    {
        private RestfulAopFilterProvider()
        {

        }

        public static IAopFilterProvider<HttpFilterContext> Instance = new RestfulAopFilterProvider();

        private IServiceProvider _serviceProvider => IocServiceProvider.Instance;
        
        public override IFilter<HttpFilterContext> GetFilter(string name)
        {
            if (!_map.ContainsKey(name))
            {
                var instance = _serviceProvider.GetService(Type.GetType(name));

                if (instance == null)
                {
                    throw new AopFilterCanNotBeCreatedException($"{name} can not be created!");
                }

                _map.TryAdd(name, (IFilter<HttpFilterContext>)instance);
            }

            return _map.TryGetValue(name, out var result) ? result : null;
        }

       
    }
}
