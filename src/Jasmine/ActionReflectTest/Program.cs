using System;

namespace ActionReflectTest
{
    class Program
    {
        static void Main(string[] args)
        {

           var test = new test();

            var t = test.GetType();
        }

        public class test
        {
            public void Test(Action act)
            {

            }
            public void write()
            {

            }
        }
    }
}
