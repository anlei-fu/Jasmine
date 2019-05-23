using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public interface IRpcRequestDispatcher
    {
       Task Dispatch(IRpcContext context);
    }
}
