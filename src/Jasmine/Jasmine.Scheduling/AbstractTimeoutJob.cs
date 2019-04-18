using Jasmine.Scheduling.Exceptions;
using System;
using System.Threading;

namespace Jasmine.Scheduling
{
    public abstract class AbstractTimeoutJob : ITimeoutJob
    {
        
        private static int _id = 0;
        private static long newId => Interlocked.Increment(ref _id);
        public ITimeOutScheduler Scheduler { get; internal set; }

        public DateTime ScheduledExcutingTime { get; set; }

        public long Id { get; } = newId;
        public JobState JobState { get ;internal set ; }
        public bool Scheduled { get; internal set; }

        public void AddJustTimeout(long millionSeconds)
        {
            if (!Scheduled)
            {
                throw new JobNotScheduledException("");
            }

            Scheduler.AdjustTimeout(Id, millionSeconds);

        }

        public void Cancel()
        {
           if(!Scheduled)
            {
                throw new JobNotScheduledException("");
            }

            Scheduler.Cancel((long)Id);
        }

        public abstract void Excute();
       
    }
}
