using System;
using System.Data.Common;
using System.Data.SqlClient;
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

            var excutor = new JasmineSqlExcutor(new SqlServerConnectionProvider("Server=HW09;Database=Test;Trusted_Connection=True;"));

            insert();

            //  excutor.Create<Animal>();

            // excutor.Insert<Animal>(new Animal());

            excutor.Query<Animal>();

            Console.Read();
        }

        static void insert()
        {
            Console.WriteLine(_provider.GetCache<Animal>().GetInsert().RawString);

            Console.WriteLine();
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

            public string Name { get; set; } = "cog";

            public int Age { get; set; } = 14;

            public bool CanFly { get; set; } = true;
            [JoinColumns()]
            public Fearture Fearture { get; set; } = new Fearture();

            [JoinTable("Age", "Max")]
            public Number Number { get; set; } = new Number();
        }

        public class Number
        {
            public int Age { get; set; } = 14;
            public int Max { get; set; } = 1;
            public int Min { get; set; } = 2;
        }
        public class Fearture
        {
            public string Food { get; set; } = "apple";
            public int MaxAge { get; set; } = 100;
            [JoinColumns]
            public Sleep Sleep { get; set; } = new Sleep();
        }

        public class Sleep
        {
            public int Length { get; set; } = 25;
        }
    }
}
