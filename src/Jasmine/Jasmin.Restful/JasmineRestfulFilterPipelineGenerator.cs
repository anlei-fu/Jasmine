using Jasmine.Common;
using Jasmine.Restful.Exceptions;
using System;

namespace Jasmine.Restful
{
    public class JasmineFilterPipelineGenerator : IRequestProcessorGenerator<HttpFilterContext, RestfulServiceGroupMetaData>
    {
        private IAopFilterProvider<HttpFilterContext> _aopProvider;
        private IParameterResolverFactory<HttpFilterContext> _parameterResolverFactory;
        private IServiceProvider _serviceProvider;
        public IRequestProcessor<HttpFilterContext> Generate(RestfulServiceGroupMetaData metaData)
        {
            var builder = new FilterPipelineBuilder<HttpFilterContext>();

            foreach (var item in metaData.Requests)
            {
                foreach (var before in item.Value.BeforeFilters)
                {
                    var filter = _aopProvider.GetFilter(before);

                    if (filter == null)
                        throw new FilterNotFoundException("");

                    builder.AddLast(filter);

                }

            }

            return null;
        }
    }
}
