using System.Collections.Concurrent;

namespace Jasmine.Common
{
    public abstract class AbstractSimpleQueuedPool<T> : IPool<T>
    {
        public AbstractSimpleQueuedPool(int capacity)
        {
            _capacity = capacity;
        }
        private int _capacity;
        private ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        public void Recycle( T item)
        {
            if (_queue.Count < _capacity)
                _queue.Enqueue(item);
        }

        public T Rent()
        {
            return _queue.TryDequeue(out var result) ?
                                             result : createNew();
        }

        protected abstract T createNew();
    }
}
