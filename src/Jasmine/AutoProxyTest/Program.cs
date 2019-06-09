using Jasmine.Orm;
using System;

namespace AutoProxyTest
{
    class Program
    {
        static ITableTemplateCacheProvider provider => DefaultTableTemplateCacheProvider.Instance;
        static void Main(string[] args)
        {

            testCreate();

            Console.Read();
        }

        static void testCreate()
        {
           Console.WriteLine(  provider.GetCache<Animal>().GetCreate());
        }
        public class Animal
        {
            public string Name { get; set; }

            public int Age { get; set; }
            public bool CanFly { get; set; }
        }

        public class Fearture
        {
            public string Food { get; set; }
            public int MaxAge { get; set; }
        }
    }
}
