using System.Threading.Tasks;

namespace Jasmine.ConfigCenter.Server
{
    public  interface INodeEventNotifier
    {
       

        Task OnChildrenCreated(string path);
        Task OnDataChanged(string path);
        Task OnNodeRemoved(string path);

        void ClearClientEvent(ConnectionInfo client);
    }
}
