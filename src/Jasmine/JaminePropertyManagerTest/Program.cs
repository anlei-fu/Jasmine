using Jasmine.Restful;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;

namespace JaminePropertyManagerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kestrel assembly location = " + typeof(KestrelServerOptions).Assembly.Location);
            Console.WriteLine("Kestrel assembly version  = " + typeof(KestrelServerOptions).Assembly.GetName().ToString());
            Console.WriteLine("Deps files = " + AppContext.GetData("APP_CONTEXT_DEPS_FILES") as string);

            // run the kestrel server
            WebHost.CreateDefaultBuilder(args).Build().Run();

        }
    }
}
