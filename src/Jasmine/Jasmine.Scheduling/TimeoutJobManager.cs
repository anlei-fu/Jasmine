using Jasmine.DataStructure;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Scheduling
{
    public class TimeoutJobManager : ITimeoutJobManager
    {
        public TimeoutJobManager(int capacity)
        {
            Capacity = capacity;
        }
        private const int DEFAULT_WAKEUP_TIMEOUT = 100*1000;//100s

        private ConcurrentDictionary<long, AvlTree<TimeoutJob>.Node> _jobs = new ConcurrentDictionary<long, AvlTree<TimeoutJob>.Node>();
        private AvlTree<TimeoutJob> _tree = new AvlTree<TimeoutJob>(new TimeoutJobCompare());
        private readonly object _locker = new object();
        public int Capacity { get; }
        public int Count =>_jobs.Count;

       /// <summary>
       /// get a new job , if sucess
       /// return a job which's  scheduling time is earlier than now
       /// </summary>
       /// <returns></returns>
        public TimeoutJob GetJob()
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
                _tree.Clear();
                _jobs.Clear();
            }
        }

        public bool Schedule(TimeoutJob job)
        {
           if(Count>Capacity||_jobs.ContainsKey(job.Id))
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

        /// <summary>
        /// remove from the tree,add time ,then re-add the value
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="millionSeconds"></param>
        /// <returns></returns>
        public bool AdjustTimeout(long jobId,long millionSeconds)
        {
            lock (_locker)
            {
                if (_jobs.TryGetValue(jobId, out var value))
                {
                    value.Data.ScheduledExcutingTime.AddMilliseconds(millionSeconds);
                    _tree.Remove(value);
                    _jobs[jobId] = _tree.Add(value.Data);

                    return true;
                }
            }

            return false;
        }

        public bool Cancel(long jobId)
        {
            lock (_locker)
            {
                if (_jobs.TryRemove(jobId, out var value))
                {
                    _tree.Remove(value);
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<TimeoutJob> GetEnumerator()
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

        public int GetNextJobExcutingTimeout()
        {
            lock(_locker)
            {
                var min = _tree.MinNode;

                if (min == null)
                    return DEFAULT_WAKEUP_TIMEOUT;

                var tiemout =min.Data.ScheduledExcutingTime<DateTime.Now?0: (min.Data.ScheduledExcutingTime - DateTime.Now).TotalMilliseconds;

                return tiemout>DEFAULT_WAKEUP_TIMEOUT?DEFAULT_WAKEUP_TIMEOUT:(int)tiemout;
            }
        }

        private class TimeoutJobCompare : Comparer<TimeoutJob>
        {
            public override int Compare(TimeoutJob x, TimeoutJob y)
            {
                return x.ScheduledExcutingTime.CompareTo(y.ScheduledExcutingTime);
            }
        }
    }
}
