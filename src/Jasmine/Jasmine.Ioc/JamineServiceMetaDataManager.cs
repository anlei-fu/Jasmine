using Jasmine.Common;
using Jasmine.Reflection;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Ioc
{
    public class DefaultServiceMetaDataManager : AbstractMetadataManager<IocServiceMetaData>, IIocServiceMetaDataManager
    {
        private DefaultServiceMetaDataManager()
        {

        }
        public static readonly IIocServiceMetaDataManager Instance = new DefaultServiceMetaDataManager();
        private ConcurrentDictionary<Type, Type> _impls = new ConcurrentDictionary<Type, Type>();

        public void SetImplementation(Type @abstract, Type impl)
        {
            if (impl.IsInterfaceOrAbstraClass())
                throw new NotImplementedException();

            if (!_impls.ContainsKey(@abstract))
            {
                _impls.TryAdd(@abstract, impl);
            }
            else
            {
                _impls[@abstract] = impl;
            }

        }
        public Type GetImplementation(Type @abstract)
        {
            return _impls.TryGetValue(@abstract, out var value) ? value : null;
        }

    }
}
