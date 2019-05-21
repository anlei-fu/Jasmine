using Jasmine.Common;
using Jasmine.Ioc;
using System;
using System.Reflection;

namespace Jasmine.Restful.Implement
{
    public class RestfulApplicationBuilder 
    {
       private int _port=10336;
       public RestfulApplicationBuilder Scan(string path)
        {
            foreach (var item in RestfulServiceScaner.Instance.Scan(path))
            {
                RestfulProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }

        public RestfulApplicationBuilder ScanFolder(string folder)
        {
            foreach (var item in RestfulServiceScaner.Instance.ScanFolder(folder))
            {
                RestfulProcessorManager.Instance.AddProcessor(item.Path,item);
            }


            return this;
        }
        public RestfulApplicationBuilder Scan(Assembly assembly)
        {
            foreach (var item in RestfulServiceScaner.Instance.Scan(assembly))
            {
                RestfulProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }
        public RestfulApplicationBuilder Scan(Assembly assembly,string nameSpace)
        {

            foreach (var item in RestfulServiceScaner.Instance.Scan(assembly,nameSpace))
            {
                RestfulProcessorManager.Instance.AddProcessor(item.Path, item);
            }

            return this;
        }

        public RestfulApplicationBuilder SetPort(int port)
        {
            _port = port;

            return this;
        }

        public RestfulApplicationBuilder ConfigProcessor(Action<IRequestProcessorManager<HttpFilterContext>> config)
        {
            config(RestfulProcessorManager.Instance);

            return this;
        }

        public RestfulApplicationBuilder ConfigService(Action<IocServiceProvider> config)
        {
            config(IocServiceProvider.Instance);

            return this;
        }

        public RestfulApplication Build()
        {
            return new RestfulApplication(_port,new RestfulDispatcher("restful-dispatcher",RestfulProcessorManager.Instance));
        }
    }
}
