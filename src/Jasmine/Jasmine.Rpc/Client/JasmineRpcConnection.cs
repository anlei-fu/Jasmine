using Jasmine.ConfigCenter.Common;
using Jasmine.Serialization;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Jasmine.Rpc.Client
{
    public class JasmineRpcConnection : AbstractRpcClientConnection
    {
        public JasmineRpcConnection(string host,int port)
        {

        }
        private string _host;
        private int _port;
        private Socket _socket;
        private ISerializer _serizlizer;
        private IEncoder _encoder;
        protected override void closeInternal()
        {
            throw new NotImplementedException();
        }

        protected override bool openInternal(string user, string password)
        {
            try
            {
                _socket.Connect(new IPEndPoint(IPAddress.Parse(_host), _port));
                _serizlizer.TrySerialize(new LoginInfo() { User = user, Password = password }, out var bytesSource);
                var loginPacket = _encoder.Encode(bytesSource);
                _socket.Send(loginPacket);
                _socket.Receive();

                return true;

            }
            catch (Exception ex)
            {

                return false;
            }


        }

        protected override void receiveInternal(Action<Exception, IRpcResponse> processor)
        {
            throw new NotImplementedException();
        }

        protected override void sendInternal(IRpcRequest request, Action<Exception> writeFinished)
        {
            throw new NotImplementedException();
        }
    }
}
