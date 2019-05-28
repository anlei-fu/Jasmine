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
            _codex = new RpcCodex(serializer);
            _serializer = serializer;
        }
        private RpcCodex _codex;
        private ISerializer _serializer;
        protected override void afterInvoke(RpcFilterContext context, object _return)
        {
            context.ReturnValue = _return;
            context.RpcContext.Response.Body = _serializer.SerializeToBytes(_return);
            context.RpcContext.Response.StatuCode = 200;

            var buffer = _codex.EncodeServerResponse(context.RpcContext.Response);

            var buffer1= context.RpcContext.HandlerContext.Allocator.Buffer(buffer.Length);

            buffer1.WriteBytes(buffer);

            context.RpcContext.HandlerContext.WriteAndFlushAsync(buffer1);
        }
    }
}
