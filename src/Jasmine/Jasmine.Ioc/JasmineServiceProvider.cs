using Jasmine.Common;
using Jasmine.Ioc.Attributes;
using Jasmine.Ioc.Exceptions;
using Jasmine.Reflection;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Jasmine.Ioc
{
    public class JasmineServiceProvider : IServiceProvider
    {
        public JasmineServiceProvider()
        {

        }
        private readonly IIocServiceMetaDataManager _metaDataManager = DefaultServiceMetaDataManager.Instance;
        private readonly IServiceMetaDataReflectResolver<IocServiceMetaData> _reflectResolver = JasmineServiceMetaDataReflectResolver.Instance;
        private readonly IInstanceCreator _instanceCreator = JamineInstanceCreator.Instance;
        private readonly IServiceMetaDataXmlResolver _xmlResolver;
        private readonly ConcurrentDictionary<Type, object> _singletons = new ConcurrentDictionary<Type, object>();
        public static readonly JasmineServiceProvider Instance = new JasmineServiceProvider();
       
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
        public object GetService(Type serviceType)
        {
            return GetService(serviceType, new DependencyCheckNode(null, serviceType));
        }
        public object GetService(Type serviceType,DependencyCheckNode node)
        {
            if (!_metaDataManager.ContainsKey(serviceType))
            {
                generateMetaData(serviceType);
            }

            _metaDataManager.TryGetValue(serviceType, out var metaData);

            if(metaData==null)//interface or abstrct class ,pre generate if found implement,_interface map should  add mapping
            {
                var impl = _metaDataManager.GetImplementation(serviceType);

                if (impl==null)//not found implementation
                    throw new ImplementationNotFoundException(null);

                generateMetaData(impl);

                _metaDataManager.TryGetValue(impl, out metaData);
            }

            if (metaData.Scope == ServiceScope.Singleton)
            {
                if (!_singletons.TryGetValue(serviceType, out var instance))
                {
                    _singletons.TryAdd(serviceType, _instanceCreator.Create(metaData,node));
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
