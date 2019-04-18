using Jasmine.Common;
using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Jasmine.Restful.Implement
{
    public class JasmineResfulMiddleWare : IMiddleware,INameFearture
    {
        public JasmineResfulMiddleWare(string name,IDispatcher<HttpFilterContext> dispatcher,IPool<HttpFilterContext> pool)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException();
            _pool = pool??throw new ArgumentNullException();
            Name = name ?? throw new ArgumentNullException();
        }
        private ILog _logger;
        private IDispatcher<HttpFilterContext> _dispatcher;
        private IPool<HttpFilterContext> _pool;

        public string Name { get; }

        public async  Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var fcontext = _pool.Rent();
            fcontext.Init(context);

            try
            {
                await _dispatcher.DispatchAsync(context.Request.Path, fcontext);
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
                fcontext.Reset();
                _pool.Recycle(fcontext);
            }

            if (next != null)
                await next(context);
        }
    }
}
