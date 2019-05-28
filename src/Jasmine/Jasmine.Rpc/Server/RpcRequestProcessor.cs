using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    public  class RpcRequestProcessor: AbstractProcessor<RpcFilterContext>
    {
        public RpcRequestProcessor()
        {
            Filter = new RpcFilterPipeline();
            ErrorFilter = new RpcFilterPipeline();
        }
    }
}
