using Jasmine.Common;

namespace Jasmine.Restful
{
    public class HttpFilterContextPool : AbstractSimpleQueuedPool<HttpFilterContext>
    {
        public HttpFilterContextPool(IDispatcher<HttpFilterContext> dispacher,int capacity) : base(capacity)
        {
            _dispatcher = dispacher;
        }

        private IDispatcher<HttpFilterContext> _dispatcher;
        protected override HttpFilterContext newInstance()
        {
            return new HttpFilterContext()
            {
                Dispatcher = _dispatcher
            };
        }
    }
}
