using System.Collections.Generic;
using System.Diagnostics;

namespace Jasmine.Common
{
    public class TimelineLogger
    {
        private Stopwatch _watch = new Stopwatch();

        private List<TimelineLoggerContent> _contents;
        private long _lastMillionSeconds;
        public TimelineLoggerContent Log(string msg)
        {
            var current = _watch.ElapsedMilliseconds;
            var elapsed =current - _lastMillionSeconds;
            _lastMillionSeconds = current;

            var result= new TimelineLoggerContent(msg, elapsed);
            _contents.Add(result);

            return result;

        }
        public void Start()
        {
            _lastMillionSeconds = 0;
            _watch.Start();
        }
        public List<TimelineLoggerContent> Stop()
        {
            _watch.Stop();
            _watch.Reset();
            var result = _contents;
            _contents.Clear();

            return result;
        }

        public class  TimelineLoggerContent
        {
            public TimelineLoggerContent(string descr,long elapsed)
            {
                Description = descr;
                ElapsedMillionSeconds = elapsed;
            }
            public string Description { get; }
            public long ElapsedMillionSeconds { get; }

            public override string ToString()
            {
                return $"[{Description}]:{ElapsedMillionSeconds}\r\n";
            }
        }
    }
}
