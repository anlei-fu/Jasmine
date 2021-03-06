﻿using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Reflection;
using Jasmine.Restful.Attributes;
using Jasmine.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class RestfulServiceMetaDataReflectResolver : IMetaDataReflectResolver<RestfulServiceMetaData>
    {
        private RestfulServiceMetaDataReflectResolver()
        {

        }

        internal static readonly RestfulServiceMetaDataReflectResolver Instance = new RestfulServiceMetaDataReflectResolver();

        private ITypeCache _typeCache => JasmineReflectionCache.Instance;
        public RestfulServiceMetaData Resolve(Type type)
        {

            var metaData = new RestfulServiceMetaData();

            metaData.RelatedType = type;

            foreach (var item in _typeCache.GetItem(type).Attributes)
            {
                var attrType = item[0].GetType();

                if (attrType == typeof(PathAttribute))
                {
                    metaData.Path = ((PathAttribute)item[0]).Path;
                }
               
                else if (attrType == typeof(GetAttribute))
                {
                    metaData.HttpMethod = HttpMethods.GET;
                }
                else if (attrType == typeof(PostAttribute))
                {
                    metaData.HttpMethod = HttpMethods.POST;
                }
                else if (attrType == typeof(DescriptionAttribute))
                {
                    metaData.Description = ((DescriptionAttribute)item[0]).Description;
                }
                else if (attrType == typeof(BeforeInterceptorAttribute))
                {
                    foreach (var before in item)
                    {
                        metaData.BeforeInterceptors.Add(((BeforeInterceptorAttribute)before).FilterType);
                    }
                }
                else if (attrType == typeof(AfterInterceptorAttribute))
                {
                    foreach (var after in item)
                    {
                        metaData.AfterInterceptors.Add(((AfterInterceptorAttribute)after).FilterType);
                    }
                }
                else if (attrType == typeof(AroundInterceptorAttribute))
                {
                    foreach (var around in item)
                    {
                        metaData.AroundInterceptors.Add(((AroundInterceptorAttribute)around).FilterType);
                    }
                }
                else if (attrType == typeof(ErrorInterceptor))
                {
                    foreach (var error in item)
                    {
                        metaData.ErrorInterceptors.Add(((ErrorInterceptor)error).FilterType);
                    }
                }
                else if (attrType == typeof(SerializationModeAtribute))
                {
                    metaData.SerializeMode = ((SerializationModeAtribute)item[0]).SerializeMode;
                }
                else if (attrType == typeof(AliasAttribute))
                {
                    metaData.Name = ((AliasAttribute)item[0]).Alias;
                }
                else if (attrType == typeof(MaxConcurrencyAttribute))
                {
                    metaData.MaxConcurrency = ((MaxConcurrencyAttribute)item[0]).MaxConcurrency;
                }

            }

            if (metaData.Name == null)
                metaData.Name = metaData.RelatedType.FullName;


            if (metaData.HttpMethod == null)
            {
                metaData.HttpMethod = HttpMethods.GET;
            }

            if (metaData.Path == null)
            {
                metaData.Path = "/" + metaData.RelatedType.Name.ToLower();
            }



            var requests = new List<RestfulRequestMetaData>();

            foreach (var item in _typeCache.GetItem(type).Methods)
            {

                var request = resolveRequest(item, metaData.Path, metaData.HttpMethod);

                if (request == null)
                    continue;

                setRequest(requests, request, metaData);

                requests.Add(request);
            }

            foreach (var item in requests)
            {
                metaData.Requests.Add(item.Path, item);
            }

            return metaData;
        }


        private void setRequest(List<RestfulRequestMetaData> list, RestfulRequestMetaData requestMetaData, RestfulServiceMetaData requestGroupMetaData)
        {
            var t = 1;

            for (int i = 0; i < list.Count; i++)
            {
                if (requestMetaData.Name == list[i].Name)
                {
                    t++;
                    requestMetaData.Name = requestMetaData.Name + t.ToString();
                    i = 0;
                }
            }

            t = 1;


            for (int i = 0; i < list.Count; i++)
            {
                if (requestMetaData.Path == list[i].Path)
                {
                    t++;
                    requestMetaData.Path = requestMetaData.Path + t.ToString();
                    i = 0;
                }
            }

        }


        private RestfulRequestMetaData resolveRequest(Method method, string groupPath, string groupMethod)
        {
            var metaData = new RestfulRequestMetaData();

            metaData.Method = method;

            foreach (var item in method.Attributes)
            {
                var attrType = item[0].GetType();
                if (attrType == typeof(RestfulIgnoreAttribute))
                {
                    return null;
                }
                else if (attrType == typeof(PathAttribute))
                {
                    metaData.Path = ((PathAttribute)item[0]).Path;
                }
                else if (attrType == typeof(GetAttribute))
                {
                    metaData.HttpMethod = HttpMethods.GET;
                }
                else if (attrType == typeof(PostAttribute))
                {
                    metaData.HttpMethod = HttpMethods.POST;
                }
                else if (attrType == typeof(DescriptionAttribute))
                {
                    metaData.Description = ((DescriptionAttribute)item[0]).Description;
                }
                else if (attrType == typeof(RestfulIgnoreAttribute))
                {
                    return null;
                }
               
                else if (attrType == typeof(BeforeInterceptorAttribute))
                {
                    foreach (var before in item)
                    {
                        metaData.BeforeInterceptors.Add(((BeforeInterceptorAttribute)before).FilterType);
                    }
                }
                else if (attrType == typeof(AfterInterceptorAttribute))
                {
                    foreach (var after in item)
                    {
                        metaData.AfterInterceptors.Add(((AfterInterceptorAttribute)after).FilterType);
                    }
                }
                else if (attrType == typeof(AroundInterceptorAttribute))
                {
                    foreach (var around in item)
                    {
                        metaData.AroundInterceptors.Add(((AroundInterceptorAttribute)around).FilterType);
                    }
                }
                else if (attrType == typeof(ErrorInterceptor))
                {
                    foreach (var error in item)
                    {
                        metaData.ErrorInterceptors.Add(((ErrorInterceptor)error).FilterType);
                    }
                }
                else if (attrType == typeof(SerializationModeAtribute))
                {
                    metaData.SerializeMode = ((SerializationModeAtribute)item[0]).SerializeMode;
                }
                else if (attrType == typeof(AliasAttribute))
                {
                    metaData.Name = ((AliasAttribute)item[0]).Alias;
                }
                else if (attrType == typeof(MaxConcurrencyAttribute))
                {
                    metaData.MaxConcurrency = ((MaxConcurrencyAttribute)item[0]).MaxConcurrency;
                }
                else if (attrType == typeof(AlternativeAttribute))
                {
                    metaData.AlternativeService = ((AlternativeAttribute)item[0]).Path;
                }

            }


            if (metaData.Name == null)
            {
                metaData.Name = method.Name;
            }
            if (metaData.HttpMethod == null)
            {
                metaData.HttpMethod = groupMethod;
            }

            if (metaData.Path == null)
            {
                metaData.Path = groupPath + "/" + metaData.Name.ToLower();
            }

            var ls = new List<RestfulRequestParameterMetaData>();

            foreach (var item in method.Parameters)
            {

                var parameterMetaData = new RestfulRequestParameterMetaData();

                parameterMetaData.RelatedType = item.ParameterType;

                var parameterFromSetted = false;

                foreach (var attr in item.Attributes)
                {
                    var attrType = item.GetType();

                    if (attrType == typeof(BodyAttribute))
                    {
                        parameterMetaData.FromBody = true;

                        parameterFromSetted = true;
                    }
                    else if (attrType == typeof(QueryStringAttribute))
                    {
                        parameterMetaData.QueryStringKey = ((QueryStringAttribute)attr[0]).Name;

                        parameterFromSetted = true;
                    }
                    else if (attrType == typeof(DataAttribute))
                    {
                        parameterMetaData.DataKey = ((DataAttribute)attr[0]).Name;
                    }
                    else if (attrType == typeof(FormAttribute))
                    {
                        parameterMetaData.DataKey = ((FormAttribute)attr[0]).Name;
                    }
                    else if (attrType == typeof(PathVariableAttribute))
                    {
                        parameterMetaData.PathVariableKey = ((PathVariableAttribute)attr[0]).VaribleName;

                        parameterFromSetted = true;
                    }
                    else if (attrType == typeof(DefaultValueAttribute))
                    {
                        parameterMetaData.DefaultValue = ((DefaultValueAttribute)attr[0]).Value;
                    }
                    else if (attrType == typeof(NotNullAttribute))
                    {
                        parameterMetaData.NotNull = true;
                    }
                    else if (attrType == typeof(DefaultImplementAttribute))
                    {
                        parameterMetaData.ImplType = ((DefaultImplementAttribute)attr[0]).Impl;
                    }

                }

                if (!parameterFromSetted)
                {
                    if (metaData.HttpMethod == HttpMethods.GET)
                    {
                        parameterMetaData.QueryStringKey = item.Name;
                    }
                    else
                    {
                        parameterMetaData.FromBody = true;
                    }
                }

                ls.Add(parameterMetaData);

            }

            metaData.Parameters = ls.ToArray();

            return metaData;
        }
    }
}
