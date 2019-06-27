using Microsoft.AspNetCore.Http;
using System;

namespace Jasmine.Restful
{
    public class RestfulTrace
    {
        public string Path { get; set; }
        public string RowPath { get; set; }
        public long TraceId { get; set; }
        public IHeaderDictionary Headers { get; set; }
        public object Output { get; set; }
        public Exception Error { get; set; }
        public string Body { get; set; }
        public IRequestCookieCollection Cookies { get; set; }
        public IQueryCollection Queries { get; set; }
        public string Method { get; set; }
        public string StartTime { get; set; } = DateTime.Now.ToString("yy-mm-dd HH:MM:ss");
        public int Elapse { get; set; }
        public int StutasCode { get; set; }
        public bool Redirected { get; set; }
    }
}
