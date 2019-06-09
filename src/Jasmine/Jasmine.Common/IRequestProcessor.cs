﻿using System.Threading.Tasks;

namespace Jasmine.Common
{
    /// <summary>
    ///  a whole  request processor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRequestProcessor<T> : IPathFearture, IServiceItem
    {
        IDispatcher<T> Dispatcher { get; set; }
        /// <summary>
        /// attached error filter
        /// </summary>
        IFilterPipeline<T> ErrorFilter { get; }
        /// <summary>
        ///  filter pipeline
        /// </summary>
        IFilterPipeline<T> Filter { get; }
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
