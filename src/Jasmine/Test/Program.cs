using Jasmine.Orm;
using Jasmine.Orm.Attributes;
using Jasmine.Orm.Implements;
using Jasmine.Orm.Model;
using Jasmine.Reflection;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var services = new ServiceCollection();

            //services.ServiceGroups = new List<ServiceGroup>();

            //services.ServiceGroups.Add(new ServiceGroup() { Name = "sss", Domain = "http://www.baidu.com", Method = "POST" });
            //services.ServiceGroups[0].Services = new List<Service>();
            //services.ServiceGroups[0].Services.Add(new Service() { Name = "WWWW" });

            //XmlSerializer xs = new XmlSerializer(typeof(ServiceCollection));
            //using (var fs=new FileStream("test.txt",FileMode.Create,FileAccess.Write))
            //{
            //    xs.Serialize(fs,services);
            //}

            //var cache = new DefaultReflectionCache();

            //cache.Cache(typeof(SaySomething));


            //SaySomething obj = new SaySomething();

            //var method=typeof(SaySomething).GetMethod("Say",new Type[]{typeof(Int32),typeof(int) });
            //var type = typeof(SaySomething);
            //const int time = 100000;
            //var invoker = cache.GetItem(type).Methods.GetItemByName("Say_Int32_Int32").Invoker;

            //Console.WriteLine($" speed test between reflect, reflect with ilgenerator and direct call method ,call time :{time} ");

            //var watch = Stopwatch.StartNew();
            //watch.Start();
            //for (int i = 0; i < time; i++)
            //{
            //    method.Invoke(obj, new object[] { 2, 5 });

            //}

            //Console.WriteLine($" reflect  call  tiem spend {watch.ElapsedMilliseconds} ");



            // watch = Stopwatch.StartNew();

            //watch.Start();
            //for (int i = 0; i < time; i++)
            //{
            //    invoker.Invoke(obj, new object[] { 2, 5 });

            //}

            //Console.WriteLine($" reflect with ilgenerator call  tiem spend {watch.ElapsedMilliseconds} ");

            //watch = Stopwatch.StartNew();

            //for (int i = 0; i < time; i++)
            //{
            //    obj.Say(2, 5);
            //}

            //Console.WriteLine($" direct call time spend:{watch.ElapsedMilliseconds} ");


            var table = "animal_table_3";
            JasmineOrmXmlConfig.LoadConfig("orm.config");

            var excutor = new JasmineSqlExcutor(null);

            var builder= new SqlBuilder(DataSourceType.SqlServer);
            var provider = new SqlConnectionProvider("sql server", @"server =HW09; database = Test; integrated security = SSPI");


            

            //excutor.Excute(builder.CreateTable<Animal>(table).Build(), provider);
           // excutor.Excute(builder.Insert(new Animal() { Age = 45, Name = "cat", Sex = 0 }, table).Build(), provider);

            var animals = new Animal[]
            {
                new Animal{Name="fish",Age=15,Sex=null },
                new Animal {Name="butterfly",Age=0,Sex=45 }
            };

            excutor.Excute(builder.Insert<Animal>(animals, table).Build(), provider);


            foreach (var item in excutor.Query(builder.Select<Animal>(table).Where("Name").Like("f%").And("Age").Bigger(9).Build(), provider))
            {
                foreach (var item1 in item)
                {
                    Console.WriteLine(item1);
                }
            }



           //  excutor.Excute(builder.Insert<Animal>(new Animal[] { new Animal {Name="monkey" },new Animal() {Name="donkey" } },"table1").Build(), provider);
           // var result1= excutor.Query<Animal>(builder.Select<Animal>("table1").Build(), provider);

            //Console.WriteLine(builder.SelectDistinct<Animal>("table1").Where(new Expression("age",14,">")).Build());
                   

           // excutor.Query<InstanceData>(builder.Build(),null);
            


              var sb = new StringBuilder();
            foreach (var item in BaseTypes.Base)
            {
                sb.Append(item.FullName + "\r\n");
            }

            File.WriteAllText("type.text", sb.ToString());

            var template = " select * from {table} where id>23 and {name}>12";

            var watch = Stopwatch.StartNew();
            watch.Start();
            var result = DefaultTemplateParser.Instance.Parse(template);

            Console.WriteLine($"elapsed :{watch.ElapsedMilliseconds}");

            foreach (var item in result)
            {
                Console.WriteLine(item.Value);
            }
            DefaultTemplateConvertor.Instance.Convert(template, new { table = "table1", name = "jk" });
            watch = Stopwatch.StartNew();
            watch.Start();

            var buildresult = DefaultTemplateConvertor.Instance.Convert(template, new { table = "11", name = "jk" });

            Console.WriteLine($" elapsed:{watch.ElapsedMilliseconds}");

            Console.WriteLine(buildresult);

            Console.Read();


        }



        class SaySomething
        {
            public int Say(int t,int k)
            {

                // while (t++ < 1000000) ;

                return 0;
            }

            public void Run()
            {
                Console.WriteLine("running");
            }
        }
    }


    public class Animal
    {

       
        public int Age { get; set; }
        [NotNull]
        public int? Sex { get; set; }
        [NotNull]
        [SqlColumnTypeAttribute("char(12)")]
       
        public string Name { get; set; }
    }
}
