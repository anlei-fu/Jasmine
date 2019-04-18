using Jasmine.Common;
using Jasmine.Rpc.Client.Exceptions;
using log4net;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Rpc.Client
{
    public abstract class AbstractRpcClientConnection: IRpcClientConnection
    {
        private ConcurrentDictionary<long, RpcCallContext<IRpcResponse>> _calls = new ConcurrentDictionary<long, RpcCallContext<IRpcResponse>>();
        private ConcurrentQueue<IRpcRequest> _requests = new ConcurrentQueue<IRpcRequest>();
        private bool _sendLoopRunning;
        private bool _receiveLoopRunning;
        private ConnectionState _connectionState;
        private readonly object _lockObject = new object();
        private readonly RpcCallContextPool _pool;
        private ILog _logger;

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

            foreach (var item in _calls.Values)// cancel all sheduled rpc call
            {
                item.Stutas = RpcCallStutas.ConnectionClosed;
                item.Lock.Exit();
            }

            closeInternal();

            _logger.Info($" connection closed! ");

        }
        public RpcCallContext<IRpcResponse> Call(IRpcRequest requetst)
        {
            if (_connectionState != ConnectionState.Connected)
                throw new ConnectionClosedException();

            var context = _pool.Rent();

            if (_calls.TryAdd(requetst.Id, context))//is call already scheduled
            {
                var token = true;
                context.Lock.Enter(ref token);// set blocked
                _requests.Enqueue(requetst);//scheduled to sending queue

                _logger?.Info($"requet({requetst.Id}) scheduled! ");

                if (!_sendLoopRunning)// start or restart send loop  ,after last packet  sending, the send loop will auto close
                {
                    _sendLoopRunning = true;
                    _logger?.Info($"sending loop started!");
                    sendLoop(null);
                }

                return context;

            }
            else
            {
                return null;
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
                    value.Stutas = RpcCallStutas.Finished;
                    value.Response = response;
                    value.Lock.Exit();
                    _calls.TryRemove(response.Id, out var _);
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
                    _calls[request.Id].Stutas = RpcCallStutas.Sended;
                }

                if (_connectionState == ConnectionState.Connected)
                {
                    sendInternal(request, sendLoop);
                }
                else
                {
                    _logger?.Info($" sending loop ended!");
                    _sendLoopRunning = false;
                }
            }
            else
            {
                _logger?.Info($" sending loop ended!");
                _sendLoopRunning = false;
            }
        }
    }
}
