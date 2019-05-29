namespace Jasmine.ConfigCenter.Server
{
    public  class NodeStore:INodeEventManager
    {
        private ConfigTree _tree;

        public void RemoveClient(ConnectionInfo client)
        {
            throw new System.NotImplementedException();
        }

        public bool SubscribeChildrebCreated(ConnectionInfo client)
        {
            throw new System.NotImplementedException();
        }

        public bool SubscribeDataChanged(ConnectionInfo client)
        {
            throw new System.NotImplementedException();
        }

        public bool SubscribeNodeRemoved(ConnectionInfo client)
        {
            throw new System.NotImplementedException();
        }

        public bool UnSubscribeChildrenCreated(ConnectionInfo client)
        {
            throw new System.NotImplementedException();
        }

        public bool UnSubscribeDataChnaged(ConnectionInfo client)
        {
            throw new System.NotImplementedException();
        }

        public bool UnSubscribeNodeRemoved(ConnectionInfo client)
        {
            throw new System.NotImplementedException();
        }
    }
}
