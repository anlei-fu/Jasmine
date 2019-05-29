using Jasmine.Common;

namespace Jasmine.Restful
{
    public class RestfulProcessorManager : AbstractProcessorManager<HttpFilterContext>
    {
        private RestfulProcessorManager()
        {

        }

        public static readonly IRequestProcessorManager<HttpFilterContext> Instance = new RestfulProcessorManager();
        public override string Name => "RestfulProcessorManager";
    }
}
