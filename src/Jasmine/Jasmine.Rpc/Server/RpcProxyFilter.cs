using Jasmine.Common;
using Jasmine.Reflection.Models;
using System;

namespace Jasmine.Rpc.Server
{
    public class RpcProxyFilter : AbstractProxyFilter<IRpcContext>
    {
        public RpcProxyFilter(object instance, Method method, IParameterResolverFactory<IRpcContext> factory) : base(instance, method, factory)
        {
        }

        protected override void afterInvoke(IRpcContext context, object _return)
        {
            throw new NotImplementedException();
        }
    }
}
