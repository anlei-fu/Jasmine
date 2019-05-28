using Jasmine.Common;

namespace Jasmine.Restful
{
    public  class RestfulRequestProcessor:AbstractProcessor<HttpFilterContext>
    {
        public RestfulRequestProcessor()
        {
            ErrorFilter = new RestfulFilterPipeline();
            Filter = new RestfulFilterPipeline();
        }
        public string HttpMethod { get; set; }

     
    }
}
