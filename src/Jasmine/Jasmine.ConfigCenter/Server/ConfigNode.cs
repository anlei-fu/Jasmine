using Jasmine.Common.Trie;
using Jasmine.ConfigCenter.Common;

namespace Jasmine.ConfigCenter.Server
{
    public class ConfigNode : AbstracTrieNode<byte[]>
    {
        private byte[] _data;

        public ConfigNode(string name) : base(name)
        {
        }

        public INodeEventManager EventManager { get; }
        public NodeType NodeType { get; set; }
        public string Owner { get; set; }

        public override ITrieNode<byte[]> Create(string name)
        {
            if (Contains(name))
                return null;

            var node = new ConfigNode(name);

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
            EventManager.OnDataChanged(FullPath);

            return true;
        }
    }
}
