using Jasmine.Common;
using Jasmine.Reflection.Models;
using System;

namespace Jasmine.Rpc.Server
{
    public class JasmineRpcProxy : AbstractProxyFilter<IRpcContext>
    {
        public JasmineRpcProxy(Method method, IParamteterResolver<IRpcContext> resolver, object instance, string name) : base(method, resolver, instance, name)
        {
        }

        protected override void afterInvoke(IRpcContext context, object _return)
        {
            throw new NotImplementedException();
        }
    }
}
