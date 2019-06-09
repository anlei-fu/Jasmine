using System;
using Jasmine.Orm;
using Jasmine.Orm.Attributes;
namespace OrmTest
{
    class Program
    {
        static ITableTemplateCacheProvider _provider=>DefaultTableTemplateCacheProvider.Instance;
        static void Main(string[] args)
        {
            create();

            query();

            queryPartial();

            queryPartial();

            queryPartial();

            queryPartialCondition();

            queryWith();

            Console.Read();
        }


        static  void create()
        {
            Console.WriteLine(_provider.GetCache<Animal>().GetCreate());

            Console.WriteLine();
        }

        static void query()
        {
            Console.WriteLine(_provider.GetCache<Animal>().GetQuery());

            Console.WriteLine();
        }

        static void queryWith()
        {

            Console.WriteLine(_provider.GetCache<Animal>().GetQueryWith("Animal"));

            Console.WriteLine();
        }
        static void queryPartial()
        {
            Console.WriteLine(_provider.GetCache<Animal>().GetQueryPartial("Age","CanFly","Fearture.MaxAge"));

            Console.WriteLine();
        }

        static void queryPartialCondition()
        {
            Console.WriteLine(_provider.GetCache<Animal>().GetQueryPartialConditional(" Age>10 ","Age", "CanFly"));

            Console.WriteLine();
        }

   
        public class Animal
        {
            [PrimaryKey]
            [SqlColumnType("varchar(100)")]
         
            public string Name { get; set; }

            public int Age { get; set; }
        
            public bool CanFly { get; set; }
            [JoinColumns()]
            public Fearture Fearture { get; set; }

            [JoinTable("Age")]
            public Number Number { get; set; }
        }

        public class Number
        {
            public int Age { get; set; }
            public int Max { get; set; }
            public int Min { get; set; }
        }
        public class Fearture
        {
            public string Food { get; set; }
            public int MaxAge { get; set; }
            [JoinColumns]
            public Sleep Sleep { get; set; }
        }

        public class Sleep
        {
            public int Length { get; set; }
        }
    }
}
