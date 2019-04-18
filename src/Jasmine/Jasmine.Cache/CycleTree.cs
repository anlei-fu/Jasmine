using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Cache
{
    public class CycleTree<T>:IEnumerable<T>
    {
        public CycleTree()
        {
            _tail = new Node(default(T));
            _head = new Node(default(T));
            Clear();
        }
        protected Node _head;
        protected Node _tail;
        protected Node _current;
        public void Add(T data)
        {
            Add(new Node(data));
        }
        public  void Add(Node node)
        {
            var pre = _tail.Previouce;
            pre.Next = node;
            node.Previouce = pre;
            _tail.Previouce = node;
            node.Next = _tail;
            Count++;
        }

    
        public void Clear()
        {
            _tail.Previouce = _head;
            _tail.Next = _head;
            _head.Next = _tail;
            _head.Previouce = _tail;
            Count = 0;
            _current = _head;
        }
        public  void Remove(Node node)
        {
            if (node.Previouce == null || node.Next == null) //node has not been add into any tree
                return;

            node.Previouce.Next = node.Next;
            node.Next.Previouce = node.Previouce;
            node.Previouce = node.Next = null;
            Count--;

        }
        public int Count { get; private set; }

        public Node Next()
        {
            if (Count == 0)
                return null;

            _current = _current.Next == _tail ? _current.Next.Next : _current.Next;

            return _current;

        }
        public Node PreVious()
        {
            if (Count == 0)
                return null;

            _current = _current.Next == _head ? _current.Previouce.Previouce : _current.Previouce;


            return _current;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public class Node
        {
            public Node(T data)
            {
                Data = data;
            }
            internal Node Previouce { get;set; }
            internal Node Next { get;  set; }
            public T Data { get; set; }

         
        }
    }
}
