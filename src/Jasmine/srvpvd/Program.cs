using Jasmine.Common.Attributes;
using Jasmine.Ioc;
using Jasmine.Ioc.Attributes;
using System;
using System.Diagnostics;

namespace srvpvd
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = IocServiceProvider.Instance;

           serviceProvider.SetImplementationMapping(typeof(ISay), typeof(Pig));
            Animal s = null;
            var watch = new Stopwatch();
            s = (Animal)serviceProvider.GetService(typeof(Animal));
            watch.Start();
          
          
            for (int i = 0; i < 100000; i++)
            {
               // s = new Animal("i'm ok", new Pig());
             s = (Animal)serviceProvider.GetService(typeof(Animal));
            }



            Console.WriteLine(watch.ElapsedMilliseconds);

            s.Say();

            Console.Read();
         



        }
    }

  [ServiceScope(ServiceScope.Singleton)]
  
    public class Animal
    {

        public Animal([DefaultValue("i'm ok!")]string s,ISay key)
        {
            _words = s;
            _animal = key;
        }
        private ISay _animal;
        private string _words;
        public void Say()
        {
            _animal.Say();
        }
    }

    public interface ISay
    {
        void Say();
    }

    public  class Pig:ISay
    {
        public  void Say()
        {
            Console.WriteLine("i'm a pig!");
        }
    }
    public class Dog : ISay
    {
        public  void Say()
        {
            Console.WriteLine("i'm a dog!");
        }
    }
}
