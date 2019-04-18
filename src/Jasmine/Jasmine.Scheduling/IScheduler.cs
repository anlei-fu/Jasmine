namespace Jasmine.Scheduling
{
    public  interface IScheduler<T>:ISchedulerStat,IJobManager<T>
        where T:Job
    {
        /// <summary>
        /// the max count of job that  run at the same time  
        /// </summary>
        int MaxConcurrency { get; }
        /// <summary>
        /// <see cref="JobState"/>
        /// </summary>
        SchedulerState State { get; }
        /// <summary>
        /// if already running
        /// return  false or return true
        /// </summary>
        /// <returns></returns>
        bool Start();
        /// <summary>
        /// if already stpped ,return false or return true
        /// </summary>
        /// <param name="waitForAllJobComplete"></param>
        /// <returns></returns>
        bool Stop(bool waitForAllJobComplete);
        /// <summary>
        /// <see cref="ISchedulerEventListener"/>
        /// a listener to listern scheduler event
        /// 
        /// include
        /// 1 .start
        /// 2. stop
        /// 3. job scheduled
        /// 4. job begin excuting
        /// 5. job excute failed
        /// 6  job excute successd
        /// </summary>
        ISchedulerEventListener Listener { get; }

    }
}
