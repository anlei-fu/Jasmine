using Jasmine.Common;
using log4net;
using System;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public class RpcMiddleware : IRpcMiddleware
    {
        public static IDispatcher<RpcFilterContext> Dispatcher;

        private ILog _logger;
        private IPool<RpcFilterContext> _pool ;

        public string Name => "Restful-Middleware";

        public async Task InvokeAsync(RpcContext context)
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
               
                _pool.Recycle(filterContext);
            }

            //if (next != null)
            //    await next(context);


        }
    }
}
