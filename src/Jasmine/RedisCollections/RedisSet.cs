using System;
using System.Collections;
using System.Collections.Generic;

namespace StackExchange.Redis.Wrapper
{
    public class RedisSet<TElement> : RedisComponent, IReadOnlyCollection<TElement>
    {
        public RedisSet(RedisComponetsProvider provider, string name) : base(provider, name)
        {
        }

        public int Count => throw new NotImplementedException();

        public void Add(TElement element)
        {
           
        }

        public bool TryAdd(TElement element)
        {
            return true;
        }

        public bool Conatains(TElement element)
        {
            return true;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
