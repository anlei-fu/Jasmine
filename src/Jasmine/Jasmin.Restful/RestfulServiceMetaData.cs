using Jasmine.Common;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public  class RestfulServiceMetaData:ServiceMetaDataBase
    {
        public string HttpMethod { get; set; }
        public IDictionary<string, RestfulRequestMetaData> Requests { get; set; }
    }
}
