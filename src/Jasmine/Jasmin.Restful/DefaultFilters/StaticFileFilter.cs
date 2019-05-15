using System.Threading.Tasks;
using Jasmine.Common;

namespace Jasmine.Restful
{
    public class StaticFileFilter : AbstractFilter<HttpFilterContext>
    {
        public StaticFileFilter(string name) : base(name)
        {
        }
        private IStaticFileProvider _fileProvider;
        public override Task FiltAsync(HttpFilterContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
