using Jasmine.Common;
using Jasmine.Extensions;
using Jasmine.Restful.Exceptions;
using Jasmine.Serialization;

namespace Jasmine.Restful
{
    internal class RestfulRequestParameterResolver : IRequestParamteterResolver<HttpFilterContext>
    {
        public RestfulRequestParameterResolver(RestfulRequestParameterMetaData[] parameters)
        {
            _parameters = parameters;
        }
        private RestfulRequestParameterMetaData[] _parameters;
        public object[] Resolve(HttpFilterContext context)
        {
            var rets = new object[_parameters.Length];

            for (int i = 0; i < _parameters.Length; i++)
            {
                var type = _parameters[i].IsAbstract ? _parameters[i].ImplType : _parameters[i].RelatedType;

                if (_parameters[i].FromBody)
                {
                    rets[i] = context.HttpContext.Request.Body.ReadData(type, SerializeMode.Json);
                }
                else if (_parameters[i].FromQueryString)
                {
                    rets[i] = JsonSerializer.Instance.Deserialize(context.HttpContext.Request.Query[_parameters[i].QueryStringKey], type);
                }
                else if (_parameters[i].FromPathVariable)
                {
                    rets[i] = JsonSerializer.Instance.Deserialize(context.HttpContext.Request.Form[_parameters[i].PathVariableKey], type);
                }
                else if (_parameters[i].FromData)
                {
                    rets[i] = context.Datas[_parameters[i].DataKey];
                }
                else
                {
                    throw new ParameterCanNotResolveException();
                }

                if (rets[i] == null)
                {
                    if (!_parameters[i].NotNull)
                    {
                        rets[i] = JasmineDefaultValueProvider.GetDefaultValue(_parameters[i].RelatedType);
                    }
                    else
                    {
                        throw new ParamterNullException();
                    }
                }

            }

            return rets;
        }

    }

}
