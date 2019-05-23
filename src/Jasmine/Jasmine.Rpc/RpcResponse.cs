namespace Jasmine.Rpc
{
    public   class RpcResponse
    {
        public static readonly RpcResponse ErrorResponse = new RpcResponse();
        public static readonly RpcResponse  UnregisterdResponse=new RpcResponse();

        public long RequestId { get; set; }
        public object Body { get; set; }
        public int StatuCode { get; set; }
    }
}
