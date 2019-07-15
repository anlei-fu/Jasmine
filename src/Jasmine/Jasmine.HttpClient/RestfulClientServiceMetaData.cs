using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Jasmine.HttpClient
{
   public class RestfulClientServiceMetaData
    {
        public Type Interface { get; set; }
        public string BaseUrl { get; set; }
        public string HttpMethod { get; set; }
        public int Timeout { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public CookieCollection Cookies { get; set; }
        public bool Throwable { get; set; }
        public int Retry { get; set; }
        public Encoding Encoding { get; set; }
        public IEnumerable<RestfulClientRequestCallMetaData> Calls { get; set; }
    }

    public class RestfulClientRequestCallMetaData
    {
        public Method Method { get; set; }
        public string BaseUrl { get; set; }
        public string HttpMethod { get; set; }
        public int Timeout { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public CookieCollection Cookies { get; set; }
        public bool Throwable { get; set; }
        public int Retry { get; set; }
        public Encoding Encoding { get; set; }
        public IEnumerable<RestfulClientRequestCallParameterMetaData> Paramenters { get; set; }
    }

    public class RestfulClientRequestCallParameterMetaData
    {
        public Parameter Parameter { get; set; }
        public string PathVaribleKey { get; set; }
        public bool ToPathVarible => PathVaribleKey != null;
        public string QueryStringKey { get; set; }
        public bool FromQueryString => QueryStringKey != null;
        public string HeaderKey { get; set; }
        public bool FromHeader => HeaderKey != null;
        public string CookieKey { get; set; }
        public bool ToCookie => CookieKey != null;
        public bool Nullable { get; set; }
    }
}
