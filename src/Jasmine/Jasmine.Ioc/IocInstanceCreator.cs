using Jasmine.Common;
using Jasmine.Configuration;
using Jasmine.Ioc.Exceptions;
using Jasmine.Reflection;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Ioc
{
    /// <summary>
    /// create a required instance by given metadata
    /// </summary>
    public class IocInstanceCreator : IInstanceCreator<IocServiceMetaData>
    {
        private IocInstanceCreator()
        {

        }
        private IIocServiceMetaDataManager _manager => IocServiceMetaDataManager.Instance;
        private IConfigrationProvider _configProvider => JasmineConfigurationProvider.Instance;
        private ConcurrentDictionary<string, IInstanceCreatedListener<IocServiceMetaData>> _listeners = new ConcurrentDictionary<string, IInstanceCreatedListener<IocServiceMetaData>>();

        public static readonly IInstanceCreator<IocServiceMetaData> Instance = new IocInstanceCreator();

        public int Count => _listeners.Count;

        public object Create(IocServiceMetaData metaData, DependencyCheckNode node)
        {
            object instance;

            // don't need to think about abstract or interface, it already be processed by service provider
            if (metaData.ConstrctorMetaData.Constructor.IsDefaultConstructor)//default constructor,none parameter
            {
                instance = metaData.ConstrctorMetaData.Constructor.DefaultInvoker.Invoke();
            }
            else
            {
                /*
                 * check dependency loop
                 */
                var newNode = new DependencyCheckNode(node, metaData.RelatedType);

                var constructor = metaData.ConstrctorMetaData;

                var paramsInstances = new object[constructor.Parameters.Length];

                // genearate construtor parameters instance
                for (int i = 0; i < paramsInstances.Length; i++)
                {
                    if (constructor.Parameters[i].IsFromConfig)//config key
                    {
                        var type = constructor.Parameters[i].IsAbstract ? constructor.Parameters[i].Impl :
                                                                          constructor.Parameters[i].RelatedType;

                        if (type == null)
                        {
                            type = IocServiceProvider.Instance.GetImplementation(metaData.RelatedType);

                            if (type == null)
                                throw new ImplementationNotFoundException($"");
                        }

                        paramsInstances[i] = _configProvider.GetConfig(type, constructor.Parameters[i].ConfigKey);
                    }
                    else if (constructor.Parameters[i].HasDefaultValue)//default value
                    {
                        paramsInstances[i] = constructor.Parameters[i].DefaultValue;
                    }
                    else if (constructor.Parameters[i].IsAbstract)//abstract  or interface
                    {
                        if (constructor.Parameters[i].HasImpl)//instructed by implementation type
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

                instance = constructor.Constructor.Invoker.Invoke(paramsInstances);
            }

            // need wire properties
            if (metaData.Properties.Length != 0)
            {
                foreach (var item in metaData.Properties)
                    wireProperty(instance, item);
            }

            // call init method
            if (metaData.HasInitMethod)
                callInitMethod(instance, metaData.InitMethod);

            // raise instance created event
            foreach (var item in _listeners)
                item.Value.OnInstanceCreated(metaData, instance);

            return instance;

        }

        private void wireProperty(object instance, IocPropertyMetaData property)
        {
            object pInstance;

            if (property.FromConfig)
            {
                var type = property.IsAbstract ? 
                                   property.HasImpl ? property.Impl : null : null;

                if (type == null)
                {
                    type = IocServiceProvider.Instance.GetImplementation(property.RelatedType);

                    if (type == null)
                        throw new ImplementationNotFoundException($"");
                }

                pInstance = JasmineConfigurationProvider.Instance.GetConfig(type, property.ConfigKey);
            }
            else if (property.HasDefaultValue)
            {
                pInstance = property.DefaultValue;
            }
            else if (property.IsAbstract)
            {
                var type = property.HasImpl ?
                              property.Impl : IocServiceProvider.Instance.GetImplementation(property.RelatedType);

                if (type == null)
                    throw new ImplementationNotFoundException($"");

                pInstance = IocServiceProvider.Instance.GetService(type);
            }
            else if (BaseTypes.Base.Contains(property.RelatedType))
            {
                pInstance = JasmineDefaultValueProvider.GetDefaultValue(property.RelatedType);
            }
            else
            {
                pInstance = IocServiceProvider.Instance.GetService(property.RelatedType);
            }

            property.Setter.Invoke(instance, pInstance);

        }

        private void callInitMethod(object instance, IocMethodMetaData method)
        {
            var paraInstances = new object[method.ParameterLength];

            var t = 0;

            foreach (var item in method.Parameters)
            {
                object para = null;

                if (item.IsFromConfig)
                {
                    var type = item.IsAbstract ? item.HasImpl ? item.Impl : null : null;

                    if (type == null)
                    {
                        type = IocServiceProvider.Instance.GetImplementation(item.RelatedType);

                        if (type == null)
                            throw new ImplementationNotFoundException($"");
                    }

                    para = JasmineConfigurationProvider.Instance.GetConfig(type, item.ConfigKey);
                }
                else if (item.HasDefaultValue)
                {
                    para = item.DefaultValue;
                }
                else if (item.IsAbstract)
                {
                    var type = item.HasImpl ? item.Impl : IocServiceProvider.Instance.GetImplementation(item.RelatedType);

                    if (type == null)
                        throw new ImplementationNotFoundException($"");

                    para = IocServiceProvider.Instance.GetService(type);
                }
                else if (BaseTypes.Base.Contains(item.RelatedType))
                {
                    para = JasmineDefaultValueProvider.GetDefaultValue(item.RelatedType);
                }
                else
                {
                    para = IocServiceProvider.Instance.GetService(item.RelatedType);
                }

                paraInstances[t] = para;

                ++t;
            }

            method.Method.Invoke(instance, paraInstances);

        }

        public void AddListener(IInstanceCreatedListener<IocServiceMetaData> listener)
        {
            _listeners.TryAdd(listener.Name, listener);
        }

        public void RemoveListener(string name)
        {
            _listeners.TryRemove(name, out var _);
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
