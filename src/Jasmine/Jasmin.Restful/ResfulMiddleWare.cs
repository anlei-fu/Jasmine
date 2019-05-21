using Jasmine.Common;
using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Jasmine.Restful.Implement
{
    public class ResfulMiddleware : IMiddleware, INameFearture
    {

        private ILog _logger;
        public static IDispatcher<HttpFilterContext> Dispatcher;
        private IPool<HttpFilterContext> _pool = new HttpFilterContextPool(10000);

        public string Name { get; } = "jjj";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var fcontext = _pool.Rent();

            fcontext.Init(context);

            try
            {

                await Dispatcher.DispatchAsync(context.Request.Path, fcontext);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                fcontext.Reset();
                _pool.Recycle(fcontext);
            }

            //if (next != null)
            //    await next(context);


        }
    }
}
