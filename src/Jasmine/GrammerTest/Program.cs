﻿using Jasmine.Interpreter.AstTree;
using Jasmine.Interpreter.AstTree.Builders;
using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;
using Jasmine.Interpreter.TypeSystem;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace GrammerTest
{
    class Program
    {
        static void Main(string[] args)
        {




            testSpeed();

            Console.WriteLine("finished!");

            Console.Read();
        }

        static void testSpeed()
        {
            var t = 100000;
            var str = new JString("");

            var dic = new Dictionary<AstNode, Any>();
           
            var node = new OperandNode(new JString(""));
            dic.Add(node, str);
            for (int j = 0; j < 20; j++)
            {
                var watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < t; i++)
                {
                    var k =(string)str;
                }
                

                var h = watch.ElapsedMilliseconds;

                Console.WriteLine("new instance elapsed:" + h);


                var queue = new ConcurrentQueue<JString>();
                for (int i = 0; i < t; i++)
                {
                    dic.Add(new OperandNode(null), null);
                }

                var watch2 = new Stopwatch();
                watch2.Start();
               
                for (int i = 0; i < t; i++)
                {
                    var k = dic[node];
                }

                h = watch2.ElapsedMilliseconds;
                Console.WriteLine("queue elapsed:" + h);
            }

           

        }

        class Animal
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public string Say()
            {
                return $"I'm a {Name}!";
            }

            public int Add()
            {
                return ++Age;
            }
        }
        static void testMapping()
        {
          var pattern = "var f; function k(){var t=0; while(false) t=t;}    function a(){ var t=0; while(t<1) ++t; return t>3; } /*a();*/ var c=b.Name; var d=b.Age; b.Age=15; /*for(var i=0;i<100;++i){ d=b.Age;b.Age=i; b.Name=\"cat\"; c=b.Say();b.Add(); f=a(); k();}*/";

            Tokenizer tg = new Tokenizer();
            var watch = new Stopwatch();
            var reader = new SequenceReader<Token>(tg.Tokenize(pattern));

            var block = new TopBlock();
            var p = new Animal { Name = "dog", Age = 10 };
            block.Declare("b", new JMappingObject(p));
            var builder = new TopBlockBuilder(reader, block);
            builder.Build();

            watch.Start();
            builder.Block.Excute(null);
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.WriteLine("c is " + block.GetVariable("c"));
            Console.WriteLine("d is " + block.GetVariable("d"));
            Console.WriteLine("f is " + block.GetVariable("f"));
            Console.WriteLine(p.Age);
            Console.WriteLine(p.Name);
            Console.Read();

        }

        static void tsetExcute()
        {
            var pattern = " var d=\"hello\"+\" world!\"; var b=d==\"hello world!\";var f=d+\" just for test! \";  var a=15; function add(b,c){ var t=0; for(var i=b;i<c;++i) t=t+i*5;   return t;}  var c= add(1,100000); ";


            Tokenizer tg = new Tokenizer();
            var watch = new Stopwatch();

          

            for (int i = 0; i < 100000; i++)
            {
                var reader = new SequenceReader<Token>(tg.Tokenize(pattern));

                var block = new TopBlock();

                var builder = new TopBlockBuilder(reader, block);
                builder.Build();

                watch.Start();
                builder.Block.Excute(null);

                Console.WriteLine(watch.ElapsedMilliseconds);
                Console.WriteLine("a is " + block.GetVariable("a"));
                Console.WriteLine("c is " + block.GetVariable("c").ToString());
                Console.WriteLine("b is " + block.GetVariable("b").ToString());
               Console.WriteLine("d is " + block.GetVariable("d"));
                Console.WriteLine("f is " + block.GetVariable("f"));
                Console.Read();
            }
            Console.WriteLine($"spend:{watch.ElapsedMilliseconds} ");


            var watch2 = new Stopwatch();

            watch2.Start();
            var a = 7;
            var b = 9;

            for (int i = 0; i < 10000; i++)
            {
                var c = (a + b * b - a) - a * 7 + a - b + b + b * a;
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

            var builder = new ExpressionBuilder(reader, true, null);


            var result = builder.Build();

            Console.WriteLine($"ast:{watch.ElapsedMilliseconds} ");
        }

        static void testDeclare()
        {
            Tokenizer tg = new Tokenizer();
            var reader = new SequenceReader<Token>(tg.Tokenize("a,b,c=35*7-89-y.x+9;"));
            var watch = new Stopwatch();

            var builder = new DeclareExpressionBuilder(reader, null);


            var result = builder.Build();

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
                s.Append(pettern);
            }
            Tokenizer tg = new Tokenizer();
            var reader = new SequenceReader<Token>(tg.Tokenize(s.ToString()));
            var watch = new Stopwatch();
            watch.Start();
            var builder = new TopBlockBuilder(reader, null);

            builder.Build();

            Console.WriteLine($"total length ={s.Length}，top build spend:{watch.ElapsedMilliseconds} ");
        }

        static void testFprBuilder()
        {
            Tokenizer tg = new Tokenizer();
            var reader = new SequenceReader<Token>(tg.Tokenize("(var i=0;i<10;i++;){}"));
            var watch = new Stopwatch();

            var builder = new ForBlockBuilder(reader, null);

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
                var builder = new AstNodeBuilder(reader, null, new string[] { ";" });
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
