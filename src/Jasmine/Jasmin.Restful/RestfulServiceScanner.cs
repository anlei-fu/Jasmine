using Jasmine.Common;
using Jasmine.Reflection;
using Jasmine.Restful.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Jasmine.Restful
{
    internal  class RestfulServiceScanner
    {
        private RestfulServiceScanner()
        {

        }
        public static readonly RestfulServiceScanner Instance = new RestfulServiceScanner();

        public RestfulServiceMetaData[] ScanFolder(string directory)
        {
            requireDirectoryExists(directory);

            var ls = new List<RestfulServiceMetaData>();

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
                        //ignore
                    }
                }
            }

            return ls.ToArray();

        }
        public RestfulServiceMetaData[] Scan(string path)
        {
            requireFileExists(path);

            try
            {
                return Scan(Assembly.Load(path));
            }
            catch 
            {
                //ignore
                return Array.Empty<RestfulServiceMetaData>();
            }
           
        }
        public RestfulServiceMetaData[] Scan(string path,string nameSpace)
        {
            return Scan(Assembly.Load(path),nameSpace);
        }
        public RestfulServiceMetaData[] Scan(Assembly assembly)
        {
            return Scan(assembly,null);
        }
        public RestfulServiceMetaData[] Scan(Assembly assembly,string nameSpace=null)
        {
            var ls = new List<RestfulServiceMetaData>();

            foreach (var item in assembly.GetTypes())
            {
                var matcheNameSpace = nameSpace != null;
                
                if (JasmineReflectionCache.Instance.GetItem(item).Attributes.Contains(typeof(RestfulAttribute)))
                {
                    if (matcheNameSpace && !item.FullName.StartsWith(nameSpace))
                        continue;

                    var metaData = RestfulServiceMetaDataReflectResolver.Instance.Resolve(item);

                    ls.Add(metaData);
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
