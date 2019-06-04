using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Ioc.Attributes;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Ioc
{
    public class IocServiceMetaDataReflectResolver : IMetaDataReflectResolver<IocServiceMetaData>
    {

        private IocServiceMetaDataReflectResolver()
        {

        }

        private ITypeCache _reflection => JasmineReflectionCache.Instance;

        public static readonly IMetaDataReflectResolver<IocServiceMetaData> Instance = new IocServiceMetaDataReflectResolver();

        public IocServiceMetaData ResolveProtoType(Type type)
        {
            return Resolve(type, ServiceScope.ProtoType);
        }
        public IocServiceMetaData ResolveRequest(Type type)
        {
            return Resolve(type, ServiceScope.Request);
        }

        public IocServiceMetaData ResolveSingleton(Type type)
        {
            return Resolve(type, ServiceScope.Singleton);
        }
        public IocServiceMetaData Resolve(Type type, ServiceScope scope)
        {
            var metaData = Resolve(type);

            /*
             *  reset service scope ,if metadata not null
             */ 
            if (metaData != null)
                metaData.Scope = scope;

            return metaData;
        }
        public IocServiceMetaData Resolve(Type type)
        {
            var metaData = new IocServiceMetaData(type);

            var typeAttrs = _reflection.GetItem(type).Attributes;

            metaData.RelatedType = type;

            if (type.IsInterfaceOrAbstraClass())
            {
                /*
                 * add impl mapping
                 */ 
                if (typeAttrs.Contains(typeof(DefaultImplementAttribute)))
                    IocServiceMetaDataManager.Instance.SetImplementation(type, typeAttrs.GetAttribute<DefaultImplementAttribute>()[0].Impl);

                return null;
            }

            /*
             * resolve scope
             */
            if (typeAttrs.Contains(typeof(ServiceScopeAttribute)))
                metaData.Scope = typeAttrs.GetAttribute<ServiceScopeAttribute>()[0].Scope;

            /*
             *  resolve name
             */ 
            if (typeAttrs.Contains(typeof(ServiceAliasAttibute)))
                metaData.Name = typeAttrs.GetAttribute<ServiceAliasAttibute>()[0].Alias;
            else
                metaData.Name = type.FullName;

            /*
             *  resolve lazy load
             */ 
            if (typeAttrs.Contains(typeof(LazyLoadAttribute)))
                metaData.LazyLoad = true;

            var constructors = _reflection.GetItem(type).Constructors.GetAll().ToArray();

            Constructor defaultConstructor = null;

            /*
             *  try find default constructor
             */ 
            foreach (var constructor in constructors)
            {
                if (constructor.Attributes.Contains(typeof(DefaultConstructor)))
                {
                    defaultConstructor = constructor;
                    break;
                }
            }

            /*
             * pick first constructor as default constructor,if type has no none-parameter constructor
             */
            if (defaultConstructor == null)
            {
                defaultConstructor = _reflection.GetItem(type).Constructors.GetDefaultConstructor();

                    defaultConstructor=    defaultConstructor==null? 
                                                                   constructors.Length > 0 ? constructors[0] : null
                                                                   :null;

            }

            /*
             *  no constructor available
             */ 
            if (defaultConstructor == null)
                return metaData;

            metaData.ConstrctorMetaData = resolveConstructor(defaultConstructor);


            /*
             * resolve  field and property 
             */ 
            var properties = new List<IocPropertyMetaData>();

            foreach (var item in _reflection.GetItem(type).Fileds.Union(_reflection.GetItem(type).Properties))
            {
                if (item.Attributes.Contains(typeof(AutoWiredAttribute)))
                    properties.Add(resolvePoperty(item));
            }

            metaData.Properties = properties.ToArray();


            /*
             * resolve init and destroy method
             */ 
            foreach (var item in _reflection.GetItem(type).Methods)
            {
                if (item.Attributes.Contains(typeof(InitiaMethodAttribute)))
                {
                    metaData.InitMethod = resolveMethod(item);
                }

                if (item.Attributes.Contains(typeof(DestroyAttribute)))
                {
                    metaData.DestroyMethod = resolveMethod(item);
                }
            }

            return metaData;
        }

        private IocMethodMetaData resolveMethod(Method method)
        {
            var metaData = new IocMethodMetaData();

            var parameters = method.Parameters.ToArray();

            Array.Sort(parameters, (x, y) => x.Index.CompareTo(y.Index));

            metaData.Parameters = new IocParameterMetaData[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                metaData.Parameters[i] = new IocParameterMetaData();

                metaData.Parameters[i].RelatedType = parameters[i].ParameterType;
                metaData.Parameters[i].Name = parameters[i].Name;
                metaData.Parameters[i].Index = i;

                if (parameters[i].Attributes.Contains(typeof(DefaultValueAttribute)))
                    metaData.Parameters[i].DefaultValue = parameters[i].Attributes.GetAttribute<DefaultValueAttribute>()[0].Value;

                if (parameters[i].Attributes.Contains(typeof(DefaultImplementAttribute)))
                    metaData.Parameters[i].Impl = parameters[i].Attributes.GetAttribute<DefaultImplementAttribute>()[0].Impl;

                if (parameters[i].Attributes.Contains(typeof(FromConfigAttribute)))
                    metaData.Parameters[i].ConfigKey = parameters[i].Attributes.GetAttribute<FromConfigAttribute>()[0].PropertyName;

                if (parameters[i].Attributes.Contains(typeof(NotNullAttribute)))
                    metaData.Parameters[i].NotNull = true;
            }


            return metaData;
        }
        private IocConstructorMetaData resolveConstructor(Constructor constructor)
        {
            var metaData = new IocConstructorMetaData(constructor);

            var parameters = constructor.Parameters.ToArray();

            Array.Sort(parameters, (x, y) => x.Index.CompareTo(y.Index));

            metaData.Parameters = new IocParameterMetaData[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                metaData.Parameters[i] = new IocParameterMetaData();

                metaData.Parameters[i].RelatedType = parameters[i].ParameterType;
                metaData.Parameters[i].Name = parameters[i].Name;
                metaData.Parameters[i].Index = i;

                if (parameters[i].Attributes.Contains(typeof(DefaultValueAttribute)))
                    metaData.Parameters[i].DefaultValue = parameters[i].Attributes.GetAttribute<DefaultValueAttribute>()[0].Value;

                if (parameters[i].Attributes.Contains(typeof(DefaultImplementAttribute)))
                    metaData.Parameters[i].Impl = parameters[i].Attributes.GetAttribute<DefaultImplementAttribute>()[0].Impl;

                if (parameters[i].Attributes.Contains(typeof(FromConfigAttribute)))
                    metaData.Parameters[i].ConfigKey = parameters[i].Attributes.GetAttribute<FromConfigAttribute>()[0].PropertyName;

                if (parameters[i].Attributes.Contains(typeof(NotNullAttribute)))
                    metaData.Parameters[i].NotNull = true;
            }

            return metaData;
        }
        private IocPropertyMetaData resolvePoperty(Field filed)
        {
            var metaData = new IocPropertyMetaData();

            metaData.RelatedType = filed.RelatedInfo.ReflectedType;
            metaData.Name = filed.Name;

            if (filed.Attributes.Contains(typeof(DefaultValueAttribute)))
                metaData.DefaultValue = filed.Attributes.GetAttribute<DefaultValueAttribute>()[0].Value;

            if (filed.Attributes.Contains(typeof(DefaultImplementAttribute)))
                metaData.Impl = filed.Attributes.GetAttribute<DefaultImplementAttribute>()[0].Impl;

            if (filed.Attributes.Contains(typeof(FromConfigAttribute)))
                metaData.ConfigKey = filed.Attributes.GetAttribute<FromConfigAttribute>()[0].PropertyName;

            return metaData;
        }




    }
}
