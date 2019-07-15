using System.Threading.Tasks;

namespace Jasmine.Common
{
    public abstract class AbstractDispatcher<TContext> : IDispatcher<TContext>
        where TContext:IFilterContext
    {
        public AbstractDispatcher(string name,IRequestProcessorManager<TContext> processorManager)
        {
            _processorManager = processorManager;
            Name = name;
        }
        protected  IRequestProcessorManager<TContext> _processorManager;

        public string Name { get; }

        public abstract Task DispatchAsync(string path, TContext context);
        
    }
}
