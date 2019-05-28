using Jasmine.Common;
using Jasmine.Reflection;
using Jasmine.Rpc.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Jasmine.Rpc.Server
{
    public class RpcServiceScanner
    {
        private RpcServiceScanner()
        {

        }

        public static RpcServiceScanner Instance = new RpcServiceScanner();

        public IRequestProcessor<RpcFilterContext>[] ScanFolder(string directory)
        {
            requireDirectoryExists(directory);

            var ls = new List<IRequestProcessor<RpcFilterContext>>();

            foreach (var item in Directory.GetFiles(directory))
            {
                if (item.EndsWith(".dll"))
                {
                    try
                    {
                        var assembly = Assembly.Load(item);

                        ls.AddRange(Scan(assembly));
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }

            return ls.ToArray();

        }
        public IRequestProcessor<RpcFilterContext>[] Scan(string path)
        {
            requireFileExists(path);

            try
            {
                return Scan(Assembly.Load(path));
            }
            catch
            {
                return Array.Empty<IRequestProcessor<RpcFilterContext>>();
            }

        }
        public IRequestProcessor<RpcFilterContext>[] Scan(string path, string nameSpace)
        {
            return Scan(Assembly.Load(path), nameSpace);
        }
        public IRequestProcessor<RpcFilterContext>[] Scan(Assembly assembly)
        {
            var ls = new List<IRequestProcessor<RpcFilterContext>>();

            foreach (var item in assembly.GetTypes())
            {
                if (JasmineReflectionCache.Instance.GetItem(item).Attributes.Contains(typeof(RpcAttribute)))
                {
                    var metaData = RpcServiceMetaDataReflectResolver.Instance.Resolve(item);

                    var processors = RpcRequestProcessorGenerator.Instance.Generate(metaData);

                    ls.AddRange(processors);
                }

            }

            return ls.ToArray();
        }
        public IRequestProcessor<RpcFilterContext>[] Scan(Assembly assembly, string nameSpace)
        {
            var ls = new List<IRequestProcessor<RpcFilterContext>>();

            foreach (var item in assembly.GetTypes())
            {

                if (JasmineReflectionCache.Instance.GetItem(item).Attributes.Contains(typeof(RpcAttribute)) && item.FullName.StartsWith(nameSpace))
                {
                    var metaData = RpcServiceMetaDataReflectResolver.Instance.Resolve(item);

                    var processors = RpcRequestProcessorGenerator.Instance.Generate(metaData);

                    ls.AddRange(processors);
                }

            }

            return ls.ToArray();
        }

        private void requireFileExists(string path)
        {
            if (!File.Exists(path))
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
