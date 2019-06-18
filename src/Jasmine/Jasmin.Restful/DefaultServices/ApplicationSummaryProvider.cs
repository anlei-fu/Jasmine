using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;

namespace Jasmine.Restful.DefaultServices
{
    [Restful]
    public  class ApplicationSummaryProvider
    {
        [Path("/api/getsummary")]
        public RestfulApplicationSummary GetSummary()
        {
            return null;
        }
    }
}
