using Jasmine.Scheduling;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Cache
{
    public class LruFileCache :IReadOnlyCollection<KeyValuePair<string, byte[]>>
    {

        public LruFileCache(byte threshold, long maxMemoryUsage, int checkInterval, IFileLoader loader)
        {
            MaxMemoryUsage = maxMemoryUsage;
            _threshold = threshold;

            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
            _scheduler = new JasmineTimeoutScheduler(new TimeoutJobManager(2), 1);
            _scheduler.Start();

            var job = new LruTimeoutJob(_precache, checkInterval);
            _scheduler.Schedule(job);
        }
        private readonly ConcurrentDictionary<string, byte[]> _innerCache = new ConcurrentDictionary<string, byte[]>();
        private readonly ConcurrentDictionary<string, byte> _precache = new ConcurrentDictionary<string, byte>();
        private readonly ConcurrentDictionary<string, Node> _orderQueue = new ConcurrentDictionary<string, Node>();

        private readonly IFileLoader _loader;
        private byte _threshold;
        private readonly object _locker = new object();
        private readonly Node _head = new Node();
        private readonly Node _tail = new Node();
        private readonly JasmineTimeoutScheduler _scheduler;
        private bool _started;
        public long MaxMemoryUsage { get; }
        public long CurrentUsage { get; private set; }
        public int Count => _innerCache.Count;
        public void Start()
        {
            lock (_locker)
            {
                if (!_started)
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


        public bool ConatinsKey(string key)
        {
            return _innerCache.ContainsKey(key);
        }

        public void Delete(string key)
        {
            if (_innerCache.TryRemove(key, out var _))
            {
                var node = _orderQueue[key];
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
                node.Next = node.Previous = null;
            }
        }

        public IEnumerator<KeyValuePair<string, byte[]>> GetEnumerator()
        {
            foreach (var item in _innerCache)
            {
                yield return item;
            }
        }

        public async Task<byte[]> GetFileAsync(string key)
        {
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
                var result =  await _loader.LoadAsync(key);

                // load failed
                if (result == null)
                {
                    return null;
                }

                lock (_locker)
                {
                    _precache.TryAdd(key, 0);

                    _precache[key]++;

                    // cache if over k
                    if (_precache[key] > _threshold)
                    {
                        var node = new Node
                        {
                            Key = key
                        };

                        CurrentUsage += value.Length;

                        _orderQueue.TryAdd(key, node);

                        _innerCache.TryAdd(key, value);

                        var temp = _head.Next;

                        temp.Previous = node;
                        node.Next = temp;
                        node.Previous = _head;
                        _head.Next = node;

                        // over capacity  remove last
                        while (CurrentUsage > MaxMemoryUsage)
                        {
                            var last = _tail.Previous;

                            last.Previous.Next = _tail;
                            _tail.Previous = last;

                            last.Previous = last.Next = null;

                            _innerCache.TryRemove(last.Key, out var discard);
                            _orderQueue.TryRemove(last.Key, out var _);
                        }
                    }

                }

                return result;

            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerCache.GetEnumerator();
        }
        private class LruTimeoutJob : TimeoutJob
        {
            public LruTimeoutJob(ConcurrentDictionary<string, byte> precache, int interval) : base(DateTime.Now.AddMilliseconds(interval))
            {
                _precache = precache;
                _checkInterval = interval;
            }
            private int _checkInterval;

            private ConcurrentDictionary<string, byte> _precache;

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
            public string Key { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }
        }
    }
}
