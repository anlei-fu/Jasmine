using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful
{
    public class ApiManagerFilter : AbstractFilter<HttpFilterContext>
    {
        public ApiManagerFilter(string name) : base(name)
        {
        }

        public override Task FiltAsync(HttpFilterContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
