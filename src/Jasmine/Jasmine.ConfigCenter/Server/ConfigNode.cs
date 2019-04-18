using Jasmine.Common.Trie;
using Jasmine.ConfigCenter.Common;

namespace Jasmine.ConfigCenter.Server
{
    public class ConfigNode : AbstracTrieNode<byte[]>
    {
        private byte[] _data;
        public INodeEventManager EventManager { get; }
        public NodeType NodeType { get; }
        public string Owner { get; set; }

        public override ITrieNode<byte[]> Create(string name)
        {
            if (Contains(name))
                return null;

            var node = new ConfigNode();

            addInternal(node);

            EventManager.OnChildrenCreated(node.FullPath);

            return node;
        }

        public override byte[] GetData()
        {
           return _data;
        }

        public override bool Remove()
        {
            var path = FullPath;

            if (Children != null)
            {
                foreach (var item in this)
                {
                    item.Remove();
                }
            }

            EventManager.OnNodeRemoved(FullPath);

            return true;

        }

        public override bool SetData( byte[] data)
        {
            _data = data;
            EventManager.OnDataChnaged(FullPath);

            return true;
        }
    }
}
