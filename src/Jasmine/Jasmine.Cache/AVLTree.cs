using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Collections.Generic
{
    /// <summary>
    /// balanced binary tree which's  a datastructure  can insert and search fast
    /// it keeps a lower height than ordinary binary tree 
    /// and it sorted as binary tree 
    /// in this implementation ,it can insert repeat value that means it can not  use to build 
    /// a dictionary structure,it's not a thead safe structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class AvlTree<T> : IEnumerable<T>, ISerializable
    {
        public AvlTree()
        {
            _compare = Comparer<T>.Default;
        }
        public AvlTree(IComparer<T> comparer)
        {
            this._compare = comparer;
        }
        protected AvlTree(SerializationInfo info, StreamingContext context)
        {
            _root = fromArray((T[])info.GetValue("data", typeof(T[])));
            _compare = (IComparer<T>)info.GetValue("comparer", typeof(IComparer<T>));
        }

        private Node _root;
        private IComparer<T> _compare;

        public static AvlTree<T> FromArray(T[] array, IComparer<T> compare = null)
        {
            if (compare == null)
                compare = Comparer<T>.Default;

            var tree = new AvlTree<T>(compare);

            tree._root = fromArray(array);

            return tree;
        }
        private static Node fromArray(T[] arr)
        {
            return fromArray(arr, 0, arr.Length);
        }
        private static Node fromArray(T[] arr, int index, int len)
        {
            if (len == 0)
                return null;

            Node re = new Node(arr[index + (len >> 1)]) { Size = len };

            if ((re.Left = fromArray(arr, index, len >> 1)) != null)
                re.Left.Parent = re;

            if ((re.Right = fromArray(arr, index + (len >> 1) + 1, len - (len >> 1) - 1)) != null)
                re.Right.Parent = re;

            return re;
        }

        /// <summary>
        /// min data node
        /// </summary>
        public Node MinNode => getMinNode();
        /// <summary>
        /// max data node
        /// </summary>
        public Node MaxNode => getMaxNode();
        /// <summary>
        /// min data
        /// </summary>
        public T Min => getMinNode().Data;
        /// <summary>
        /// max data
        /// </summary>
        /// <returns></returns>
        public T Max() => getMaxNode().Data;
        public bool IsEmpty => _root == null;
        public int Count => IsEmpty ? 0 : _root.Size;

        /// <summary>
        /// it will create a new node ,and insert at proper position
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Node Add(T v)
        {
            Node ret = null;

            if (_root == null)
            {
                ret = new Node(v);
                _root = ret;
            }
            else
                ret = add(ref _root, v);

            return ret;
        }
        public void AddRange(T[] array)
        {
            if (_root == null)
            {
                _root = fromArray(array);

                return;
            }

            foreach (var a in array)
                Add(a);
        }
        public void Clear()
        {
            _root = null;
        }
        public void CopyTo(T[] array, int index)
        {
            var i = 0;

            foreach (var a in this)
                array[index + i++] = a;
        }
        /// <summary>
        ///  contains any <see cref="T"/> value
        ///  will retun true
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Contains(T v)
        {
            return findNode(v) != null;
        }
        /// <summary>
        /// the count of value <see cref="v"/>
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int CountOf(T v)
        {
            var t = GetPositionDescOf(v);

            return t == -1 ? 0 : GetPositionAscOf(v) - t + 1;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("data", ToArray());
            info.AddValue("comparer", _compare);
        }

        /// <summary>
        /// remove a node,remove by <see cref="Object.ReferenceEquals(object, object)"/> 
        /// </summary>
        /// <param name="node"></param>
        public void Remove(Node node)
        {
            remove(ref _root, node);
        }
        /// <summary>
        /// remove all value of<see cref="v"/>
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Remove(T v)
        {
            if (!Contains(v))
                return false;

            remove(ref _root, v);

            return true;
        }
        public T[] ToArray()
        {
            var array = new T[Count];
            var index = 0;
            copyTo(array, ref index, _root);

            return array;
        }
        /// <summary>
        /// get the first position of <see cref="v"/> in Desc Order
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int GetPositionDescOf(T v)
        {
            return getPositionDesc(findMinNode(v));
        }
        /// <summary>
        /// get the first position of<see cref=""/> in Asc Order
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int GetPositionAscOf(T v)
        {
            return getPositionDesc(fidnMaxNode(v));
        }

        public T GetDataAtPositionDesc(int rank)
        {
            if (rank > Count || rank < 1)
                throw new ArgumentOutOfRangeException("rank must between 1 to count", "rank");

            return getNodeAt(_root, rank).Data;
        }
        /// <summary>
        /// the cloest value near  <see cref="v"/> which is smaller than <see cref="v"/>
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public T GetClosestDataSmallerThan(T v)
        {
            var node = getCloestDataSmallerThan(findMinNode(_root, v));

            return node == null ? default(T) : node.Data;
        }

        /// <summary>
        /// the cloest value near  <see cref="v"/> which is bigger than <see cref="v"/>
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public T GetCloestDataBiggerThan(T v)
        {
            var note = getCloestBiggerThan(findMaxNode(_root, v));

            return note == null ? default(T) : note.Data;
        }
        public IEnumerator<T> GetEnumerator() { return new SBT_Enumrator(this, true); }
        IEnumerator IEnumerable.GetEnumerator() { return new SBT_Enumrator(this, true); }
        public IEnumerable AsReverse() { return new SBT_R_EnumeraDev(this); }


        private void copyTo(T[] array, ref int index, Node node)
        {
            if (node == null)
                return;

            copyTo(array, ref index, node.Left);

            array[index++] = node.Data;

            copyTo(array, ref index, node.Right);
        }
        private bool contains(Node node)
        {
            while (node.Parent != null)
                node = node.Parent;

            return ReferenceEquals(_root, node);
        }
        private Node add(ref Node node, T v)
        {
            ++node.Size;

            Node ret = null;
            if (_compare.Compare(v, node.Data) <= 0)
            {
                if (node.left == null)
                {
                    ret = new Node(v);
                    node.left = ret;
                    node.left.Parent = node;
                }
                else
                {
                    ret = add(ref node.left, v);
                }

                Node.Maintain(ref node, false);

            }
            else
            {
                if (node.right == null)
                {
                    ret = new Node(v);
                    node.right = ret;
                    node.right.Parent = node;
                }
                else
                {
                    ret = add(ref node.right, v);
                }

                Node.Maintain(ref node, true);
            }

            return ret;
        }
        private T remove(ref Node node, Node toRemove)
        {
            node.Size -= 1;
            var result = _compare.Compare(toRemove.Data, node.Data);

            if ((result == 0 && ReferenceEquals(node, toRemove)) || (result < 0 && node.Left == null) || (result > 0 && node.Right == null))
            {
                var data = node.Data;

                if (node.Left == null)
                    node = node.Right;
                else if (node.Right == null)
                    node = node.Left;
                else
                    node.Data = remove(ref node.left, node.Right);

                return data;
            }
            else if (result < 0)
            {
                return remove(ref node.left, toRemove);
            }
            else
            {
                return remove(ref node.right, toRemove);
            }
        }
        private T remove(ref Node node, T v)
        {
            node.Size -= 1;
            var result = _compare.Compare(v, node.Data);

            if (result == 0 || (result < 0 && node.Left == null) || (result > 0 && node.Right == null))
            {
                var data = node.Data;

                if (node.Left == null)
                    node = node.Right;
                else if (node.Right == null)
                    node = node.Left;
                else
                    node.Data = remove(ref node.left, node.Right.Data);

                return data;
            }
            else if (result < 0)
            {
                return remove(ref node.left, v);
            }
            else
            {
                return remove(ref node.right, v);
            }
        }
        private Node findNode(Node node, T v)
        {
            if (node == null)
                return null;

            var result = _compare.Compare(v, node.Data);

            if (result == 0)
                return node;

            if (result < 0)
                return findNode(node.Left, v);
            else
                return findNode(node.Right, v);
        }
        private Node findNode(T v)
        {
            return findNode(_root, v);
        }
        private Node findMinNode(Node node, T v)
        {
            if (node == null)
                return null;

            var result = _compare.Compare(v, node.Data);

            if (result == 0)
            {
                var temp = node;

                while (temp.Left != null && _compare.Compare(temp.Left.Data, v) == 0)
                    temp = temp.Left;

                return temp;
            }

            if (result < 0)
                return findNode(node.Left, v);
            else
                return findNode(node.Right, v);
        }
        private Node findMinNode(T v)
        {
            return findMinNode(_root, v);
        }
        private Node findMaxNode(Node node, T v)
        {
            if (node == null)
                return null;

            var result = _compare.Compare(v, node.Data);

            if (result == 0)
            {
                Node re = node;
                while (re.Right != null && _compare.Compare(re.Right.Data, v) == 0) re = re.Right;
                return re;
            }

            if (result < 0)
                return findNode(node.Left, v);
            else
                return findNode(node.Right, v);
        }
        private Node fidnMaxNode(T v)
        {
            return findMaxNode(_root, v);
        }
        private int getPositionDesc(Node node)
        {
            if (node == null)
                return -1;

            int pos;

            if (node.Left == null)
                pos = 1;
            else
                pos = node.Left.Size + 1;

            while (node.Parent != null)
            {
                if (ReferenceEquals(node.Parent.Right, node))
                    pos += node.Parent.Size - node.Size;

                node = node.Parent;
            }

            return pos;
        }
        private Node getNodeAt(Node node, int pos)
        {
            if (node == null)
                return null;

            var result = pos - ((node.Left == null) ? 0 : node.Left.Size) - 1;

            if (result == 0)
                return node;
            else if (result < 0)
                return getNodeAt(node.Left, pos);
            else
                return getNodeAt(node.Right, pos - ((node.Left == null) ? 0 : node.Left.Size) - 1);
        }
        private Node getCloestDataSmallerThan(Node node)
        {
            if (node.Left != null)
            {
                Node result = node.Left;

                while (result.Right != null)
                    result = result.Right;

                return result;
            }
            else
            {
                Node re = node;

                while (re.Parent != null && !ReferenceEquals(re.Parent.Right, re))
                    re = re.Parent;

                return re.Parent;
            }
        }
        private Node getCloestBiggerThan(Node node)
        {
            if (node.Right != null)
            {
                Node re = node.Right;

                while (re.Left != null)
                    re = re.Left;

                return re;
            }
            else
            {
                Node re = node;

                while (re.Parent != null && !Object.ReferenceEquals(re.Parent.Left, re))
                    re = re.Parent;

                return re.Parent;
            }
        }
        private Node getMinNode()
        {
            if (_root == null)
                return null;

            var node = _root;

            while (node.Left != null)
                node = node.Left;

            return node;
        }
        private Node getMaxNode()
        {
            if (_root == null)
                return null;

            var node = _root;

            while (node.Right != null)
                node = node.Right;

            return node;
        }


        [Serializable]
        public sealed class Node : IEquatable<Node>, ISerializable
        {
            public Node()
            {
            }
            public Node(T data)
            {
                Data = data; Size = 1;
            }
            private Node(SerializationInfo info, StreamingContext context)
            {
                Data = (T)info.GetValue("Data", typeof(T));
                Left = (Node)info.GetValue("Left", typeof(Node));
                Right = (Node)info.GetValue("Right", typeof(Node));
                Parent = (Node)info.GetValue("Father", typeof(Node));
                Size = info.GetInt32("Size");
            }
            internal Node left;
            internal Node right;

            public T Data { get; internal set; }
            public int Size { get; internal set; }
            public Node Left
            {
                get
                {
                    return left;
                }
                internal set
                {
                    left = value;
                }
            }
            public Node Right
            {
                get
                {
                    return right;
                }
                internal set
                {
                    right = value;
                }
            }
            public Node Parent { get; internal set; }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                info.AddValue("Data", Data);
                info.AddValue("Left", Left);
                info.AddValue("Right", Right);
                info.AddValue("Father", Parent);
                info.AddValue("Size", Size);
            }
            public static void RightRotate(ref Node node)
            {
                Node tempNode = node.Left;

                if ((node.Left = tempNode.Right) != null)
                    tempNode.Right.Parent = node;

                node.Size += (tempNode.Right == null ? 0 : tempNode.Right.Size) - tempNode.Size;
                tempNode.Right = node; tempNode.Parent = node.Parent; node.Parent = tempNode;
                tempNode.Size = tempNode.Right.Size + (tempNode.Left == null ? 0 : tempNode.Left.Size) + 1;
                node = tempNode;
            }
            public static void LeftRotate(ref Node node)
            {
                Node T = node.Right;

                if ((node.Right = T.Left) != null)
                    T.Left.Parent = node;

                node.Size += (T.Left == null ? 0 : T.Left.Size) - T.Size;
                T.Left = node; T.Parent = node.Parent; node.Parent = T;
                T.Size = T.Left.Size + (T.Right == null ? 0 : T.Right.Size) + 1;
                node = T;
            }


            public static void Maintain(ref Node node, bool maintain_right)
            {
                if (node == null)
                {
                    return;
                }

                if (maintain_right)
                {
                    if (node.Right == null)
                    {
                        return;
                    }

                    int lsize;

                    if (node.Left == null)
                    {
                        lsize = 0;
                    }
                    else
                    {
                        lsize = node.Left.Size;
                    }

                    if (node.Right.Right != null && node.Right.Right.Size > lsize)
                    {
                        Node.LeftRotate(ref node);
                    }
                    else if (node.Right.Left != null && node.Right.Left.Size > lsize)
                    {
                        RightRotate(ref node.right);
                        LeftRotate(ref node);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (node.Left == null)
                    {
                        return;
                    }

                    int rsize;

                    if (node.Right == null)
                    {
                        rsize = 0;
                    }
                    else
                    {
                        rsize = node.Right.Size;
                    }



                    if (node.Left.Left != null && node.Left.Left.Size > rsize)
                    {
                        RightRotate(ref node);
                    }
                    else if (node.Left.Right != null && node.Left.Right.Size > rsize)
                    {
                        LeftRotate(ref node.left);
                        RightRotate(ref node);
                    }
                    else
                    {
                        return;
                    }
                }

                Maintain(ref node.left, false);
                Maintain(ref node.right, true);
                Maintain(ref node, false);
                Maintain(ref node, true);
            }

            public bool Equals(Node node)
            {
                return Data.Equals(node.Data);
            }
            public bool Equals(T data)
            {
                return Data.Equals(data);
            }
            public override bool Equals(object obj)
            {
                if (obj is T)
                    return Equals((T)obj);
                else if (obj is Node)
                    return Equals((Node)obj);
                return false;
            }
            public override int GetHashCode()
            {
                return Data.GetHashCode();
            }
            public override string ToString()
            {
                return Data.ToString();
            }
        }
        public struct SBT_Enumrator : IEnumerator, IEnumerator<T>
        {
            internal SBT_Enumrator(AvlTree<T> from, bool forward)
            {
                current_SBT = from;
                this.forward = forward;
                current_Node = current_SBT.getMinNode();
            }
            private bool forward;
            private Node current_Node;
            private AvlTree<T> current_SBT;
            public T Current { get { return current_Node.Data; } }
            object IEnumerator.Current { get { return Current; } }
            public bool MoveNext()
            {
                if (current_Node == null) return false;
                if (!current_SBT.contains(current_Node)) throw new InvalidOperationException("已经对SBT集合进行了修改，并且Current已经被删除");
                Node re;
                if (forward)
                    re = current_SBT.getCloestBiggerThan(current_Node);
                else
                    re = current_SBT.getCloestDataSmallerThan(current_Node);
                if (re == null) return false;
                current_Node = re;
                return true;
            }
            public void Reset()
            {
                current_Node = current_SBT.getMinNode();
            }
            public void Dispose()
            {
                current_SBT = null;
                current_Node = null;
            }
        }
        public struct SBT_R_EnumeraDev : IEnumerable<T>
        {
            internal SBT_R_EnumeraDev(AvlTree<T> from) { current_SBT = from; }
            AvlTree<T> current_SBT;
            public IEnumerator<T> GetEnumerator() { return new SBT_Enumrator(current_SBT, false); }
            IEnumerator IEnumerable.GetEnumerator() { return new SBT_Enumrator(current_SBT, false); }
        }




    }
}