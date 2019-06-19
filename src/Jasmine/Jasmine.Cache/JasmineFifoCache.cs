using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Cache
{
    public class JasmineFifoCache<Tkey, Tvalue> : ICache<Tkey, Tvalue>
    {
        public JasmineFifoCache(int capacity)
        {
            Capacity = capacity;
        }
        public ConcurrentDictionary<Tkey, Tvalue> _innerCache = new ConcurrentDictionary<Tkey, Tvalue>();

        public ConcurrentQueue<Tkey> _queue = new ConcurrentQueue<Tkey>();

        private readonly object _locker = new object();

        public int Capacity { get; }
        public int Count => _innerCache.Count;

        public IList<Tkey> Keys => _innerCache.Keys.ToList();

        public IList<Tvalue> Values => _innerCache.Values.ToList();

        public bool ConatinsKey(Tkey key)
        {
            return _innerCache.ContainsKey(key);
        }

        public void Delete(Tkey key)
        {
            _innerCache.TryRemove(key, out var _);
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            foreach (var item in _innerCache)
            {
                yield return item;
            }
        }

        public Tvalue GetValue(Tkey key)
        {
            return _innerCache.TryGetValue(key, out var result) ? result : default(Tvalue);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerCache.GetEnumerator();
        }

        public void Cache(Tkey key,Tvalue value)
        {
           lock(_locker)
            {
                if(_innerCache.ContainsKey(key))
                {
                    //update should lock?
                    _innerCache[key] = value;
                }
                else
                {
                    _innerCache.TryAdd(key, value);

                    if(Count>Capacity&&_queue.TryDequeue(out var oldKey))
                    {
                        _innerCache.TryRemove(oldKey, out var _);
                    }
                }
            }
        }
    }
}
