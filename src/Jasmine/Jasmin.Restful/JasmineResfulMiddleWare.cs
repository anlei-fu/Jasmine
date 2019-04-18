using Jasmine.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Jasmine.Restful.Implement
{
    public class JasmineResfulMiddleWare : IMiddleware
    {
       
        public bool UseFtp{ get; set; }
        public string FtpRoot { get; set; }
        public bool UseStaticFile { get; set; }
        public string StaticFileRoot { get; set; }
        private Func<string, IFilterPipeline<HttpFilterContext>> _filterPielineProvider;
        private IPool<HttpFilterContext> _pool;


        public  Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            var filter = _filterPielineProvider(context.Request.Path);

            Task task = Task.CompletedTask;

            if (filter != null)
            {
                var filterContext = _pool.Rent();
                try
                {
                    task = filter.Root.Filt(filterContext);
                }
                catch(Exception ex)
                {

                }
                finally
                {

                }
            }
            else if(UseFtp)
            {

            }
            else if(UseStaticFile)
            {

            }

            return  next != null ? next(context) : task;
           
        }
    }
}
