using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class FilterPipeline<TContext> : IRequestProcessor<TContext>
    {
        public IFilter<TContext> ErrorFilter { get; internal set; }

        public IFilter<TContext> Filter { get; internal set; }

        public IMetric<IStatItem> Metric { get; internal set; }

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
