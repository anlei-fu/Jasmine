using Jasmine.Configuration;
using System;

namespace ConfigTest
{
    class Program
    {
        static void Main(string[] args)
        {
        

            var provider = new JasmineConfigurationProvider();

            provider.LoadConfig("test.config");
            Console.WriteLine(provider.GetConfig("service2.remote[host:192.168.0.1,port:80]"));
            Console.WriteLine(provider.GetConfig("service.local"));

            Console.Read();
        }
    }
}
