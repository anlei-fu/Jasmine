using Jasmine.Common;

namespace Jasmine.Restful
{
    public  class RestfulRequestProcessor:AbstractProcessor<HttpFilterContext>
    {
        public RestfulRequestProcessor(int maxConcurrency,string name):base(maxConcurrency,name)
        {
            ErrorPileline = new RestfulFilterPipeline();
            Pipeline = new RestfulFilterPipeline();
        }
        public string HttpMethod { get; set; }
        public RestfulRequestMetaData MetaData { get; set; }
     
    }
}
