using System;
using System.Threading;

namespace Jasmine.Scheduling
{
    public abstract class Job 
    {
        private static int _id = 0;

        protected readonly object _locker = new object();
        /// <summary>
        /// a global  id use to mark a unique job
        /// </summary>
        public long Id { get; } = Interlocked.Increment(ref _id);
        /// <summary>
        /// <see cref="JobState"/>
        /// </summary>
        public JobState State { get; internal set; }
        /// <summary>
        /// when it begin to  excuting excute ,will be set by<see cref="IScheduler{T}"/>
        /// </summary>
        public DateTime ScheduledTime { get; internal set; }
        /// <summary>
        /// when it excute failed will be set <see cref="IScheduler{T}"/>
        /// </summary>
        public Exception Error { get; internal set; }
        /// <summary>
        /// the sheduler run job
        /// </summary>
        public IScheduler<Job> Scheduler { get; internal set; }
        /// <summary>
        /// cancel ,if the job has been exuting or excuted will failed
        /// </summary>
        public  bool Cancel()
        {
            lock (_locker)
            {
                return State != JobState.Scheduled
                           ? false : Scheduler.Cancel(Id);
            }

        }
        /// <summary>
        /// the method call by sheduler
        /// </summary>
        public abstract void Excute();
       
    }
}
