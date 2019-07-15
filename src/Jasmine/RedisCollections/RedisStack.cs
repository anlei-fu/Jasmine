using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchange.Redis.Wrapper
{
    public class RedisStack<TElement> :RedisComponent, IReadOnlyCollection<TElement>
    {
        internal RedisStack(RedisComponetsProvider provider, string name) : base(provider, name)
        {
        }

        public int Count => throw new NotImplementedException();

        public IEnumerator<TElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {

        }
        public Task ClearAsync()
        {
            return null;
        }
        public TElement Peek()
        {
            throw new NotImplementedException();
        }

        public Task<TElement> PeekAsync()
        {
            throw new NotImplementedException();
        }
        public TElement Pop()
        {
            throw new NotImplementedException();
        }

        public bool TryPop(out TElement result)
        {
            result = default;

            return true;
        }

        public IEnumerable<TElement> Pop(int count)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<TElement>> PopAsync(int count)
        {
            throw new NotImplementedException();
        }
        public Task<TElement> PopAsync()
        {
            throw new NotImplementedException();
        }
        public void Push(TElement item)
        {
            throw new NotImplementedException();
        }
        public Task PushAsync(TElement item)
        {
            throw new NotImplementedException();
        }



    }
}
