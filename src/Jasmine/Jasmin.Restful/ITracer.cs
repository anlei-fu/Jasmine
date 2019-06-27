using System.Collections.Generic;

namespace Jasmine.Restful
{
    public interface ITracer
    {
        void BeginTrace(HttpFilterContext context);
        void EndTrace(HttpFilterContext context);
        IEnumerable<RestfulTrace> GetTrace(string path);
        
    }
}
