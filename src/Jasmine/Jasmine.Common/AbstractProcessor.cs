namespace Jasmine.Common
{
    public abstract class AbstractProcessor<T> : IRequestProcessor<T>
    {
        public  string Name { get; set; }
        public IFilterPipeline<T> ErrorFilter { get;  set; }

        public IFilterPipeline<T> Filter { get;  set; }

        public IMetric Metric { get; internal set; }

        public int MaxConcurrency { get;  set; }

        public bool Available { get; set; } = true;

        public string Path { get;  set; }

        public string GroupName { get;  set; }
       
    }
}
