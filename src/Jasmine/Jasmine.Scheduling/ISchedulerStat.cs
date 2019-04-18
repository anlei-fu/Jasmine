using System;

namespace Jasmine.Scheduling
{
    public interface ISchedulerStat
    {
        DateTime? StartTime{ get; }
        DateTimeOffset? RunningTime { get; }
        int JobExcuted { get; }
        int JobScheduled { get; }
        int JobExcuting { get; }
        int JobUnExcute { get; }
        int JobFailed { get; }
        int JobSuccessed { get; }
    }
}
