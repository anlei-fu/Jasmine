using System.Collections.Generic;

namespace Jasmine.Rpc
{
    public class DefaultRpcRequest : IRpcRequest
    {
        private static long ID = 0;
        public long Id { get; set; }

        public string Group { get; set; }

        public string Name { get; set; }

        public IDictionary<string, object> Parameter { get; set; }

        public int Timeout { get; set; }
    }
}
