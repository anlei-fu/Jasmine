using System.Threading.Tasks;

namespace Jasmine.Common
{
    /// <summary>
    ///  a whole  request processor
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IRequestProcessor<TContext> : IPathFearture, IServiceItem
        where TContext:IFilterContext
    {
        IDispatcher<TContext> Dispatcher { get; set; }
        /// <summary>
        /// error work flow
        /// </summary>
        IFilterPipeline<TContext> ErrorPileline { get; }
        /// <summary>
        ///  work flow
        /// </summary>
        IFilterPipeline<TContext> Pipeline { get; }
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

        Task ProcessAsysnc(TContext context);

        void SetAvailable(bool available);

        void ResetMaxConcurrency(int maxConcurrency);

        string AlternativeServicePath { get; set; }

        bool HasAlternativeService { get; }
        string Description { get; set; }
        

    }
}
