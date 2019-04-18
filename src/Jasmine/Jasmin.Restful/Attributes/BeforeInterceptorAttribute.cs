using System;

namespace Jasmine.Restful.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeInterceptorAttribute:Attribute
    {
        public string InterceptorName { get; set; }
    }
}
