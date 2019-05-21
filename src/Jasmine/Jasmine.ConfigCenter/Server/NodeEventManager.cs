using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.ConfigCenter.Server
{
    public class NodeEventManager : INodeEventManager
    {
        private INodeEventHandler _handler;
        private HashSet<ConnectionInfo> _childrenCreatedListeners;
        private HashSet<ConnectionInfo> _dataChangedListeners;
        private HashSet<ConnectionInfo> _nodeRemovedListerns;
        public Task OnChildrenCreated(string path)
        {
            return _handler.OnChildrenCreated(path, _childrenCreatedListeners);
        }

        public Task OnDataChanged(string path)
        {
            return _handler.OnDataChanged(path, _dataChangedListeners);
        }

        public Task OnNodeRemoved(string path)
        {
            return _handler.OnNodeRemoved(path, _nodeRemovedListerns);
        }

        public void RemoveClient(ConnectionInfo client)
        {
            _childrenCreatedListeners.Remove(client);
            _dataChangedListeners.Remove(client);
            _nodeRemovedListerns.Remove(client);
        }

        public bool SubscribeChildrebCreated(ConnectionInfo client)
        {
            if (!_childrenCreatedListeners.Contains(client))
            {
                _childrenCreatedListeners.Add(client);

                return true;
            }

            return false;
        }

        public bool SubscribeDataChanged(ConnectionInfo client)
        {
            if (!_dataChangedListeners.Contains(client))
            {
                _dataChangedListeners.Add(client);

                return true;
            }

            return false;
        }

        public bool SubscribeNodeRemoved(ConnectionInfo client)
        {
            if (!_nodeRemovedListerns.Contains(client))
            {
                _nodeRemovedListerns.Add(client);

                return true;
            }

            return false;
        }

        public bool UnSubscribeNodeRemoved(ConnectionInfo client)
        {
            return _nodeRemovedListerns.Remove(client);
        }

        public bool UnSubscribeChildrenCreated(ConnectionInfo client)
        {
            return _childrenCreatedListeners.Remove(client);
        }

        public bool UnSubscribeDataChnaged(ConnectionInfo client)
        {
            return _dataChangedListeners.Remove(client);
        }
    }
}
