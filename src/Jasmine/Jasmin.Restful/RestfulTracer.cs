using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class RestfulTracer :ITracer
    {
        public  void BeginTrace(HttpFilterContext context)
        {
            if(context.PipelineDatas.ContainsKey("_trace_"))
            {

                if(!_cache.ContainsKey(context.Path))
                {
                    _cache.TryAdd(context.Path, new ConcurrentQueue<RestfulTrace>());
                }

                var trace = new RestfulTrace();

                trace.Body = (string)context.PipelineDatas["_body_"];
                trace.Headers = context.HttpContext.Request.Headers;
                trace.Cookies = context.HttpContext.Request.Cookies;
                trace.Queries = context.HttpContext.Request.Query;

                _cache[context.Path].Enqueue(trace);

                context.PipelineDatas.Add("_trace_", trace);
            }

        }

        public  void EndTrace(HttpFilterContext context)
        {

        }

        private ConcurrentDictionary<string, ConcurrentQueue<RestfulTrace>> _cache = new ConcurrentDictionary<string, ConcurrentQueue<RestfulTrace>>();

        public  IEnumerable<RestfulTrace> GetTrace(string path)
        {
            return null;
        }

    }

  
}
