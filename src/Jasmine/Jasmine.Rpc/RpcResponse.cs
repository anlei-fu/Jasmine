using DotNetty.Buffers;

namespace Jasmine.Rpc
{
    public   class RpcResponse
    {
        public static readonly RpcResponse ErrorResponse = new RpcResponse();
        public static readonly RpcResponse  UnregisterdResponse=new RpcResponse();
        public static readonly RpcResponse ServiceNotFoundResponse = new RpcResponse();
        public static readonly RpcResponse ServiceNotAvailableResponse = new RpcResponse();
        public static readonly RpcResponse LoginSuccessFulResponse = new RpcResponse();
        public static readonly RpcResponse LoginFialedResponse = new RpcResponse();

        public long RequestId { get; set; } = 10001;
        public byte[] Body { get; set; }
        public int StatuCode { get; set; } = 100;
    }
}
