using System;

namespace Jasmine.Scheduling
{
    public abstract class TimeoutJob :Job
    {
        public TimeoutJob(DateTime excuteTime)
        {
            ScheduledExcutingTime = excuteTime;
        }
    
        public DateTime ScheduledExcutingTime { get;internal set; }
     
        public new JasmineTimeoutScheduler Scheduler { get; internal set; }
        public bool AdjustTimeout(long timeout)
        {
            lock (_locker)
            {
                return State != JobState.Scheduled
                             ? false: Scheduler.AdjustTimeout(Id, timeout);
            }
        }

        public override abstract void Excute();
       
    }
}
