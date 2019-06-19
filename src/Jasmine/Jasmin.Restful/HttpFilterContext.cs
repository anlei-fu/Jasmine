using Jasmine.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class HttpFilterContext
    {
        public HttpContext HttpContext { get; set; }
        public Exception Error { get; set; }
        public object ReturnValue { get; set; }
        public byte[] Body { get; set; }
        public Type ReturnValueType => ReturnValue?.GetType();
        public IDictionary<string, object> PipelineDatas { get; set; } = new Dictionary<string, object>();
        public IDispatcher<HttpFilterContext> Dispatcher { get; internal set; }
        public string Path { get; set; }

        public void Init(HttpContext context)
        {
            HttpContext = context;
            Error = null;
            ReturnValue = null;
            PipelineDatas.Clear();
            Body = null;
        }
        public void Reset()
        {
            HttpContext = null;
        }
    }
}
