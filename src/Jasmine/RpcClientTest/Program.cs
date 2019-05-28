using Jasmine.Rpc.Client;
using Jasmine.Serialization;
using System;
using System.Threading;

namespace RpcClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            run();
            Console.Read();
        }

       async static void run()
        {

            var client = new RpcClientBuilder().ConfigAdress("127.0.0.1", 10336)
                                               .ConfigUser("001", "001")
                                               .Build();



            await client.Connect();

            while (true)
            {
                var response = await client.Call<string>("/test/say?what=haha");
                Console.WriteLine(response);

                Thread.Sleep(200);
            }

            


        }


     
    }
}
