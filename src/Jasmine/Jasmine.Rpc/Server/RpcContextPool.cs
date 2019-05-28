using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    public class RpcContextPool : AbstractSimpleQueuedPool<RpcContext>
    {
        public RpcContextPool(int capacity) : base(capacity)
        {
        }

        protected override RpcContext newInstance()
        {
            return new RpcContext();
        }
    }
}
