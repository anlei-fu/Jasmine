using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful
{
    public class ApiManagerFilter : AbstractFilter<HttpFilterContext>
    {
        public ApiManagerFilter() 
        {
        }

        public override Task FiltsAsync(HttpFilterContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
