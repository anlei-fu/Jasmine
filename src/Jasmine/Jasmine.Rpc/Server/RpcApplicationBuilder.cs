using Jasmine.Common;
using Jasmine.Configuration;
using Jasmine.Ioc;
using Jasmine.Serialization;
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Jasmine.Rpc.Server
{
    public class RpcApplicationBuilder
    {
        private int _port = 10336;
        private ISerializer _serizlizer=JsonSerializer.Instance;
        private X509Certificate _certs;
        private ILoginValidator _validator;
        private int _backlog=100;

   
        /// <summary>
        /// look up  Rpc service by scanning types in assembly ,which marked by <see cref="RpcAttribute"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RpcApplicationBuilder Scan(string path)
        {
            foreach (var item in RpcServiceScanner.Instance.Scan(path))
            {
                RpcRequestProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public RpcApplicationBuilder Scan(Assembly assembly)
        {
            foreach (var item in RpcServiceScanner.Instance.Scan(assembly))
            {
                RpcRequestProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }
        /// <summary>
        /// look up  Rpc services by scanning types in assembly, limited by namespace
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public RpcApplicationBuilder Scan(Assembly assembly, string nameSpace)
        {

            foreach (var item in RpcServiceScanner.Instance.Scan(assembly, nameSpace))
            {
                RpcRequestProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }

        public RpcApplicationBuilder ConfigDispatcher(Action<RpcDispatcher> dispatcher)
        {
          

            return this;
        }
        /// <summary>
        /// scan all assemblies by given folder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>

        public RpcApplicationBuilder ScanFolder(string folder)
        {
            foreach (var item in RpcServiceScanner.Instance.ScanFolder(folder))
            {
                RpcRequestProcessorManager.Instance.AddProcessor(item.Path, item);
            }


            return this;
        }

        public RpcApplicationBuilder ConfigSsl(string path)
        {
            _certs = X509Certificate.CreateFromCertFile(path);

            return this;
        }

        public RpcApplicationBuilder ConfigSsl(X509Certificate certs)
        {
            _certs = certs;

            return this;
        }
        /// <summary>
        ///  confg server listen backlog,default value is 100
        /// </summary>
        /// <param name="backlog"></param>
        /// <returns></returns>
        public RpcApplicationBuilder ConfigBacklog(int backlog)
        {
            _backlog = backlog;

            return this;
        }
        public RpcApplicationBuilder ConfigSerializer(ISerializer serializer)
        {
            _serizlizer = serializer;

            return this;
        }
        public RpcApplicationBuilder ConfigValidator(ILoginValidator validator)
        {
            _validator = validator;
            return this;
        }
        /// <summary>
        /// to load some config , which instantiate object  needs or other 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RpcApplicationBuilder LoadConfig(string path)
        {
            JasmineConfigurationProvider.Instance.LoadConfig(path);

            return this;
        }
        /// <summary>
        /// config  listening port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public RpcApplicationBuilder ConfigPort(int port)
        {
            _port = port;

            return this;
        }
        /// <summary>
        /// custum config Rpc service
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public RpcApplicationBuilder ConfigProcessor(Action<IRequestProcessorManager<RpcFilterContext>> config)
        {
            config(RpcRequestProcessorManager.Instance);

            return this;
        }
        /// <summary>
        /// config service provider <see cref="IocServiceProvider"/>
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>

        public RpcApplicationBuilder ConfigServiceProvider(Action<IocServiceProvider> config)
        {
            config(IocServiceProvider.Instance);

            return this;
        }
        /// <summary>
        /// build a Rpc app
        /// </summary>
        /// <returns></returns>
        public RpcApplication Build()
        {
            return new RpcApplication(_port,new RpcMiddleware(new RpcDispatcher("RpcDispatcher",RpcRequestProcessorManager.Instance,_serizlizer)),_serizlizer,_certs,_validator,_backlog );
        }
    }
}
