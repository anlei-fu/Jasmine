using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common.Trie
{
    public abstract class AbstracTrieNode<T> : ITrieNode<T>
    {
        public string Name { get; }

        public int Depth
        {
            get
            {
                var node = this;
                var depth = 1;

                while (node.Parent!=null)
                {
                    node = node.Parent;
                    ++depth;
                }

                return depth;

            }
        }

        public string FullPath
        {
            get
            {
                var path = "/"+Name;
                var node = this;

                while(node.Parent!=null)
                {
                    path = path +"/"+ node.Parent.Name;
                }

                return path;
            }
        }
        public IDictionary<string, ITrieNode<T>> Children { get; } = new ConcurrentDictionaryIDictonaryAdapter<string, ITrieNode<T>>();

        public AbstracTrieNode<T> Parent { get ; internal set ; }

        public bool Contains(string name)
        {
            return Children.ContainsKey(name);
        }

        public abstract ITrieNode<T> Create(string name);


        public abstract T GetData();
        
       

        public abstract bool Remove();


        public abstract bool SetData( T data);

        protected bool removeInternal(string name)
        {
            return Children.Remove(name);
        }

        protected bool addInternal(ITrieNode<T> node)
        {
            Children.Add(node.Name, node);

            return true;
        }
       

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Children.Values.GetEnumerator();
        }

        IEnumerator<ITrieNode<T>> IEnumerable<ITrieNode<T>>.GetEnumerator()
        {
            foreach (var item in Children.Values)
            {
                yield return item;
            }
        }
    }
}
