using Jasmine.Common;
using Jasmine.Reflection;
using Jasmine.Serialization;

namespace Jasmine.Rpc.Server
{
    public class RpcProxyFilter : AbstractProxyFilter<RpcFilterContext>
    {
        public RpcProxyFilter(Method method,
                              IRequestParamteterResolver<RpcFilterContext> resolver,
                              object instance,
                              string name,
                              ISerializer serializer) : base(method, resolver, instance, name)
        {
            _codex = new RpcRequestResponseEncoder(serializer);
            _serializer = serializer;
        }
        private RpcRequestResponseEncoder _codex;
        private ISerializer _serializer;
        protected override void afterInvoke(RpcFilterContext context, object _return)
        {
            context.ReturnValue = _return;
            context.RpcContext.Response.Body = _serializer.SerializeToBytes(_return);
            context.RpcContext.Response.StatuCode = 200;
        }
    }
}
