using DotNetty.Codecs;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Jasmine.Common;
using Jasmine.Rpc.Server.Exceptions;
using Jasmine.Serialization;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public class RpcServer
    {
        public RpcServer(int port,
                         IRpcMiddleware middleware,
                         X509Certificate certs,
                         ISerializer serialzier,
                         ILoginValidator validator,
                         int backlog)
        {
            _port = port;
            _middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
            _serializer = serialzier ?? JsonSerializer.Instance;
            _loginValidator = validator ;
            _certificate = certs;
            _backLog = backlog;
        }

        private int _backLog;
        private int _port;
        private IChannel _listenerChannel;
        private MultithreadEventLoopGroup _listenerGroup;
        private MultithreadEventLoopGroup _workerGroup;
        private X509Certificate _certificate;
        private IRpcMiddleware _middleware;
        private ISerializer _serializer;
        private ILoginValidator _loginValidator;

        public async Task RunAsync()
        {
            _listenerGroup = new MultithreadEventLoopGroup(1);

            _workerGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootStrap = new ServerBootstrap();

                bootStrap
               .Group(_listenerGroup, _workerGroup)
               .Channel<TcpServerSocketChannel>()
               .Option(ChannelOption.SoBacklog, _backLog)
               .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
               {
                   var pipeline = channel.Pipeline;
                   /*
                    * ssl handler
                    */
                   if (_certificate != null)
                       pipeline.AddLast(TlsHandler.Server(_certificate));
                   /*
                    *  encoder
                    */
                   pipeline.AddLast(new LengthFieldPrepender(CondecsConfig.LengthFiledLength));
                   /*
                    *   decoder
                    */
                   pipeline.AddLast(new LengthFieldBasedFrameDecoder(CondecsConfig.MaxPacketLength, 0, CondecsConfig.LengthFiledLength));
                   /*
                    *  bussness handler
                    */
                   pipeline.AddLast(new RpcHandler(_serializer, _middleware, _loginValidator));

               }));


                _listenerChannel = await bootStrap.BindAsync(_port);


            }
            catch (Exception ex)
            {
                throw new AppStartingException(ex);
            }
        }

        public async Task StopAysnc()
        {
            try
            {
                if (_listenerChannel != null)
                    await _listenerChannel.CloseAsync();
            }
            finally
            {
                if (_listenerGroup != null)
                    Task.WaitAll(_listenerGroup.ShutdownGracefullyAsync(), _workerGroup.ShutdownGracefullyAsync());
            }
        }
    }


}
