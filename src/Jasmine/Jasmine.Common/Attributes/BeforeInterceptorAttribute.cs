using System;

namespace Jasmine.Common.Attributes
{
   
    public class BeforeInterceptorAttribute : InterceptorAttribute
    {
        public BeforeInterceptorAttribute(Type type) : base(type)
        {
        }
    }
}
