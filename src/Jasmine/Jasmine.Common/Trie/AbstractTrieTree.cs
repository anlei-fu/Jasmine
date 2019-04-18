using System;
using System.Collections.Generic;

namespace Jasmine.Common.Trie
{
    public abstract class BasicTrieTree<T> :ITrieTree<T>
    {
        private ITrieNode<T> _root;

        public int Count => throw new NotImplementedException();

        public int Height => throw new NotImplementedException();

        public ITrieNode<T> GetNode(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var nodes = path.Split('/');

            var node = _root;

            for (int i = 0; i < nodes.Length - 1; i++)
            {
                if (node.Contains(nodes[i]))
                {
                    node = node.Children[nodes[i]];
                }
                else
                {
                    return null;
                }
            }

            return node;
        }
        public T GetData(string path)
        {
            var node = GetNode(path);

            return node==null?default(T):node.GetData();
        }
        public ITrieNode<T> CreateNode(string path)
        {
            var node = GetNode(path);

            if (node != null)
                return null;

            var nodes = path.Split('/');

            node = _root;

            for (int i = 0; i < nodes.Length - 1; i++)
            {
                if (node.Contains(nodes[i]))
                {
                    node = node.Children[nodes[i]];
                }
                else
                {
                    node = node.Create(nodes[i]);

                }
            }


            var newNode = node.Create(nodes[nodes.Length - 1]);

            return newNode;
        }
        public bool RemoveNode(string path)
        {
            var node = GetNode(path);

            if (node == null)
                return false;

            node.Remove();

            return true;
        }
        public  bool SetData(string path, T data)
        {
            var node = GetNode(path);

            if (node == null)
                return false;

            node.SetData(data);

            return true;
        }

        public List<ITrieNode<T>> GetAllNode()
        {
            var ls = new List<ITrieNode<T>>();

            return ls;
        }
    }
}
