using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Jasmine.Spider.SpiderUrlFilter
{
    public class UrlCache
    {
        private ConcurrentQueue<string> _queue;
        private IUrlFilter _filter;
        public UrlFilterStatics Statics;
        public void Cache(IEnumerable<string> urls)
        {
            var ls = new List<string>();

            foreach (var item in urls)
            {
                Interlocked.Increment(ref Statics.TotalFiltCount);

                if (!_filter.Filt(item))
                {
                    _queue.Enqueue(item);
                    ls.Add(item);
                    Interlocked.Increment(ref Statics.CacheCount);
                    Interlocked.Increment(ref Statics.NewUrlFound);
                }
                else
                {
                    Interlocked.Increment(ref Statics.RepeatCount);
                }
            }

            //store to database
        }
       
        public List<string> GetUrls(int count)
        {
            var ls = new List<string>();

            while (ls.Count < count && _queue.TryDequeue(out var url))
            {
                ls.Add(url);
                Interlocked.Decrement(ref Statics.CacheCount);
            }

            return ls;
        }
        
    }
}
