using Jasmine.Scheduling;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Cache
{
    public class JasmineTimeoutCache<Tkey, Tvalue> : ITimeoutCache<Tkey, Tvalue>
    {
        public JasmineTimeoutCache(int capacity, Action<Tkey, Tvalue> itemRemovedCallback)
        {
            Capacity = capacity;
            _itemRemovedCallback = itemRemovedCallback;
            _scheduler =new JasmineTimeoutScheduler(new TimeoutJobManager(capacity));
            _scheduler.Start();
        }

        private ConcurrentDictionary<Tkey, Tvalue> _innerCache = new ConcurrentDictionary<Tkey, Tvalue>();

        private ConcurrentDictionary<Tkey, TimeoutCacheJob> _jobMap = new ConcurrentDictionary<Tkey, TimeoutCacheJob>();

        private JasmineTimeoutScheduler _scheduler;

        private Action<Tkey, Tvalue> _itemRemovedCallback;

        public int Capacity { get; }


        public int Count => _innerCache.Count;

        public bool AdjustTimeout(Tkey key,  long timeout)
        {
            if(_innerCache.ContainsKey(key))
            {
                _jobMap[key].AdjustTimeout(timeout);

                return true;
            }

            return false;
        }

        public bool Cache(Tkey key, Tvalue value, long timeout)
        {
            if (_innerCache.TryAdd(key,value))
            {
                var job = new TimeoutCacheJob(key, this, DateTime.Now.AddMilliseconds(timeout));

                _jobMap.TryAdd(key, job);

                _scheduler.Schedule(job);

                return true;
            }

            return false;
        }

        public bool ConatinsKey(Tkey key)
        {
            return _innerCache.ContainsKey(key);
        }

        public void Delete(Tkey key)
        {
           if(_innerCache.TryRemove(key,out var value))
            {
                if (_jobMap.TryRemove(key, out var job))
                    job.Cancel();
            }
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
            return _innerCache.TryGetValue(key ,out var value) ? value : default(Tvalue);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerCache.GetEnumerator();
        }


        private class TimeoutCacheJob : TimeoutJob
        {
            public TimeoutCacheJob(Tkey key,JasmineTimeoutCache<Tkey,Tvalue> cache,DateTime excuteTime) : base(excuteTime)
            {
                _cache = cache;
                _key = key;
            }
            private Tkey _key;
            private JasmineTimeoutCache<Tkey, Tvalue> _cache;

            public override void Excute()
            {
                _cache._jobMap.TryRemove(_key, out var _);

                if(_cache._innerCache.TryRemove(_key,out var value))
                {
                    _cache._itemRemovedCallback?.Invoke(_key, value);
                }

            }
        }
    }
}
