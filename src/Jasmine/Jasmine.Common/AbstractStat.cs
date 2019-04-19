using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Jasmine.Common
{
    public class AbstractStat<TItem> : IStat<TItem>
         where TItem : IStatItem
    {
        
        private int _total;
        private int _successed;
        private int _failed;

        private readonly object _lockObject = new object();
        private List<TItem> _items = new List<TItem>();
        public int Avarage { get; private set; }

        public int Total => _total;

        public int Success => _successed;
        public int Failed => _failed;

        public int Fatest { get; private set; }

        public int Slowest { get; private set; }

        public float FaileRate => _failed / (float)_total;
        public float SuccesRate => _successed / (float)_total;
        public int Count => _total;

        public DateTime LastCaculateTime { get; private set; }

        public void Add(TItem item)
        {
            Interlocked.Increment(ref _total);

            if (item.Sucessed)
                Interlocked.Increment(ref _successed);
            else
                Interlocked.Increment(ref _failed);

            lock(_lockObject)
            {
                if (item.Time > Slowest)
                    Slowest = item.Time;

                if (item.Time < Fatest)
                    Fatest = item.Time;
            }

            _items.Add(item);


        }

        public void Caculate()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            lock(_lockObject)
            {
                foreach (var item in _items)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_lockObject)
                return _items.GetEnumerator();
        }

        public void Clear()
        {
            lock(_lockObject)
            {
                Fatest = Slowest = Avarage =_total =_successed=_failed= 0;
                _items.Clear();
            }
        }
    }
}
