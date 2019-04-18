using System.Collections.Generic;

namespace Jasmine.Rpc.Client
{
    public   interface IRpcClient
    {
        IRpcResponse Call(IRpcRequest request);
        IRpcResponse Call(string group, string name, IDictionary<string, object> parameters,int timeout=10000);
        IRpcResponse Call(string name, IDictionary<string, object> parameters, int timeout = 10000);
        IRpcResponse Call(string group, string name, object parameter, int timeout = 10000);
        IRpcResponse Call(string name, object parameter, int timeout = 10000);
        T Call<T>(IRpcRequest request);
        T Call<T>(string group, string name, IDictionary<string, object> parameters, int timeout = 10000);
        T Call<T>(string name, IDictionary<string, object> parameters, int timeout = 10000);
        T Call<T>(string group, string name, object parameter, int timeout = 10000);
        T Call<T>(string name, object parameter, int timeout = 10000);
    }
}
