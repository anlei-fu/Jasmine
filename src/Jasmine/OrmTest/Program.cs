using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using Jasmine.Orm;
using Jasmine.Orm.Attributes;
using Microsoft.EntityFrameworkCore;

namespace OrmTest
{
    class Program
    {
        static ITableTemplateCacheProvider _provider=>DefaultTableTemplateCacheProvider.Instance;
        static void Main(string[] args)
        {
            create();

            query();

            //queryPartial();

            //queryPartial();

            //queryPartial();

            //queryPartialCondition();

            //queryWith();

            var provider = new SqlServerConnectionProvider("Server=HW09;Database=Test;Trusted_Connection=True;");

            var excutor = new JasmineSqlExcutor(provider);

           // excutor.Create<Animal>();
          
            insert();

            //  excutor.Create<Animal>();


            excutor.Excute("delete from animal ");

            int t =100;
            Console.WriteLine($"test sqlexcutor and dapper,data size:{t}");

            Console.WriteLine($"\r\ntest single row insert");
            var inserWatch = new Stopwatch();
            inserWatch.Start();
            var connetion = provider.Rent();
            var ls = new List<Animal>();

            for (int i = 0; i < t; i++)
            {
                var animal = new Animal();

                //animal.Age = animal.Number.Max = i;
                animal.Name = i.ToString();

                
               connetion.Execute("insert into animal(name,age,canfly) values (@Name,@Age,@CanFly)",animal);
            }

            Console.WriteLine($" dapper insert {t} row elapse:{inserWatch.ElapsedMilliseconds} ");

            excutor.Excute("delete from animal ");

            inserWatch.Reset();
            inserWatch.Restart();
            for (int i = 0; i < t; i++)
            {
                var animal = new Animal();

                //animal.Age = animal.Number.Max = i;
                animal.Name = i.ToString();


                excutor.Insert(animal);
            }

            excutor.Excute("delete from animal ");

            Console.WriteLine($" sqlexcutor insert {t} row elapse:{inserWatch.ElapsedMilliseconds} ");


            Console.WriteLine($"\r\ntest insert partial single row insert");
             inserWatch = new Stopwatch();
            inserWatch.Start();
             connetion = provider.Rent();
             ls = new List<Animal>();

            for (int i = 0; i < t; i++)
            {
                var animal = new Animal();

                //animal.Age = animal.Number.Max = i;
                animal.Name = i.ToString();


                connetion.Execute("insert into animal(name,age) values (@Name,@Age)", animal);
            }

            Console.WriteLine($" dapper insert {t} row elapse:{inserWatch.ElapsedMilliseconds} ");

            excutor.Excute("delete from animal ");

            inserWatch.Reset();
            inserWatch.Restart();
            for (int i = 0; i < t; i++)
            {
                var animal = new Animal();

                //animal.Age = animal.Number.Max = i;
                animal.Name = i.ToString();


                excutor.InsertPartial(animal,"Name","Age");
            }


            Console.WriteLine($" sqlexcutor insert {t} row elapse:{inserWatch.ElapsedMilliseconds} ");




            Console.WriteLine($"\r\ntest query");
            // excutor.Insert<Animal>(new Animal());

            inserWatch.Reset();
            inserWatch.Restart();

            var result = connetion.Query<Animal>("select * from animal");

        //var ls=    excutor.Query<Animal>();

            Console.WriteLine($"dapper query {t} row elapse:{inserWatch.ElapsedMilliseconds} ");

            inserWatch.Reset();
            inserWatch.Restart();

           //  result = excutor.Query<Animal>("select * from animal");

            Console.WriteLine($"sqlexcutor query {t} row elapse:{inserWatch.ElapsedMilliseconds} ");



            Console.WriteLine($"test batch insert");



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

         
            //[JoinColumns()]
            //public Fearture Fearture { get; set; } = new Fearture();

            //[JoinTable("Age", "Max")]
            //public Number Number { get; set; } = new Number();
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
