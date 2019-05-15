using System.Threading.Tasks;

namespace Jasmine.Common
{
    public abstract class AbstractDispatcher<T> : IDispatcher<T>
    {
        public AbstractDispatcher(string name,IRequestProcessorManager<T> pipelineManager)
        {
            _pipelineManager = pipelineManager;
            Name = name;
        }
        protected  IRequestProcessorManager<T> _pipelineManager;

        public string Name { get; }

        public abstract Task DispatchAsync(string path, T context);
        
    }
}
