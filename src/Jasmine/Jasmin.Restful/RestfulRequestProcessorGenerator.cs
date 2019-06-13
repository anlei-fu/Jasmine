using Jasmine.Common;
using Jasmine.Ioc;
using Jasmine.Restful.Exceptions;
using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class RestfulRequestProcessorGenerator : IRequestProcessorGenerator<HttpFilterContext, RestfulServiceMetaData>
    {

        private RestfulRequestProcessorGenerator()
        {

        }

        private IAopFilterProvider<HttpFilterContext> _aopProvider=>RestfulAopFilterProvider.Instance;
        private IParameterResolverFactory<HttpFilterContext,RestfulRequestMetaData> _parameterResolverFactory=>RestfulParameterResolverFactory.Instance;
        private IServiceProvider _serviceProvider => IocServiceProvider.Instance;


        public static readonly IRequestProcessorGenerator<HttpFilterContext, RestfulServiceMetaData> Instance = new RestfulRequestProcessorGenerator();



        public IRequestProcessor<HttpFilterContext>[] Generate(RestfulServiceMetaData metaData)
        {
            var ls = new List<IRequestProcessor<HttpFilterContext>>();

            foreach (var item in metaData.Requests)
            {
                var processor = new RestfulRequestProcessor(1000,item.Key);

                foreach (var before in item.Value.BeforeFilters)
                {
                    var filter = _aopProvider.GetFilter(before);

                    if (filter == null)
                        throw new FilterNotFoundException($" before filter {before} can not be found ,in type {metaData.RelatedType}, method {item.Value.Method.Name}");

                    processor.Filter.AddLast(filter);

                }

                foreach ( var around in item.Value.AroundFilters)
                {
                    var filter = _aopProvider.GetFilter(around);

                    if (filter == null)
                        throw new FilterNotFoundException($" around filter {around} can not be found ,in type {metaData.RelatedType}, method {item.Value.Method.Name}");

                     processor.Filter.AddLast(filter);
                }

                var proxy = new RestfulProxyFilter(item.Value.Method, _parameterResolverFactory.Create(item.Value), _serviceProvider.GetService(metaData.RelatedType));

                processor.Filter.AddLast(proxy);

                foreach (var around in item.Value.AroundFilters)
                {
                    var filter = _aopProvider.GetFilter(around);

                    if (filter == null)
                        throw new FilterNotFoundException($" around filter {around} can not be found ,in type {metaData.RelatedType}, method {item.Value.Method.Name}");

                    processor.Filter.AddLast(filter);
                }

                foreach (var after in item.Value.AfterFilters)
                {
                    var filter = _aopProvider.GetFilter(after);

                    if (filter == null)
                        throw new FilterNotFoundException($" after filter {after} can not be found ,in type {metaData.RelatedType}, method {item.Value.Method.Name}");

                    processor.Filter.AddLast(filter);
                }

                foreach (var error in item.Value.ErrorFilters)
                {
                    var filter = _aopProvider.GetFilter(error);

                    if (filter == null)
                        throw new FilterNotFoundException($" error filter {error} can not be found ,in type {metaData.RelatedType}, method {item.Value.Method.Name}");

                    processor.ErrorFilter.AddLast(filter);
                }

                processor.Description = item.Value.Description;

                processor.MaxConcurrency = item.Value.MaxConcurrency;

                processor.AlternativeServicePath = item.Value.AlternativeService;

                processor.Name = item.Value.Name;

                processor.Path = item.Value.Path;

                processor.HttpMethod = item.Value.HttpMethod;

                processor.GroupName = metaData.Name;

                ls.Add(processor);
            }

            return ls.ToArray(); 
        }
    }
}
