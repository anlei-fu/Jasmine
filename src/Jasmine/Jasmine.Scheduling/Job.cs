using System;
using System.Threading;

namespace Jasmine.Scheduling
{
    public abstract class Job 
    {
        private static int _id = 0;
        public long Id { get; } = Interlocked.Increment(ref _id);

        public JobState State { get; internal set; }
        public DateTime ScheduledTime { get; internal set; }

        public Exception Error { get; internal set; }

        public IScheduler<Job> Scheduler { get; internal set; }

        public  void Cancel()
        {
            if (State != JobState.Scheduled)
                return;

            Scheduler.Cancel(Id);

        }
        public abstract void Excute();
       
    }
}
