using DotNetty.Transport.Channels;

namespace Jasmine.Rpc
{
    public   class RpcContext
    {
        /// <summary>
        ///  request
        /// </summary>
        public RpcRequest Request { get;private set; }
        /// <summary>
        /// response
        /// </summary>
        public RpcResponse Response { get; private set; }
        public IChannelHandlerContext HandlerContext { get; set; }
        public void Init(RpcRequest request,IChannelHandlerContext context)
        {
            Response = new RpcResponse();

            Response.RequestId = request.RequestId;

            Request = request;

            HandlerContext = context;
        }
    }
}
