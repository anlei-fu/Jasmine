using System;

namespace Jasmine.Restful
{
    internal  static class RestfulApplicationGlobalConfig
    {
        public static bool DebugMode { get; set; }
        public static bool GenerateTestFile { get; set; }
        public static RestfulProcessorManager ProcessorManager { get; set; }
        public static bool GenerateDescriptionFile { get; set; }
        public static bool UseDashBorad { get; set; }
        public static bool StaticFileEnabled { get; set; }
        public static string VirtueRootPath { get; set; }
        public static long MaxFileCacheMeomoryUsage { get; set; }
        public static bool UseDefaultErrorFilter { get; set; }
        public static Type DefaultErrorFilter { get; set; }
        public static ServerConfig ListenOption { get; set; }
        public static bool EnableCrossDomain { get; set;}
       
    }
}
