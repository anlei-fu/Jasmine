using Castle.DynamicProxy;
using System;

namespace Jasmine.AutoProxy
{
    public class SampleInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("intercepted");
        }
    }
}
