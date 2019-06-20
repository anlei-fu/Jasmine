using System;
using System.Collections.Generic;
using System.Threading;

namespace Jasmine.Common
{
    public class Metric : IMetric
    {
        
        private int _total;
        private int _successed;
        private int _failed;

        private readonly object _lockObject = new object();
        public List<IStatItem> Items = new List<IStatItem>();
        public int Avarage { get; private set; }

        public int Total => _total;

        public int Success => _successed;
        public int Failed => _failed;

        public int Fastest { get; private set; }

        public int Slowest { get; private set; }

        public float FaileRate => _failed / (float)_total;
        public float SuccesRate => _successed / (float)_total;
        public int Count => _total;

        public string LastCaculateTime { get; private set; }
        public string StartTime { get; } = DateTime.Now.ToString();

        public int Median { get; set; }

        public void Add(IStatItem item)
        {
            Interlocked.Increment(ref _total);

            if (item.Sucessed)
                Interlocked.Increment(ref _successed);
            else
                Interlocked.Increment(ref _failed);

            lock(_lockObject)
            {
                if (item.Elapsed > Slowest)
                    Slowest = (int)item.Elapsed;

                if (item.Elapsed < Fastest)
                    Fastest = (int)item.Elapsed;
                
            }

            Items.Add(item);

            if (Items.Count > 100)
            {
                Caculate();

                LastCaculateTime = DateTime.Now.ToString();
            }

        }

        public void Caculate()
        {
            lock(_lockObject)
            {
                Items.Clear();

                LastCaculateTime = DateTime.Now.ToString();
            }
        }

        public IEnumerator<IStatItem> GetEnumerator()
        {
            lock(_lockObject)
            {
                foreach (var item in Items)
                {
                    yield return item;
                }
            }
        }

       
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    lock (_lockObject)
        //        return _items.GetEnumerator();
        //}

        public void Clear()
        {
            lock(_lockObject)
            {
                Fastest = Slowest = Avarage =_total =_successed=_failed= 0;
                Items.Clear();
            }
        }
    }
}
