using System;

namespace Jasmine.Scheduling
{
    public  interface ISchedulerEventListener
    {
       void  OnJobScheduled(long id);
        void OnJobBeginExcuting(long id);
        void OnJobEcuteFailed(long id,Exception ex);
        void OnJobExcuteSusccefully(long id);
        void OnSchedulerStarted();
        void OnSchedulerStppped();

    }
}
