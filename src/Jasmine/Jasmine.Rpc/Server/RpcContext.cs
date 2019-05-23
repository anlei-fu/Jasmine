namespace Jasmine.Rpc
{
    public   class RpcContext
    {
        public RpcRequest Request { get;private set; }
        public RpcResponse Response { get; }
        public void Init(RpcRequest request)
        {

        }
    }
}
