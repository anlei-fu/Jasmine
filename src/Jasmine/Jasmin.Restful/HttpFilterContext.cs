using Jasmine.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class HttpFilterContext:IRequestProcessingContext
    {
        public HttpContext HttpContext { get; set; }
        public Exception Error { get; set; }
        public object ReturnValue { get; set; }
        public Type ReturnValueType => ReturnValue?.GetType();
        public IDictionary<string, object> Datas { get; set; } = new Dictionary<string, object>();
        public IDispatcher<HttpFilterContext> Dispatcher { get; internal set; }
        public string Path => HttpContext.Request.Path;

        public void Init(HttpContext context)
        {
            HttpContext = context;
            Error = null;
            ReturnValue = null;
            Datas.Clear();
            Dispatcher = null;
        }
        public void Reset()
        {
            HttpContext = null;
        }
    }
}
