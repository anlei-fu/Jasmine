using Jasmine.Cache;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Scheduling
{
    public class RoundRobinManager : IRoundRobinJobManager
    {
        private const int DEFAULT_WAKE_UP_TIMEOUT = 100 * 1000;
        private readonly object _locker = new object();
        private CycleTree<RoundRobinJob> _tree = new CycleTree<RoundRobinJob>();
        private Dictionary<long, CycleTree<RoundRobinJob>.Node> _jobs = new Dictionary<long, CycleTree<RoundRobinJob>.Node>(); 

        public int Capacity { get; }

        public int Count => _jobs.Count;

        public bool Cancel(long id)
        {
            lock(_locker)
            {
                if (_jobs.ContainsKey(id))
                {
                    _tree.Remove(_jobs[id]);
                    _jobs.Remove(id);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Clear()
        {
          lock(_locker)
            {
                _jobs.Clear();
                _tree.Clear();

            }
        }

        public IEnumerator<RoundRobinJob> GetEnumerator()
        {
            lock (_locker)
            {
                foreach (var item in _tree)
                {
                    yield return item;
                }
            }
        }

        public RoundRobinJob GetJob()
        {
           lock(_locker)
            {
               return  _tree.Next()?.Data;
            }
        }

   

        public bool Schedule(RoundRobinJob job)
        {
            lock(_locker)
            {
                if (_jobs.ContainsKey(job.Id))
                    return false;

                var node = new CycleTree<RoundRobinJob>.Node(job);

                _jobs.Add(job.Id, node);
                _tree.Add(node);

                return true;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

       public int GetNextJobExcutingTimeout()
        {
            return DEFAULT_WAKE_UP_TIMEOUT;
        }
    }
}
