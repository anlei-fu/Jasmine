using System.Collections.Generic;

namespace Jasmine.Common
{
    /// <summary>
    ///  a whole  request processor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  interface IRequestProcessor<T>:IPathFearture,IGroupItem
    {
      
        /// <summary>
        /// attached error filter
        /// </summary>
        IFilterPipeline<T> ErrorFilter { get;  }
        /// <summary>
        ///  filter pipeline
        /// </summary>
        IFilterPipeline<T> Filter { get;  }
        /// <summary>
        /// service metric
        /// </summary>
        IMetric Metric { get;  }
        /// <summary>
        /// 
        /// </summary>
        int MaxConcurrency { get; }
        /// <summary>
        /// 
        /// </summary>
        bool Available { get; }
       
    
    }
}
