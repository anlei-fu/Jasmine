namespace Jasmine.Rpc.Client
{
    public  class ResponseWaitSource
    {
        private readonly object _lockObject = new object();
        private RpcCallStutas _stuta=RpcCallStutas.Scheduled;
        public RpcCallStutas Stuta
        {
            get => _stuta;
            set
            {
                lock (_lockObject)
                    _stuta = value;
            }
        }
        public IRpcResponse Response { get; set; }
    }
}
