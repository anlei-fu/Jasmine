using System.Collections.Generic;

namespace Jasmine.Rpc.Client
{
    public interface IRpcRequestFactory
    {
        IRpcRequest Create(string group, string name, IDictionary<string, object> parameter, int timeout);
        IRpcRequest Create(string name, IDictionary<string, object> parameter, int timeout);
        IRpcRequest Create(string group, string name, object parameter, int timeout);
        IRpcRequest Create(string name, object parameter, int timeout);
    }
}
