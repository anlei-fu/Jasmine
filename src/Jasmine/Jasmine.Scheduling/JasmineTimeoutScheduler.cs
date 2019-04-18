namespace Jasmine.Scheduling
{
    public class JasmineTimeoutScheduler : JasmineScheduler<TimeoutJob>
    {
        public JasmineTimeoutScheduler(ITimeoutJobManager manager ,int maxConccurency=0):base(manager,maxConccurency)
        {
            
        }
        private readonly object _locker = new object();
        public bool AdjustTimeout(long id, long timeout)
        {
            lock (_locker)
            {
                return ((ITimeoutJobManager)_jobManager).AdjustTimeout(id, timeout);
            }
        }

        protected override void setScheduler(TimeoutJob job)
        {
            job.Scheduler = this;
        }
    }
}
