using System;

namespace Jasmine.Rpc.Client
{
    public class JasmineRpcConnection : AbstractRpcClientConnection
    {
        protected override void closeInternal()
        {
            throw new NotImplementedException();
        }

        protected override bool openInternal(string user, string password)
        {
            throw new NotImplementedException();
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
