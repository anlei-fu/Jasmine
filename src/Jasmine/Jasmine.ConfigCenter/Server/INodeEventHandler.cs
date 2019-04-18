using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.ConfigCenter.Server
{
    public  interface INodeEventHandler
    {
        Task OnDataChanged(string path, IEnumerable<ConnectionInfo> clients);
        Task OnChildrenCreated(string path, IEnumerable<ConnectionInfo> clients);
        Task OnNodeRemoved(string path, IEnumerable<ConnectionInfo> clients);
    }
}
