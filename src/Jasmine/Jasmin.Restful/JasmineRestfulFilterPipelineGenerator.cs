using Jasmine.Common;
using Jasmine.Restful.Exceptions;
using System;

namespace Jasmine.Restful
{
    public class JasmineFilterPipelineGenerator : IFilterPipelineGenerator<HttpFilterContext, RestfulServiceMetaData>
    {
        private IAopFilterProvider<HttpFilterContext> _aopProvider;
        private IParameterResolverFactory<HttpFilterContext> _parameterResolverFactory;
        private IServiceProvider _serviceProvider;
        public IFilterPipeline<HttpFilterContext> Generate(RestfulServiceMetaData metaData)
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

                var proxyFilter = new AbstractProxyFilter<HttpFilterContext>(item.Value.Method,
                                                                            _parameterResolverFactory.Create(item.Value.Method),
                                                                             _serviceProvider.GetService(metaData.RelatedType),
                                                                             "");

                builder.AddLast(proxyFilter);

                foreach (var after in collection)
                {

                }



            }
        }
    }
}
