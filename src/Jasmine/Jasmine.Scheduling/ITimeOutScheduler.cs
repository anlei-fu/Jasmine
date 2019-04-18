using System.Collections.Generic;

namespace Jasmine.Scheduling
{
    public interface ITimeOutScheduler :ITimeJobManager, IReadOnlyCollection<ITimeoutJob>
    {
      
        int Capacity { get; }
        int MaxConcurrency { get; }
        int SleepInterval { get; }
        SchedulerState State { get; }
 
        bool Start();
        bool Stop(bool waitForAllJobComplete);
        
    }
}
