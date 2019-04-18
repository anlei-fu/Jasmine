namespace Jasmine.Scheduling
{
    public abstract class RoundRobinJob : Job
    {
        public new JasminRoundRobinScheduler Scheduler { get; internal set; }
        public override abstract void Excute();
      
    }
}
