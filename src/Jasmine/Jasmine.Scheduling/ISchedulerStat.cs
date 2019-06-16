using System;

namespace Jasmine.Scheduling
{
    public interface ISchedulerStat
    {
        /// <summary>
        /// startup time
        /// </summary>
        DateTime? StartTime{ get; }
        /// <summary>
        /// the timespan since lasted start
        /// if shceduler stopped ,it'will be null
        /// and <see cref="StartTime"/>will reset to current time
        /// after start
        /// </summary>
        TimeSpan? RunningTime { get; }
        /// <summary>
        /// the count of job which has been excuted
        /// </summary>
        int JobExcuted { get; }
        /// <summary>
        /// the count of job which has been scheduled 
        /// include the canceled job and excuted job
        /// </summary>
        int JobScheduled { get; }
        /// <summary>
        /// the count of job running at current now
        /// </summary>
        int JobExcuting { get; }
        /// <summary>
        /// the count of job wait to excute
        /// </summary>
        int JobUnExcute { get; }
        /// <summary>
        /// the count of job which excute failed
        /// </summary>
        int JobFailed { get; }
        /// <summary>
        /// the count of job which excute successed
        /// </summary>
        int JobSuccessed { get; }
    }
}
