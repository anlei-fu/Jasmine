using Jasmine.Common;

namespace Jasmine.Restful
{
    public  class RestfulRequestProcessor:AbstractProcessor<HttpFilterContext>
    {
        public RestfulRequestProcessor()
        {
            ErrorFilter = new RestfulfilterPipeline();
            Filter = new RestfulfilterPipeline();
        }
        public string HttpMethod { get; set; }

     
    }
}
