using Jasmine.Common;

namespace Jasmine.Restful
{
    public class HttpFilterContextPool : AbstractSimpleQueuedPool<HttpFilterContext>
    {
        public HttpFilterContextPool(int capacity) : base(capacity)
        {
        }

        protected override HttpFilterContext newInstance()
        {
            return new HttpFilterContext();
        }
    }
}
