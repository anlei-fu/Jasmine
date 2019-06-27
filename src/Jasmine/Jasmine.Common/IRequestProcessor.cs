using System.Threading.Tasks;

namespace Jasmine.Common
{
    /// <summary>
    ///  a whole  request processor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRequestProcessor<T> : IPathFearture, IServiceItem
        where T:IFilterContext
    {
        IDispatcher<T> Dispatcher { get; set; }
        /// <summary>
        /// attached error filter
        /// </summary>
        IFilterPipeline<T> ErrorPileline { get; }
        /// <summary>
        ///  filter pipeline
        /// </summary>
        IFilterPipeline<T> Pipeline { get; }
        /// <summary>
        /// service metric
        /// </summary>
        IMetric Metric { get; }
        /// <summary>
        /// 
        /// </summary>
        int MaxConcurrency { get; }
        /// <summary>
        /// 
        /// </summary>
        bool Available { get; }

        Task FiltsAsysnc(T context);

        void SetAvailable(bool available);

        void ResetMaxConcurrency(int concurrency);

        string AlternativeServicePath { get; set; }

        bool HasAlternativeService { get; }
        string Description { get; set; }
        

    }
}
