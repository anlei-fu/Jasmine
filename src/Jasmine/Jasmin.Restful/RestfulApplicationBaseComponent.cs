﻿using Jasmine.Common;
using Jasmine.Ioc;
using Microsoft.AspNetCore.Http;
using System;

namespace Jasmine.Restful
{
    public class RestfulApplicationBaseComponents
    {
        public static IStaticFileProvider StaticFileProvider { get;internal set; }
        public static IRequestProcessorManager<HttpFilterContext> RequestfulServiceManager => RestfulServiceManager.Instance;
        public static IDispatcher<HttpFilterContext> Dispatcher { get; internal set; }
        public static IMiddleware RestfulMiddleware { get; internal set; }
        public static IServiceProvider ServicePovider => IocServiceProvider.Instance;
        public static ResfulServiceMetaDataManager RestfulServiceMetaDataManager => ResfulServiceMetaDataManager.Instance;
        public static IRequestProcessorGenerator<HttpFilterContext, RestfulServiceMetaData> ProcessorGenerator => RestfulRequestProcessorGenerator.Instance;
        public static RestfulFilterMetaDataManager FilterMetaDataManager => RestfulFilterMetaDataManager.Instance;
        public static IAopFilterProvider<HttpFilterContext> FilterManager => RestfulAopFilterProvider.Instance;



    }
}
