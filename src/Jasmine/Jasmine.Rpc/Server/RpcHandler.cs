using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Jasmine.Common;
using log4net;
using System;

namespace Jasmine.Rpc.Server
{
    public  class RpcHandler:ChannelHandlerAdapter
    {
        private ILog _logger;
        private ChannelConnectiveChecker _checker;
        private RpcDecoder _decoder;
        private IPool<RpcContext> _pool;
        private const byte HEARTBEAT_HEADER = 6;
        private const byte REQUEST = 7;
        private IRpcMiddleware _middleWare;
        public override  void ChannelRead(IChannelHandlerContext context, object message)
        {
           if(message is IByteBuffer buffer)
            {
                var header = buffer.Array[0];

                if(header==HEARTBEAT_HEADER)
                {
                   if(ensureRegisted(context))
                    {
                        _checker.UpdateTimeout(context.Channel.Id.AsLongText());
                    }
                }
                else if(header==REQUEST)
                {

                    if (ensureRegisted(context))
                    {
                        var rpcContext = _pool.Rent();

                        try
                        {
                           var request= _decoder.Decode(buffer.Array);

                            rpcContext.Init(request);

                            _middleWare.InvokeAsync(rpcContext);
                           
                        }
                        catch (Exception ex)
                        {
                            _logger?.Error(ex); 

                            context.WriteAndFlushAsync(RpcResponse.ErrorResponse);
                        }
                        finally
                        {
                            _pool.Recycle(rpcContext);
                        }
                    }

                }
            }
           else
            {

            }
        }

        private bool ensureRegisted(IChannelHandlerContext context)
        {
            if (!_checker.IsRegistered(context.Channel.Id.AsLongText()))
            {
               context.WriteAsync(RpcResponse.UnregisterdResponse);

                return false;
            }

            return true;
        }
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);
        }
    }
}
