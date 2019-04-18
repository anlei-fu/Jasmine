using Jasmine.Common;

namespace Jasmine.Restful
{
    public   class RestfulRequestMetaData:ServiceMetaDataBase
    {
        public string HttpMethod { get; set; }
        public RestfulRequestParameterMetaData[] Parameters { get; set; }
    }
}
