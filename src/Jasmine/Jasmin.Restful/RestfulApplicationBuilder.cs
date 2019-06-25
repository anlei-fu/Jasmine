using Jasmine.Configuration;
using Jasmine.Ioc;
using Jasmine.Restful.DefaultFilters;
using Jasmine.Restful.DefaultServices;
using System;
using System.IO;

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
            config(RestfulApplicationGlobalConfig.ServerConfig);

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
            RestfulApplicationGlobalConfig.DebugMode = true;
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
            RestfulApplicationGlobalConfig.UseDashBorad = false;

            return this;
        }

        public RestfulApplicationBuilder DisnableSystemHttpApi()
        {
            RestfulApplicationGlobalConfig.EnableSystemApi = false;

            return this;
        }

        /// <summary>
        /// config restful service <see cref="RestfulServiceManager"/>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder ConfigRestfulService(Action<ResfulServiceMetaDataManager> config)
        {
            config(RestfulApplicationBaseComponents.RestfulServiceMetaDataManager);

            return this;
        }

        public RestfulApplicationBuilder UseStaticFile(long maxMemoryUsage, string virtueRootPath = null)
        {
            RestfulApplicationGlobalConfig.StaticFileEnabled = true;
            RestfulApplicationGlobalConfig.VirtueRootPath = virtueRootPath ?? Directory.GetCurrentDirectory();
            RestfulApplicationGlobalConfig.MaxFileCacheMeomoryUsage = maxMemoryUsage;

            return this;
        }

        /// <summary>
        /// config global  configuration  povider <see cref="JasmineConfigurationProvider"/>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder ConfigXmlConfiguration(Action<JasmineConfigurationProvider> config)
        {
            config(JasmineConfigurationProvider.Instance);

            return this;
        }
        /// <summary>
        /// set default error filter ,the default is <see cref="Jasmine.Restful.DefaultFilters.DefaultErrorFilter"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public RestfulApplicationBuilder ConfigGlobalInterceptor(Action<GloabalIntercepterConfig> config)
        {
            config(RestfulApplicationGlobalConfig.GlobalIntercepterConfig);

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
            if(RestfulApplicationGlobalConfig.EnableSystemApi)
            {
                RestfulApplicationBaseComponents.RestfulServiceMetaDataManager.AddRestfulService(typeof(RestfulServiceManager));
                RestfulApplicationBaseComponents.ServicePovider.AddSigleton(typeof(RestfulServiceManager), RestfulApplicationBaseComponents.RestfulServiceManager);
            }


            if (RestfulApplicationGlobalConfig.UseDashBorad)
            {

                RestfulApplicationBaseComponents.RestfulServiceMetaDataManager.AddRestfulService(typeof(LoginService));
                RestfulApplicationBaseComponents.RestfulServiceMetaDataManager.AddRestfulService(typeof(XmlStoreUserManager));

                var userManager = new XmlStoreUserManager();



                userManager.CreateGroup("admins", AuthenticateLevel.SupperAdmin);
                userManager.CreateUser("fuanlei", "001", "admins");
                var sessionManager = new DefaultSessionManager();

                RestfulApplicationBaseComponents.ServicePovider.AddSigleton(typeof(XmlStoreUserManager), userManager);


                RestfulApplicationBaseComponents.ServicePovider.AddSigleton(typeof(LoginAfterInterceptor), new LoginAfterInterceptor(sessionManager,userManager));

                RestfulApplicationBaseComponents.ServicePovider.AddSigleton(typeof(SessionValidateFilter), new SessionValidateFilter(sessionManager));
                RestfulApplicationBaseComponents.ServicePovider.AddSigleton(typeof(LoginService), new LoginService(userManager));
                RestfulApplicationBaseComponents.ServicePovider.AddSigleton(typeof(AuthenticateFilter), new AuthenticateFilter(AuthenticateLevel.SupperAdmin));

            }


            /* generate all service processor instance */
            foreach (var item in RestfulApplicationBaseComponents.RestfulServiceMetaDataManager)
            {
                foreach (var restful in RestfulApplicationBaseComponents.ProcessorGenerator.Generate(item.Value))
                {
                    RestfulApplicationBaseComponents.RestfulServiceManager.AddProcessor(restful.Path, restful);
                }

            }



            return new RestfulApplication();
        }
        public RestfulApplicationBuilder LoadConfigFile(string xmlConfigFilePath)
        {
            return this;
        }
    }
}
