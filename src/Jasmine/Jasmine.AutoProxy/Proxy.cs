using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.AutoProxy
{
   public class Proxy
    {

        public static T CreateInstance<T>()
        {
            var generator = new ProxyGenerator();

            return (T) generator.CreateClassProxy(typeof(T), new SampleInterceptor());

        }
    }
    public class People
    {
        public void Say()
        {

        }
    }
}
