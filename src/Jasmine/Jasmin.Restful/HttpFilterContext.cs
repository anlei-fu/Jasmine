using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class HttpFilterContext
    {
        public HttpContext HttpContext { get; set; }
        public Exception Exception { get; set; }
        public object ReturnValue { get; set; }
        public Type ReturnValueType { get; set; }
        public IDictionary<string, object> Datas { get; set; }
      
    }
}
