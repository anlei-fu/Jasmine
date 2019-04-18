using System.Threading.Tasks;

namespace Jasmine.Common
{
    public abstract class AbstractDispatcher<T> : IDispatcher<T>
    {
        public AbstractDispatcher(string name,IFilterPipelineManager<T> pipelineManager)
        {
            _pipelineManager = pipelineManager;
            Name = name;
        }
        protected  IFilterPipelineManager<T> _pipelineManager;

        public string Name { get; }

        public abstract Task DispatchAsync(string path, T context);
        
    }
}
