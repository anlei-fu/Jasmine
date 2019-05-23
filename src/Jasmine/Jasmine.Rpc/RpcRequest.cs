using System.Collections.Generic;
using System.IO;

namespace Jasmine.Rpc
{
    public  class RpcRequest
    {
        public long RequestId { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> Query { get; set; }
        public Stream Body { get; set; }
    }
}
