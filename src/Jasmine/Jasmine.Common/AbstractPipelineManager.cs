using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractPipelineManager<TContext> : IFilterPipelineManager<TContext>
    {
        public AbstractPipelineManager(string name)
        {
            Name = name ?? throw new ArgumentNullException();
        }

        private ConcurrentDictionary<string, IFilterPipeline<TContext>> _internalDic = new ConcurrentDictionary<string, IFilterPipeline<TContext>>();

        public int Count =>_internalDic.Count;

        public string Name { get; }

        public void AddFilterPipeline(string path, IFilterPipeline<TContext> pipeline)
        {
            _internalDic.TryAdd(path, pipeline);
        }

        public bool Contains(string path)
        {
            return _internalDic.ContainsKey(path);
        }

        public IEnumerator<IFilterPipeline<TContext>> GetEnumerator()
        {
            foreach (var item in _internalDic.Values)
            {
                yield return item;
            }
        }

        public IFilterPipeline<TContext> GetPipeline(string path)
        {
            return _internalDic.TryGetValue(path, out var value) ? value : null;
        }

        public void RemoveFilterPipeline(string path)
        {
            _internalDic.TryRemove(path, out var _);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalDic.GetEnumerator();
        }
    }
}
