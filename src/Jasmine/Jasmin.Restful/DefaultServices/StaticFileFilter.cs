using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful
{
    public class StaticFileFilter : AbstractFilter<HttpFilterContext>
    {
        public StaticFileFilter()
        {
        }
        private IStaticFileProvider _fileProvider;
        public override Task FiltsAsync(HttpFilterContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
