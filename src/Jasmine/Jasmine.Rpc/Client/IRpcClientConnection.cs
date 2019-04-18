namespace Jasmine.Rpc.Client
{
    public  interface  IRpcClientConnection
    {
        RpcCallStutas GetStuta(long id);
        IRpcResponse GetResponse(long id);
        void Call(IRpcRequest requetst);
        bool Open(string user, string password);
        void Close();
    }
}
