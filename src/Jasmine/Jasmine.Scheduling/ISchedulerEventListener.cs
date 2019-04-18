using System;

namespace Jasmine.Scheduling
{
    public  interface ISchedulerEventListener
    {
       void  OnJobScheduled(long id);
        void OnJobBeginExcuting(long id);
        void OnJobFailed(long id,Exception ex);
        void OnJobSuscced(long id);
        void OnSchedulerStarted();
        void OnSchedulerStppped();

    }
}
