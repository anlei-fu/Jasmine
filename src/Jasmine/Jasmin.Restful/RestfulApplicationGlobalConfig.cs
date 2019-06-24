using System;
using System.Collections.Generic;
using System.IO;

namespace Jasmine.Restful
{
    internal   class RestfulApplicationGlobalConfig
    {
        public static bool DebugMode { get; set; } = false;
        public static bool GenerateTestFile { get; set; } = false;
        public static bool GenerateDescriptionFile { get; set; } = false;
        public static bool AxiosFile { get; set; } = false;
        public static bool GenareteClientCode { get; set; } = false;
        public static bool UseDashBorad { get; set; } = true;
        public static bool StaticFileEnabled { get; set; } = true;
        public static string VirtueRootPath { get; set; } = Directory.GetCurrentDirectory();
        public static long MaxFileCacheMeomoryUsage { get; set; } = 1024L * 1024 * 1024 * 2;
        public static bool EnableSystemApi { get; set; } = true;
        public static ServerConfig ServerConfig { get; } = new ServerConfig();
        public static bool EnableCrossDomain { get; set; } = false;
        internal static GloabalIntercepterConfig GlobalIntercepterConfig { get; } = new GloabalIntercepterConfig();
       
    }

    public  class GloabalIntercepterConfig
    {

        private List<Type> _beforeFilters=new List<Type>();
        private List<Type> _aroundFilters = new List<Type>();
        private List<Type> _afterFilters = new List<Type>();
        private Type _errorFilter;
        public List<Type> GetBeforeFilters()
        {
            return _beforeFilters;
        }

        public void RemoveBeforeFilter(Type type)
        {
            _beforeFilters.Remove(type);
        }
        public void RemoveAround(Type type)
        {
            _aroundFilters.Remove(type);
        }

        public void RemoveAfter(Type type)
        {
            _afterFilters.Remove(type);
        }

        public List<Type> GetAroundFilters()
        {
            return _aroundFilters;
        }

        public List<Type> GetAfterFilters()
        {
            return _afterFilters;
        }
        public Type GetErrorFilter()
        {
            return _errorFilter;
        }

        public void AddBeforeFiler(Type type)
        {
            _beforeFilters.Add(type);
        }
        public void AddAroundFilter(Type type)
        {
            _aroundFilters.Add(type);
        }
        public void AddAfterFilter(Type type)
        {
            _afterFilters.Add(type);
        }
        internal void SetDefaultErrorFilter(Type type)
        {
            _errorFilter = type;
        }
    }
}
