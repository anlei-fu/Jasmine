using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jasmine.Scheduling
{
    public class JasmineTimeoutScheduler : ITimeOutScheduler,ISchedulerStat
    {
        public JasmineTimeoutScheduler(int maxConccurency=4,int capacity=1000000,int sleepInterval=10)
        {
            MaxConcurrency = maxConccurency;
            _jobManager = new TimeoutJobManager(capacity);
            SleepInterval = sleepInterval;
        }


        private ITimeJobManager _jobManager;
        private int _jobSchuduled;
        private int _jobUnExcute;
        private int _jobExcuting;
        private int _jobExcuted;
     
        private int _jobSuccessed;
        private int _jobFailed;
  
        private readonly object _locker = new object();
        public int MaxConcurrency { get; }

        public int JobExcuted => _jobExcuted;

        public int JobScheduled => _jobSchuduled;

        public int JobExcuting => _jobExcuting;

        public int SleepInterval { get; }

        public int JobFailed => _jobFailed;

        public int JobSuccessed => _jobSuccessed;

        public int Count => _jobManager.Count;

        public SchedulerState State { get; private set; } = SchedulerState.Stopped;

        public int JobUnExcute => _jobUnExcute;

        public DateTime? StartTime { get; private set; }

        public DateTimeOffset? RunningTime { get; private set; }

        public int Capacity { get; }

        public bool AdjustTimeout(long id, long timeout)
        {
          return  _jobManager.AdjustTimeout(id, timeout);
        }

        public bool Cancel(long id)
        {
            return _jobManager.Cancel(id);
        }

        public IEnumerator<ITimeoutJob> GetEnumerator()
        {
            foreach (var item in _jobManager)
            {
                yield return item;
            }
        }

        public bool Schecule(ITimeoutJob job)
        {
            if( _jobManager.Schecule(job))
            {
                ((AbstractTimeoutJob)job).Scheduled = true;
                ((AbstractTimeoutJob)job).Scheduler = this;

                Interlocked.Increment(ref _jobSchuduled);
                Interlocked.Increment(ref _jobUnExcute);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Start()
        {
            lock (_locker)
            {
                if (State!=SchedulerState.Stopped)
                {
                    return false;
                }
                else
                {
                    doWorkLoop();

                    return true;
                }
           }
        }

        public bool Stop(bool waitForAllJobsComplete)
        {
            lock (_locker)
            {
                if (State != SchedulerState.Running)
                    return false;
                else
                {
                    State = SchedulerState.Stopping;
                }
            }

            if(waitForAllJobsComplete)
            {
                while (_jobExcuting != 0) ;
            }

            State = SchedulerState.Stopped;

            return true;
        }

        //need find a new way to replace sleep(),it's not good
        private void doWorkLoop()
        {
            lock (_locker)
                State = SchedulerState.Running;

            while (State==SchedulerState.Running)
            {
                if(_jobExcuting<MaxConcurrency&&State==SchedulerState.Running)
                {
                    var job = _jobManager.GetJob();

                    if(job==null)
                    {
                        Thread.Sleep(SleepInterval);
                    }
                    else
                    {

                        Task.Run(() =>
                        {
                            try
                            {
                                Interlocked.Decrement(ref _jobUnExcute);
                                ((AbstractTimeoutJob)job).JobState = JobState.Excuting;

                                Interlocked.Increment(ref _jobExcuting);

                                job.Excute();
                                Interlocked.Increment(ref _jobSuccessed);
                            }
                            catch (Exception ex)
                            {
                                Interlocked.Increment(ref _jobFailed);
                            }
                            finally
                            {
                                ((AbstractTimeoutJob)job).JobState = JobState.Excuted;
                                Interlocked.Decrement(ref _jobExcuted);
                            }

                        });
                    }
                }
                else
                {
                    Thread.Sleep(SleepInterval);
                }


            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _jobManager.GetEnumerator();
        }

        public ITimeoutJob GetJob()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _jobManager.Clear();
        }
    }
}
