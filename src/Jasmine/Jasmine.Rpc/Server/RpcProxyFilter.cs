using Jasmine.Common;
using Jasmine.Reflection;
using System;

namespace Jasmine.Rpc.Server
{
    public class RpcProxyFilter : AbstractProxyFilter<RpcFilterContext>
    {
        public RpcProxyFilter(Method method, IRequestParamteterResolver<RpcFilterContext> resolver, object instance, string name) : base(method, resolver, instance, name)
        {
        }

        protected override void afterInvoke(RpcFilterContext context, object _return)
        {
            context.ReturnValue = _return;
        }
    }
}
