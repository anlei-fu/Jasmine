using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Reflection;
using System;

namespace Jasmine.Restful
{
    public class RestfulFilterMetaDataReflectResolver : IMetaDataReflectResolver<FilterMetaData>
    {
        private RestfulFilterMetaDataReflectResolver()
        {

        }
        public static readonly RestfulFilterMetaDataReflectResolver Instance = new RestfulFilterMetaDataReflectResolver();
        public FilterMetaData Resolve(Type type)
        {
            var attrs = JasmineReflectionCache.Instance.GetItem(type).Attributes;

            var metaData = new FilterMetaData();


            foreach (var item in attrs.GetAttribute<AfterInterceptorAttribute>())
                metaData.AfterFilters.Add(item.FilterType);

            foreach (var item in attrs.GetAttribute<BeforeInterceptorAttribute>())
                metaData.BeforeFilters.Add(item.FilterType);

            foreach (var item in attrs.GetAttribute<AroundInterceptorAttribute>())
                metaData.AroundFilters.Add(item.FilterType);

            return metaData;

        }
    }
}
