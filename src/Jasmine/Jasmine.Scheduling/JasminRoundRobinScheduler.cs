namespace Jasmine.Scheduling
{
    public class JasminRoundRobinScheduler : AbstractScheduler<RoundRobinJob>
    {
        public JasminRoundRobinScheduler(IJobManager<RoundRobinJob> jobManager, int maxConcurrency = 4) : base(jobManager, maxConcurrency)
        {
        }

        protected override void setScheduler(RoundRobinJob job)
        {
            job.Scheduler = this;
        }
    }
}
