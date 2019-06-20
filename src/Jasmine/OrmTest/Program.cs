using Dapper;
using Jasmine.Orm;
using Jasmine.Orm.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OrmTest
{
    class Program
    {
        static void Start(string title)
        {
            Console.WriteLine($"{title} testing start....");
            Console.WriteLine();
        }
        static void End()
        {
            Console.WriteLine("end....");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        static Stopwatch _watch = new Stopwatch();

        static void startWatch()
        {
            _watch.Reset();
            _watch.Restart();
        }

       static void PrintDapper()
        {
            Console.WriteLine($"dapper--elapsed:{_watch.ElapsedMilliseconds} ms");
        }
       static void PrintExcutor()
        {
            Console.WriteLine($"sqlexcutor--elapsed:{_watch.ElapsedMilliseconds} ms");
        }
        static ITableTemplateCacheProvider _provider=>DefaultTableTemplateCacheProvider.Instance;
        static void Main(string[] args)
        {
            /*
             * 
             * Raw sql generate ability
             * 
             */ 
            create();

            query();

            //queryPartial();

            //queryPartial();

            //queryPartial();

            //queryPartialCondition();

            //queryWith();

            //insert();

            /*
             * create table 
             */
            // excutor.Create<Animal>();



            var provider = new SqlServerConnectionProvider("Server=HW09;Database=Test;Trusted_Connection=True;");

            var excutor = new JasmineSqlExcutor(provider);

            excutor.DeleteAll<Animal>();

            excutor.Create<Animal>();

            int dataSize =1000;


            Console.WriteLine($"test sqlexcutor and dapper speed,data size is {dataSize} row");


            var connetion = provider.Rent();

            //data
            var datas = new List<Animal>();

            for (int i = 0; i < dataSize; i++)
            {
                var animal = new Animal();

                animal.Name = i.ToString();

                datas.Add(animal);
            }




            Start("single row insert");

            startWatch();

            foreach (var item in datas)
                connetion.Execute("insert into animal(name,age,canfly) values (@Name,@Age,@CanFly)", item);

            PrintDapper();
           

            excutor.Excute("delete from animal ");

            startWatch();

            foreach (var item in datas)
                excutor.Insert<Animal>(item);

            PrintExcutor();

            End();




            excutor.Excute("delete from animal ");





            Start("test single row partial insert");
           
             connetion = provider.Rent();

            startWatch();

            foreach (var item in datas)
                connetion.Execute("insert into animal(name,age) values (@Name,@Age)", item);

            PrintDapper();
            

            excutor.Excute("delete from animal ");

            startWatch();

            foreach (var item in datas)
                excutor.InsertPartial<Animal>(item, null, "Name", "Age");

            PrintExcutor();
            End();



            Start(" full query");

            startWatch();
          
            var result = connetion.Query<Animal>("select * from animal");
            PrintDapper();

            startWatch();
            result = excutor.Query<Animal>();
            PrintExcutor();
            End();


            excutor.Excute("delete from animal ");




            Start("batch insert");
            startWatch();
            connetion.Execute("insert into animal(name,age,canfly) values(@Name,@Age,@CanFly)", datas);
            PrintDapper();
            excutor.Excute("delete from animal ");

            startWatch();
            excutor.BatchInsert<Animal>(datas);
            PrintExcutor();
            End();




            excutor.Excute("delete from animal ");

            Start("test batch insert partial....");
            startWatch();
            connetion.Execute("insert into animal(name,age) values(@Name,@Age)", datas);
            PrintDapper();

            excutor.Excute("delete from animal ");

            startWatch();
            excutor.BatchInsertPartial<Animal>(new string[] {"Name","Age" },datas);
            PrintExcutor();
            End();

            foreach (var item in excutor.QueryTopOrderByDesc<Animal>(10, "Name"))
            {
                Console.WriteLine(item.Name);
            }


            Start("test delete");

            startWatch();

            connetion.Execute("delete from  animal where name in @ids", new { ids = new string[] { "2", "3", "4" } });
            PrintDapper();

            startWatch();
            excutor.Delete<Animal>("name in @ids", new { ids = new string[] { "4", "5", "6" } });

            PrintExcutor();

            End();


            Start("test delete2 ,test cache is useless? ");

            startWatch();

            connetion.Execute("delete from  animal where name in @ids", new { ids = new string[] { "2", "3", "4" } });
            PrintDapper();

            startWatch();
            excutor.Delete<Animal>("name in @ids", new { ids = new string[] { "4", "5", "6" } });

            PrintExcutor();

            End();


            Start("test query conditional");

            startWatch();

            connetion.Query<Animal>("select * from animal where name=@name", new { name = "99" });

            PrintDapper();

            startWatch();

            excutor.QueryConditional<Animal>("name=@name", new { name = "99" });

            PrintExcutor();



            Start("test select top ");

            startWatch();

          
            excutor.QueryTop<Animal>(20);

            PrintExcutor();

            End();


            Start("test select order by ");

            startWatch();


            var results= excutor.QueryPartialConditionalOrderByAsc<Animal>("age>10",null,"name","name");

            PrintExcutor();

            foreach (var item in results)
            {
                Console.WriteLine(item.Name);
            }

          
            End();





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
