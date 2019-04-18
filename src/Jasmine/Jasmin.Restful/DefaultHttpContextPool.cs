using Jasmine.Common;

namespace Jasmine.Restful
{
    public class DefaultHttpContextPool : AbstractSimpleQueuedPool<HttpFilterContext>
    {
        public DefaultHttpContextPool(int capacity) : base(capacity)
        {
        }

        protected override HttpFilterContext createNew()
        {
            throw new System.NotImplementedException();
        }
    }
}
