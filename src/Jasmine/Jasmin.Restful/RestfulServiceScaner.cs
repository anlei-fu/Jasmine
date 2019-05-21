using Jasmine.Common;
using Jasmine.Reflection;
using Jasmine.Restful.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Jasmine.Restful
{
    public  class RestfulServiceScaner
    {

        private RestfulServiceScaner()
        {

        }
        public static RestfulServiceScaner Instance = new RestfulServiceScaner();

        public IRequestProcessor<HttpFilterContext>[] ScanFolder(string directory)
        {
            requireDirectoryExists(directory);

            var ls = new List<IRequestProcessor<HttpFilterContext>>();

            foreach (var item in Directory.GetFiles(directory))
            {
                if(item.EndsWith(".dll"))
                {
                    try
                    {
                        var assembly= Assembly.Load(item);

                        ls.AddRange(Scan(assembly));
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }

            return ls.ToArray();

        }
        public IRequestProcessor<HttpFilterContext>[] Scan(string path)
        {
            requireFileExists(path);

            try
            {
                return Scan(Assembly.Load(path));
            }
            catch 
            {
                return null;
            }
           
        }
        public IRequestProcessor<HttpFilterContext>[] Scan(string path,string nameSpace)
        {
            return Scan(Assembly.Load(path),nameSpace);
        }
        public IRequestProcessor<HttpFilterContext>[] Scan(Assembly assembly)
        {

            var ls = new List<IRequestProcessor<HttpFilterContext>>();

            foreach (var item in assembly.GetTypes())
            {
                if(JasmineReflectionCache.Instance.GetItem(item).Attributes.Contains(typeof(RestfulAttribute)))
                {
                    var metaData = RestfulServiceMetaDataReflectResolver.Instance.Resolve(item);

                    var processors = RestfulRequestProcessorGenerator.Instance.Generate(metaData);

                    ls.AddRange(processors);
                }

            }

            return ls.ToArray();
        }
        public IRequestProcessor<HttpFilterContext>[] Scan(Assembly assembly,string nameSpace)
        {
            var ls = new List<IRequestProcessor<HttpFilterContext>>();

            foreach (var item in assembly.GetTypes())
            {
                
                if (JasmineReflectionCache.Instance.GetItem(item).Attributes.Contains(typeof(RestfulAttribute))&&item.FullName.StartsWith(nameSpace))
                {
                    var metaData = RestfulServiceMetaDataReflectResolver.Instance.Resolve(item);

                    var processors = RestfulRequestProcessorGenerator.Instance.Generate(metaData);

                    ls.AddRange(processors);
                }

            }

            return ls.ToArray();
        }

        private void requireFileExists(string path)
        {
            if(!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

        }
        private void requireDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }
        }

    }
}
