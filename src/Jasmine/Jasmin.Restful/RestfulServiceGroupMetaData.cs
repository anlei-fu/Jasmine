using Jasmine.Common;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public  class RestfulServiceGroupMetaData:AopServiceMetaData
    {
        public SerializeMode SerializeMode { get; set; }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public IDictionary<string, RestfulServiceMetaData> Requests { get; set; }
    }
}
