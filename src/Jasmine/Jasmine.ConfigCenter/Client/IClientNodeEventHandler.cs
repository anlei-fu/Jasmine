using System.Threading.Tasks;

namespace Jasmine.ConfigCenter.Client
{
    public  interface IClientNodeEventHandler
    {
        Task OnDataChanged(string path);
        Task OnChildrenCreated(string path);
        Task OnNodeRemoved(string path);
    }
}
