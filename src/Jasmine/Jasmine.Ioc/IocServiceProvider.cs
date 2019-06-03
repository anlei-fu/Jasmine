using Jasmine.Common;
using Jasmine.Ioc.Attributes;
using Jasmine.Ioc.Exceptions;
using Jasmine.Reflection;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Jasmine.Ioc
{
    public class IocServiceProvider : IServiceProvider
    {
        private IocServiceProvider()
        {

        }
        private  IIocServiceMetaDataManager _metaDataManager => IocServiceMetaDataManager.Instance;
        private  IMetaDataReflectResolver<IocServiceMetaData> _reflectResolver => IocServiceMetaDataReflectResolver.Instance;
        private  IInstanceCreator<IocServiceMetaData> _instanceCreator => IocInstanceCreator.Instance;

        private readonly IServiceMetaDataXmlResolver _xmlResolver;
        private readonly ConcurrentDictionary<Type, object> _singletons = new ConcurrentDictionary<Type, object>();


        public static readonly IocServiceProvider Instance = new IocServiceProvider();
       
        public void SetImplementationMapping(Type abs,Type impl)
        {
            _metaDataManager.SetImplementation(abs, impl);
        }
        public void AddProtoType(Type serviceType,object instance)
        {
            addInstance(serviceType, instance, ServiceScope.ProtoType);
        }
        public void AddSigleton(Type serviceType,object instance)
        {
            addInstance(serviceType, instance, ServiceScope.Singleton);
        }
        public void LoadConfig(string path)
        {
            _xmlResolver.Resolve(path);
        }
        public void Scan(Assembly assembly)
        {
            foreach (var item in assembly.GetTypes())
            {
                if (item.GetCustomAttribute<ServiceAttribute>() != null)
                    generateMetaData(item);
            }  
        }

        public T GetService<T>(Type type)
        {
            return default(T);
        }
        public object GetService(Type serviceType)
        {
            return GetService(serviceType, new DependencyCheckNode(null, serviceType));
        }
        internal object GetService(Type serviceType,DependencyCheckNode node)
        {
            /*
             *  generate metadata ,if metadata has not been generated!
             */ 
            if (!_metaDataManager.ContainsKey(serviceType))
            {
                generateMetaData(serviceType);
            }

            _metaDataManager.TryGetValue(serviceType, out var metaData);

            if(metaData==null)//interface or abstrct class ,pregenerate if found implement,_interface map should  add mapping
            {
                var impl = _metaDataManager.GetImplementation(serviceType);

                if (impl==null)//not found implementation
                    throw new ImplementationNotFoundException(null);

                /*
                 * generate impl metadata
                 */ 
                generateMetaData(impl);

                _metaDataManager.TryGetValue(impl, out metaData);
            }

            /*
             * create instance and cache instance
             */ 
            if (metaData.Scope == ServiceScope.Singleton)
            {
                if (!_singletons.TryGetValue(serviceType, out var instance))
                {
                    instance = _instanceCreator.Create(metaData, node);

                    _singletons.TryAdd(serviceType,instance);
                }

                return instance;
            }
            else
            {
             
                return _instanceCreator.Create(metaData,node);
            }
        }

        private void addInstance(Type serviceType, object instance, ServiceScope scope)
        {
            if (!instance.GetType().IsDerivedFrom(serviceType))
                throw new NotImplementedException();

            if (!_metaDataManager.TryGetValue(serviceType, out var value))
                generateMetaData(serviceType);

            _metaDataManager[serviceType].Scope = scope;

            if (!_singletons.TryAdd(serviceType, instance))
                _singletons[serviceType] = instance;

        }
        private void generateMetaData(Type serviceType)
        {
            var metaData = _reflectResolver.Resolve(serviceType);

            if (metaData != null)
                _metaDataManager.Add(serviceType, metaData);
        }

    }
}
