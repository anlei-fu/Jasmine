using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StackExchange.Redis.Wrapper
{
    /// <summary>
    /// <see cref="SortedSet{T}"/>
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public class RedisSortedSet<TElement> : RedisComponent, IReadOnlyCollection<TElement>
    {
        internal RedisSortedSet(RedisComponetsProvider provider, string name) : base(provider, name)
        {
        }

        public TElement Min { get; set; }
        public TElement Max { get; set; }

        public int Count => throw new System.NotImplementedException();


        public IEnumerable<TElement> GetViewBetween()
        {
            return Enumerable.Empty<TElement>();
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
