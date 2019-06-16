using Jasmine.Common;
using Jasmine.Reflection;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Ioc
{
    public class IocServiceMetaDataManager : AbstractMetadataManager<IocServiceMetaData>, IIocServiceMetaDataManager
    {
        private IocServiceMetaDataManager()
        {

        }

        private ConcurrentDictionary<Type, Type> _implMappings = new ConcurrentDictionary<Type, Type>();

        public static readonly IIocServiceMetaDataManager Instance = new IocServiceMetaDataManager();
       

        public void SetImplementationMapping(Type abs, Type impl)
        {
            if (impl.IsInterfaceOrAbstractClass())
                throw new NotImplementedException();

            if(!impl.IsDerivedFrom(abs))
                throw new NotImplementedException();

            if (!_implMappings.ContainsKey(abs))
            {
                _implMappings.TryAdd(abs, impl);
            }
            else
            {
                _implMappings[abs] = impl;
            }

        }
        public Type GetImplementation(Type abs)
        {
            return _implMappings.TryGetValue(abs, out var value) ? value : null;
        }

    }
}
