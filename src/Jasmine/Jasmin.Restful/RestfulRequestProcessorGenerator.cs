using Jasmine.Common;
using Jasmine.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Restful
{
    internal class RestfulRequestProcessorGenerator : IRequestProcessorGenerator<HttpFilterContext, RestfulServiceMetaData>
    {

        private RestfulRequestProcessorGenerator()
        {

        }

        private IAopFilterProvider<HttpFilterContext> _aopProvider=>RestfulAopFilterProvider.Instance;
        private IParameterResolverFactory<HttpFilterContext,RestfulRequestMetaData> _parameterResolverFactory=>RestfulParameterResolverFactory.Instance;
        private IServiceProvider _serviceProvider => IocServiceProvider.Instance;


        public static readonly IRequestProcessorGenerator<HttpFilterContext, RestfulServiceMetaData> Instance = new RestfulRequestProcessorGenerator();

        private IList<IFilter<HttpFilterContext>> buildFilters(IEnumerable<Type> filterTypes)
        {
            var ls = new List<IFilter<HttpFilterContext>>();

            foreach (var item in filterTypes)
            {
                ls.AddRange(buildFilter(item));
            }

            return ls;
        }

        private IList<IFilter<HttpFilterContext>> buildFilter(Type type)
        {
           if(!RestfulFilterMetaDataManager.Instance.ContainsKey(type))
            {
                RestfulFilterMetaDataManager.Instance.Add(type, RestfulFilterMetaDataReflectResolver.Instance.Resolve(type));
            }

            var ls = new List<IFilter<HttpFilterContext>>();

           if(RestfulFilterMetaDataManager.Instance.TryGetValue(type,out var metaData))
            {
                foreach (var item in metaData.BeforeFilters)
                    ls.AddRange(buildFilter(item));

                foreach (var item in metaData.AroundFilters)
                    ls.AddRange(buildFilter(item));

                ls.Add(_aopProvider.GetFilter(type));

                foreach (var item in metaData.AroundFilters)
                    ls.AddRange(buildFilter(item));

                foreach (var item in metaData.AfterFilters)
                    ls.AddRange(buildFilter(item));
            }

            return ls;
        }

        public IRequestProcessor<HttpFilterContext>[] Generate(RestfulServiceMetaData metaData)
        {
            var ls = new List<IRequestProcessor<HttpFilterContext>>();

            var instance = _serviceProvider.GetService(metaData.RelatedType);

            foreach (var item in metaData.Requests)
            {
                var processor = new RestfulRequestProcessor(1000,item.Key);

                processor.Pipeline = new RestfulFilterPipeline();
                processor.ErrorPileline = new RestfulFilterPipeline();

                foreach (var before in buildFilters(RestfulApplicationGlobalConfig.GlobalIntercepterConfig.GetBeforeFilters().Union(item.Value.BeforeInterceptors).Union(metaData.BeforeInterceptors)))
                {
                    processor.Pipeline.AddLast(before);
                }

                foreach (var around in buildFilters(RestfulApplicationGlobalConfig.GlobalIntercepterConfig.GetAroundFilters().Union(item.Value.AroundInterceptors).Union(metaData.AroundInterceptors)))
                {
                    processor.Pipeline.AddLast(around);
                }

                var proxy = new RestfulInvokationProxyFilter(item.Value.Method, RestfulParameterResolverFactory.Instance.Create(item.Value), instance);

                processor.Pipeline.AddLast(proxy);

                foreach (var around in buildFilters(RestfulApplicationGlobalConfig.GlobalIntercepterConfig.GetAroundFilters().Union(item.Value.AroundInterceptors).Union(metaData.AroundInterceptors)))
                {
                    processor.Pipeline.AddLast(around);
                }

                foreach (var after in buildFilters(RestfulApplicationGlobalConfig.GlobalIntercepterConfig.GetAfterFilters().Union(item.Value.AfterInterceptors).Union(metaData.AfterInterceptors)))
                {
                    processor.Pipeline.AddLast(after);
                }


                foreach (var error in buildFilters(item.Value.ErrorInterceptors.Union(metaData.ErrorInterceptors)))
                {
                    processor.ErrorPileline.AddLast(error);
                }

                var errorFilter = RestfulApplicationGlobalConfig.GlobalIntercepterConfig.GetErrorFilter();

                if(errorFilter!=null)
                {
                    foreach (var error in buildFilter(errorFilter))
                    {
                        processor.ErrorPileline.AddLast(error);
                    }
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
