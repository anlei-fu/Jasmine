using Jasmine.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Jasmine.Rpc.Client
{
 public   class RpcClientBuilder
    {
        private ISerializer _serializer;
        private string _host;
        private int _port;
        private X509Certificate _certs;
        private string _user;
        private string _password;

        public RpcClientBuilder ConfigAdress(string host,int port)
        {
            _host = host;
            _port = port;
            return this;
        }

        public RpcClientBuilder ConfigSsl(string path)
        {


            _certs = X509Certificate.CreateFromCertFile(path);

            return this;
        }

        public RpcClientBuilder ConfigSsl(X509Certificate certs)
        {
            _certs = certs;
            return this;
        }

        public RpcClientBuilder ConfigSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            return this;
        }
        public RpcClientBuilder ConfigUser(string userName,string password)
        {
            _user = userName;
            _password = password;
            return this;
        }
        public RpcClient Build()
        {
            return new RpcClient(_host, _port,_certs, _serializer, _user, _password);
        }
    }
}
