using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    public  class RpcRequestProcessor: AbstractProcessor<RpcFilterContext>
    {
        public RpcRequestProcessor(int maxConcurrency,string name):base(maxConcurrency,name)
        {
            Filter = new RpcFilterPipeline();
            ErrorFilter = new RpcFilterPipeline();
        }
    }
}
