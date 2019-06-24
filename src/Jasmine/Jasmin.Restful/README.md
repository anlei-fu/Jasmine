# Jasmie restful application
### a middleware of asp.netcore
## Usage

``` C#
using Jasmine.Common.Attributes;
using Jasmine.Ioc.Attributes;
using Jasmine.Restful;
using Jasmine.Restful.Attributes;
using Jasmine.Restful.Implement;
using System;
using System.IO;
using System.Reflection;

namespace restfulAppTest
{
    class Program
    {
       static  void Main(string[] args)
        {
            var builder = new RestfulApplicationBuilder();

          
           
       var app= builder.ConfigServer(serverConfig=>
          {
              serverConfig.Port = 10336;
          }).ConfigServiceProvider(serviceProvider=>
          {
              serviceProvider.AddSigleton(typeof(Test), new Test("robot-001"));
          }).ConfigRestfulService(manager=>
          {
              manager.Scan(Assembly.GetExecutingAssembly());
          }).Build();


            app.StartAsync();

            Console.Read();
        }

        [Restful]
       
        [Path("/test")]
        public class Test
        {
            
            public Test([FromConfig("test.prefix[name:001]")]string prefix)
            {
                _prefix = prefix;
            }

            private string _prefix;

            [Path("/do")]
            public string Say(string what)
            {
                return $" {_prefix}: you want me to say {what}";
            }

            public bool BigThan(int value)
            {
                return value > 10;
            }
        }

        [Restful]
        public class Caculator
        {
            public int Add(int x,int y)
            {
                return x + y;
            }
            [Post]
            public int Pow2(int x)
            {
                return x * x;
            }
        }

    }
}

```