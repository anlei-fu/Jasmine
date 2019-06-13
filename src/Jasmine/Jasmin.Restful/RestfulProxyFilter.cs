using Jasmine.Common;
using Jasmine.Reflection;

namespace Jasmine.Restful
{
    /// <summary>
    /// reflect invoke service method
    /// </summary>
    public class RestfulProxyFilter : AbstractProxyFilter<HttpFilterContext>
    {
        public RestfulProxyFilter(Method method, IRequestParamteterResolver<HttpFilterContext> resolver, object instance) : base(method, resolver, instance, "restful-proxy-filter")
        {
        }

        protected  override void afterInvoke(HttpFilterContext context, object _return)
        {
            context.ReturnValue = _return;

            context.HttpContext.Response.StatusCode =200;
        }
    }
}
