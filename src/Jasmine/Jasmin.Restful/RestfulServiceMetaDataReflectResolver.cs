using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Reflection;
using Jasmine.Restful.Attributes;
using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class RestfulServiceMetaDataReflectResolver : IServiceMetaDataReflectResolver<RestfulServiceMetaData>
    {
        private ITypeCache _typeCache = JasmineReflectionCache.Instance;
        public RestfulServiceMetaData Resolve(Type type)
        {
            if (!_typeCache.GetItem(type).Attributes.Contains(typeof(RestfulAttribute)))
                return null;

            var metaData = new RestfulServiceMetaData();

            foreach (var item in _typeCache.GetItem(type).Attributes)
            {
                var attrType = item.GetType();

                if(attrType== typeof(PathAttribute))
                {
                    metaData.Path = ((PathAttribute)item).Path;
                }
                else if(attrType==typeof(HttpMethodAttribute))
                {
                    metaData.HttpMethod = ((HttpMethodAttribute)item).Method;
                }
                else if (attrType == typeof(BeforeInterceptorAttribute))
                {
                    metaData.BeforeFilters.Add(((BeforeInterceptorAttribute)item).Name);
                }
                else if (attrType == typeof(AfterInterceptorAttribute))
                {
                    metaData.AfterFilters.Add(((AfterInterceptorAttribute)item).Name);
                }
                else if (attrType == typeof(AroundInterceptorAttribute))
                {
                    metaData.AroundFilters.Add(((AroundInterceptorAttribute)item).Name);
                }
                else if (attrType == typeof(ErrorInterceptor))
                {
                    metaData.ErrorFilters.Add(((ErrorInterceptor)item).Name);
                }

            }

            var requests = new List<RestfulRequestMetaData>();

            foreach (var item in _typeCache.GetItem(type).Methods)
            {
                if(item.Attributes.Contains(typeof(PathAttribute)))
                {
                    requests.Add(resolveMethodMetaData(item));
                }

            }

            foreach (var item in requests)
            {
                metaData.Requests.Add(item.Path, item);
            }

        

            return metaData;
        }


        public RestfulRequestMetaData resolveMethodMetaData(Method method)
        {
            var metaData = new RestfulRequestMetaData();

            metaData.Method = method;
            foreach (var item in method.Attributes)
            {
                var attrType = item.GetType();

                if (attrType == typeof(PathAttribute))
                {
                    metaData.Path = ((PathAttribute)item).Path;
                }
                else if (attrType == typeof(HttpMethodAttribute))
                {
                    metaData.HttpMethod = ((HttpMethodAttribute)item).Method;
                }
                else if (attrType == typeof(BeforeInterceptorAttribute))
                {
                    metaData.BeforeFilters.Add(((BeforeInterceptorAttribute)item).Name);
                }
                else if (attrType == typeof(AfterInterceptorAttribute))
                {
                    metaData.AfterFilters.Add(((AfterInterceptorAttribute)item).Name);
                }
                else if (attrType == typeof(AroundInterceptorAttribute))
                {
                    metaData.AroundFilters.Add(((AroundInterceptorAttribute)item).Name);
                }
                else if (attrType == typeof(ErrorInterceptor))
                {
                    metaData.ErrorFilters.Add(((ErrorInterceptor)item).Name);
                }

            }

            foreach (var item in method.Parameters)
            {

                var parameterMetaData = new RestfulRequestParameterMetaData();

                foreach (var attr in item.Attributes)
                {

                    var attrType = item.GetType();

                    if(attrType==typeof(BodyAttribute))
                    {
                        parameterMetaData.FromBody = true;
                    }
                    else if(attrType==typeof(QueryStringAttribute))
                    {
                        parameterMetaData.QueryString = ((QueryStringAttribute)attr).Name;
                    }
                    else if (attrType == typeof(DataAttribute))
                    {
                        parameterMetaData.DataString = ((DataAttribute)attr).Name;
                    }
                    else if(attrType==typeof(FormAttribute))
                    {
                        parameterMetaData.DataString = ((FormAttribute)attr).Name;
                    }
                    else if(attrType==typeof(PathVariableAttribute))
                    {
                        parameterMetaData.PathVariable= ((PathVariableAttribute)attr).Name;
                    }
                    else if(attrType==typeof(DefaultValueAttribute))
                    {
                        parameterMetaData.DefaultValue = ((DefaultValueAttribute)attr).Value;
                    }
                    else if(attrType==typeof(NotNullAttribute))
                    {
                        parameterMetaData.NotNull = true;
                    }
                    else if(attrType==typeof(DefaultImplementAttribute))
                    {
                        parameterMetaData.ImplType = ((DefaultImplementAttribute)attr).Impl;
                    }


                }

            }



            return null;
        }
    }
}
