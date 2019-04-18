using Jasmine.Common.Trie;
using Jasmine.ConfigCenter.Common;

namespace Jasmine.ConfigCenter.Server
{
    public class ConfigTree:BasicTrieTree<byte[]>
    {
        public NodeOperateResult CreateNode(string path,NodeType type,ConnectionInfo connection)
        {
            if (GetNode(path) != null)
                return NodeOperateResult.AlreadyExist;
            else
            {
                var node = CreateNode(path);

                ((ConfigNode)node).NodeType = type;
            }

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult SubscribeChildrebCreated(string path, ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NotExist;

            ((ConfigNode)node).EventManager.SubscribeChildrebCreated(client);

            return NodeOperateResult.Successced;

        }

        public NodeOperateResult UnSubscribeChildrenCreated(string path,ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NotExist;

            ((ConfigNode)node).EventManager.UnSubscribeChildrenCreated(client);

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult SubscribeDataChanged(string path,ConnectionInfo client)
        {
             var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NotExist;

            ((ConfigNode)node).EventManager.SubscribeChildrebCreated(client);

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult UnSubscribeDataChnaged(string path, ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NotExist;

            ((ConfigNode)node).EventManager.UnSubscribeChildrenCreated(client);

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult SubscribeNodeRemoved(string path,ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NotExist;

            ((ConfigNode)node).EventManager.SubscribeNodeRemoved(client);

            return NodeOperateResult.Successced;
        }

        public NodeOperateResult UnSbscribeNodeRemoved(string path, ConnectionInfo client)
        {
            var node = GetNode(path);

            if (node == null)
                return NodeOperateResult.NotExist;

           ((ConfigNode)node).EventManager.UnSubscribeNodeRemoved(client);

            return NodeOperateResult.Successced;
        }

        
    }
}
