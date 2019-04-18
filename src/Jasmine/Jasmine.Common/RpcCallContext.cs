using System.Threading;

namespace Jasmine.Common
{
    public  class RpcCallContext<T>
    {
        private readonly object _lockObject = new object();
        private RpcCallStutas _stutas=RpcCallStutas.Scheduled;
        public RpcCallStutas Stutas
        {
            get => _stutas;
            set
            {
                lock (_lockObject)
                    _stutas = value;
            }
        }
        public SpinLock Lock { get; set; }
        public T Response { get; set; }
    }
}
