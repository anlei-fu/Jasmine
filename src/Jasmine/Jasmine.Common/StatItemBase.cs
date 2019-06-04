using System;

namespace Jasmine.Common
{
    public class StatItemBase : IStatItem
    {
        public long Elapsed { get; set; }

        public bool Sucessed { get; set; }

        public string Time { get; set; }
    }
}
