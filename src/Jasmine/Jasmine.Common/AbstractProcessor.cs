namespace Jasmine.Common
{
    public class AbstractProcessor<T> : IRequestProcessor<T>
    {
        public IFilter<T> ErrorFilter { get; }

        public IFilter<T> Filter { get; }

        public IMetric<IStatItem> Metric { get; }

        public int MaxConcurrency { get; }

        public bool Available { get; }

        public int Count { get; }

        public string Path { get; }

        public string GroupName { get; set; }

        public string Name { get; set; }
     
    }
}
