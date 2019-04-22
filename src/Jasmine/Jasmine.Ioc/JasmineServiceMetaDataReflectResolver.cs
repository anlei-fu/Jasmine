using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Ioc.Attributes;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Ioc
{
    public class JasmineServiceMetaDataReflectResolver : IServiceMetaDataReflectResolver<IocServiceMetaData>
    {
        private ITypeCache _reflection = JasmineReflectionCache.Instance;

        public static readonly IServiceMetaDataReflectResolver<IocServiceMetaData> Instance = new JasmineServiceMetaDataReflectResolver();

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
                if (typeAttrs.Contains(typeof(DefaultImplementAttribute)))
                    DefaultServiceMetaDataManager.Instance.SetImplementation(type, typeAttrs.GetAttribute<DefaultImplementAttribute>().Impl);

                return null;
            }

            if (typeAttrs.Contains(typeof(ServiceScopeAttribute)))
                metaData.Scope = typeAttrs.GetAttribute<ServiceScopeAttribute>().Scope;

            if (typeAttrs.Contains(typeof(ServiceAliasAttibute)))
                metaData.Name = typeAttrs.GetAttribute<ServiceAliasAttibute>().Alias;
            else
                metaData.Name = type.FullName;

            if (typeAttrs.Contains(typeof(LazyLoadAttribute)))
                metaData.LazyLoad = true;


            var constructors = _reflection.GetItem(type).Constructors.GetAll().ToArray();

            Constructor defaultConstructor = null;

            foreach (var constructor in constructors)
            {
                if (constructor.Attributes.Contains(typeof(DefaultConstructor)))
                {
                    defaultConstructor = constructor;
                    break;
                }
            }

            if (defaultConstructor == null)
                defaultConstructor = _reflection.GetItem(type).Constructors.GetDefaultConstructor() ??
                                                                                                 constructors[0];

            metaData.ConstrctorMetaData = resolveConstructor(defaultConstructor);


            var properties = new List<IocPropertyMetaData>();

            foreach (var item in _reflection.GetItem(type).Fileds.Union(_reflection.GetItem(type).Properties))
            {
                if (item.Attributes.Contains(typeof(AutoWirdAttribute)))
                    properties.Add(generatePoperty(item));
            }


            metaData.Properties = properties.ToArray();

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
                    metaData.Parameters[i].DefaultValue = parameters[i].Attributes.GetAttribute<DefaultValueAttribute>().Value;

                if (parameters[i].Attributes.Contains(typeof(DefaultImplementAttribute)))
                    metaData.Parameters[i].Impl = parameters[i].Attributes.GetAttribute<DefaultImplementAttribute>().Impl;

                if (parameters[i].Attributes.Contains(typeof(FromConfigAttribute)))
                    metaData.Parameters[i].ConfigKey = parameters[i].Attributes.GetAttribute<FromConfigAttribute>().PropertyName;

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
                    metaData.Parameters[i].DefaultValue = parameters[i].Attributes.GetAttribute<DefaultValueAttribute>().Value;

                if (parameters[i].Attributes.Contains(typeof(DefaultImplementAttribute)))
                    metaData.Parameters[i].Impl = parameters[i].Attributes.GetAttribute<DefaultImplementAttribute>().Impl;

                if (parameters[i].Attributes.Contains(typeof(FromConfigAttribute)))
                    metaData.Parameters[i].ConfigKey = parameters[i].Attributes.GetAttribute<FromConfigAttribute>().PropertyName;

                if (parameters[i].Attributes.Contains(typeof(NotNullAttribute)))
                    metaData.Parameters[i].NotNull = true;
            }


            return metaData;
        }
        private IocPropertyMetaData generatePoperty(Field filed)
        {
            var metaData = new IocPropertyMetaData();

            metaData.RelatedType = filed.RelatedInfo.ReflectedType;
            metaData.Name = filed.Name;


            if (filed.Attributes.Contains(typeof(DefaultValueAttribute)))
                metaData.DefaultValue = filed.Attributes.GetAttribute<DefaultValueAttribute>().Value;

            if (filed.Attributes.Contains(typeof(DefaultImplementAttribute)))
                metaData.Impl = filed.Attributes.GetAttribute<DefaultImplementAttribute>().Impl;

            if (filed.Attributes.Contains(typeof(FromConfigAttribute)))
                metaData.ConfigKey = filed.Attributes.GetAttribute<FromConfigAttribute>().PropertyName;

            return metaData;
        }




    }
}
