using Jasmine.Common;
using Jasmine.Ioc;
using System;

namespace Jasmine.Rpc.Server
{
    public class RpcAopFilterProvider :AbstractAopFilterProvider<RpcFilterContext>
    {
        private RpcAopFilterProvider()
        {

        }

        public static readonly IAopFilterProvider<RpcFilterContext> Instance = new RpcAopFilterProvider();

        private IServiceProvider _serviceProvider => IocServiceProvider.Instance;

        public override IFilter<RpcFilterContext> GetFilter(string name)
        {
            if (!_map.ContainsKey(name))
            {
                var instance = _serviceProvider.GetService(Type.GetType(name));

                if (instance == null)
                {
                   
                }

                _map.TryAdd(name, (IFilter<RpcFilterContext>)instance);
            }


            return _map.TryGetValue(name, out var result) ? result : null;
        }
    }
}
