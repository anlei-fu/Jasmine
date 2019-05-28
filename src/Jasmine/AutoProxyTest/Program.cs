using Jasmine.AutoProxy;
using System;

namespace AutoProxyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var obg = Proxy.CreateInstance<People>();

            obg.Say();

            Console.ReadLine();

        }

        
    }
}
