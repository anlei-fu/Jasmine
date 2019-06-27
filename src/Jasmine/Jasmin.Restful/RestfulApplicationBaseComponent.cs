using Jasmine.Common;
using Jasmine.Ioc;
using Jasmine.Restful.DefaultFilters;
using Jasmine.Restful.Implement;
using Microsoft.AspNetCore.Http;
using System;

namespace Jasmine.Restful
{
    public class RestfulApplicationBaseComponents
    {
        public static IStaticFileProvider StaticFileProvider { get;internal set; }
        public static RestfulServiceManager RestfulServiceManager => RestfulServiceManager.Instance;
        public static IDispatcher<HttpFilterContext> Dispatcher { get; internal set; } = new RestfulDispatcher("restful-dispatcher", RestfulServiceManager);
        public static IMiddleware RestfulMiddleware { get; internal set; } = new ResfulMiddleware();
        public static IocServiceProvider ServicePovider => IocServiceProvider.Instance;
        public static ResfulServiceMetaDataManager RestfulServiceMetaDataManager => ResfulServiceMetaDataManager.Instance;
        public static IRequestProcessorGenerator<HttpFilterContext, RestfulServiceMetaData> ProcessorGenerator => RestfulRequestProcessorGenerator.Instance;
        public static RestfulFilterMetaDataManager FilterMetaDataManager => RestfulFilterMetaDataManager.Instance;
        public static IAopFilterProvider<HttpFilterContext> FilterManager => RestfulAopFilterProvider.Instance;
        public static RestfulTracer Tracer { get; set; }


    }
}
