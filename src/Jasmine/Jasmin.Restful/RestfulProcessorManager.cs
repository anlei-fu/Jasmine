using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;
using System;
using System.Reflection;

namespace Jasmine.Restful
{
    [Restful]
    [Path("/api")]
    [BeforeInterceptor(typeof(SessionValidateFilter))]
    public class RestfulProcessorManager : AbstractProcessorManager<HttpFilterContext>
    {
        private RestfulProcessorManager()
        {

        }
        public static readonly IRequestProcessorManager<HttpFilterContext> Instance = new RestfulProcessorManager();
        public override string Name => "Restful-Processor-Manager";

        public void Scan()
        {

        }
        /// <summary>
        /// look up  restful service by scanning types in assembly ,which marked by <see cref="RestfulAttribute"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [RestfulIgnore]
        public void Scan(string path)
        {
            foreach (var item in RestfulServiceScanner.Instance.Scan(path))
            {
                AddProcessor(item.Path, item);
            }

           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        [RestfulIgnore]
        public void Scan(Assembly assembly)
        {
            foreach (var item in RestfulServiceScanner.Instance.Scan(assembly))
            {
                AddProcessor(item.Path, item);
            }

          
        }
        /// <summary>
        /// scan all assemblies by given folder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [RestfulIgnore]
        public void ScanFolder(string folder)
        {
            foreach (var item in RestfulServiceScanner.Instance.ScanFolder(folder))
            {
                AddProcessor(item.Path, item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RestfulIgnore]
        public void AddRestfulService(Type type)
        {
            var restful = RestfulServiceMetaDataReflectResolver.Instance.Resolve(type);

            foreach (var item in RestfulRequestProcessorGenerator.Instance.Generate(restful))
            {
                AddProcessor(item.Path, item);
            }

           
        }
        /// <summary>
        /// look up  restful services by scanning types in assembly, limited by namespace
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        [RestfulIgnore]
        public void Scan(Assembly assembly, string nameSpace)
        {
            foreach (var item in RestfulServiceScanner.Instance.Scan(assembly, nameSpace))
            {
                AddProcessor(item.Path, item);
            }
            
        }
     
    }
}
