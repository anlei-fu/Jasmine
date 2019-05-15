using System.Collections.Generic;

namespace Jasmine.Common
{
    public abstract class AbstractFilterPipeline<T> : IFilterPipeline<T>,INameFearture
    {
        public AbstractFilterPipeline()
        {
            _list = new LinkedList<IFilter<T>>();
            _nameMap = new Dictionary<string, LinkedListNode<IFilter<T>>>();
        }

        private LinkedList<IFilter<T>> _list;
        private IDictionary<string, LinkedListNode<IFilter<T>>> _nameMap;

        public abstract string Name { get; }
        public IFilter<T> First =>_list.First.Value;

        public IFilter<T> Last => _list.Last.Value;

        public int Count => _list.Count;
        public void AddAfter(string name, IFilter<T> filter)
        {
             var node= ensureFilterExistsAndGetFilterNode(name);

            _list.AddAfter(node, filter);

        }

        public void AddBefore(string name, IFilter<T> filter)
        {
            var node = ensureFilterExistsAndGetFilterNode(name);

            _list.AddBefore(node, filter);
        }

        public void AddFirst(IFilter<T> filter)
        {
            ensureFilterNotExists(filter.Name);

            _nameMap.Add(filter.Name, _list.AddFirst(filter));

        }

        public void AddLast(IFilter<T> filter)
        {
            ensureFilterNotExists(filter.Name);

            _nameMap.Add(filter.Name, _list.AddFirst(filter));
        }

        public bool Contains(string name)
        {
            return _nameMap.ContainsKey(name);
        }

        private LinkedListNode<IFilter<T>> ensureFilterExistsAndGetFilterNode(string name)
        {
            if (!_nameMap.ContainsKey(name))
                throw new System.Exception();

            return _nameMap[name];
        }
        private void ensureFilterNotExists(string name)
        {
            if (_nameMap.ContainsKey(name))
            {

            }
        }
    }
}
