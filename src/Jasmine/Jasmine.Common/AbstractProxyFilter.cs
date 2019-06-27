using Jasmine.Reflection;
using System;
using System.Threading.Tasks;

namespace Jasmine.Common
{
    /// <summary>
    /// proxy a method invokation ,in a filter pipeline 
    /// </summary>
    /// <typeparam name="TContext"> context type</typeparam>
    public abstract class AbstractInvokationProxyFilter<TContext> : AbstractFilter<TContext>
        where TContext:IFilterContext
    {
        public AbstractInvokationProxyFilter(Method method, IRequestParamteterResolver<TContext> resolver, object instance) 
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(method));
            _method = method ?? throw new ArgumentException(nameof(resolver));
            _resolver = resolver ?? throw new ArgumentException(nameof(instance));
        }
        private object _instance;
        private Method _method;
        private IRequestParamteterResolver<TContext> _resolver;


        public override Task<bool> FiltsAsync(TContext context)
        {

            var parameters =_method.HasParameter?_resolver.Resolve(context)
                                                :Array.Empty<object>();
          
            var result = _method.Invoke(_instance, parameters);
          
            afterInvoke(context, result);

            return  Task.FromResult(true);
        }

        /// <summary>
        ///  left a intercept interface to do something after method susccessfully invoked
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_return"> invoke result</param>
        protected abstract void afterInvoke(TContext context, object _return);

    }
}
