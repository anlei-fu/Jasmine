using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class RestfulApplication
    {
        private RestfulApplication()
        {
           
        }

        private readonly object _locker = new object();
        private bool _running = false;


        private IWebHost _server;

        public async Task<bool> StartAsync()
        {
            lock(_locker)
            {
                if (_running)
                    return false;

                _running = true;
            }

            _server = WebHost.CreateDefaultBuilder().Build();

            await _server.RunAsync();

            return true;

        }
        public  async Task<bool> StopAsync()
        {
            lock (_locker)
            {
                if (_running)
                    return false;

                _running = false;
            }

            await _server.StopAsync();

            return true;
        }


    }
}
