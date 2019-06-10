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
        public void AdjustTimeout(long millionSeconds)
        {
            if (State!=JobState.Scheduled)
            {
                return;
            }

           Scheduler.AdjustTimeout(Id, millionSeconds);
        }

        public override abstract void Excute();
       
    }
}
