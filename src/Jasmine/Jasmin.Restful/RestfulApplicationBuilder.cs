using Jasmine.Common;
using Jasmine.Configuration;
using Jasmine.Ioc;
<<<<<<< HEAD
=======
using Jasmine.Restful.Attributes;
using Jasmine.Restful.DefaultServices;
>>>>>>> d602d8538ab8a3387686c49e63db63bc558f7820
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
<<<<<<< HEAD
            
=======
            ConfigServiceProvider(x =>
            {
                x.SetImplementationMapping(typeof(IRequestProcessorManager<HttpFilterContext>), typeof(RestfulProcessorManager));
                x.AddSigleton(typeof(RestfulProcessorManager), RestfulProcessorManager.Instance);
            });

            AddRestfulService(typeof(RestfulProcessorManager));
            AddRestfulService(typeof(LoginService));
>>>>>>> d602d8538ab8a3387686c49e63db63bc558f7820
        }
        /// <summary>
        ///  a service stat and config web ui 
        ///  use any browser to open http://localhost :port/api/dashboard
        /// </summary>
        /// <returns></returns>
        public RestfulApplicationBuilder UseDashBoard()
        {
            return this;
        }
        /// <summary>
        /// if do't use ssl ,it doesn't need to config
        /// <see cref="SslOption"/>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder UseSsl(Action<SslOption> config)
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
            return this;
        }
        /// <summary>
        /// create a test file use to test application
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder GenerateTestFile(bool option)
        {
            return this;
        }
        /// <summary>
        /// create a description file to descript application
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder GenerateDescriptionFile(bool option)
        {
            return this;
        }
        /// <summary>
        ///  use static file  service
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder UseStaticFile(Action<StaticFileOption> config)
        {
            return this;
        }
        /// <summary>
        /// config global  configuration  povider <see cref="JasmineConfigurationProvider"/>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder ConfigXmlConfiguration(Action<JasmineConfigurationProvider>config)
        {
            return this;
        }
        /// <summary>
        /// set default error filter ,the default is <see cref="Jasmine.Restful.DefaultFilters.DefaultErrorFilter"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public RestfulApplicationBuilder UseDefaultErrorFileter<T>() where T:IFilter<HttpFilterContext>
        {
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
        public RestfulApplication Build(string xmlConfigFilePath)
        {
            return null;
        }
    }
}
