using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;

namespace Jasmine.Restful
{
    [Restful]
    [Path("/apimgr")]
    public class RestfulProcessorManager : AbstractProcessorManager<HttpFilterContext>
    {
        private RestfulProcessorManager()
        {

        }

        public static readonly IRequestProcessorManager<HttpFilterContext> Instance = new RestfulProcessorManager();
        public override string Name => "RestfulProcessorManager";
    }
}
