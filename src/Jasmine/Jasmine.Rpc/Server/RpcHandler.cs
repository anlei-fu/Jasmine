using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using Jasmine.Common;
using Jasmine.Extensions;
using Jasmine.Serialization;
using log4net;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Jasmine.Rpc.Server
{
    public class RpcHandler : ChannelHandlerAdapter
    {

        public RpcHandler(ISerializer serializer,
                          IRpcMiddleware middleware,
                          ILoginValidator validator)
        {
            _codex = new RpcRequestResponseEncoder(serializer);
            _middleWare = middleware ?? throw new ArgumentNullException(nameof(middleware));
            _validator = validator;
        }
        /// <summary>
        /// heart beat header
        /// </summary>
        private const byte HEARTBEAT_HEADER = 5;
        /// <summary>
        /// request header
        /// </summary>
        private const byte REQUEST = 6;
        /// <summary>
        /// 
        /// </summary>
        private ILog _logger;
        /// <summary>
        /// use to encode  <see cref="RpcResponse"/>
        /// </summary>
        private RpcRequestResponseEncoder _codex;
        /// <summary>
        /// identity validate
        /// </summary>
        private ILoginValidator _validator;
        /// <summary>
        ///  a plugging
        /// </summary>
        private IRpcMiddleware _middleWare;
        /// <summary>
        ///  a recycle pool
        /// </summary>
        private IPool<RpcContext> _pool = new RpcContextPool(10000);
        /// <summary>
        /// check connection activity
        /// </summary>
        private ChannelConnectivityChecker _checker = new ChannelConnectivityChecker();



        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (message is IByteBuffer buffer)
            {

                var id=0l;
                /*
                 * skip length field
                 */

                buffer.SkipBytes(4);

                var header = buffer.ReadByte();

                /*
                 * process heart beat
                 */
                if (header == HEARTBEAT_HEADER)
                {
                    if (ensureRegisterd(context))
                    {
                        _checker.UpdateTimeout(context.Channel.Id.AsLongText());
                    }

                    //else ignore
                }
                else if (header == REQUEST)
                {
                    var rpcContext = _pool.Rent();

                    try
                    {
                        /*
                         *  resolve request
                         */
                        var request = new RpcRequest();

                        request.RequestId = buffer.ReadLongLE();

                        id = request.RequestId;

                        var length = buffer.ReadIntLE();

                        var path = buffer.ReadString(length, Encoding.UTF8);

                        parseQuery(request, path);

                        length = buffer.ReadIntLE();

                        request.Body = new byte[length];

                        buffer.ReadBytes(request.Body);

                        /*
                         * login check
                         */
                        if (_validator != null)// configed require  identity validate
                        {
                            try
                            {
                                if (!_checker.IsRegistered(context.Channel.Id.AsLongText()))
                                {
                                    var resposne = _validator.Validate(request.Query["id"], request.Query["password"]) ? RpcResponse.CreateResponse(200, request.RequestId)
                                                                                                                       : RpcResponse.CreateLoginFialedResponse(request.RequestId);

                                    var sendBuffer = _codex.EncodeServerResponse(resposne);

                                    var sendBuffer1 = context.Allocator.Buffer(sendBuffer.Length);

                                    sendBuffer1.WriteBytes(sendBuffer);

                                    context.WriteAndFlushAsync(sendBuffer1);

                                }
                            }
                            catch(Exception ex)
                            {
                                var sendBuffer = _codex.EncodeServerResponse(RpcResponse.CreateErrorResponse(request.RequestId));

                                var sendBuffer1 = context.Allocator.Buffer(sendBuffer.Length);

                                sendBuffer1.WriteBytes(sendBuffer);

                                context.WriteAndFlushAsync(sendBuffer1);

                                return;
                            }
                        }
                      
                        /*
                         *  process request
                         */

                        rpcContext.Init(request, context);

                        _middleWare.ProcessRequest(rpcContext);

                    }
                    catch (Exception ex)
                    {
                        _logger?.Error(ex);

                        context.WriteAndFlushAsync(RpcResponse.CreateErrorResponse(10000));
                    }
                    finally
                    {
                        _pool.Recycle(rpcContext);
                    }
                }


            }
            else
            {
                _logger?.Warn($"unexcepted input {message}");
            }


        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void parseQuery(RpcRequest request, string path)
        {
            var index = path.IndexOf("?");

            request.Path = path.ToLower();

            if (index != -1)
            {
                request.Query = getQuery(request.Path.Substring(index + 1, request.Path.Length - index - 1));
                request.Path = request.Path.Substring(0, index);
            }
        }

        private IDictionary<string, string> getQuery(string str)
        {
            var dic = new Dictionary<string, string>();

            foreach (var item in StringExtensions.Splite1(str, "&"))
            {
                var pair = StringExtensions.Splite1(item, "=");

                if (pair.Count == 2)
                {
                    if (!dic.ContainsKey(pair[0]))
                        dic.Add(pair[0], pair[1]);
                }
            }

            return dic;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool ensureRegisterd(IChannelHandlerContext context)
        {
            if (!_checker.IsRegistered(context.Channel.Id.AsLongText()))
            {
                context.WriteAsync(RpcResponse.CreateUnregisterdResponse(10000));

                return false;
            }

            return true;
        }



        public override void ChannelInactive(IChannelHandlerContext context)
        {
            _checker.Unregister(context.Channel.Id.AsLongText());
        }
    }
}
