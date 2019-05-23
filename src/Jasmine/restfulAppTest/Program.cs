using Jasmine.Ioc.Attributes;
using Jasmine.Restful.Attributes;
using Jasmine.Restful.Implement;
using System;
using System.IO;
using System.Reflection;

namespace restfulAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new RestfulApplicationBuilder();

            builder.LoadConfig("test.config");
            builder.Scan(Assembly.GetExecutingAssembly());
            builder.ConfigDispatcher(dispacther =>
            {
                dispacther.UseStaticFile = true;
                dispacther.VirtuePathRoot = Directory.GetCurrentDirectory() + "/www";
            });
            builder.Build().StartAsyn();

            Console.Read();
        }

        [Restful]
        [HttpMethod("get")]
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

    }
}
