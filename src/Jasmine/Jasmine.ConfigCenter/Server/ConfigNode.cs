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

        private INodeEventManager _eventManager;
        public NodeType NodeType { get; set; }
        public string Owner { get; set; }

        public override ITrieNode<byte[]> Create(string name)
        {
            if (Contains(name))
                return null;

            var node = new ConfigNode(name);

            addInternal(node);


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


            return true;

        }
        public override bool SetData(byte[] data)
        {
            _data = data;

            return true;
        }
    }
}
