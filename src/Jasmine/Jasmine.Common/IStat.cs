using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IMetric:IReadOnlyCollection<IStatItem>
      
    {
        int Avarage { get; }
        int Total { get; }
        int Success { get; }
        int Failed { get; }
        int Fatest { get; }
        int Slowest { get; }
        float FaileRate { get; }
        float SuccesRate { get; }
        DateTime LastCaculateTime { get; }
        void Add(IStatItem item);
        void Caculate();
        void Clear();
    }
}
