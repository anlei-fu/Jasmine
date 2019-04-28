using GrammerTest.Grammer;
using GrammerTest.Grammer.AstTreeBuilders;
using Jasmine.Spider.Grammer;
using System;
using System.Diagnostics;

namespace GrammerTest
{
    class Program
    {
        static void Main(string[] args)
        {

            testParenthisis();

            Console.WriteLine("finished!");

            Console.Read();
        }



        static void testDeclare()
        {
            TokenStreamGenerator tg = new TokenStreamGenerator();
            var reader = new TokenStreamReader(tg.GetTokenStream("a,b,c=35*7-89-y.x+9;"));
            var watch = new Stopwatch();

            var builder = new DeclareExpressionBuilder(reader);


            var result=    builder.Build();

            Console.WriteLine($"declare build spend:{watch.ElapsedMilliseconds} ");
        }

        static void testTopBuilder()
        {
            TokenStreamGenerator tg = new TokenStreamGenerator();
            var reader = new TokenStreamReader(tg.GetTokenStream("function a(){a+b;}"));
            var watch = new Stopwatch();

            var builder = new TopScopeBuilder(reader);

              builder.Build();

            Console.WriteLine($"top build spend:{watch.ElapsedMilliseconds} ");
        }

        static void testFprBuilder()
        {
            TokenStreamGenerator tg = new TokenStreamGenerator();
            var reader = new TokenStreamReader(tg.GetTokenStream("(var i=0;i<10;i++;){}"));
            var watch = new Stopwatch();

            var builder = new ForBlockBuilder(reader);

            builder.Build();

            Console.WriteLine($"top build spend:{watch.ElapsedMilliseconds} ");
        }


        static void testParenthisis()
        {
            TokenStreamGenerator tg = new TokenStreamGenerator();


            var pattern = "6*(3+5)-(5*(3-2));";
           
            
            var watch = new Stopwatch();

            watch.Start();

            var t = 0;

            for (int i = 0; i < 10000000; i++)
            {
                var reader = new TokenStreamReader(tg.GetTokenStream(pattern));
                var builder = new AstNodeBuilder(reader, new string[] { ";" });
                var result = builder.Build();
                t += pattern.Length;
            }




            Console.WriteLine($"length is {t}");
            Console.WriteLine($"top build spend:{watch.ElapsedMilliseconds} ");
        }
    }
}
