﻿using System.Threading.Tasks;

namespace Jasmine.ConfigCenter.Server
{
    public  interface INodeEventManager
    {
        bool SubscribeChildrebCreated(ConnectionInfo client);
        bool UnSubscribeChildrenCreated(ConnectionInfo client);
        bool SubscribeDataChanged(ConnectionInfo client);
        bool UnSubscribeDataChnaged(ConnectionInfo client);
        bool SubscribeNodeRemoved(ConnectionInfo client);
        bool UnSubscribeNodeRemoved(ConnectionInfo client);
        void RemoveClient(ConnectionInfo client);

        Task OnChildrenCreated(string path);
        Task OnDataChanged(string path);
        Task OnNodeRemoved(string path);

        void ClearClientEvent(ConnectionInfo client);
    }
}
