namespace Jasmine.Scheduling
{
    public  interface ITimeoutJobManager:IJobManager<TimeoutJob>
    {
        bool AdjustTimeout(long jobId, long millionSeconds);
    }
}
