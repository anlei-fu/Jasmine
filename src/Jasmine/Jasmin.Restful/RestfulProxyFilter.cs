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

        protected override void afterInvoke(HttpFilterContext context, object _return)
        {

            Console.WriteLine($"request processed! {_return==null}");

            var bytes = JsonSerializer.Instance.SerializeToBytes(_return);

            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.Response.Body.Write(bytes,0,bytes.Length);
            context.HttpContext.Response.Body.Flush();


            context.ReturnValue = _return;
        }
    }
}
