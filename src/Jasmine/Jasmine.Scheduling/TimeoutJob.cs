using System;

namespace Jasmine.Scheduling
{
    public abstract class TimeoutJob :Job
    {
        public TimeoutJob(DateTime time)
        {
            ScheduledExcutingTime = time;
        }
    
        public DateTime ScheduledExcutingTime { get;internal set; }
     
        public new JasmineTimeoutScheduler Scheduler { get; internal set; }
        public void AddJustTimeout(long millionSeconds)
        {
            if (JobState!=JobState.Scheduled)
            {
                return;
            }

           Scheduler.AdjustTimeout(Id, millionSeconds);
        }

        public override abstract void Excute();
       
    }
}
