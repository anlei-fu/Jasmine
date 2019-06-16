using Jasmine.Common;
using log4net;
using System;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public class RpcRequestDispatcher : AbstractDispatcher<RpcFilterContext>
    {
        public RpcRequestDispatcher(string name, IRequestProcessorManager<RpcFilterContext> processorManager,ILog logger) : base(name, processorManager)
        {
            _logger = logger;
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
                        await processor.Pipeline.First.FiltsAsync(context);
                    }
                    catch (Exception ex)
                    {
                        context.Error = ex;

                        _logger?.Error(ex);

                        await processor.ErrorPileline.First.FiltsAsync(context);
                    }
                }
                else
                {
                   
                }
            }
            else
            {

            }
        }
    }
}
