namespace Jasmine.Scheduling
{
    public enum SchedulerState
    {
        /// <summary>
        /// started
        /// </summary>
        Running,
        /// <summary>
        /// wait for fully stopped
        /// </summary>
        Stopped,
        /// <summary>
        /// stopping
        /// </summary>
        Stopping,
    }
}
