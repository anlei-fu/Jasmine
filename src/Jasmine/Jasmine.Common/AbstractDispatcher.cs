using System.Threading.Tasks;

namespace Jasmine.Common
{
    public abstract class AbstractDispatcher<T> : IDispatcher<T>
    {
        public AbstractDispatcher(string name,IRequestProcessorManager<T> processorManager)
        {
            _processorManager = processorManager;
            Name = name;
        }
        protected  IRequestProcessorManager<T> _processorManager;

        public string Name { get; }

        public abstract Task DispatchAsync(string path, T context);
        
    }
}
