using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenStreamGenerator tg = new TokenStreamGenerator();

            foreach (var item in tg.GetTokenStream("var a= new object();"))
            {
                Console.WriteLine(item.TokenType + "   " + item.Value + "   " + item.OperatorType.ToString());
            }

            Console.Read();
        }
    }
}
