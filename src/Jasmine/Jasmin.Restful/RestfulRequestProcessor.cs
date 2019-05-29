using Jasmine.Common;

namespace Jasmine.Restful
{
    public  class RestfulRequestProcessor:AbstractProcessor<HttpFilterContext>
    {
        public RestfulRequestProcessor(int maxConcurrency,string name):base(maxConcurrency,name)
        {
            ErrorFilter = new RestfulFilterPipeline();
            Filter = new RestfulFilterPipeline();
        }
        public string HttpMethod { get; set; }

     
    }
}
