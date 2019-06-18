using Jasmine.Common;
using Jasmine.Configuration;
using Jasmine.Ioc;
using System;

namespace Jasmine.Restful
{
    /// <summary>
    ///   a restful application builder ,you can build a restful application
    ///   through lots of config or a xml config file,the file name require is 'restful.config'
    /// </summary>
    public class RestfulApplicationBuilder
    {

        public RestfulApplicationBuilder()
        {
          
        }
   
        public RestfulApplicationBuilder ConfigServer(Action<ServerConfig> config)
        {
            return this;
        }
        public RestfulApplicationBuilder GenerateTestFile()
        {
            RestfulApplicationGlobalConfig.GenerateTestFile = true;

            return this;
        }

        public RestfulApplicationBuilder ConfigLog4()
        {
            return this;
        }

        public RestfulApplicationBuilder SetDebugMode()
        {
            return this;
        }

        public RestfulApplicationBuilder GenerateDescription()
        {
            RestfulApplicationGlobalConfig.GenerateTestFile = true;
            return this;
        }
        /// <summary>
        ///  a service stat and config web ui 
        ///  use any browser to open http://localhost :port/api/dashboard
        /// </summary>
        /// <returns></returns>
        public RestfulApplicationBuilder DisableDashBoard()
        {
           RestfulApplicationGlobalConfig.UseDashBorad=false;
            return this;
        }

        public RestfulApplicationBuilder DisnableSystemHttpApi()
        {
            return this;
        }
       
        /// <summary>
        /// config restful service <see cref="RestfulProcessorManager"/>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder ConfigRestfulService(Action<RestfulProcessorManager> config)
        {
            config(RestfulApplicationGlobalConfig.ProcessorManager);

            return this;
        }
      
        public RestfulApplicationBuilder UseStaticFile(string virtueRootPath,long maxMemoryUsage)
        {
            RestfulApplicationGlobalConfig.VirtueRootPath = virtueRootPath;
            RestfulApplicationGlobalConfig.StaticFileEnabled = true;
            RestfulApplicationGlobalConfig.MaxFileCacheMeomoryUsage = maxMemoryUsage;

            return this;
        }
  
        /// <summary>
        /// config global  configuration  povider <see cref="JasmineConfigurationProvider"/>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder ConfigXmlConfiguration(Action<JasmineConfigurationProvider>config)
        {
            config(JasmineConfigurationProvider.Instance);

            return this;
        }
        /// <summary>
        /// set default error filter ,the default is <see cref="Jasmine.Restful.DefaultFilters.DefaultErrorFilter"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public RestfulApplicationBuilder UseDefaultErrorFileter<T>() where T:IFilter<HttpFilterContext>
        {
            RestfulApplicationGlobalConfig.UseDefaultErrorFilter = true;

            RestfulApplicationGlobalConfig.DefaultErrorFilter = typeof(T);

            return this;
        }
        
        /// <summary>
        /// config ioc service provider <see cref="IocServiceProvider"/>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>

        public RestfulApplicationBuilder ConfigServiceProvider(Action<IocServiceProvider> config)
        {
            config(IocServiceProvider.Instance);

            return this;
        }
        /// <summary>
        /// build a restful app
        /// </summary>
        /// <returns></returns>
        public RestfulApplication Build()
        {
            return null;
        }
        public RestfulApplicationBuilder LoadConfigFile(string xmlConfigFilePath)
        {
            return null;
        }
    }
}
