using Jasmine.Common;

namespace Jasmine.Restful
{
    public   class RestfulRequestParameterMetaData:ParameterMetaDataBase
    {
        public bool FromData { get; set; }
        public bool FromBody { get; set; }
        public bool FromQueryString { get; set; }
        public string QueryString { get; set; }
        public string DataString { get; set; }
        public string PathVariable { get; set; }
        public bool FromPathVariable { get; set; }
        public bool FromForm { get; set; }
    }
}
