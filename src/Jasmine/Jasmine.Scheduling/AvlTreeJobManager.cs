using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Scheduling
{
    public class TimeoutJobManager : ITimeJobManager
    {
        public TimeoutJobManager(int capacity)
        {
            _capacity = capacity;
        }
        
        private ConcurrentDictionary<long, AvlTree<ITimeoutJob>.Node> _jobs = new ConcurrentDictionary<long, AvlTree<ITimeoutJob>.Node>();
        private AvlTree<ITimeoutJob> _tree = new AvlTree<ITimeoutJob>(new TimerJobCompare());
        private readonly object _locker = new object();
        private int _capacity;
        public int Count =>_jobs.Count;

        private class TimerJobCompare : Comparer<ITimeoutJob>
        {
            public override int Compare(ITimeoutJob x, ITimeoutJob y)
            {
                return x.ScheduledExcutingTime.CompareTo(y.ScheduledExcutingTime);
            }
        }
        public ITimeoutJob GetJob()
        {
            lock(_locker)
            {
                var node = _tree.MinNode;

                if(node!=null&&node.Data.ScheduledExcutingTime<DateTime.Now)
                {
                    _tree.Remove(node);
                    _jobs.TryRemove(node.Data.Id, out var _);
                    return node.Data;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Clear()
        {
           lock(_locker)
            {
                _tree = new AvlTree<ITimeoutJob>();
                _jobs.Clear();
            }
        }

        public bool Schecule(ITimeoutJob job)
        {
           if(Count>_capacity||_jobs.ContainsKey(job.Id))
            {
                return false;
            }

           lock(_locker)
            {
                var node = _tree.Add(job);

                _jobs.TryAdd(job.Id, node);
                return true;
            }


        }

        public bool AdjustTimeout(long jobId,long millionSeconds)
        {
            if(_jobs.TryGetValue(jobId,out var value))
            {
                value.Data.ScheduledExcutingTime.AddMilliseconds(millionSeconds);
                _tree.Remove(value);
                _jobs[jobId] = _tree.Add(value.Data);

                return true;
            }

            return false;
        }

        public bool Cancel(long jobId)
        {
            if (_jobs.TryRemove(jobId, out var value))
            {
                _tree.Remove(value);
                return true;
            }

            return false;
        }

        public IEnumerator<ITimeoutJob> GetEnumerator()
        {
            foreach (var item in _jobs.Values)
            {
                yield return item.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _jobs.Values.GetEnumerator();
        }
    }
}
