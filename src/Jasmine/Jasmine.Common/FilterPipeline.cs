using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class FilterPipeline<TContext> : IFilterPipeline<TContext>
    {
        public IFilter<TContext> Error { get; internal set; }

        public IFilter<TContext> Root { get; internal set; }

        public IStat<IStatItem> Stat { get; internal set; }

        public int Count { get; internal set; }

        public string Path  { get; internal set; }

        public IEnumerator<IFilter<TContext>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
