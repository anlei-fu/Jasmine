using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Tls;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public class RpcServer
    {
        private int _port;
        private IChannel _listenerChannel;
        private MultithreadEventLoopGroup _listenerGroup;
        private MultithreadEventLoopGroup _workerGroup;
        private X509Certificate2 _certificate ;
        private RpcHandler _rpcHandler;

        public async Task RunAsync()
        {
            _listenerGroup= new MultithreadEventLoopGroup(1);

            _workerGroup = new MultithreadEventLoopGroup();

            try
            {
                var bootStrap = new ServerBootstrap();

                bootStrap
               .Group(_listenerGroup,_workerGroup)
               .Channel<TcpServerSocketChannel>()
               .Option(ChannelOption.SoBacklog, 100)
               .Handler(new LoggingHandler("LSTN"))
               .ChildHandler(new ActionChannelInitializer<ISocketChannel>(channel =>
               {
                   var pipeline = channel.Pipeline;

                   if (_certificate != null)
                   {
                       pipeline.AddLast(TlsHandler.Server(_certificate));
                   }

                   pipeline.AddLast(new LoggingHandler("CONN"));

                   pipeline.AddLast(new LengthFieldBasedFrameDecoder(10, 10, 10), _rpcHandler);

               }));

                _listenerChannel = await bootStrap.BindAsync(_port);

            }
            finally
            {
                Task.WaitAll(_listenerGroup.ShutdownGracefullyAsync(), _workerGroup.ShutdownGracefullyAsync());
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
