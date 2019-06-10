using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Jasmine.DataStructure
{
    public class WeightedCycleTree<T> : IReadOnlyCollection<T>
        where T : ILongId
    {


        private Node _head;
        private Node _tail;
        private Node _current;

        private Dictionary<long, List<Node>> _dic = new Dictionary<long,List<Node>>();

        public int Capacity { get; }

        public int Count => _dic.Keys.Count;

        public bool Add(T data, int weight=1)
        {
            if (_dic.ContainsKey(data.Id))
                return false;

            if (weight < 1)
                throw new ArgumentOutOfRangeException();

            var nodes = new Node[weight];

            for (int i = 0; i < weight; i++)
                nodes[i] = new Node(data);

            _dic.Add(data.Id, nodes.ToList());

            for (int i = 0; i < weight; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    Next();
                }

                _current.Previous.Next = nodes[i];
                nodes[i].Previous = _current.Previous;
                _current.Next.Previous = nodes[i];
                nodes[i].Next = _current.Next;
            }

            return true;
        }

        public T Next()
        {
            if (Count == 0)
                return default(T);

            _current = _current.Next == _tail ? _tail.Next.Next : _current.Next;

            return _current.Data;
        }
        public T Previous()
        {
            if (Count == 0)
                return default(T);

            _current = _current.Previous == _head ? _head.Previous : _head.Previous;

            return _current.Data;


        }
        public bool IncrementWeight(long id, int weight)
        {
            if (!_dic.ContainsKey(id))
                return false;

            if (weight < 0)
                throw new ArgumentOutOfRangeException();

            var nodes = new Node[weight];

            for (int i = 0; i < weight; i++)
            {
                nodes[i] = new Node(_dic[id][0].Data);
            }

            _dic[id].AddRange(nodes);

            for (int i = 0; i < weight; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    Next();
                }

               
                _current.Previous.Next = nodes[i];
                nodes[i].Previous = _current.Previous;
                _current.Next.Previous = nodes[i];
                nodes[i].Next = _current.Next;

            }

            return true;
        }
        public bool DecrementWeight(long id, int weight)
        {
            if (!_dic.ContainsKey(id))
                return false;

            if (weight < 0)
                throw new ArgumentOutOfRangeException();

            var count = weight >= _dic[id].Count ? _dic[id].Count : weight;

            var nodes = new Node[count];

            for (int i = 0; i < count; i++)
            {
                nodes[i] = _dic[id][0];
                _dic[id].RemoveAt(0);
            }

            if (_dic[id].Count == 0)
                _dic.Remove(id);

            for (int i = 0; i < count; i++)
            {
                nodes[i].Previous.Next = nodes[i].Next;
                nodes[i].Next.Previous = nodes[i].Previous;
                nodes[i].Next = nodes[i].Previous = null;
            }


            
            return true;
        }
        public void Remove()
        {

        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }


        private class Node
        {
            public Node(T data)
            {
                Data = data;
            }
           public Node Next { get; set; }
            public Node Previous { get; set; }
           public T Data { get; set; }
        }
    }
}
