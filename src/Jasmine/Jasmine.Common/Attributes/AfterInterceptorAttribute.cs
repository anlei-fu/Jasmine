using System;

namespace Jasmine.Common.Attributes
{
    public class AfterInterceptorAttribute : InterceptorAttribute
    {
        public AfterInterceptorAttribute(Type type) : base(type)
        {
        }
    }
}
