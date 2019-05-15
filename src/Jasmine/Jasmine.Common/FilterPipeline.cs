using System;

namespace Jasmine.Common
{
    public class FilterPipeline<TContext> : IRequestProcessor<TContext>
    {
        public IFilter<TContext> ErrorFilter => throw new NotImplementedException();

        public IFilter<TContext> Filter => throw new NotImplementedException();

        public IMetric<IStatItem> Metric => throw new NotImplementedException();

        public int MaxConcurrency => throw new NotImplementedException();

        public bool Available => throw new NotImplementedException();

        public string Path => throw new NotImplementedException();

        public string GroupName => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();
    }
}
