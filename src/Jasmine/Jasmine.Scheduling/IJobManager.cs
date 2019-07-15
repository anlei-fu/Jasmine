using System.Collections.Generic;

namespace Jasmine.Scheduling
{
    public  interface IJobManager<T>:IReadOnlyCollection<T>
        where T:Job
    {
        /// <summary>
        /// the time which scheduler should  block
        /// </summary>
        /// <returns></returns>
        int GetNextJobExcutingTimeout();
        /// <summary>
        /// get a new job to scheduler to run
        /// </summary>
        /// <returns></returns>
        T GetJob();
        /// <summary>
        /// cache or store a job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        bool Schedule(T job);
        /// <summary>
        /// cancel a job 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Cancel(long id);
        /// <summary>
        ///  cancel all jobs
        /// </summary>
        void Clear();
        /// <summary>
        /// max count of job taht manager can store
        /// </summary>
        int Capacity { get; }
    }
}
