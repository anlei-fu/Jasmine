using System;
using System.Threading.Tasks;
using Jasmine.Common;
using log4net;

namespace Jasmine.Restful
{
    public class RestfulDispatcher : AbstractDispatcher<HttpFilterContext>
    {
        public RestfulDispatcher(string name, IRequestProcessorManager<HttpFilterContext> processorManager) : base(name, processorManager)
        {
        }
        private ILog _logger;
        public bool AllowUpload { get; set; }
        public bool UseStaticFile { get; set; }
        public string VirtuePathRoot { get; set; }
        public bool UseHotUpdate { get; set; }
        public bool UseApiManager { get; set; }
        public override async Task DispatchAsync(string path, HttpFilterContext context)
        {
            if (_processorManager.ContainsProcessor(path))
            {
                var processor = _processorManager.GetProcessor(path);

                if (processor.Available)
                {
                    try
                    {
                        await processor.Filter.First.FiltsAsync(context);
                    }
                    catch (Exception ex)
                    {
                        context.Exception = ex;

                        _logger?.Error(ex);

                        await processor.ErrorFilter.First.FiltsAsync(context);
                    }
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 401;
                }
            }
            else if(UseStaticFile)
            {

            }
        }
    }
}
