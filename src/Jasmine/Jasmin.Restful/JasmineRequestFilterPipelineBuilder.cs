using Jasmine.Common;

namespace Jasmine.Restful
{
    public class JasmineRequestFilterPieLineBuilder : IFilterPipelineBuilder<HttpFilterContext>
    {
        public IFilterPipelineBuilder<HttpFilterContext> AddFirst(IFilter<HttpFilterContext> filter)
        {
            throw new System.NotImplementedException();
        }

        public IFilterPipelineBuilder<HttpFilterContext> AddLast(IFilter<HttpFilterContext> filter)
        {
            throw new System.NotImplementedException();
        }

        public IFilterPipelineBuilder<HttpFilterContext> Append(IFilter<HttpFilterContext> filter)
        {
            throw new System.NotImplementedException();
        }

        public IFilterPipeline<HttpFilterContext> Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
