using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    public class RpcFilterContextPool : AbstractSimpleQueuedPool<RpcFilterContext>
    {
        public RpcFilterContextPool(int capacity) : base(capacity)
        {
        }

        protected override RpcFilterContext newInstance()
        {
            throw new System.NotImplementedException();
        }
    }
}
