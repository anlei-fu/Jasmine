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

        private ConcurrentDictionary<Type, Type> _impls = new ConcurrentDictionary<Type, Type>();

        public static readonly IIocServiceMetaDataManager Instance = new IocServiceMetaDataManager();
       

        public void SetImplementation(Type abs, Type impl)
        {
            if (impl.IsInterfaceOrAbstraClass())
                throw new NotImplementedException();

            if (!_impls.ContainsKey(abs))
            {
                _impls.TryAdd(abs, impl);
            }
            else
            {
                _impls[abs] = impl;
            }

        }
        public Type GetImplementation(Type abs)
        {
            return _impls.TryGetValue(abs, out var value) ? value : null;
        }

    }
}
