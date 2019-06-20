using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jasmine.Cache;
using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;

namespace Jasmine.Restful.DefaultFilters
{
    [Restful]
    public class RestfulTraceFilter : AbstractFilter<HttpFilterContext>
    {
        private ConcurrentDictionary<string, JasmineFifoCache<long,RestfulTraceItem>> _traces = new ConcurrentDictionary<string, JasmineFifoCache<long,RestfulTraceItem>>();
        [RestfulIgnore]
        public override Task FiltsAsync(HttpFilterContext context)
        {
           if(!context.PipelineDatas.ContainsKey("RestfulTrace"))
            {

            }
           else if(context.Error!=null)
            {

            }
           else
            {
                var trace = _traces[context.Path].GetValue((long)context.PipelineDatas["RestfulTrace"]);

                if(trace!=null)
                {
                    trace.Elapse = (DateTime.Now - trace.Time).TotalMilliseconds.ToString();
                }
            }

            return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
        }
        [Description("get service trace log at latest")]
        [Path("/api/gettrace")]
        public List<RestfulTraceItem> GetTrace([Description("the trace log you want to get by path ")]string path)
        {
            return _traces.TryGetValue(path, out var traces) ? traces.Values.ToList():null;
        }

       
    }

    public class RestfulTraceItem
    {
        private static long _id=1000;
        public string RemoteAddress { get; set; }
        public string Queries { get; set; }
        public string IdString { get; set; }
        public long TraceId { get; } = Interlocked.Increment(ref _id);
        public string Headers { get; set; }
        public string Forms { get; set; }
        public string Input { get; set; }
        public string Cookies { get; set; }
        public string StutaCode { get; set; }
        public string Method { get; set; }
        public string TimeString { get; set; }
        public DateTime Time { get; set; }
        public bool Sucessed { get; set; }
        public string Output { get; set; }
        public string Elapse { get; set; }
    }
}
