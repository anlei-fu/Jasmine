using log4net;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jasmine.Common
{
    public abstract class AbstractProcessor<T> : IRequestProcessor<T>
    {
        public AbstractProcessor(int maxConcurrency, string name)
        {
            MaxConcurrency = maxConcurrency;
            Name = name;
        }

        public AbstractProcessor()
        {
        }

        private ILog _logger;

        private int _currentConcurrency;

        public int CurrentConcurrency => _currentConcurrency;

        private bool _available=true;

        private readonly object _locker = new object();

        public string Name { get; set; }
        [JsonIgnore]
        public IFilterPipeline<T> ErrorPileline { get;  set; }
        [JsonIgnore]
        public IFilterPipeline<T> Pipeline { get;  set; }

        public IMetric Metric { get; } = new Metric();

        public int MaxConcurrency { get; set; }

        public bool Available
        {
            get
            {
               return  _available && _currentConcurrency < MaxConcurrency;
            }
        }

        public string Path { get; set; }

        public string GroupName { get; set; }
        public string AlternativeServicePath { get; set; }

        public bool HasAlternativeService => AlternativeServicePath != null;

        [JsonIgnore]
        public IDispatcher<T> Dispatcher { get; set; }
        public string Description { get; set; }

        public async Task FiltsAsysnc(T context)
        {

            Interlocked.Increment(ref _currentConcurrency);

            var timeStart = DateTime.Now;

            var item = new StatItemBase();

            try
            {

                await Pipeline.First.FiltsAsync(context).ConfigureAwait(false);

                item.Sucessed = true;

            }
            catch (Exception ex)
            {
                item.Sucessed = false;
                _logger?.Error(ex);

                await ErrorPileline.First.FiltsAsync(context).ConfigureAwait(false);


            }
            finally
            {
                item.Elapsed = (int)(DateTime.Now - timeStart).TotalMilliseconds;
                item.Time = timeStart.ToString();
                Metric.Add(item);

                Interlocked.Decrement(ref _currentConcurrency);
            }
        }

        public void SetAvailable(bool available)
        {
            lock (_locker)
                _available = available;
        }

        public void ResetMaxConcurrency(int concurrency)
        {
            lock (_locker)
                MaxConcurrency = concurrency;
        }
    }
}
