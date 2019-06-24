using Jasmine.Common;
using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Jasmine.Restful.Implement
{
    public class ResfulMiddleware : IMiddleware, INameFearture
    {
        public ResfulMiddleware()
        {
            _pool = new HttpFilterContextPool(_dispatcher, 1000);
        }
        private IDispatcher<HttpFilterContext> _dispatcher=>RestfulApplicationBaseComponents.Dispatcher;

        private ILog _logger=LogManager.GetLogger(typeof(ResfulMiddleware));

        private IPool<HttpFilterContext> _pool;

        public string Name =>"Restful-Middleware";

        public  Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var filterContext = _pool.Rent();

            filterContext.Init(context);

            try
            {
                return _dispatcher.DispatchAsync(context.Request.Path.ToString().ToLower(), filterContext);
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

            return Task.CompletedTask;

            //if (next != null)
            //    await next(context);


        }
    }
}
