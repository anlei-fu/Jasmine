using System.Collections.Generic;

namespace Jasmine.ConfigCenter.Common
{
    public  class ConfigCenterServiceRequest
    {
        private static volatile int _id;




        public long RequestId { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> Parameter;
    }
}
