using Jasmine.Reflection;
using System;
using System.Threading.Tasks;

namespace Jasmine.Common
{
    /// <summary>
    /// proxy a method invokation ,in a filter pipeline 
    /// </summary>
    /// <typeparam name="T"> context type</typeparam>
    public abstract class AbstractInvokationProxyFilter<T> : AbstractFilter<T>
    {
        public AbstractInvokationProxyFilter(Method method, IRequestParamteterResolver<T> resolver, object instance) 
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(method));
            _method = method ?? throw new ArgumentException(nameof(resolver));
            _resolver = resolver ?? throw new ArgumentException(nameof(instance));
        }
        private object _instance;
        private Method _method;
        private IRequestParamteterResolver<T> _resolver;


        public override Task FiltsAsync(T context)
        {

            var parameters =_method.HasParameter?_resolver.Resolve(context)
                                                :Array.Empty<object>();
          
            var result = _method.Invoke(_instance, parameters);
          
            afterInvoke(context, result);

            return HasNext ? Next.FiltsAsync(context) : Task.CompletedTask;
        }

        /// <summary>
        ///  leave a intercept interface to do something after method susccessfully invoked
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_return"> invoke result</param>
        protected abstract void afterInvoke(T context, object _return);

    }
}
