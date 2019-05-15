using Jasmine.Common;

namespace Jasmine.Restful
{
    public   class RestfulRequestParameterMetaData:ParameterMetaDataBase
    {
        public bool FromData => DataKey != null;
        public bool FromBody { get; set; }
        public bool FromQueryString => QueryStringKey != null;
        public string QueryStringKey { get; set; }
        public string DataKey { get; set; }
        public string PathVariableKey { get; set; }
        public bool FromPathVariableKey { get; set; }
        public bool FromForm { get; set; }
    }
}
