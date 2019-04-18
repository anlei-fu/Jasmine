using Jasmine.Rpc.Client.Exceptions;
using log4net;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Rpc.Client
{
    public abstract class AbstractRpcClientConnection: IRpcClientConnection
    {
        private ConcurrentDictionary<long, ResponseWaitSource> _calls = new ConcurrentDictionary<long, ResponseWaitSource>();
        private ConcurrentQueue<IRpcRequest> _requests = new ConcurrentQueue<IRpcRequest>();
        private bool _sendLoopRunning;
        private bool _receiveLoopRunning;
        private ConnectionState _connectionState;
        private readonly object _lockObject = new object();
        private ILog _logger;
        public void Call(IRpcRequest requetst)
        {
            if (_connectionState != ConnectionState.Connected)
                throw new ConnectionClosedException();

            if (_calls.TryAdd(requetst.Id, new ResponseWaitSource()))
            {
                _requests.Enqueue(requetst);
                _logger?.Info($"requet({requetst.Id}) scheduled! ");

                if (!_sendLoopRunning)
                {
                    _sendLoopRunning = true;
                    _logger?.Info($"sending loop started!");
                    sendLoop(null);
                }
            }
        }

        public bool Open(string user, string password)
        {
            if (openInternal(user, password))
            {
                _connectionState = ConnectionState.Connected;
                _receiveLoopRunning = true;
                receiveLoop(null, null);
                _logger?.Info($"connected ,receiving loop started!");
                return true;
            }

            return false;
        }
        public void Close()
        {
            lock (_lockObject)
                _connectionState = ConnectionState.Closing;

            while (_receiveLoopRunning || _sendLoopRunning) ;

            _connectionState = ConnectionState.Closed;

            closeInternal();

            _logger.Info($" connection closed! ");

        }

        public IRpcResponse GetResponse(long id)
        {
            if (_calls.TryGetValue(id, out var value))
            {
                throw new RpcCallNotExistsException();
            }
            else
            {
                if (_calls[id].Response == null)
                {
                    throw new ResponseHasNotBeenSetException();
                }

                var response = _calls[id].Response;
                _calls.TryRemove(id, out var _);

                return response;
            }
        }

        public RpcCallStutas GetStuta(long id)
        {
            if (_calls.TryGetValue(id, out var value))
            {
                throw new RpcCallNotExistsException();
            }
            else
            {
                return _calls[id].Stuta;
            }
        }
        protected abstract bool openInternal(string user, string password);
        protected abstract void sendInternal(IRpcRequest request, Action<Exception> writeFinished);
        protected abstract void receiveInternal(Action<Exception, IRpcResponse> processor);
        protected abstract void closeInternal();

      
      
        private void receiveLoop(Exception ex, IRpcResponse response)
        {
            if (ex != null)
            {
                if (_connectionState != ConnectionState.Closed)
                {
                    _logger?.Error($"connection closed start closing ", ex);
                    Close();
                }
            }

            if (response != null)
            {
                if (_calls.TryGetValue(response.Id, out var value))
                {
                    _calls[response.Id].Stuta = RpcCallStutas.Finished;
                    _calls[response.Id].Response = response;
                }

                _logger.Info($" request({response.Id}) finished!");
            }

            if (_connectionState == ConnectionState.Connected)
            {
                receiveInternal(receiveLoop);
            }
            else
            {
                _receiveLoopRunning = false;

                _logger?.Info($" receiving loop ended!");
            }
        }

        private void sendLoop(Exception ex)
        {
            if (ex != null)
            {
                if (_connectionState != ConnectionState.Closed)
                {
                    _logger?.Error($"connection closed start closing ", ex);
                    Close();
                }
            }

            if (_requests.TryDequeue(out var request))
            {
                if (_calls.TryGetValue(request.Id, out var value))
                {
                    _calls[request.Id].Stuta = RpcCallStutas.Sended;
                }

                if (_connectionState == ConnectionState.Connected)
                {
                    sendInternal(request, sendLoop);
                }
                else
                {
                    _logger?.Info($" sending loop ended!");
                    _receiveLoopRunning = false;
                }
            }
            else
            {
                _logger?.Info($" sending loop ended!");
                _receiveLoopRunning = false;
            }
        }
    }
}
