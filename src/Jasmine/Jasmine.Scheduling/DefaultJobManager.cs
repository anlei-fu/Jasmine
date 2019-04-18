using System;
using System.Collections.Concurrent;

namespace Jasmine.Scheduling
{
    public class DefaultJobManager 
    {
        private JobList _list;
        private ConcurrentDictionary<long, JobNode> _map = new ConcurrentDictionary<long, JobNode>();

      
        public bool AdjustTimeOut(long id, long timeout)
        {
            throw new NotImplementedException();
        }

        public bool Cancel(long id)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
           
        }

        public ITimeoutJob GetJob()
        {
            throw new NotImplementedException();
        }

        public void Initia()
        {
            
        }

        public long Schecule(ITimeoutJob job)
        {
            throw new NotImplementedException();
        }


    }
}
