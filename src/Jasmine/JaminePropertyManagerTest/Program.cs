using Jasmine.Common;
using System;

namespace JaminePropertyManagerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = JasminePropertyManagerXmlLoader.Load("jasmine.property");

            Console.WriteLine(manager.GetValue("animal"));

            Console.Read();
            
        }
    }
}
