using Jasmine.Rpc.Client.Exceptions;
using System.Collections.Generic;

namespace Jasmine.Rpc.Client
{
    public class JasmineRpcClient : IRpcClient
    {
        private IRpcClientConnetionProvider _provider;
        private IRpcRequestFactory _factory;
       


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

            connection.Call(request);

            while (true)// block until response received or timeout or connection closed
            {
                switch (connection.GetStuta(request.Id))
                {
                    case RpcCallStutas.Scheduled:
                        break;
                    case RpcCallStutas.Sended:
                        break;
                    case RpcCallStutas.WaitForResponse:
                        break;
                    case RpcCallStutas.Timeout:

                        throw new RpcCallTimeoutException();

                    case RpcCallStutas.Finished:

                        return connection.GetResponse(request.Id);

                    case RpcCallStutas.ConnectionClosed:

                        throw new ConnectionClosedException();
                }
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
            var response = Call(request);

            switch (response.ErrorCode)
            {
                case RpcResponseErrorCode.SUCESSED:
                   // return (T)response.Result;
                default:
                    break;
            }

            return default(T);

        }
    }
}
