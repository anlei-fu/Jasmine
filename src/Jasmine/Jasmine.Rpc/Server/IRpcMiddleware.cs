using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public interface IRpcMiddleware
    {
        Task InvokeAsync(RpcContext context);
    }
}
