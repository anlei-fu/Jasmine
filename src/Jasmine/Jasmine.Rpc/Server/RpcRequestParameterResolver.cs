using Jasmine.Common;
using Jasmine.Common.Exceptions;
using Jasmine.Extensions;
using Jasmine.Serialization;

namespace Jasmine.Rpc.Server
{
    internal class RpcRequestParameterResolver : IRequestParamteterResolver<RpcFilterContext>
    {
        public RpcRequestParameterResolver(RpcRequestParameterMetaData[] parameters)
        {
            _parameters = parameters;
        }
        private RpcRequestParameterMetaData[] _parameters;
        public object[] Resolve(RpcFilterContext context)
        {
            var rets = new object[_parameters.Length];

            for (int i = 0; i < _parameters.Length; i++)
            {
                var type = _parameters[i].IsAbstract ? _parameters[i].ImplType : _parameters[i].RelatedType;

                if (_parameters[i].FromBody)
                {
                    rets[i] = context.RpcContext.Request.Body.ReadData(type, SerializeMode.Json);
                }
                else if (_parameters[i].FromQueryString)
                {
                    rets[i] = JsonSerializer.Instance.Deserialize(context.RpcContext.Request.Query[_parameters[i].QueryStringKey], type);
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
