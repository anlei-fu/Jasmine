using System;

namespace Jasmine.Scheduling
{
    public interface ITimeoutJob:IJob
    {
         ITimeOutScheduler Scheduler { get; }
        DateTime ScheduledExcutingTime { get; }
        void AddJustTimeout(long millionSeconds);

    }
}
