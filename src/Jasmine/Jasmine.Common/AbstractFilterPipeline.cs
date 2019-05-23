using System.Collections;
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
        public IFilterPiplelineBuilder<T> AddAfter(string name, IFilter<T> filter)
        {
             var node= ensureFilterExistsAndGetFilterNode(name);

            _list.AddAfter(node, filter);

            return this;

        }

        public IFilterPiplelineBuilder<T> AddBefore(string name, IFilter<T> filter)
        {
            var node = ensureFilterExistsAndGetFilterNode(name);

            _list.AddBefore(node, filter);

            return this;
        }

        public IFilterPiplelineBuilder<T> AddFirst(IFilter<T> filter)
        {
            ensureFilterNotExists(filter.Name);

            _nameMap.Add(filter.Name, _list.AddFirst(filter));

            return this;

        }

        public IFilterPiplelineBuilder<T> AddLast(IFilter<T> filter)
        {
            ensureFilterNotExists(filter.Name);

            _nameMap.Add(filter.Name, _list.AddFirst(filter));

            return this;
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

        public IFilterPiplelineBuilder<T> Remove(string name)
        {
            if(_nameMap.TryGetValue(name,out var value))
            {
                _list.Remove(value);
            }

            return this;
        }

        public IEnumerator<IFilter<T>> GetEnumerator()
        {
            foreach (var item in _nameMap.Values)
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nameMap.Values.GetEnumerator();
        }
    }
}
