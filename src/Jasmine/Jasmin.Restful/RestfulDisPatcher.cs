using System;
using System.Threading.Tasks;
using Jasmine.Common;
using log4net;

namespace Jasmine.Restful
{
    public class RestfulDisPatcher : AbstractDispatcher<HttpFilterContext>
    {
        public RestfulDisPatcher(string name, IFilterPipelineManager<HttpFilterContext> pipelineManager) : base(name, pipelineManager)
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
            if(_pipelineManager.Contains(path))
            {
                var pipeline = _pipelineManager.GetPipeline(path);

                try
                {
                   await  pipeline.Root.FiltAsync(context);
                }
                catch (Exception ex)
                {
                    context.Exception = ex;
                    _logger?.Error(ex);
                    await pipeline.ErrorFilter.FiltAsync(context);
                }
                
            }
            else if(UseStaticFile)
            {

            }
        }
    }
}
