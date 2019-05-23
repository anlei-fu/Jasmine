using Jasmine.Common;
using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public class RpcRequestDispatcher : AbstractDispatcher<RpcFilterContext>
    {
        public RpcRequestDispatcher(string name, IRequestProcessorManager<RpcFilterContext> processorManager) : base(name, processorManager)
        {
        }

        public override Task DispatchAsync(string path, RpcFilterContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
