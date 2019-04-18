using System;
using System.Threading.Tasks;
using Jasmine.Reflection.Models;

namespace Jasmine.Common
{
    public abstract class AbstractProxyFilter<T> : AbstractFilter<T>
    {
        public AbstractProxyFilter(Method method,IParamteterResolver<T> resolver, object instance, string name) : base(name)
        {
            _instance = instance ?? throw new ArgumentNullException();
            _method = method ?? throw new ArgumentException();
            _resolver = resolver ?? throw new ArgumentException();
        }
        private object _instance;
        private Method _method;
        private IParamteterResolver<T> _resolver;


        public override Task Filt(T context)
        {

            var parameters = _resolver.Resolve(context);

            var result = _method.Invoke(_instance, parameters);
            afterInvoke(context, result);

            if (Next != null)
                return Next.Filt(context);
            else
                return Task.CompletedTask;
        }


        protected abstract void afterInvoke(T context, object _return);
      
    }
}
