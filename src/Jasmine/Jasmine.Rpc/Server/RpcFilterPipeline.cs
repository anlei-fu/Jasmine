using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    public class RpcFilterPipeline : AbstractFilterPipeline<RpcFilterContext>
    {
        public RpcFilterPipeline()
        {

        }
        public override string Name => "Rpc.Filter.Pipeline";
    }
}
