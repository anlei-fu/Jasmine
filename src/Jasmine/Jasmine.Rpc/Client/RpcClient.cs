using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Jasmine.Rpc.Client.Exceptions;
using Jasmine.Rpc.Server;
using Jasmine.Serialization;
using log4net;
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Client
{
    public class RpcClient
    {

        public RpcClient(string host,
                         int port,
                         X509Certificate certs,
                         ISerializer serializer,
                         string user,
                         string password)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            _serializer = serializer ?? JsonSerializer.Instance;
            Port = port;
            User = user ?? throw new ArgumentNullException(nameof(user));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            _certs = certs;
            _codex = new RpcRequestResponseEncoder(_serializer);
        }


        private const int DEFAULT_TIMEOUT = 10 * 1000 * 10;
        private MultithreadEventLoopGroup _eventGroup;
        private RpcRequestResponseEncoder _codex;
        private ISerializer _serializer;
        private X509Certificate _certs;
        private IChannel _channel;
        private readonly object _locker = new object();
        private ILog _logger;
        private RpcCallManager _manager;

        /// <summary>
        /// host to connect
        /// </summary>
        public string Host { get; }
        /// <summary>
        /// remote address  port
        /// </summary>
        public int Port { get; }
        /// <summary>
        /// configed user
        /// </summary>
        public string User { get; }
        /// <summary>
        /// configed  password
        /// </summary>
        public string Password { get; }
        /// <summary>
        /// mark client is available
        /// </summary>
        public bool Registered { get; private set; }

        /// <summary>
        /// call with body 
        /// </summary>
        /// <typeparam name="T">return value type</typeparam>
        /// <param name="path">service path</param>
        /// <param name="body"></param>
        /// <returns></returns>
        public T Call<T>(string path, object body)
        {
            return Call<T>(RpcRequest.Create(path, body, _serializer));
        }
        /// <summary>
        /// call without body
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T Call<T>(string path)
        {
            return Call<T>(RpcRequest.Create(path));
        }
        /// <summary>
        /// call with request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public T Call<T>(RpcRequest request)
        {
            var call = call0(request);
           
            if (call.Response == null)
            {

            }
            else 
            {
                checkThrowStutaCode(call.Response.StatuCode);
            }

            return _serializer.Deserialize<T>(call.Response.Body);
        }
        /// <summary>
        /// call with no return value and no body
        /// </summary>
        /// <param name="path"></param>
        public void Invoke(string path)
        {
            Invoke(RpcRequest.Create(path));
        }
        /// <summary>
        /// call with body
        /// </summary>
        /// <param name="path"></param>
        /// <param name="body"></param>
        public void Invoke(string path, object body)
        {
            Invoke(RpcRequest.Create(path, body, _serializer));
        }
        /// <summary>
        /// call with request
        /// </summary>
        /// <param name="request"></param>
        public void Invoke(RpcRequest request)
        {
            var call = call0(request);

            if (call.Response == null)
            {
                throw new RpcCallTimeoutException();
            }
            else
            {
                checkThrowStutaCode(call.Response.StatuCode);
            }
          
        }
        /// <summary>
        /// close
        /// </summary>
        /// <returns></returns>
        public async Task Close()
        {
            if (_channel != null)
            {
                await _channel.CloseAsync();
                await _eventGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
                _channel = null;
            }

        }
        /// <summary>
        /// connect
        /// </summary>
        /// <returns></returns>
        public async Task Connect()
        {
            _eventGroup = new MultithreadEventLoopGroup();

            try
            {
                var builder = new Bootstrap();

                builder
               .Group(_eventGroup)
               .Channel<TcpSocketChannel>()
               .Option(ChannelOption.TcpNodelay, true)
               .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
               {
                   IChannelPipeline pipeline = channel.Pipeline;

                   if (_certs != null)
                       pipeline.AddLast(TlsHandler.Server(_certs));

                   pipeline.AddLast(new LengthFieldPrepender(CondecsConfig.LengthFiledLength));//encoder
                   pipeline.AddLast(new LengthFieldBasedFrameDecoder(CondecsConfig.MaxPacketLength, 0, CondecsConfig.LengthFiledLength));//decoder
                   pipeline.AddLast(new ClientRpcHandler(_manager));//business handler


               }));

                _channel = await builder.ConnectAsync(new IPEndPoint(IPAddress.Parse(Host), Port));

                /*
                 *  login in and registe client
                 */

                Invoke($"/login_internal?user={User}&password={Password}");

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                throw ex;
            }
        }
        /// <summary>
        /// call final
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private RpcCall call0(RpcRequest request)
        {
            /*
             * not registerd ,only login request can pass here
             */ 
            if(!Registered&&!request.Path.StartsWith("login_internal"))
            {
                throw new NotRegistedException("clien has not been registered,you can call nothing at now !");
            }

            var call = new RpcCall()
            {
                Id = request.RequestId
            };

            _manager.AddCall(call);

            sendRequest(request);

            /*
             * wait until timeout ,if response received ,rpc client handler will paulse
             */ 
            Monitor.Wait(call.Locker, DEFAULT_TIMEOUT);

            _manager.Remove(call.Id);

            return call;
        }
        /// <summary>
        /// send request to server
        /// </summary>
        /// <param name="request"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void sendRequest(RpcRequest request)
        {
            var requestBuffer = _codex.EncodeClientRequest(request);

            var bufferToSend = Unpooled.Buffer(requestBuffer.Length);

            bufferToSend.WriteBytes(requestBuffer);

            _channel.WriteAndFlushAsync(bufferToSend);
        }

        private void checkThrowStutaCode(int code)
        {
            if(code==RpcStutaCode.ERROR_REQUEST)
            {
                throw new RequestIncorrectException();
            }
            else if(code==RpcStutaCode.FORBIDDEN)
            {
                throw new ForbidenedException();
            }
            else if(code==RpcStutaCode.NOT_FPOUND)
            {
                throw new ServiceNotFoundException();
            }
            else if(code==RpcStutaCode.SERVER_ERROR)
            {
                throw new ServerException();
            }
            else if(code==RpcStutaCode.TIME_OUT)
            {
                throw new ServerException();
            }
            else if(code==RpcStutaCode.VALIDATE_FAILED)
            {
                throw new LoginFailedException("");
            }
            else if(code==RpcStutaCode.SERVER_NOT_AVAILABLE)
            {
                throw new ServiceNotAvailableException();
            }
        }

    }
}
