using Jasmine.Common;
using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Jasmine.Restful.Implement
{
    public class ResfulMiddleware : IMiddleware, INameFearture
    {
        public static IDispatcher<HttpFilterContext> Dispatcher;

        private ILog _logger;

        private IPool<HttpFilterContext> _pool = new HttpFilterContextPool(10000);

        public string Name =>"Restful-Middleware";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var filterContext = _pool.Rent();

            filterContext.Init(context);

            try
            {
                await Dispatcher.DispatchAsync(context.Request.Path.ToString().ToLower(), filterContext);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                filterContext.Reset();

                _pool.Recycle(filterContext);
            }

            //if (next != null)
            //    await next(context);


        }
    }
}
