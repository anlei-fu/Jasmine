using Jasmine.Common;

namespace Jasmine.Restful
{
    public class RestfulFilterPipeline : AbstractFilterPipeline<HttpFilterContext>
    {
        public override string Name => "Restful-Filter-Pipeline";

        public RestfulFilterPipeline Clone()
        {
            return null;
        }
    }

  
}
