using System;

namespace Jasmine.Restful.Attributes
{
    public  class HttpMethodAttribute:Attribute
    {
        public HttpMethodAttribute(string method)
        {
            Method = method;
        }
        public string Method { get;  }
    }
}
