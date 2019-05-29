using Jasmine.Common;
using Jasmine.Common.Exceptions;
using Jasmine.Ioc;
using Jasmine.Serialization;
using System;
using System.Collections.Generic;

namespace Jasmine.Rpc.Server
{
    public class RpcRequestProcessorGenerator : IRequestProcessorGenerator<RpcFilterContext, RpcServiceMetaData>
    {
        private IAopFilterProvider<RpcFilterContext> _aopProvider => RpcAopFilterProvider.Instance;
        private IParameterResolverFactory<RpcFilterContext, RpcRequestMetaData> _parameterResolverFactory => RpcParameterResolverFactory.Instance;
        private IServiceProvider _serviceProvider => IocServiceProvider.Instance;

        public static readonly IRequestProcessorGenerator<RpcFilterContext, RpcServiceMetaData> Instance = new RpcRequestProcessorGenerator();

        public IRequestProcessor<RpcFilterContext>[] Generate(RpcServiceMetaData metaData)
        {
            var ls = new List<IRequestProcessor<RpcFilterContext>>();

            foreach (var item in metaData.Requests)
            {
                var processor = new RpcRequestProcessor(1000,item.Key);

                foreach (var before in item.Value.BeforeFilters)
                {
                    var filter = _aopProvider.GetFilter(before);

                    if (filter == null)
                        throw new FilterNotFoundException($" before filter {before} can not be found ,in type {metaData.RelatedType}, method {item.Value.Method.Name}");

                    processor.Filter.AddLast(filter);

                }

                foreach (var around in item.Value.AroundFilters)
                {
                    var filter = _aopProvider.GetFilter(around);

                    if (filter == null)
                        throw new FilterNotFoundException($" around filter {around} can not be found ,in type {metaData.RelatedType}, method {item.Value.Method.Name}");

                    processor.Filter.AddLast(filter);
                }

                var proxy = new RpcProxyFilter(item.Value.Method, _parameterResolverFactory.Create(item.Value), _serviceProvider.GetService(metaData.RelatedType), item.Value.Name,JsonSerializer.Instance);

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

                processor.Name = item.Value.Name;

                processor.Path = item.Value.Path;

                processor.GroupName = metaData.Name;

                ls.Add(processor);
            }

            return ls.ToArray();
        }
    }
}
