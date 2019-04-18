using System;
using Jasmine.Scheduling;

namespace SchedulerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sc = new JasmineTimeoutScheduler(new TimeoutJobManager(100000));

            sc.Schedule(new EchoJob(DateTime.Now.AddMilliseconds(10000)));
            sc.Schedule(new EchoJob(DateTime.Now.AddMilliseconds(10000)));
            sc.Schedule(new EchoJob(DateTime.Now.AddMilliseconds(20000)));
            sc.Schedule(new EchoJob(DateTime.Now.AddMilliseconds(30000)));
            sc.Schedule(new EchoJob(DateTime.Now.AddMilliseconds(40000)));
            sc.Start();

            Console.Read();
        }

        class EchoJob : TimeoutJob
        {
            public EchoJob(DateTime time) : base(time)
            {
            }

            public override void Excute()
            {
                Console.WriteLine($"my id is {Id}, i scheduled run at {ScheduledExcutingTime}, and i'running now at {DateTime.Now} ");
            }
        }
    }
}
