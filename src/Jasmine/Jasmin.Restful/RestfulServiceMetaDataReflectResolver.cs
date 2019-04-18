using Jasmine.Common;
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

                }
                else if (attrType == typeof(AfterInterceptorAttribute))
                {

                }
                else if (attrType == typeof(AroundInterceptorAttribute))
                {

                }
                else if (attrType == typeof(ErrorInterceptor))
                {

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

            foreach (var item in method.Attributes)
            {
                var attrType = item.GetType();

                if(attrType==typeof(PathAttribute))
                {

                }
                else if(attrType==typeof(HttpMethodAttribute))
                {

                }
                else if(attrType==typeof(BeforeInterceptorAttribute))
                {

                }
                else if(attrType==typeof(AfterInterceptorAttribute))
                {

                }
                else if(attrType==typeof(AroundInterceptorAttribute))
                {

                }
                else if(attrType==typeof(ErrorInterceptor))
                {

                }
                else if(attrType==typeof(SerializationModeAtribute))
                {

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

                    }
                    else if(attrType==typeof(QueryStringAttribute))
                    {

                    }
                    else if (attrType == typeof(DataAttribute))
                    {

                    }
                    else if(attrType==typeof(FormAttribute))
                    {

                    }
                    else if(attrType==typeof(PathVariableAttribute))
                    {

                    }
                    else if(attrType==typeof(defaultvalue))
                    {

                    }


                }

            }



            return null;
        }
    }
}
