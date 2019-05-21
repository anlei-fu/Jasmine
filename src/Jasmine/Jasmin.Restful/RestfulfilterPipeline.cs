using Jasmine.Common;

namespace Jasmine.Restful
{
    public class RestfulfilterPipeline : AbstractFilterPipeline<HttpFilterContext>
    {
        public override string Name => "Restful-Filter-Pipeline";
    }
}
