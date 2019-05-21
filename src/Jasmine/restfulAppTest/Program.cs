using Jasmine.Restful.Attributes;
using Jasmine.Restful.Implement;
using System;
using System.Reflection;

namespace restfulAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new RestfulApplicationBuilder();

            builder.Scan(Assembly.GetExecutingAssembly());

            builder.Build().StartAsyn();

            Console.Read();
        }

        [Restful]
        [HttpMethod("get")]
        [Path("/test")]
        public class Test
        {
            
            public string Say(string what)
            {
                return $"you want me to say {what}";
            }

            public bool BigThan(int value)
            {
                return value > 10;
            }
        }

    }
}
