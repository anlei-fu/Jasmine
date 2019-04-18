using Jasmine.Common;
using Jasmine.Ioc.Exceptions;
using Jasmine.Reflection;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Ioc
{
    public class JamineInstanceCreator : IInstanceCreator
    {
        private ConcurrentDictionary<string,IInstanceCreatedListener> _listeners=new ConcurrentDictionary<string, IInstanceCreatedListener>();
        private readonly IPropertyManager _propertyManager;
        private readonly IIocServiceMetaDataManager _manager;
        public static readonly IInstanceCreator Instance = new JamineInstanceCreator();
        public object Create(IocServiceMetaData metaData, DependencyCheckNode node)
        {
            if (metaData.ConstrctorMetaData.Constructor.IsDefault)//default constructor
                return metaData.ConstrctorMetaData.Constructor.DefaultInvoker.Invoke();

            var newNode = new DependencyCheckNode(node, metaData.RelatedType);
            var constructor = metaData.ConstrctorMetaData;
            var paramsInstances = new object[constructor.Parameters.Length];

            for (int i = 0; i < paramsInstances.Length; i++)
            {
                if (constructor.Parameters[i].IsFromConfig)//config key
                {
                    var type = constructor.Parameters[i].IsAbstract ? constructor.Parameters[i].ImplType :
                                                                      constructor.Parameters[i].RelatedType;

                    paramsInstances[i] = JasminePropertyStringConvertor.Convert(type,_propertyManager.GetValue(constructor.Parameters[i].PropertyKey));
                }
                else if (constructor.Parameters[i].HasDefaultValue)//default value
                {
                    paramsInstances[i] = constructor.Parameters[i].DefaultValue;
                }
                else if (constructor.Parameters[i].IsAbstract)//abstract  or interface
                {
                    if (constructor.Parameters[i].HasImplementType)//instructed  implementation type
                    {
                        paramsInstances[i] = JasmineServiceProvider.Instance.GetService(constructor.Parameters[i].ImplType, newNode);
                    }
                    else
                    {
                        var impl = _manager.GetImplementation(constructor.Parameters[i].RelatedType);//try find impl 

                        if (impl == null)
                            throw new ImplementationNotFoundException($"construct({constructor}) parameter({constructor.Parameters[i]}) {1}");

                        paramsInstances[i] = JasmineServiceProvider.Instance.GetService(impl, newNode);
                    }

                }
                else if (constructor.Parameters[i].RelatedType.IsBaseType())//config default value
                {
                    paramsInstances[i] = JasmineDefaultValueProvider.GetDefaultValue(constructor.Parameters[i].RelatedType);
                }
                else
                {
                    paramsInstances[i] = JasmineServiceProvider.Instance.GetService(constructor.Parameters[i].RelatedType, newNode);
                }
            }


            var instance = constructor.Constructor.Invoker.Invoke(paramsInstances);

            foreach (var item in _listeners)
            {
                item.Value.OnInstanceCreated(metaData, instance);
            }

            return instance;

        }

        public void AddListener(IInstanceCreatedListener listener)
        {
            _listeners.TryAdd(listener.Name, listener);
        }

        public void RemoveListener(string name)
        {
            _listeners.TryRemove(name, out var _ );
        }
    }
}
