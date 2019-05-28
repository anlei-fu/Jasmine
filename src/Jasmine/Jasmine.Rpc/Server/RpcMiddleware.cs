using Jasmine.Common;
using log4net;
using System;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public class RpcMiddleware : IRpcMiddleware
    {

        public RpcMiddleware(IDispatcher<RpcFilterContext> dispatcher)
        {
            _dispatcher = dispatcher;
        }

        private IDispatcher<RpcFilterContext> _dispatcher;

        private ILog _logger;

        private IPool<RpcFilterContext> _pool = new RpcFilterContextPool(10000);

        public string Name => "Restful.Middleware";

        public async Task ProcessRequest(RpcContext context)
        {
            var filterContext = _pool.Rent();

            filterContext.Init(context);

            try
            {
                await _dispatcher.DispatchAsync(context.Request.Path, filterContext);
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
