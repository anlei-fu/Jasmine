using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    public class RpcRequestProcessorManager : AbstractProcessorManager<RpcFilterContext>
    {
        private RpcRequestProcessorManager()
        {

        }
        public static readonly IRequestProcessorManager<RpcFilterContext> Instance = new RpcRequestProcessorManager();
        public override string Name => "Jasmine.Rpc.RequestProcessorManager";
    }
}
