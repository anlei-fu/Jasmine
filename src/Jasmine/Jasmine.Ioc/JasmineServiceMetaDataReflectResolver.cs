using Jasmine.Ioc.Attributes;
using Jasmine.Reflection;
using Jasmine.Reflection.Interfaces;
using Jasmine.Reflection.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Ioc
{
    public class JasmineServiceMetaDataReflectResolver : IServiceMetaDataReflectResolver
    {
        private ITypeCache _reflection = DefaultReflectionCache.Instance;

        public static readonly IServiceMetaDataReflectResolver Instance = new JasmineServiceMetaDataReflectResolver();

        public ServiceMetaData ResolveProtoType(Type type)
        {
            return Resolve(type, ServiceScope.ProtoType);
        }
        public ServiceMetaData ResolveRequest(Type type)
        {
            return Resolve(type, ServiceScope.Request);
        }

        public ServiceMetaData ResolveSingleton(Type type)
        {
            return Resolve(type, ServiceScope.Singleton);
        }
        public ServiceMetaData Resolve(Type type, ServiceScope scope)
        {
            var metaData = Resolve(type);

            if (metaData != null)
                metaData.Scope = scope;

            return metaData;
        }
        public ServiceMetaData Resolve(Type type)
        {
            var metaData = new ServiceMetaData(type);

            var typeAttrs = _reflection.GetItem(type).Attributes;

            metaData.RelatedType = type;

            if (type.IsInterfaceOrAbstraClass())
            {
                if (typeAttrs.Contains(typeof(DefaultImplementAttribute)))
                    DefaultServiceMetaDataManager.Instance.SetImplement(type, typeAttrs.GetAttribute<DefaultImplementAttribute>().Implement);

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

            metaData.ConstrctorMetaData = generateConstructor(defaultConstructor);


            var properties = new List<PropertyMetaData>();

            foreach (var item in _reflection.GetItem(type).Fileds.Union(_reflection.GetItem(type).Properties))
            {
                if (item.Attributes.Contains(typeof(AutoWirdAttribute)))
                    properties.Add(generatePoperty(item));
            }


            metaData.Properties = properties.ToArray();

            return metaData;
        }
        private ConstructorMetaData generateConstructor(Constructor constructor)
        {
            var metaData = new ConstructorMetaData(constructor);

            var parameters = constructor.Parameters.ToArray();

            Array.Sort(parameters, (x, y) => x.Index.CompareTo(y.Index));

            metaData.Parameters = new ParameterMetaData[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                metaData.Parameters[i] = new ParameterMetaData();

                metaData.Parameters[i].RelatedType = parameters[i].ParameterType;
                metaData.Parameters[i].Name = parameters[i].Name;
                metaData.Parameters[i].Index = i;

                if (parameters[i].Attributes.Contains(typeof(DefaultValueAttribute)))
                    metaData.Parameters[i].DefaultValue = parameters[i].Attributes.GetAttribute<DefaultValueAttribute>().Value;

                if (parameters[i].Attributes.Contains(typeof(DefaultImplementAttribute)))
                    metaData.Parameters[i].ImplType = parameters[i].Attributes.GetAttribute<DefaultImplementAttribute>().Implement;

                if (parameters[i].Attributes.Contains(typeof(FromConfigAttribute)))
                    metaData.Parameters[i].PropertyKey = parameters[i].Attributes.GetAttribute<FromConfigAttribute>().PropertyName;

                if (parameters[i].Attributes.Contains(typeof(NotNullAttribute)))
                    metaData.Parameters[i].NotNull = true;
            }


            return metaData;
        }
        private PropertyMetaData generatePoperty(Field filed)
        {
            var metaData = new PropertyMetaData();

            metaData.RelatedType = filed.RelatedInfo.ReflectedType;
            metaData.Name = filed.Name;


            if (filed.Attributes.Contains(typeof(DefaultValueAttribute)))
                metaData.DefaultValue = filed.Attributes.GetAttribute<DefaultValueAttribute>().Value;

            if (filed.Attributes.Contains(typeof(DefaultImplementAttribute)))
                metaData.ImplType = filed.Attributes.GetAttribute<DefaultImplementAttribute>().Implement;

            if (filed.Attributes.Contains(typeof(FromConfigAttribute)))
                metaData.PropertyKey = filed.Attributes.GetAttribute<FromConfigAttribute>().PropertyName;

            return metaData;
        }

    


    }
}
