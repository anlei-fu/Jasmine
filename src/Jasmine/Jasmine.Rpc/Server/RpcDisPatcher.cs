using System;
using System.Threading.Tasks;
using Jasmine.Common;
using Jasmine.Serialization;
using log4net;

namespace Jasmine.Rpc.Server
{
    public class RpcDispatcher : AbstractDispatcher<RpcFilterContext>
    {
        public RpcDispatcher(string name, IRequestProcessorManager<RpcFilterContext> processorManager,ISerializer serializer) : base(name, processorManager)
        {
            _codex = new RpcCodex(serializer);
        }

        private ILog _logger;

        private RpcCodex _codex;

     
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

                    var buffer = _codex.EncodeServerResponse(context.RpcContext.Response);

                    var buffer1 = context.RpcContext.HandlerContext.Allocator.Buffer(buffer.Length);

                    buffer1.WriteBytes(buffer);

                   await context.RpcContext.HandlerContext.WriteAndFlushAsync(buffer1);
                }
            }
          else
            {
                context.RpcContext.Response.StatuCode = 404;

                var buffer = _codex.EncodeServerResponse(context.RpcContext.Response);

                var buffer1 = context.RpcContext.HandlerContext.Allocator.Buffer(buffer.Length);

                buffer1.WriteBytes(buffer);

                await context.RpcContext.HandlerContext.WriteAndFlushAsync(buffer1);
            }

            
        }
    }
}
