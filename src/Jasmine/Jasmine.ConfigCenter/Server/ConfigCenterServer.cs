using System.Threading.Tasks;

namespace Jasmine.ConfigCenter.Server
{
    public  class ConfigCenterServer
    {
       public  Task StartAsync()
        {
            return Task.CompletedTask;
        }
        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
