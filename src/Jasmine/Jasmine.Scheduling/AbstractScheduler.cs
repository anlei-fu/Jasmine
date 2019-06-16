using Jasmine.Scheduling.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jasmine.Scheduling
{
    public abstract class AbstractScheduler<T> : IScheduler<T>
        where T : Job
    {
        public AbstractScheduler(IJobManager<T> jobmanager, int maxConcurrency =0)
        {
            MaxConcurrency = maxConcurrency==0?DEFAULT_CONCURRENCY:maxConcurrency;
            _concurrencyLock = new SemaphoreSlim(MaxConcurrency);
            _jobManager = jobmanager;

        }
        protected IJobManager<T> _jobManager;

        private static readonly int DEFAULT_CONCURRENCY = Environment.ProcessorCount * 2;

        private SemaphoreSlim _concurrencyLock;
        private readonly object _locker = new object();
        private int _jobExcuted;
        private int _jobScheduled;
        private int _jobExcuting;
        private int _jobUnExcute;
        private int _jobFailed;
        private int _jobSuccessed;
        private bool _isSleeping;

        public int Capacity => _jobManager.Capacity;
        public int MaxConcurrency { get; }
        public SchedulerState State { get; private set; } = SchedulerState.Stopped;
        public DateTime? StartTime { get; private set; }
        public TimeSpan? RunningTime => StartTime == null ? null : (TimeSpan?)(DateTime.Now - (DateTime)StartTime);
        public int JobExcuted => _jobExcuted;
        public int JobScheduled => _jobScheduled;
        public int JobExcuting => _jobExcuting;
        public int JobUnExcute => _jobUnExcute;
        public int JobFailed => _jobFailed;
        public int JobSuccessed => _jobSuccessed;
        public int Count => _jobManager.Count;

        public ISchedulerEventListener Listener { get; set; }

        public bool Start()
        {
            lock (_locker)
            {
                if (State != SchedulerState.Stopped)
                {
                    return false;
                }
                else
                {
                    //a long run task ,use a thread which use by iteself only ,do not get the thread from default-task-scheduler's thread pool

                    Task.Factory.StartNew(doWorkLoop, TaskCreationOptions.LongRunning);
                  
                    return true;
                }
            }
        }

        public bool Stop(bool waitForAllJobComplete)
        {
            lock (_locker)
            {
                if (State != SchedulerState.Running)
                {
                    return false;
                }
                else
                {
                    State = SchedulerState.Stopping;
                }
            }

            if (waitForAllJobComplete)
            {
                while (JobExcuting != 0) ;
            }

            State = SchedulerState.Stopped;

            Listener?.OnSchedulerStopped();

            return true;
        }

   

        public bool Cancel(long id)
        {
            if (_jobManager.Cancel(id))
            {
                Interlocked.Decrement(ref _jobUnExcute);

                return true;
            }

            return false;
        }

        public void Clear()
        {
            _jobManager.Clear();

            _jobUnExcute = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T GetJob()
        {
           return _jobManager.GetJob();
        }

        public bool Schedule(T job)
        {
            if (_jobManager.Schedule(job))
            {
                job.State = JobState.Scheduled;
                
                // set job's scheduler by sub-class ,to solve cast exception
                setScheduler(job);

                job.ScheduledTime = DateTime.Now;

                Interlocked.Increment(ref _jobScheduled);
                Interlocked.Increment(ref _jobUnExcute);

                Listener?.OnJobScheduled(job.Id);
             
                lock(_locker)
                {
                    if(_isSleeping)
                    {
                        _isSleeping = false;
                        
                        //  awake  work-loop thread
                        Monitor.PulseAll(_locker);
                    }
                }

                return true;
            }

            return false;
        }

        protected abstract void setScheduler(T job);
     

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _jobManager)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _jobManager.GetEnumerator();
        }
        private void doWorkLoop()
        {
            lock (_locker)
                State = SchedulerState.Running;

            while (State == SchedulerState.Running)
            {
                var job = GetJob();

                if (job == null)//no job to run 
                {
                    lock (_locker)
                    {
                        _isSleeping = true;

                        Monitor.Wait(_locker, GetNextJobExcutingTimeout());//sleep some time, the thread will keep running when wait timeout or new job scheduled
                    }
                }
                else
                {
                    //control max cocurrency
                    _concurrencyLock.Wait();

                    Task.Run(() =>
                    {
                        try
                        {
                            Interlocked.Decrement(ref _jobUnExcute);

                            job.State = JobState.Excuting;

                            Interlocked.Increment(ref _jobExcuting);

                            Listener?.OnJobBeginExcuting(job.Id);

                            job.Excute();

                            job.State = JobState.CompleteSuccessfully;

                            Interlocked.Increment(ref _jobSuccessed);

                            Listener?.OnJobExcuteSusccefully(job.Id);
                        }
                        catch (Exception ex)
                        {
                            Interlocked.Increment(ref _jobFailed);

                            job.State = JobState.CompleteFailed;

                            job.Error = new JobExcuteException(job.Id, ex);

                            Listener?.OnJobEcuteFailed(job.Id, ex);
                        }
                        finally
                        {

                            Interlocked.Decrement(ref _jobExcuting);

                            Interlocked.Increment(ref _jobExcuted);

                            _concurrencyLock.Release();
                        }

                    });
                }

            }

        }

        public int GetNextJobExcutingTimeout()
        {
            return _jobManager.GetNextJobExcutingTimeout();
        }
    }
}
