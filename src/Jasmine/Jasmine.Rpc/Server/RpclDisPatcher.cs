using System;
using System.Threading.Tasks;
using Jasmine.Common;
using log4net;

namespace Jasmine.Rpc.Server
{
    public class RestfulDispatcher : AbstractDispatcher<RpcFilterContext>
    {
        public RestfulDispatcher(string name, IRequestProcessorManager<RpcFilterContext> processorManager) : base(name, processorManager)
        {
        }

        private ILog _logger;
       
     
        public override async Task DispatchAsync(string path, RpcFilterContext context)
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
                        context.Error = ex;

                        _logger?.Error(ex);

                        await processor.ErrorFilter.First.FiltsAsync(context);
                    }
                }
                else
                {
                    context.RpcContext.Response.StatuCode = 401;
                }
            }
          else
            {

            }

            
        }
    }
}
