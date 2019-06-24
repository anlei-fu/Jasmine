using Jasmine.Common;
using Jasmine.Common.Attributes;
using Jasmine.Restful.Attributes;

namespace Jasmine.Restful
{
    [Restful]
    [Path("/api")]
   // [BeforeInterceptor(typeof(SessionValidateFilter))]
    public class RestfulServiceManager : AbstractProcessorManager<HttpFilterContext>
    {
        private RestfulServiceManager()
        {

        }
        public static readonly RestfulServiceManager Instance = new RestfulServiceManager();
        public override string Name => "Restful-Processor-Manager";
        [RestfulIgnore]
        public void Remount(bool all)
        {

        }

    }
}
