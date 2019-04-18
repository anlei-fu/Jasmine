using System.Collections.Generic;

namespace Jasmine.Scheduling
{
    public  interface ITimeJobManager:IReadOnlyCollection<ITimeoutJob>
    {
        ITimeoutJob GetJob();
        bool Schecule(ITimeoutJob job);
        bool AdjustTimeout(long jobId, long millionSeconds);
        bool Cancel(long id);
        void Clear();
    }
}
