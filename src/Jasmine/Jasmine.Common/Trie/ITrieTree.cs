using System.Collections.Generic;

namespace Jasmine.Common.Trie
{
    public interface ITrieTree<T>
    {
        ITrieNode<T> GetNode(string path);
        ITrieNode<T> CreateNode(string path);
        bool RemoveNode(string path);
        T GetData(string path);
        bool SetData(string path, T data);
        List<ITrieNode<T>> GetAllNode();
        int Count { get; }
        int Height { get; }
    }
}
