using Jasmine.Common;
using Jasmine.Ioc;
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


        public override IFilter<HttpFilterContext> GetFilter<T>()
        {
            return GetFilter(typeof(T));
        }

        public override IFilter<HttpFilterContext> GetFilter(Type type)
        {
            if(!_map.ContainsKey(type))
            {
                var instance = _serviceProvider.GetService(type);

                _map.TryAdd(type, (IFilter<HttpFilterContext>)instance);
            }

            return _map[type];
        }
    }
}
