﻿using Jasmine.Scheduling;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Cache
{
    public class JasmineTimeoutCache<Tkey, Tvalue> : ITimeoutCache<Tkey, Tvalue>
    {
        public JasmineTimeoutCache(int capacity, Action<Tkey, Tvalue> itemRemoveCallback)
        {
            Capacity = capacity;
            _itemRemoveCallback = itemRemoveCallback;
            _scheduler =new JasmineTimeoutScheduler(new TimeoutJobManager(capacity));
            _scheduler.Start();
        }

        public int Capacity { get; }

        private ConcurrentDictionary<Tkey, Tvalue> _innerCache = new ConcurrentDictionary<Tkey, Tvalue>();

        private ConcurrentDictionary<Tkey, TimeoutCacheJob> _jobMap = new ConcurrentDictionary<Tkey, TimeoutCacheJob>();

        private JasmineTimeoutScheduler _scheduler;

        private Action<Tkey, Tvalue> _itemRemoveCallback;


        public int Count => _innerCache.Count;

        public bool AddTime(Tkey key,  long timeout)
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

        public bool Conatins(Tkey key)
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
            throw new System.NotImplementedException();
        }

        public Tvalue GetValue(Tkey key)
        {
            return _innerCache.TryGetValue(key ,out var value) ? value : default(Tvalue);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
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
                _cache._jobMap.TryRemove(_key, out var jobId);

                if(_cache._innerCache.TryRemove(_key,out var value))
                {

                    _cache._itemRemoveCallback?.Invoke(_key, value);
                }

            }
        }
    }
}
