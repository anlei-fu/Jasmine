using System.Collections.Generic;

namespace Jasmine.Common.Trie
{
    public interface ITrieNode<T>:INameFearture,IEnumerable<ITrieNode<T>>
    {
        IDictionary<string, ITrieNode<T>> Children { get; }
        ITrieNode<T> Parent { get; set; }
        int Depth { get; }
        string FullPath { get; }

        bool Contains(string name);


       ITrieNode<T> Create(string name);

        bool Remove();

        T GetData();

        bool SetData( T data);
       
    }
}
