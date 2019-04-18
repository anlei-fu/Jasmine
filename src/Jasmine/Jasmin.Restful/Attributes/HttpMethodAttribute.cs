using System;

namespace Jasmine.Restful.Attributes
{
    public  class HttpMethodAttribute:Attribute
    {
        public string Method { get; set; }
    }
}
