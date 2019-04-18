using Jasmine.Common;
using System;

namespace Jasmine.Rpc.Client
{
    public class RpcCallContextPool : AbstractSimpleQueuedPool<RpcCallContext<IRpcResponse>>
    {
        public RpcCallContextPool(int capacity) : base(capacity)
        {
        }

        protected override RpcCallContext<IRpcResponse> createNew()
        {
            throw new NotImplementedException();
        }
    }
}
