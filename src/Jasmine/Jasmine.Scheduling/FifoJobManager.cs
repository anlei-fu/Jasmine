using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Scheduling
{
    public class FifoJobManager<T> : IJobManager<T>
        where T : Job
    {
        public int Capacity => throw new System.NotImplementedException();

        public int Count => throw new System.NotImplementedException();

        public bool Cancel(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public T GetJob()
        {
            throw new System.NotImplementedException();
        }

        public int GetNextJobExcutingTimeout()
        {
            throw new System.NotImplementedException();
        }

        public bool Schedule(T job)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
