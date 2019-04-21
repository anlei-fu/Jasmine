using System;
using System.Collections.Generic;
using System.Net;

namespace Jasmine.Spider.Common
{
    public class SpiderWorkerInfo : SpiderComponentBase
    {
        public override string Name { get; set; }
        public override EndPoint EndPoint { get; set; }
        public int Level { get; set; }
        public int TaskExcuted { get; set; }
        public int PageExtracted { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime LastRun { get; set; }
        public List<long> TaskRunning { get; set; }
        public List<string> BlockedDomian { get; set; }
    }
}
