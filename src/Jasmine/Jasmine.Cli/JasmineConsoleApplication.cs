using Jasmine.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Cli
{
 public  class JasmineConsoleApplication
    {

        public const string LIST = "ls";
        public const string EXIT = "exit";
        public const string VERSION = "v+ersion";+gbv
        public void Run()
        {
            Console.WriteLine("Welcome to Jasmine Cli !");

            while (true)
            {
                var line = Console.ReadLine();


                var words = line.ToWords();


                if (words.Count == 0)
                    continue;

                if (words.Count == 1 && words[0] == "exit")
                    break;

            }
        }
    }
}
