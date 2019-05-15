using Jasmine.Common;

namespace Jasmine.Restful
{
    public class JasmineRequestFilterPieLineBuilder : IRequestProcessorBuilder<HttpFilterContext>
    {
        public IRequestProcessorBuilder<HttpFilterContext> AddErrorFirst(IFilter<HttpFilterContext> filter)
        {
            throw new System.NotImplementedException();
        }

        public IRequestProcessorBuilder<HttpFilterContext> AddErrorLast(IFilter<HttpFilterContext> filter)
        {
            throw new System.NotImplementedException();
        }

        public IRequestProcessorBuilder<HttpFilterContext> AddFirst(IFilter<HttpFilterContext> filter)
        {
            throw new System.NotImplementedException();
        }

        public IRequestProcessorBuilder<HttpFilterContext> AddLast(IFilter<HttpFilterContext> filter)
        {
            throw new System.NotImplementedException();
        }

        public IRequestProcessorBuilder<HttpFilterContext> Append(IFilter<HttpFilterContext> filter)
        {
            throw new System.NotImplementedException();
        }

        public IRequestProcessor<HttpFilterContext> Build()
        {
            throw new System.NotImplementedException();
        }

        public IRequestProcessorBuilder<HttpFilterContext> SetStat()
        {
            throw new System.NotImplementedException();
        }
    }
}
