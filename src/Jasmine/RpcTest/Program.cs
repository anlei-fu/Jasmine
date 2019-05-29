using Jasmine.Common.Attributes;
using Jasmine.Ioc.Attributes;
using Jasmine.Rpc.Attributes;
using Jasmine.Rpc.Server;
using Jasmine.Serialization;
using System;
using System.Reflection;

namespace RpcTest
{
    class Program
    {
        static void Main(string[] args)
        {
            run();
        
            Console.Read();
        }

       async static void run()
        {
            var app = new RpcApplicationBuilder().ScanRpcService(Assembly.GetExecutingAssembly())
                                                 .ConfigPort(10336)
                                                 .ConfigValidator(new TrueValidator())
                                                 .ConfigSerializer(JsonSerializer.Instance)
                                                 .Build();

            await app.RunAsync();


        }

        [Rpc]
        [Path("/test")]
        public class Test
        {

            public Test([DefaultValue("rpc-bot")]string prefix)
            {
                _prefix = prefix;
            }

            private string _prefix;
            [Path("/say")]
            public string Say(string what)
            {
                return $" {_prefix}: you want me to say {what}";
            }

            public bool BigThan(int value)
            {
                return value > 10;
            }
        }

    }
}
