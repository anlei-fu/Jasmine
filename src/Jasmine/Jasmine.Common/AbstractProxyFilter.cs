using Jasmine.Reflection;
using System;
using System.Threading.Tasks;

namespace Jasmine.Common
{
    /// <summary>
    /// proxy a method invokation ,in a filter pipeline 
    /// </summary>
    /// <typeparam name="T"> context type</typeparam>
    public abstract class AbstractProxyFilter<T> : AbstractFilter<T>
    {
        public AbstractProxyFilter(Method method, IRequestParamteterResolver<T> resolver, object instance, string name) : base(name)
        {
            _instance = instance ?? throw new ArgumentNullException();
            _method = method ?? throw new ArgumentException();
            _resolver = resolver ?? throw new ArgumentException();
        }
        private object _instance;
        private Method _method;
        private IRequestParamteterResolver<T> _resolver;


        public override Task FiltsAsync(T context)
        {
            
            var parameters = _resolver.Resolve(context);
          
            var result = _method.Invoke(_instance, parameters);
          
            afterInvoke(context, result);

            if (Next != null)
                return Next.FiltsAsync(context);
            else
                return Task.CompletedTask;
        }

        /// <summary>
        ///  leave a interceptor , after method susccessfully invoked
        /// </summary>
        /// <param name="context"></param>
        /// <param name="_return"> invoke result</param>
        protected abstract void afterInvoke(T context, object _return);

    }
}
