using Jasmine.Scheduling.Exceptions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Jasmine.Scheduling
{
    public abstract class AbstractScheduler<T> : IScheduler<T>
        where T : Job
    {
        public AbstractScheduler(IJobManager<T> jobmanager, int maxConcurrency =0)
        {
            MaxConcurrency = maxConcurrency==0?DEFAULT_CONCURRENCY:maxConcurrency;
            _jobManager = jobmanager;

        }

        private static readonly int DEFAULT_CONCURRENCY = Environment.ProcessorCount * 2;
        private readonly object _locker = new object();
        private volatile int _jobExcuted;
        private volatile int _jobScheduled;
        private volatile int _jobExcuting;
        private volatile int _jobUnExcute;
        private volatile int _jobFailed;
        private volatile int _jobSuccessed;
        private volatile int _threadRunning;

        private ConcurrentDictionary<long, Job> _currentExcutingJobs = new ConcurrentDictionary<long, Job>();

        private  bool _isSleeping;

        protected IJobManager<T> _jobManager;

        /// <summary>
        /// cacapcity
        /// </summary>
        public int Capacity => _jobManager.Capacity;
        /// <summary>
        /// count of job excuted
        /// </summary>
        public int JobExcuted => _jobExcuted;
        public int JobScheduled => _jobScheduled;
        public int JobExcuting => _jobExcuting;
        public int JobUnExcute => _jobUnExcute;
        public int JobFailed => _jobFailed;
        public int JobSuccessed => _jobSuccessed;
        public int Count => _jobManager.Count;
        public int MaxConcurrency { get; }
        public SchedulerState State { get; private set; } = SchedulerState.Stopped;
        public DateTime? StartTime { get; private set; }
        public TimeSpan? RunningTime => StartTime == null 
                                        ? null : (TimeSpan?)(DateTime.Now - (DateTime)StartTime);
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
                    for (int i = 0; i < MaxConcurrency; i++)
                    {
                        var tr = new Thread(doWorkLoop);

                        tr.Start();

                        Interlocked.Increment(ref _threadRunning);
                    }
                  
                    return true;
                }
            }
        }

        public bool Stop(bool waitForAllJobComplete=false)
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

        public IEnumerable<Job> GetExcutingJobs()
        {
            return _currentExcutingJobs.Values.ToArray();
        }

        public bool Schedule(T job)
        {
            if (_jobManager.Schedule(job))
            {
                job.State = JobState.Scheduled;
                
             
                lock(_locker)
                {
                    if(_isSleeping)//会唤醒所有线程,有点浪费,这里还要优化，只需要唤醒一个
                    {
                        _isSleeping = false;
                      
                        Monitor.PulseAll(_locker);
                    }
                }

                setScheduler(job);
                job.ScheduledTime = DateTime.Now;
                Interlocked.Increment(ref _jobScheduled);
                Interlocked.Increment(ref _jobUnExcute);
                Listener?.OnJobScheduled(job);

                return true;
            }

            return false;
        }

       

        public int GetNextJobExcutingTimeout()
        {
            return _jobManager.GetNextJobExcutingTimeout();
        }
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
        protected abstract void setScheduler(T job);

        private void doWorkLoop()
        {

            while (State == SchedulerState.Running)
            {
                var job = GetJob();
                //no job to run 
                if (job == null)
                {
                    lock (_locker)
                    {
                        _isSleeping = true;

                        //block some time, the thread will keep running when wait timeout or new job scheduled
                        Monitor.Wait(_locker, GetNextJobExcutingTimeout());
                    }
                }
                else
                {
                        try
                        {
                            Interlocked.Decrement(ref _jobUnExcute);

                            job.State = JobState.Excuting;

                            Interlocked.Increment(ref _jobExcuting);

                            Listener?.OnJobBeginExcuting(job);

                           _currentExcutingJobs.TryAdd(job.Id, job);

                            job.Excute();

                            job.State = JobState.CompleteSuccessfully;

                            Interlocked.Increment(ref _jobSuccessed);

                            Listener?.OnJobExcuteSusccefully(job);
                        }
                        catch (Exception ex)
                        {
                            Interlocked.Increment(ref _jobFailed);

                            job.State = JobState.CompleteFailed;

                            job.Error = new JobExcuteException(job.Id, ex);

                            Listener?.OnJobEcuteFailed(job);
                        }
                        finally
                        {

                        _currentExcutingJobs.TryRemove(job.Id, out var _);
                            Interlocked.Decrement(ref _jobExcuting);

                            Interlocked.Increment(ref _jobExcuted);

                        }
                }

            }


            Interlocked.Decrement(ref _threadRunning);
        }

       
    }
}
