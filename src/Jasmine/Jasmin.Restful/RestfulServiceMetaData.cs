using Jasmine.Common;
using Jasmine.Serialization;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public  class RestfulServiceMetaData:AopServiceMetaData
    {
        public string IgnorePattern { get; set; }
        public string Description { get; set; }
        public int MaxConcurrency { get; set; }
        public SerializeMode SerializeMode { get; set; }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public IDictionary<string, RestfulRequestMetaData> Requests { get; set; } = new Dictionary<string, RestfulRequestMetaData>();
    }
}
