using Jasmine.ConfigCenter.Server;

namespace Jasmine.ConfigCenter.Common
{
    public  class ConfigCenterRequestContext
    {
        public ConnectionInfo Connection { get; set; }
        public ConfigCenterServiceRequest Request { get; set; }
        public ConfigCenterServiceResponse Response { get; set; }
    }
}
