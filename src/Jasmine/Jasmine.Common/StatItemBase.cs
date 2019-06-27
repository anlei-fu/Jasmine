using System;

namespace Jasmine.Common
{
    public class StatItem : IStatItem
    {
        public long Elapsed { get; set; }

        public bool Sucessed { get; set; } = true;

        public string Time { get; set; } = DateTime.Now.ToString("HH:MM:ss");
    }
}
