using System;
using System.Threading.Tasks;

namespace Jasmine.ConfigCenter.Server
{
    public class NodeEventNotifier : INodeEventNotifier
    {
        public void ClearClientEvent(ConnectionInfo client)
        {
            throw new NotImplementedException();
        }

        public Task OnChildrenCreated(string path)
        {
            throw new NotImplementedException();
        }

        public Task OnDataChanged(string path)
        {
            throw new NotImplementedException();
        }

        public Task OnNodeRemoved(string path)
        {
            throw new NotImplementedException();
        }

        public void RemoveClient(ConnectionInfo client)
        {
            throw new NotImplementedException();
        }

        public bool SubscribeChildrebCreated(ConnectionInfo client)
        {
            throw new NotImplementedException();
        }

        public bool SubscribeDataChanged(ConnectionInfo client)
        {
            throw new NotImplementedException();
        }

        public bool SubscribeNodeRemoved(ConnectionInfo client)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeChildrenCreated(ConnectionInfo client)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeDataChnaged(ConnectionInfo client)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeNodeRemoved(ConnectionInfo client)
        {
            throw new NotImplementedException();
        }
    }
}
