using Jasmine.Common;
using Jasmine.Reflection;
using Jasmine.Serialization;
using System;

namespace Jasmine.Restful
{
    public class RestfulProxyFilter : AbstractProxyFilter<HttpFilterContext>
    {
        public RestfulProxyFilter(Method method, IRequestParamteterResolver<HttpFilterContext> resolver, object instance, string name) : base(method, resolver, instance, name)
        {
        }

        protected  override void afterInvoke(HttpFilterContext context, object _return)
        {
            context.ReturnValue = _return;

            context.HttpContext.Response.StatusCode =200;
        }
    }
}
