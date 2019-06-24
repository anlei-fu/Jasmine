namespace Jasmine.Scheduling
{
    public class JasmineFifoScheduler<T> : AbstractScheduler<T>
         where T : Job
    {
        public JasmineFifoScheduler(FifoJobManager<T> jobmanager, int maxConcurrency = 0) : base(jobmanager, maxConcurrency)
        {
        }

        protected override void setScheduler(T job)
        {
            throw new System.NotImplementedException();
        }
    }
}
