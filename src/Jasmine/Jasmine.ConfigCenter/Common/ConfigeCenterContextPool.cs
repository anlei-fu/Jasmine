using Jasmine.Common;

namespace Jasmine.ConfigCenter.Common
{
    public class ConfigeCenterContextPool : AbstractSimpleQueuedPool<ConfigCenterRequestContext>
    {
        public ConfigeCenterContextPool(int capacity) : base(capacity)
        {
        }

        protected override ConfigCenterRequestContext newInstance()
        {
            return new ConfigCenterRequestContext()
            {
                Response = new ConfigCenterServiceResponse()
                {
                    ResponseId = 0,
                    Message = null,
                    Result = null,
                    ResponseCode = ResponseCode.Suceessed
                }
            };
        }
    }
}
