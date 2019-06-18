using System;

namespace Jasmine.Common.Attributes
{
    public class AroundInterceptorAttribute : InterceptorAttribute
    {
        public AroundInterceptorAttribute(Type type) : base(type)
        {
        }
    }
}
