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
using log4net.Core;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Client
{
    public  class RpcClient
    {

        public RpcClient(string host,
                         int port,
                         X509Certificate certs,
                         ISerializer serializer,
                         string user,
                         string password)
        {
            _host = host??throw new ArgumentNullException(nameof(host));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _port = port;
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            _certs = certs;
            _codex = new RpcCodex(_serializer);
        }

        private string _host="127.0.0.1";
        private int _port=10336;
        private const int DEFAULT_TIMEOUT = 10 * 1000*10;
        internal RpcResponse Response { get; set; }
        private MultithreadEventLoopGroup _eventGroup;
        private RpcCodex _codex;
        private ISerializer _serializer;
        private string _user;
        private string _password;
        private X509Certificate _certs;


        private ILog _logger;
       

        public readonly object Locker = new object();

        public  async Task<T> Call<T>(string path)
        {

            var response = await getResponce(createRequest(path));

            return _serializer.Deserialize<T>(response.Body);
        }


        private async  Task<RpcResponse> getResponce(RpcRequest request)
        {
            Response = null;

            var buffer = _codex.EncodClientRequest(request);

            var buffer1=  Unpooled.Buffer(buffer.Length);

            buffer1.WriteBytes(buffer);

            await  _channel.WriteAndFlushAsync(buffer1);

            lock (Locker)
                Monitor.Wait(Locker, DEFAULT_TIMEOUT);

            if (Response==null)
            {
                await Close();
            }

            return Response;
        }

        private IChannel _channel;

        public async Task Close()
        {
            if (_channel != null)
            {
                await _channel.CloseAsync();
                await _eventGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
                _channel = null;
            }

        }
        public async Task Connect()
        {
            _eventGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootstrap = new Bootstrap();

                     bootstrap
                    .Group(_eventGroup)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;

                        if (_certs != null)
                            pipeline.AddLast(TlsHandler.Server(_certs));

                        pipeline.AddLast(new LengthFieldPrepender(CondecsConfig.LengthFiledLength));
                        pipeline.AddLast(new LengthFieldBasedFrameDecoder(CondecsConfig.MaxPacketLength, 0,  CondecsConfig.LengthFiledLength));
                        pipeline.AddLast(new ClientRpcHandler(this));
                       
                       
                    }));


                _channel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(_host),_port));

                /*
                 *  login 
                 */ 
                var response = await getResponce(createLoginRequest());

                var result = _serializer.Deserialize<LoginResult>(response.Body);

                if (!result.Success)
                {
                    throw new LoginFailedException(result.Message);
                }


            }
            catch(Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private RpcRequest createLoginRequest()
        {
            var request = new RpcRequest();

            request.Path = $"/login00?id={_user}&password={_password}";
            
            return request;
        }

        private RpcRequest createRequest(string path)
        {
            var request = new RpcRequest();

            request.Path = path;

            return request;
        }

    }
}
