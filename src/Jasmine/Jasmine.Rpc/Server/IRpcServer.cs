using System.Threading.Tasks;

namespace Jasmine.Rpc.Server
{
    public interface IRpcServer
    {
        Task StartAsync();
        Task StopAsync();
    }
}
