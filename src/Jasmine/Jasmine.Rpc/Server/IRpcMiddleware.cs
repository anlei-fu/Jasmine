using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    /// <summary>
    ///  a plug  interface for <see cref="RpcServer"/>
    /// </summary>
    public interface IRpcMiddleware
    {
        Task ProcessRequest(RpcContext context);
    }
}
