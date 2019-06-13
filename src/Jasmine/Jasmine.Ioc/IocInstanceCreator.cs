using Jasmine.Common;
using Jasmine.Configuration;
using Jasmine.Ioc.Exceptions;
using Jasmine.Reflection;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Ioc
{
    public class IocInstanceCreator : IInstanceCreator<IocServiceMetaData>
    {
        private IocInstanceCreator()
        {

        }
        private IIocServiceMetaDataManager _manager => IocServiceMetaDataManager.Instance;

        private ConcurrentDictionary<string,IInstanceCreatedListener<IocServiceMetaData>> _listeners=new ConcurrentDictionary<string, IInstanceCreatedListener<IocServiceMetaData>>();

        private  IConfigrationProvider _configProvider=> JasmineConfigurationProvider.Instance;
        

        public static readonly IInstanceCreator<IocServiceMetaData> Instance = new IocInstanceCreator();

        public int Count => _listeners.Count;


        public object Create(IocServiceMetaData metaData, DependencyCheckNode node)
        {
            // don't need to think about abstract or interface, it already be processed by service provider
            if (metaData.ConstrctorMetaData.Constructor.IsDefaultConstructor)//default constructor,none parameter
                return metaData.ConstrctorMetaData.Constructor.DefaultInvoker.Invoke();

            /*
             * check dependency loop
             */ 
            var newNode = new DependencyCheckNode(node, metaData.RelatedType);

            var constructor = metaData.ConstrctorMetaData;

            var paramsInstances = new object[constructor.Parameters.Length];

            for (int i = 0; i < paramsInstances.Length; i++)
            {
                if (constructor.Parameters[i].IsFromConfig)//config key
                {
                    var type = constructor.Parameters[i].IsAbstract ? constructor.Parameters[i].Impl :
                                                                      constructor.Parameters[i].RelatedType;

                    if (type == null)
                        throw new ImplementationNotFoundException("");

                    paramsInstances[i] = _configProvider.GetConfig(type,constructor.Parameters[i].ConfigKey);
                }
                else if (constructor.Parameters[i].HasDefaultValue)//default value
                {
                    paramsInstances[i] = constructor.Parameters[i].DefaultValue;
                }
                else if (constructor.Parameters[i].IsAbstract)//abstract  or interface
                {
                    if (constructor.Parameters[i].HasImplementType)//instructed by implementation type
                    {
                        paramsInstances[i] = IocServiceProvider.Instance.GetService(constructor.Parameters[i].Impl, newNode);
                    }
                    else
                    {
                        var impl = _manager.GetImplementation(constructor.Parameters[i].RelatedType);//try find impl 

                        if (impl == null)
                            throw new ImplementationNotFoundException($"construct({constructor}),parameter({constructor.Parameters[i]}) ");

                        paramsInstances[i] = IocServiceProvider.Instance.GetService(impl, newNode);
                    }

                }
                else if (constructor.Parameters[i].RelatedType.IsBaseType())//config base type default value
                {
                    paramsInstances[i] = JasmineDefaultValueProvider.GetDefaultValue(constructor.Parameters[i].RelatedType);
                }
                else
                {
                    paramsInstances[i] = IocServiceProvider.Instance.GetService(constructor.Parameters[i].RelatedType, newNode);
                }
            }


            var instance = constructor.Constructor.Invoker.Invoke(paramsInstances);

            foreach (var item in _listeners)
            {
                item.Value.OnInstanceCreated(metaData, instance);
            }

            return instance;

        }

        public void AddListener(IInstanceCreatedListener<IocServiceMetaData> listener)
        {
            _listeners.TryAdd(listener.Name, listener);
        }

        public void RemoveListener(string name)
        {
            _listeners.TryRemove(name, out var _ );
        }

        public bool ContainsListener(string name)
        {
            return _listeners.ContainsKey(name);
        }

        public IEnumerator<IInstanceCreatedListener<IocServiceMetaData>> GetEnumerator()
        {
            foreach (var item in _listeners.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _listeners.Values.GetEnumerator();
        }
    }
}
