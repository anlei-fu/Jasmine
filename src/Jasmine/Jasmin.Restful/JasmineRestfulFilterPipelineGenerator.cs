using Jasmine.Common;

namespace Jasmine.Restful
{
    public class JasmineFilterPipelineGenerator : IFilterPipelineGenerator<HttpFilterContext, RestfulServiceMetaData>
    {
        public IFilterPipeline<HttpFilterContext> Generate(RestfulServiceMetaData metaData)
        {
            throw new System.NotImplementedException();
        }
    }
}
