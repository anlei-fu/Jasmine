using Jasmine.Common;
using System;

namespace Jasmine.ConfigCenter.Common
{
    public class ConfigeCenterContextPool : AbstractSimpleQueuedPool<ConfigCenterRequestContext>
    {
        public ConfigeCenterContextPool(int capacity) : base(capacity)
        {
        }

        protected override ConfigCenterRequestContext createNew()
        {
            throw new NotImplementedException();
        }
    }
}
