using Jasmine.Common;
using Jasmine.Serialization;
using System.Collections.Generic;

namespace Jasmine.Rpc.Server
{
    public  class RpcServiceMetaData:AopServiceMetaData
    {
        public SerializeMode SerializeMode { get; set; }
        public string Path { get; set; }
        public IDictionary<string, RpcRequestMetaData> Requests { get; set; } = new Dictionary<string, RpcRequestMetaData>();
    }
}
