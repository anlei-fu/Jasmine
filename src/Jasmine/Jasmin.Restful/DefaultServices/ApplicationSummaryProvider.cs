using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;

namespace Jasmine.Restful.DefaultServices
{
    [Restful]
    public  class ApplicationSummaryProvider
    {
        [Path("/api/getsummary")]
        public SystemSummary GetSummary()
        {
            return null;
        }
    }
}
