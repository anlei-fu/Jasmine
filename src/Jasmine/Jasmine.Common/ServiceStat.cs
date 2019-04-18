using System.Collections.Generic;
using System.Threading;

namespace Jasmine.Surpport
{
    public  class ServiceStat
    {
        public ServiceStat(int max=10000)
        {
            _maxCount = max;
        }
        private List<ServiceStatItem> _items = new List<ServiceStatItem>();
        private int _maxCount;
        private readonly object _lockObj = new object();
        private int _totalCount;
        private long _totalTime;
        /// <summary>
        ///all spend time by visiting service / visting count
        /// </summary>
        public int AvarageTime
        {
            get
            {
                var t = 0;

                lock(_lockObj)
                {
                    foreach (var item in _items)
                    {
                        t += item.TimeSpend;
                    }

                    return t / _items.Count;
                }

            }
        }
        public int TotalCount
        {
            get
            {
                lock (_lockObj)
                    return _totalCount;
            }
        }

        public long TotalTime
        {
            get
            {
                lock (_lockObj)
                    return _totalTime;
            }
        }



        public int SuccessedCount
        {
            get
            {
                var t = 0;

                lock(_lockObj)
                {
                    foreach (var item in _items)
                    {
                        if (item.Successed)
                            ++t;
                    }

                    return t;
                }
            }
        }
        public int FailedCount
        {
            get
            {
                var s = SuccessedCount;

               lock(_lockObj)
                {
                   return _items.Count - s;
                }
            }
        }
        public int MiddleTime
        {
            get
            {
                var ls = new List<int>();

                lock(_lockObj)
                {
                    foreach (var item in _items)
                        ls.Add(item.TimeSpend);

                    ls.Sort();

                    return ls[ls.Count / 2];
                }

            }
        }
      
        public void Clear()
        {
            lock (_lockObj)
                _items.Clear();
        }
        public void Add(ServiceStatItem item)
        {

            Interlocked.Increment(ref _totalCount);

            lock (_lockObj)
            {
                if (_items.Count > _maxCount)
                    _items.RemoveAt(0);

                _totalTime += item.TimeSpend;

                _items.Add(item);
            }
                
        }
    }
}
