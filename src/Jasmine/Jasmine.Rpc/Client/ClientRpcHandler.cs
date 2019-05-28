using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using log4net;
using System;
using System.Threading;

namespace Jasmine.Rpc.Client
{
    public class ClientRpcHandler : ChannelHandlerAdapter
    {
        public ClientRpcHandler(RpcClient client)
        {
            _client = client;
        }
        private ILog _logger;
        private RpcClient _client;

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer buffer)
            {
                try
                {
                    _client.Response = new RpcResponse();

                    /*
                     * skip lengthfield and header
                     */
                    buffer.ReadInt();
                    buffer.ReadByte();

                    /*
                     *  resolve  response
                     */
                    _client.Response.RequestId = buffer.ReadLongLE();

                    _client.Response.StatuCode = buffer.ReadIntLE();

                    var length = buffer.ReadIntLE();

                    _client.Response.Body = new byte[length];

                    buffer.ReadBytes(_client.Response.Body);

                    /*
                     * singal request finished
                     */  

                    lock (_client.Locker)
                        Monitor.PulseAll(_client.Locker);

                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
            else
            {

            }
        }




    }
}
