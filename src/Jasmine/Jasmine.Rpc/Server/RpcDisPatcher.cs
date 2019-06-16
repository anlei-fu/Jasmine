using System;
using System.Runtime.CompilerServices;
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
            _codex = new RpcRequestResponseEncoder(serializer);
        }

        private ILog _logger;

        private RpcRequestResponseEncoder _codex;

     
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

                        await writeResponse(context.RpcContext.Response, context);
                    }
                    catch (Exception ex)
                    {
                        context.Error = ex;

                        _logger?.Error(ex);

                        await processor.ErrorPileline.First.FiltsAsync(context);

                        await writeResponse(context.RpcContext.Response, context);
                    }
                }
                else
                {
                    await writeResponse(RpcResponse.CreateServiceNotAvailableResponse(context.RpcContext.Request.RequestId),context);
                }
            }
          else
            {
                await writeResponse(RpcResponse.CreateServiceNotFoundResponse(context.RpcContext.Request.RequestId),context);
            }

            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async Task writeResponse(RpcResponse response,RpcFilterContext context)
        {

            var responseBuffer = _codex.EncodeServerResponse(response);

            var bufferToSend = context.RpcContext.HandlerContext.Allocator.Buffer(responseBuffer.Length);

            bufferToSend.WriteBytes(responseBuffer);

           await context.RpcContext.HandlerContext.WriteAndFlushAsync(bufferToSend);

        }
    }
}
