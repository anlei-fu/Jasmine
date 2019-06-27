using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Jasmine.Common
{
    public class Metric : IMetric
    {

        private int _total;
        private int _successed;
        private int _failed;

        private readonly object _lockObject = new object();
        public ConcurrentQueue<IStatItem> CurrentStat = new ConcurrentQueue<IStatItem>();

        public ConcurrentQueue<StatGroup> History { get; set; } = new ConcurrentQueue<StatGroup>();
        public int Avarage { get; private set; }

        public long TotalTime { get; private set; }


        public int Total => _total;

        public int Success => _successed;
        public int Failed => _failed;

        public int Fastest { get; private set; }

        public int Slowest { get; private set; }

        public float FaileRate => _total == 0 ? 0 : _failed / (float)_total;
        public float SuccesRate => _total == 0 ? 0 : _successed / (float)_total;


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

            lock (_lockObject)
            {
                TotalTime += item.Elapsed;

                if (item.Elapsed > Slowest)
                    Slowest = (int)item.Elapsed;

                if (item.Elapsed < Fastest)
                    Fastest = (int)item.Elapsed;

                CurrentStat.Enqueue(item);
            }



            if (CurrentStat.Count >= 100)
            {
                Caculate();
            }

        }

        public void Caculate()
        {
            lock (_lockObject)
            {
                var result = CurrentStat.ToList().FindAll(x => x.Sucessed).ToList();

                result.Sort((x, y) => x.Elapsed.CompareTo(y.Elapsed));
                var sum = result.Sum(x => x.Elapsed);

                var group = new StatGroup()
                {
                    Avarage = result.Count != 0 ? (int)sum / result.Count : 0,
                    Median = result.Count == 0 ? 0 : (int)result[result.Count / 2].Elapsed,
                    Failed = CurrentStat.Count - result.Count,
                    Sucessed = result.Count,
                };

                History.Enqueue(group);

                Median = (group.Median + Median) / 2;
                Avarage = (group.Avarage + Avarage) / 2;

                while (CurrentStat.TryDequeue(out var _)) ;

                if (History.Count > 10000)
                    History.TryDequeue(out var _);


                LastCaculateTime = DateTime.Now.ToString();
            }
        }


        public void Clear()
        {
            lock (_lockObject)
            {
                Fastest = Slowest = Avarage = _total = _successed = _failed = 0;
                while (CurrentStat.TryDequeue(out var _)) ;
                while (History.TryDequeue(out var g)) ;
            }
        }


        public class StatGroup
        {
            public string CreateTime { get; set; } = DateTime.Now.ToString("mm-dd HH:MM:ss");
            public int Avarage { get; set; }
            public int Median { get; set; }
            public int Sucessed { get; set; }
            public int Failed { get; set; }
            public int Fatest { get; set; }
        }
    }


}
