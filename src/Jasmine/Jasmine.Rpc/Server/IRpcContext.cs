using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public  interface IRpcContext
    {
        IRpcRequest Request { get; }
        IRpcResponse Response { get; }
        object ReturnValue { get; }

    }
}
