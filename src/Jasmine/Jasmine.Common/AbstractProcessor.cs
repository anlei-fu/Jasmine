namespace Jasmine.Common
{
    public class AbstractProcessor<T> : IRequestProcessor<T>
    {
        public string Name { get; }
        public IFilterPipeline<T> ErrorFilter { get; internal set; }

        public IFilterPipeline<T> Filter { get; internal set; }

        public IMetric Metric { get; }

        public int MaxConcurrency { get; }

        public bool Available { get; }

        public string Path { get; }

        public string GroupName { get; }
       
    }
}
