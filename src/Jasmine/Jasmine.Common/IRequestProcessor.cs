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
        IFilter<T> ErrorFilter { get;  }
        /// <summary>
        ///  filter pipeline
        /// </summary>
        IFilter<T> Filter { get;  }
        /// <summary>
        /// service metric
        /// </summary>
        IMetric<IStatItem> Metric { get;  }
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
