using System;

namespace Jasmine.Surpport
{
    public class ServiceStatItem
    {
        public DateTime FinishTime { get; set; }
        public bool Successed { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public int TimeSpend => 0;
    }
}
