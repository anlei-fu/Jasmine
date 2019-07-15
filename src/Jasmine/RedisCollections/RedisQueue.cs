using StackExchange.Redis;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchange.Redis.Wrapper
{
    public class RedisQueue<TElement> : RedisComponent,IReadOnlyCollection<TElement>
    {
        public int Count => throw new System.NotImplementedException();

        internal RedisQueue(RedisComponetsProvider provider, string name) : base(provider, name)
        {
        }

        public void Enqueue(TElement element)
        {

        }
        public void Enqueue(IEnumerable<TElement> element)
        {

        }
        public Task EnqueueAsync(TElement element)
        {
            return null;
        }
        public Task EnqueueAsync(IEnumerable<TElement> element)
        {
            return null;
        }

        public TElement Dequeue()
        {
            return default(TElement);
        }

        public IEnumerable<TElement> Dequeue(int count)
        {
            return null;
        }
        public Task<TElement> DequeueAsync()
        {
            return null;
        }


        public Task<IEnumerable<TElement>> DequeueAsync(int count)
        {
            return null;
        }
     
        public void Clear()
        {

        }
        public Task ClearAsync()
        {
            return null;
        }

        public Queue<TElement> Copy()
        {
            return null;
        }

        public Queue<TElement> CopyAsync()
        {
            return null;
        }

        public TElement Peek()
        {
            return default(TElement);
        }
        public Task<TElement> PeekAsync()
        {
            return null;
        }

        public bool TryDequeue(out TElement elemet)
        {
            elemet = default;

            return true;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
