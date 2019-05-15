using System;
using System.Threading.Tasks;
using Jasmine.Common;
using log4net;

namespace Jasmine.Restful
{
    public class RestfulDispatcher : AbstractDispatcher<HttpFilterContext>
    {
        public RestfulDispatcher(string name, IRequestProcessorManager<HttpFilterContext> pipelineManager) : base(name, pipelineManager)
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
            if(_pipelineManager.ContainsProcessor(path))
            {
                var pipeline = _pipelineManager.GetProcessor(path);

                try
                {
                   await  pipeline.Filter.First.FiltAsync(context);
                }
                catch (Exception ex)
                {
                    context.Exception = ex;
                    _logger?.Error(ex);
                    await pipeline.ErrorFilter.First.FiltAsync(context);
                }
                
            }
            else if(UseStaticFile)
            {

            }
        }
    }
}
