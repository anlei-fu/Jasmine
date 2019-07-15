using System;

namespace Jasmine.Scheduling
{
    /// <summary>
    /// listen and handle some events which raised by <see cref="IScheduler{T}"/> 
    /// </summary>
    public  interface ISchedulerEventListener
    {
        /// <summary>
        /// when job be add into sheduler's task queue
        /// </summary>
        /// <param name="job"></param>
        void  OnJobScheduled(Job job);
        /// <summary>
        /// when job begin  excuting
        /// </summary>
        /// <param name="jobId"></param>
        void OnJobBeginExcuting(Job job);
        /// <summary>
        /// when job finish excuting and faild the <see cref="Job.Error"/>where be set by sheduler 
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ex"></param>
        void OnJobEcuteFailed(Job job);
        /// <summary>
        /// when job finish excuting and successed
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="ex"></param>
        void OnJobExcuteSusccefully(Job job);
        /// <summary>
        /// when sheduler started
        /// </summary>
        void OnSchedulerStarted();
        /// <summary>
        /// when sheduled stoppped
        /// </summary>
        void OnSchedulerStopped();

    }
}
