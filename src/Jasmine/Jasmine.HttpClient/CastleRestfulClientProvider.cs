using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.HttpClient
{
    public class CastleRestfulClientProvider : IRestfulClientProvider
    {
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }
        public object Get(Type type)
        {
            throw new NotImplementedException();


            ProxyGenerator
        }

       
    }
}
