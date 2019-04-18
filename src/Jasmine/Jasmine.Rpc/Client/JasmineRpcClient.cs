using Jasmine.Common;
using Jasmine.Rpc.Client.Exceptions;
using Jasmine.Serialization;
using System.Collections.Generic;

namespace Jasmine.Rpc.Client
{
    public class JasmineRpcClient : IRpcClient
    {
        private IRpcClientConnetionProvider _provider;
        private IRpcRequestFactory _factory;
        private ISerializer _serializer;

        public IRpcResponse Call(string group, string name, IDictionary<string, object> parameters, int timeout = 10000)
        {
            return Call(_factory.Create(group, name, parameters, timeout));
        }

        public IRpcResponse Call(string name, IDictionary<string, object> parameters, int timeout = 10000)
        {
            return Call(_factory.Create(name, parameters, timeout));
        }

        public IRpcResponse Call(string group, string name, object parameter, int timeout = 10000)
        {
            return Call(_factory.Create(group, name, parameter, timeout));
        }

        public IRpcResponse Call(string name, object parameter, int timeout = 10000)
        {
            return Call(_factory.Create(name, parameter, timeout));
        }
        public IRpcResponse Call(IRpcRequest request)
        {
            var connection = _provider.Get(request.Group);

            if (connection == null)
                throw new ServiceNotAvailableException();

            var context=  connection.Call(request);

            if (context == null)
                throw new RpcCallAlreadyScheduledException();

            bool token = true;

            context.Lock.Enter(ref token);//block  until response received or time out 

            switch (context.Stutas)
            {
                case RpcCallStutas.Timeout:
                    throw new RpcCallTimeoutException();
                case RpcCallStutas.Finished:
                    return context.Response;
                case RpcCallStutas.ConnectionClosed:
                    throw new ConnectionClosedException();
                default:
                    throw new System.Exception();
            }

        }


        public T Call<T>(string group, string name, IDictionary<string, object> parameters, int timeout = 10000)
        {
            return Call<T>(_factory.Create(group, name, parameters, timeout));
        }

        public T Call<T>(string name, IDictionary<string, object> parameters, int timeout = 10000)
        {
            return Call<T>(_factory.Create(name, parameters, timeout));
        }

        public T Call<T>(string group, string name, object parameter, int timeout = 10000)
        {
            return Call<T>(_factory.Create(group, name, parameter, timeout));
        }

        public T Call<T>(string name, object parameter, int timeout = 10000)
        {
            return Call<T>(_factory.Create(name, parameter, timeout));
        }

        public T Call<T>(IRpcRequest request)
        {
            var response = Call(request);//

            switch (response.ErrorCode)
            {
                case ResponseErrorCode.ParameterIncorrect:
                    throw new ParameterIncorrectException();
                case ResponseErrorCode.ParameterNull:
                    throw new ParameterIncorrectException();
                case ResponseErrorCode.CallNotFound:
                    throw new RpcCallNotExistsException();
                case ResponseErrorCode.Successed:

                    if (_serializer.TryDeserialize<T>(response.Data, out var value))
                    {
                        return value;
                    }
                    else
                    {
                        throw new RpcSerializationException();
                    }
                default:
                    throw new System.Exception();
            }
        }
    }
}
