using Jasmine.Common;
using Jasmine.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public   class RpcApplication
    {

        internal RpcApplication(int port,
                              IRpcMiddleware middleware,
                              ISerializer serializer,
                              X509Certificate certs,
                              ILoginValidator validator,
                              int backlog)
        {
            _port = port;
            _middleware = middleware;
            _serizlizer = serializer;
            _certs = certs;
            _validator = validator;
            _backLog = backlog;
        }
        private int _backLog;
        private X509Certificate _certs;
        private int _port;
        private IRpcMiddleware _middleware;
        private ISerializer _serizlizer;
        private ILoginValidator _validator;

        private RpcServer _server;

        public async Task RunAsync()
        {
             _server = new RpcServer(_port, _middleware, _certs, _serizlizer, _validator,_backLog);

            await _server.RunAsync();
        }
        public async Task StopAsync()
        {
            await _server.StopAysnc();
        }
    }
}
