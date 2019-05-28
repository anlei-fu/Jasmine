using System;
using System.Collections.Generic;
using System.Threading;

namespace Jasmine.Rpc
{
    public  class RpcRequest
    {
        private static long _id;

        private static long newId()
        {
          return   Interlocked.Increment(ref _id);
        }
        public static readonly RpcRequest HEARTBEAT = new RpcRequest();
        public static readonly byte[] EMPTY_BODY = Array.Empty<byte>();
        public long RequestId { get; set; } = newId();
        public string Path { get; set; } = string.Empty;
        public IDictionary<string, string> Query = new Dictionary<string, string>();
        public byte[] Body { get; set; } = EMPTY_BODY;
    }
}
