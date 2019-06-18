using System.Collections.Concurrent;
using System.Threading;

namespace Jasmine.Common
{
    public abstract class AbstractMaxConcurrencyPool<T> : IPool<T>
    {
        public AbstractMaxConcurrencyPool(int maxConcurrency)
        {
            _maxConcurrency = maxConcurrency;
        }
        private int _currentCreated;
        private int _maxConcurrency;
        private bool _isSleeping = false;

        private readonly object _locker = new object();

        private ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        public void Recycle(T item)
        {
            _queue.Enqueue(item);

            lock(_locker)
            {
                if(_isSleeping)
                {
                    Monitor.PulseAll(_locker);

                    _isSleeping = false;
                }
            }
        }

        public T Rent()
        {
          if(_queue.TryDequeue(out var result))
            {
                return result;
            }
          else
            {
                if(_maxConcurrency>_currentCreated)
                {
                    Interlocked.Increment(ref _currentCreated);

                    return newInstance();
                }
                else
                {
                    _isSleeping = true;

                    Monitor.Wait(_locker, int.MaxValue);

                    //maybe stack overflow ,but posibility is very small
                    return Rent();

                }
            }
        }

        protected abstract T newInstance();
            
    }
}
