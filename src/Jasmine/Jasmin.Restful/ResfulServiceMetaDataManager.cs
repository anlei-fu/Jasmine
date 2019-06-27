using Jasmine.Common;
using Jasmine.Restful.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jasmine.Restful
{
    public class ResfulServiceMetaDataManager:AbstractMetadataManager<RestfulServiceMetaData>
    {
        private ResfulServiceMetaDataManager()
        {

        }
        public static readonly ResfulServiceMetaDataManager Instance = new ResfulServiceMetaDataManager();

        /// <summary>
        /// look up  restful service by scanning types in assembly ,which marked by <see cref="RestfulAttribute"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [RestfulIgnore]
        public void Scan(string path)
        {
            var metas = RestfulServiceScanner.Instance.Scan(path);

            addMetaDatas(metas);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        [RestfulIgnore]
        public void Scan(Assembly assembly)
        {
            var metas = RestfulServiceScanner.Instance.Scan(assembly);

            addMetaDatas(metas);
        }
        /// <summary>
        /// scan all assemblies by given folder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [RestfulIgnore]
        public void ScanFolder(string folder)
        {
            var metas = RestfulServiceScanner.Instance.ScanFolder(folder);

            addMetaDatas(metas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RestfulIgnore]
        public void AddRestfulService(Type type)
        {
            var meta = RestfulServiceMetaDataReflectResolver.Instance.Resolve(type);

           Add(meta.RelatedType, meta);
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
            var metas = RestfulServiceScanner.Instance.Scan(assembly, nameSpace);

            addMetaDatas(metas);
        }

      
        private void addMetaDatas(IEnumerable<RestfulServiceMetaData> metaDatas)
        {
            foreach (var item in metaDatas)
            {
                Add(item.RelatedType, item);
            }
        }
    }
}
