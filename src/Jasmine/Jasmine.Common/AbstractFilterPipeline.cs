using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public abstract class AbstractFilterPipeline<TContext> : IFilterPipeline<TContext>, INameFearture
        where TContext:IFilterContext
    {
        public AbstractFilterPipeline()
        {
            _list = new LinkedList<IFilter<TContext>>();
            _nameMap = new Dictionary<string, LinkedListNode<IFilter<TContext>>>();
        }

        private LinkedList<IFilter<TContext>> _list;

        private IDictionary<string, LinkedListNode<IFilter<TContext>>> _nameMap;

        public abstract string Name { get; }
        public IFilter<TContext> First => _list.First?.Value;

        public IFilter<TContext> Last => _list.Last?.Value;

        public int Count => _list.Count;

        public IRequestProcessor<TContext> Processor { get; set; }

        public IFilterPiplelineBuilder<TContext> AddAfter(string name, IFilter<TContext> filter)
        {
            var node = ensureFilterExistsAndGetFilterNode(name);

            var next = node.Next;

            _nameMap.Add(filter.Name, _list.AddAfter(node, filter));

            return this;

        }

        public IFilterPiplelineBuilder<TContext> AddBefore(string name, IFilter<TContext> filter)
        {
            var node = ensureFilterExistsAndGetFilterNode(name);

            var pre = node.Previous;

            _nameMap.Add(filter.Name, _list.AddBefore(node, filter));

            return this;
        }

        public IFilterPiplelineBuilder<TContext> AddFirst(IFilter<TContext> filter)
        {
            ensureFilterNotExists(filter.Name);

            _nameMap.Add(filter.Name, _list.AddFirst(filter));

            return this;

        }

        public IFilterPiplelineBuilder<TContext> AddLast(IFilter<TContext> filter)
        {
            ensureFilterNotExists(filter.Name);

            _nameMap.Add(filter.Name, _list.AddLast(filter));

            return this;
        }

        public bool Contains(string name)
        {
            return _nameMap.ContainsKey(name);
        }

        private LinkedListNode<IFilter<TContext>> ensureFilterExistsAndGetFilterNode(string name)
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

        public IFilterPiplelineBuilder<TContext> Remove(string name)
        {
            if (_nameMap.TryGetValue(name, out var value))
            {
                _list.Remove(value);
            }

            return this;
        }

        public IEnumerator<IFilter<TContext>> GetEnumerator()
        {
            foreach (var item in _list)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
