using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Jasmine.Scheduling
{
    public class DefaultScheduler 
    {
        public virtual long Schedule(IJob job)
        {
            throw new NotImplementedException();
        }
        private int _sleepInterval;
        protected ConcurrentQueue<IJob> _queue = new ConcurrentQueue<IJob>();
   
        protected virtual IJob getJob()
        {
            return _queue.TryDequeue(out var result)?result:null;
        }

        private SemaphoreSlim _lock;

        private void doWork()
        {
            while (true)
            {
                var job = getJob();

                if (job == null)
                    Thread.Sleep(_sleepInterval);
                else
                {
                    Task.Run(() =>
                    {
                        //job.Excute(job.State);

                    });
                }

            }
        }

        public bool Cancel(long id)
        {
            throw new NotImplementedException();
        }
    }
}
