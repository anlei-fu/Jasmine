using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    internal   class RestfulApplicationGlobalConfig
    {
        public static bool DebugMode { get; set; }
        public static bool GenerateTestFile { get; set; }
        public static RestfulProcessorManager ProcessorManager { get; }
        public static bool GenerateDescriptionFile { get; set; }
        public static bool UseDashBorad { get; set; }
        public static bool StaticFileEnabled { get; set; }
        public static string VirtueRootPath { get; set; }
        public static long MaxFileCacheMeomoryUsage { get; set; }
        public static bool UseDefaultErrorFilter { get; set; }
        public static ServerConfig ListenOption { get; }
        public static bool EnableCrossDomain { get; set;}
        public static DefaultIntercepterConfig DefaultIntercepterConfig { get; }
       
    }

    public  class DefaultIntercepterConfig
    {
        public List<Type> GetBeforeFilters()
        {
            return null;
        }

        public List<Type> GetAroundFilters()
        {
            return null;
        }

        public List<Type> GetAfterFilters()
        {
            return null;
        }

        public void AddBeforeFiler(Type type)
        {
           
        }
        public void AddAroundFilter(Type type)
        {

        }
        public void AddAfterFilter(Type type)
        {

        }
        internal void SetDefaultErrorFilter(Type type)
        {

        }
    }
}
