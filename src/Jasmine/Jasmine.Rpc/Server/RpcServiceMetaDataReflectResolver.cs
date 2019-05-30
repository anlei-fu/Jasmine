using System;
using System.Collections.Generic;
using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Reflection;
using Jasmine.Rpc.Attributes;
using Jasmine.Serialization.Attributes;

namespace Jasmine.Rpc.Server
{
    public class RpcServiceMetaDataReflectResolver : IMetaDataReflectResolver<RpcServiceMetaData>
    {

        public static readonly IMetaDataReflectResolver<RpcServiceMetaData> Instance = new RpcServiceMetaDataReflectResolver();

        private ITypeCache _typeCache => JasmineReflectionCache.Instance;
        public RpcServiceMetaData Resolve(Type type)
        {
            if (!_typeCache.GetItem(type).Attributes.Contains(typeof(RpcAttribute)))
                return null;

            var metaData = new RpcServiceMetaData();

            metaData.RelatedType = type;

            foreach (var item in _typeCache.GetItem(type).Attributes)
            {
                var attrType = item.GetType();

                if (attrType == typeof(PathAttribute))
                {
                    metaData.Path = ((PathAttribute)item[0]).Path;
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

            }

            if (metaData.Name == null)
                metaData.Name = metaData.RelatedType.FullName;




            if (metaData.Path == null)
            {
                metaData.Path = "/" + metaData.RelatedType.Name.ToLower();
            }

            var requests = new List<RpcRequestMetaData>();

            foreach (var item in _typeCache.GetItem(type).Methods)
            {
                var request = resolveRequest(item, metaData.Path);

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

        /// <summary>
        /// handle  method overlay 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="requestMetaData"></param>
        /// <param name="requestGroupMetaData"></param>
        private void setRequest(List<RpcRequestMetaData> list, RpcRequestMetaData requestMetaData, RpcServiceMetaData requestGroupMetaData)
        {

            for (int i = 0; i < list.Count; i++)
            {
                if (requestMetaData.Name == list[i].Name)
                {
                    requestMetaData.Name = requestMetaData.Name + i.ToString();

                    i = 0;
                }
            }

            if (requestMetaData.Path == null)
                requestMetaData.Path = requestGroupMetaData.Path + "/" + requestMetaData.Name.ToLower();

        }


        private RpcRequestMetaData resolveRequest(Method method, string groupPath)
        {
            var metaData = new RpcRequestMetaData();

            metaData.Method = method;

            foreach (var item in method.Attributes)
            {
                var attrType = item.GetType();

                if (attrType == typeof(PathAttribute))
                {
                    metaData.Path = ((PathAttribute)item[0]).Path;
                }
                else if (attrType == typeof(RpcIgnoreAttribute))
                {
                    return null;
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

            }


            if (metaData.Name == null)
            {
                metaData.Name = method.Name;
            }

            if (metaData.Path == null)
            {
                metaData.Path = groupPath + "/" + metaData.Name.ToLower();
            }


            var ls = new List<RpcRequestParameterMetaData>();

            foreach (var item in method.Parameters)
            {
                var parameterMetaData = new RpcRequestParameterMetaData();

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
                    parameterMetaData.QueryStringKey = item.Name;

                }

                ls.Add(parameterMetaData);

            }

            metaData.Parameters = ls.ToArray();

            return metaData;
        }
    }
}
