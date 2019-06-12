using Jasmine.Common;
using Jasmine.Configuration;
using Jasmine.Ioc;
using Jasmine.Restful.Attributes;
using Jasmine.Restful.DefaultServices;
using System;
using System.Reflection;

namespace Jasmine.Restful.Implement
{
    public class RestfulApplicationBuilder
    {
        public RestfulApplicationBuilder()
        {
            ConfigServiceProvider(x =>
            {
                x.SetImplementationMapping(typeof(IRequestProcessorManager<HttpFilterContext>), typeof(RestfulProcessorManager));
                x.AddSigleton(typeof(RestfulProcessorManager), RestfulProcessorManager.Instance);
            });

            AddRestfulService(typeof(RestfulProcessorManager));
            AddRestfulService(typeof(LoginService));
        }

        private int _port = 10336;

        private RestfulDispatcher _dispatcher = new RestfulDispatcher("restful-dispatcher", RestfulProcessorManager.Instance);
        /// <summary>
        /// look up  restful service by scanning types in assembly ,which marked by <see cref="RestfulAttribute"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder Scan(string path)
        {
            foreach (var item in RestfulServiceScanner.Instance.Scan(path))
            {
                RestfulProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder Scan(Assembly assembly)
        {
            foreach (var item in RestfulServiceScanner.Instance.Scan(assembly))
            {
                item.Dispatcher = _dispatcher;

                RestfulProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RestfulApplicationBuilder AddRestfulService(Type type)
        {
            var restful= RestfulServiceMetaDataReflectResolver.Instance.Resolve(type);

            foreach (var item in RestfulRequestProcessorGenerator.Instance.Generate(restful))
            {
                RestfulProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }
        /// <summary>
        /// look up  restful services by scanning types in assembly, limited by namespace
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder Scan(Assembly assembly, string nameSpace)
        {

            foreach (var item in RestfulServiceScanner.Instance.Scan(assembly, nameSpace))
            {
                RestfulProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }


        public RestfulApplicationBuilder ConfigSsl()
        {
            return this;
        }
        public RestfulApplicationBuilder ConfigDispatcher(Action<RestfulDispatcher> dispatcher)
        {
            dispatcher(_dispatcher);

            return this;
        }
        /// <summary>
        /// scan all assemblies by given folder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>

        public RestfulApplicationBuilder ScanFolder(string folder)
        {
            foreach (var item in RestfulServiceScanner.Instance.ScanFolder(folder))
            {
                RestfulProcessorManager.Instance.AddProcessor(item.Path, item);
            }


            return this;
        }
       
        /// <summary>
        /// to load some config , which instantiate object  needs or other 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder LoadConfig(string path)
        {
            JasmineConfigurationProvider.Instance.LoadConfig(path);

            return this;
        }
        /// <summary>
        /// config  listening port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder SetPort(int port)
        {
            _port = port;

            return this;
        }
        /// <summary>
        /// custum config restful service
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RestfulApplicationBuilder ConfigProcessor(Action<IRequestProcessorManager<HttpFilterContext>> config)
        {
            config(RestfulProcessorManager.Instance);

            return this;
        }
        /// <summary>
        /// config service provider <see cref="IocServiceProvider"/>
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
            return new RestfulApplication(_port,_dispatcher );
        }
    }
}
