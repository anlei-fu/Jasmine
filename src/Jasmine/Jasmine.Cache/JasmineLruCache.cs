using Jasmine.Scheduling;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Cache
{
    /// <summary>
    /// lru-k cache
    /// it cache latest recent used item wthin in capacity
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="Tvalue"></typeparam>
    public class JasmineLruCache<Tkey, Tvalue> : ICache<Tkey, Tvalue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="threshlod"> over it in time interval will be pushed into cache  </param>
        /// <param name="capacity"></param>
        /// <param name="loader">laod value by key <see cref="ILoader{Tkey, TValue}"/></param>
        /// <param name="checkInterval"><see cref="LruTimeoutJob"/>  will clear precache every interval</param>
        public JasmineLruCache(byte threshlod, int capacity, ILoader<Tkey, Tvalue> loader, int checkInterval)
        {
            Capacity = capacity;
            _threshold = threshlod;

            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
            _scheduler = new JasmineTimeoutScheduler(new TimeoutJobManager(2), 1);
            _scheduler.Start();

            var job = new LruTimeoutJob(_precache, checkInterval);
            _scheduler.Schedule(job);
        }

        private readonly ConcurrentDictionary<Tkey, Tvalue> _innerCache = new ConcurrentDictionary<Tkey, Tvalue>();
        private readonly ConcurrentDictionary<Tkey, byte> _precache = new ConcurrentDictionary<Tkey, byte>();
        private readonly ConcurrentDictionary<Tkey, Node> _orderQueue = new ConcurrentDictionary<Tkey, Node>();
        private readonly ILoader<Tkey, Tvalue> _loader;
        private byte _threshold;
        private readonly object _locker = new object();
        private readonly Node _head = new Node();
        private readonly Node _tail = new Node();
        private readonly JasmineTimeoutScheduler _scheduler;
        private bool _started;
        public int Capacity { get; }
        public int Count => _innerCache.Count;

        public void Start()
        {
            lock(_locker)
            {
                if(!_started)
                {
                    _started = true;

                    _scheduler.Start();
                }
            }
        }

        public void Stop()
        {
            lock (_locker)
            {
                if (_started)
                {
                    _started = false;

                    _scheduler.Stop(false);
                }
            }
        }
        public bool ConatinsKey(Tkey key)
        {
            return _innerCache.ContainsKey(key);
        }

        public void Delete(Tkey key)
        {
            if (_innerCache.TryRemove(key, out var _))
            {
                var node = _orderQueue[key];
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
                node.Next = node.Previous = null;
            }
        }

        public Tvalue GetValue(Tkey key)
        {
            // move to first
            if (_innerCache.TryGetValue(key, out var value))
            {
                lock (_locker)
                {

                    var node = _orderQueue[key];

                    node.Previous.Next = node.Next;
                    node.Next.Previous = node.Previous;

                    var temp = _head.Next;

                    _head.Next = node;
                    node.Next = temp;
                    temp.Previous = node;

                }

                return value;
            }
            else
            {
                // try load value
                var result = _loader.Load(key);

                lock (_locker)
                {
                    // load failed
                    if (result.Equals(default(Tvalue)))
                    {
                        return result;
                    }

                    _precache.TryAdd(key, 0);

                    _precache[key]++;

                    // cache if over k
                    if (_precache[key] > _threshold)
                    {
                        var node = new Node
                        {
                            Key = key
                        };

                        _orderQueue.TryAdd(key, node);

                        // over capacity  remove last
                        if (_orderQueue.Count > Capacity)
                        {
                            var last = _tail.Previous;

                            last.Previous.Next = _tail;
                            _tail.Previous = last;

                            last.Previous = last.Next = null;

                            var temp = _head.Next;

                            temp.Previous = node;
                            node.Next = temp;
                            node.Previous = _head;
                            _head.Next = node;

                            _orderQueue.TryRemove(last.Key, out var _);
                        }
                    }

                }

                return result;

            }
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            foreach (var item in _innerCache)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerCache.GetEnumerator();
        }

        private class LruTimeoutJob : TimeoutJob
        {
            public LruTimeoutJob(ConcurrentDictionary<Tkey, byte> precache, int interval) : base(DateTime.Now.AddMilliseconds(interval))
            {
                _precache = precache;
                _checkInterval = interval;
            }
            private int _checkInterval;

            private ConcurrentDictionary<Tkey, byte> _precache;

            /// <summary>
            /// need to think about  how  kill this checking thread;
            /// </summary>
            public override void Excute()
            {
                _precache.Clear();

                var job = new LruTimeoutJob(_precache, _checkInterval);

                Scheduler.Schedule(job);
            }
        }
        private class Node
        {
            public Tkey Key { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }
        }
    }
}
