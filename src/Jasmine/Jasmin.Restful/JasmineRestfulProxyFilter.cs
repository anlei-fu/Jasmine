using Jasmine.Common;
using Jasmine.Reflection.Models;

namespace Jasmine.Restful
{
    public class JasmineRestfulProxyFilter : AbstractProxyFilter<HttpFilterContext>
    {
        public JasmineRestfulProxyFilter(Method method, IParamteterResolver<HttpFilterContext> resolver, object instance, string name) : base(method, resolver, instance, name)
        {
        }

        protected override void afterInvoke(HttpFilterContext context, object _return)
        {
            context.ReturnValue = _return;
        }
    }
}
