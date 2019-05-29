namespace Jasmine.Rpc
{
    /// <summary>
    /// represent a rpc response
    /// </summary>
    public   class RpcResponse
    {
        private static readonly byte[] Empty_Body = new byte[] { };
        /// <summary>
        /// create an error response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RpcResponse CreateErrorResponse(long id) => CreateResponse(500, id);
        /// <summary>
        /// create an un registered response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RpcResponse CreateUnregisterdResponse(long id) => CreateResponse(503, id);
        /// <summary>
        /// create a service not available resposne
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RpcResponse CreateServiceNotFoundResponse(long id) => CreateResponse(404, id);

        public static RpcResponse CreateServiceNotAvailableResponse(long id) => CreateResponse(503, id);

        public static RpcResponse CreateLoginSuccessFulResponse(long id) => CreateResponse(200, id);

        public static RpcResponse CreateLoginFialedResponse(long id) => CreateResponse(401, id);
       
        public static RpcResponse CreateResponse(int statusCode,long id)
        {
            return new RpcResponse()
            {
                RequestId = id,
                StatuCode = statusCode
            };
        }
        /// <summary>
        /// request id ,it should match to  every request posted
        /// </summary>
        public long RequestId { get; set; } = 10001;
        /// <summary>
        /// return value serilized with bytes
        /// </summary>
        public byte[] Body { get; set; } = Empty_Body;
        /// <summary>
        /// statu code
        /// </summary>
        public int StatuCode { get; set; } = 100;
    }
}
