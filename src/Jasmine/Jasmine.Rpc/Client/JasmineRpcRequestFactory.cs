using System.Collections.Generic;

namespace Jasmine.Rpc.Client
{
    public class JasmineRpcRequestFactory : IRpcRequestFactory
    {
        public IRpcRequest Create(string group, string name, IDictionary<string, object> parameter, int timeout)
        {
            throw new System.NotImplementedException();
        }

        public IRpcRequest Create(string name, IDictionary<string, object> parameter, int timeout)
        {
            throw new System.NotImplementedException();
        }

        public IRpcRequest Create(string group, string name, object parameter, int timeout)
        {
            throw new System.NotImplementedException();
        }

        public IRpcRequest Create(string name, object parameter, int timeout)
        {
            throw new System.NotImplementedException();
        }
    }
}
