using Jasmine.Common;

namespace Jasmine.Rpc.Client
{
    public  interface  IRpcClientConnection
    {
        RpcCallContext<IRpcResponse> Call(IRpcRequest requetst);
        bool Open(string user, string password);
        void Close();
    }
}
