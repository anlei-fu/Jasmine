using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Reflection;
using Jasmine.Restful.Attributes;
using Jasmine.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class RestfulServiceMetaDataReflectResolver : IServiceMetaDataReflectResolver<RestfulServiceMetaData>
    {
        private RestfulServiceMetaDataReflectResolver()
        {

        }

        public static readonly IServiceMetaDataReflectResolver<RestfulServiceMetaData> Instance = new RestfulServiceMetaDataReflectResolver();

        private ITypeCache _typeCache => JasmineReflectionCache.Instance;
        public RestfulServiceMetaData Resolve(Type type)
        {
            if (!_typeCache.GetItem(type).Attributes.Contains(typeof(RestfulAttribute)))
                return null;

            var metaData = new RestfulServiceMetaData();

            metaData.RelatedType = type;

            foreach (var item in _typeCache.GetItem(type).Attributes)
            {
                var attrType = item.GetType();

                if(attrType== typeof(PathAttribute))
                {
                    metaData.Path = ((PathAttribute)item[0]).Path;
                }
                else if(attrType==typeof(HttpMethodAttribute))
                {
                    metaData.HttpMethod = ((HttpMethodAttribute)item[0]).Method;
                }
                else if (attrType == typeof(BeforeInterceptorAttribute))
                {
                    foreach (var before in item)
                    {
                        metaData.BeforeFilters.Add(((BeforeInterceptorAttribute)before).Name);
                    }
                }
                else if (attrType == typeof(AfterInterceptorAttribute))
                {
                    foreach (var after in item)
                    {
                        metaData.BeforeFilters.Add(((AfterInterceptorAttribute)after).Name);
                    }
                }
                else if (attrType == typeof(AroundInterceptorAttribute))
                {
                    foreach (var around in item)
                    {
                        metaData.BeforeFilters.Add(((AroundInterceptorAttribute)around).Name);
                    }
                }
                else if (attrType == typeof(ErrorInterceptor))
                {
                    foreach (var error in item)
                    {
                        metaData.BeforeFilters.Add(((ErrorInterceptor)error).Name);
                    }
                }
                else if(attrType==typeof(SerializationModeAtribute))
                {
                    metaData.SerializeMode = ((SerializationModeAtribute)item[0]).SerializeMode;
                }
                else if(attrType==typeof(AliasAttribute))
                {
                    metaData.Name= ((AliasAttribute)item[0]).Alias;
                }
                else if(attrType==typeof(MaxConcurrencyAttribute))
                {
                    metaData.MaxConcurrency = ((MaxConcurrencyAttribute)item[0]).MaxConcurrency;
                }

            }

            if (metaData.Name == null)
                metaData.Name = metaData.RelatedType.FullName;


            if(metaData.HttpMethod==null)
            {
                metaData.HttpMethod = HttpMethods.GET;
            }
            
            if(metaData.Path==null)
            {
                metaData.Path = "/"+metaData.RelatedType.Name.ToLower();
            }



            var requests = new List<RestfulRequestMetaData>();

            foreach (var item in _typeCache.GetItem(type).Methods)
            {

                var request = resolveRequest(item,metaData.Path,metaData.HttpMethod);

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


        private void setRequest(List<RestfulRequestMetaData> list,RestfulRequestMetaData requestMetaData,RestfulServiceMetaData requestGroupMetaData)
        {
            var t = 1;

            for (int i = 0; i < list.Count; i++)
            {
                if(requestMetaData.Name==list[i].Name)
                {
                    t++;
                    requestMetaData.Name = requestMetaData.Name + t.ToString();
                    i = 0;
                }
            }


            if (requestMetaData.Path == null)
                requestMetaData.Path = requestGroupMetaData.Path + "/" + requestMetaData.Name.ToLower();

            if (requestMetaData.HttpMethod == null)
                requestMetaData.HttpMethod = requestGroupMetaData.HttpMethod;

        }


        private RestfulRequestMetaData resolveRequest(Method method,string groupPath,string groupMethod)
        {
            var metaData = new RestfulRequestMetaData();

            metaData.Method = method;

            foreach (var item in method.Attributes)
            {
                var attrType = item.GetType();

                if (attrType == typeof(PathAttribute))
                {
                    metaData.Path = ((PathAttribute)item[0]).Path;
                }
                else if(attrType==typeof(RestfulIgnoreAttribute))
                {
                    return null;
                }
                else if (attrType == typeof(HttpMethodAttribute))
                {
                    metaData.HttpMethod = ((HttpMethodAttribute)item[0]).Method;
                }
                else if (attrType == typeof(BeforeInterceptorAttribute))
                {
                    foreach (var before in item)
                    {
                        metaData.BeforeFilters.Add(((BeforeInterceptorAttribute)before).Name);
                    }
                }
                else if (attrType == typeof(AfterInterceptorAttribute))
                {
                    foreach (var after in item)
                    {
                        metaData.BeforeFilters.Add(((AfterInterceptorAttribute)after).Name);
                    }
                }
                else if (attrType == typeof(AroundInterceptorAttribute))
                {
                    foreach (var around in item)
                    {
                        metaData.BeforeFilters.Add(((AroundInterceptorAttribute)around).Name);
                    }
                }
                else if (attrType == typeof(ErrorInterceptor))
                {
                    foreach (var error in item)
                    {
                        metaData.BeforeFilters.Add(((ErrorInterceptor)error).Name);
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
                    metaData.AlternativeService= ((AlternativeAttribute)item[0]).Path;
                }

            }


            if(metaData.Name==null)
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

                    if(attrType==typeof(BodyAttribute))
                    {
                        parameterMetaData.FromBody = true;

                        parameterFromSetted = true;
                    }
                    else if(attrType==typeof(QueryStringAttribute))
                    {
                        parameterMetaData.QueryStringKey = ((QueryStringAttribute)attr[0]).Name;

                        parameterFromSetted = true;
                    }
                    else if (attrType == typeof(DataAttribute))
                    {
                        parameterMetaData.DataKey = ((DataAttribute)attr[0]).Name;
                    }
                    else if(attrType==typeof(FormAttribute))
                    {
                        parameterMetaData.DataKey = ((FormAttribute)attr[0]).Name;
                    }
                    else if(attrType==typeof(PathVariableAttribute))
                    {
                        parameterMetaData.PathVariableKey= ((PathVariableAttribute)attr[0]).Name;

                        parameterFromSetted = true;
                    }
                    else if(attrType==typeof(DefaultValueAttribute))
                    {
                        parameterMetaData.DefaultValue = ((DefaultValueAttribute)attr[0]).Value;
                    }
                    else if(attrType==typeof(NotNullAttribute))
                    {
                        parameterMetaData.NotNull = true;
                    }
                    else if(attrType==typeof(DefaultImplementAttribute))
                    {
                        parameterMetaData.ImplType = ((DefaultImplementAttribute)attr[0]).Impl;
                    }

                }

                if(!parameterFromSetted)
                {
                    if(metaData.HttpMethod==HttpMethods.GET)
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
