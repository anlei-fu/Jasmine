using System.Collections.Generic;

namespace Jasmine.ConfigCenter.Common
{
    public  class ConfigCenterServiceRequest
    {
        public long RequestId { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> Parameter;
    }
}
