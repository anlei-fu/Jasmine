using Jasmine.ConfigCenter.Common;
using System;

namespace Jasmine.ConfigCenter.Server
{
    public class ConfigTree
    {
        private ConfigNode _root;
       public ConfigNode GetNode(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var nodes = path.Split('/');

            var node = _root;

            foreach (var item in nodes)
            {
                 
            }

            return null;
        }
        public byte[] GetData(string path)
        {
            var node = GetNode(path);

            return node?.GetData();
        }
      
        public NodeOperateResult CreateNode(string path,NodeType type,ConnectionInfo connection)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            return NodeOperateResult.Successced;
        }
        public NodeOperateResult RemoveNode(string path)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            node.Remove();

            return NodeOperateResult.Successced;
        }
        public NodeOperateResult SetData(string path,byte[] data)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            node.SetData(data);

            return NodeOperateResult.Successced;
        }
        

        public NodeOperateResult SubscribeChildrebCreated(string path, ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            node.EventManager.SubscribeChildrebCreated(client);

            return NodeOperateResult.Successced;

        }

        public NodeOperateResult UnSubscribeChildrenCreated(string path,ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            node.EventManager.UnSubscribeChildrenCreated(client);

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult SubscribeDataChanged(string path,ConnectionInfo client)
        {
             var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            node.EventManager.SubscribeChildrebCreated(client);

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult UnSubscribeDataChnaged(string path, ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            node.EventManager.UnSubscribeChildrenCreated(client);

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult SubscribeNodeRemoved(string path,ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            node.EventManager.SubscribeNodeRemoved(client);

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult UnSbscribeNodeRemoved(string path, ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NodeNotExists;

            node.EventManager.UnSubscribeNodeRemoved(client);

            return NodeOperateResult.Successced;
        }

        
    }
}
