using GrammerTest.Grammer;
using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GrammerTest
{
    class Program
    {
        static void Main(string[] args)
        {




            testTopBuilder();

            Console.WriteLine("finished!");

            Console.Read();
        }


        static void tsetExcute()
        {
            var pattern = "var c=b+(7+8*8-0)-8*7+9-8+78+9*8;";


            Tokenizer tg = new Tokenizer();
            var watch = new Stopwatch();

            watch.Start();

            for (int i = 0; i < 10000; i++)
            {
                var reader = new SequenceReader<Token>(tg.Tokenize(pattern));


                var builder = new TopBlockBuilder(reader, new TopBlock());
                builder.Build();

                builder.Block.Excute();
            }
            Console.WriteLine($"spend:{watch.ElapsedMilliseconds} ");


            var watch2 = new Stopwatch();

            watch2.Start();
            var a = 7;
            var b = 9;

            for (int i = 0; i < 10000; i++)
            {
                var c = (a+ b * b - a) - a * 7 + a - b + b + b * a;
            }

            Console.WriteLine(watch2.ElapsedMilliseconds);
          
        }

        static void testAst()
        {
            var pattern = "7+8;";

            Tokenizer tg = new Tokenizer();
            var reader = new SequenceReader<Token>(tg.Tokenize(pattern));
            var watch = new Stopwatch();

            watch.Start();

            var builder = new ExpressionBuilder(reader,true,null);


            var result = builder.Build();

            Console.WriteLine($"ast:{watch.ElapsedMilliseconds} ");
        }

        static void testDeclare()
        {
            Tokenizer tg = new Tokenizer();
            var reader = new SequenceReader<Token>(tg.Tokenize("a,b,c=35*7-89-y.x+9;"));
            var watch = new Stopwatch();

            var builder = new DeclareExpressionBuilder(reader,null);


            var result=    builder.Build();

            Console.WriteLine($"declare build spend:{watch.ElapsedMilliseconds} ");
        }

        static void testTopBuilder()
        {

            var pettern = @"
try
{
  a=a()+6;
}
catch(var ex)
{

}
finally
{
}

a=c.b(9*5-9*8)+6>7?b?v:h:d?j:k;

function a()
{
     
}
for(var i=0;i<10;++i)
{
   if(i>9)
      break;
  elif(v<10)
     break;
  else
   continue;
}

while(true)
  ++a[y*7];

do
{
}while(true)

  h=(f()+6)*g;
++i;

foreach(var d in a.getAll())
{
}

++a.b(s,7+8)+b.b.b;
            ";
            var s = new StringBuilder();
            for (int i = 0; i < 50000; i++)
            {
                s .Append( pettern);
            }
            Tokenizer tg = new Tokenizer();
            var reader = new SequenceReader<Token>(tg.Tokenize(s.ToString()));
            var watch = new Stopwatch();
            watch.Start();
            var builder = new TopBlockBuilder(reader,null);

              builder.Build();

            Console.WriteLine($"total length ={s.Length}，top build spend:{watch.ElapsedMilliseconds} ");
        }

        static void testFprBuilder()
        {
            Tokenizer tg = new Tokenizer();
            var reader = new SequenceReader<Token>(tg.Tokenize("(var i=0;i<10;i++;){}"));
            var watch = new Stopwatch();

            var builder = new ForBlockBuilder(reader,null);

            builder.Build();

            Console.WriteLine($"top build spend:{watch.ElapsedMilliseconds} ");
        }


        static void testParenthisis()
        {
            Tokenizer tg = new Tokenizer();


            var pattern = "6*(3+5)-(5*(3-2));";
           
            
            var watch = new Stopwatch();

            watch.Start();

            var t = 0;

            for (int i = 0; i < 10000000; i++)
            {
                var reader = new SequenceReader<Token>(tg.Tokenize(pattern));
                var builder = new AstNodeBuilder(reader,null, new string[] { ";" });
                var result = builder.Build();
                t += pattern.Length;
            }




            Console.WriteLine($"length is {t}");
            Console.WriteLine($"top build spend:{watch.ElapsedMilliseconds} ");
        }


        static void testBlockbuilder()
        {



        }

    }
}
