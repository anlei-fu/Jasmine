using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Jasmine.Rpc.Client.Exceptions;
using log4net;
using System;
using System.Threading;

namespace Jasmine.Rpc.Client
{
    public class ClientRpcHandler : ChannelHandlerAdapter
    {
        public ClientRpcHandler(RpcCallManager manager)
        {
            _manager = manager;
        }

        private RpcCallManager _manager;
        private ILog _logger;

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer buffer)
            {
                try
                {
                    var response = new RpcResponse();

                    /*
                     * skip lengthfield and header
                     */
                    buffer.ReadInt();
                    buffer.ReadByte();

                    /*
                     *  resolve  response
                     */
                    response.RequestId = buffer.ReadLongLE();

                    response.StatuCode = buffer.ReadIntLE();

                    var length = buffer.ReadIntLE();

                    response.Body = new byte[length];

                    buffer.ReadBytes(response.Body);

                    /*
                     * singal request finished
                     */

                    var call = _manager.GetCall(response.RequestId);

                    if (call != null)
                    {
                        call.Response = response;

                        Monitor.PulseAll(call.Locker);
                    }

                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
            else
            {
                _logger.Warn($" unexcepted message type received {message}");
            }
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            foreach (var item in _manager)
            {
                Monitor.PulseAll(item.Locker);
            }


            throw new ConnectionClosedException();
        }



    }
}
