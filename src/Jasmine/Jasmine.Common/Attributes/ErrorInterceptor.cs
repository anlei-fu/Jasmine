using System;

namespace Jasmine.Common.Attributes
{
    public class ErrorInterceptor : InterceptorAttribute
    {
        public ErrorInterceptor(Type type) : base(type)
        {
        }
    }
}
