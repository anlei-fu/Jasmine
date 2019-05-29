using Jasmine.Serialization;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Jasmine.Rpc
{
    /// <summary>
    ///  represent a rpc request ,every request has a unique id
    /// </summary>
    public  class RpcRequest
    {
        private static long _id;

        private static long newId()
        {
          return   Interlocked.Increment(ref _id);
        }

        private static readonly byte[] EMPTY_BODY = Array.Empty<byte>();

        public static RpcRequest Create(string path)
        {
            var request = new RpcRequest();

            request.Path = path;

            return request;
        }
        public static RpcRequest Create(string path,object body,ISerializer serializer)
        {
            var request = new RpcRequest();

            request.Path = path;

            request.Body = serializer.SerializeToBytes(body);

            return request;
        }
        /// <summary>
        ///  unique id
        /// </summary>
        public long RequestId { get; set; } = newId();
        /// <summary>
        /// service path
        /// </summary>
        public string Path { get; set; } = string.Empty;
        /// <summary>
        /// query ,just for server
        /// 
        /// it 's no meaning in client
        /// 
        /// </summary>
        public IDictionary<string, string> Query = new Dictionary<string, string>();
        /// <summary>
        /// data to send
        /// </summary>
        public byte[] Body { get; set; } = EMPTY_BODY;
    }
}
