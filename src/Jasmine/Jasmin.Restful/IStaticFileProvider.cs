using System.IO;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public interface IStaticFileProvider
    {
        Task<Stream> GetStreamAsync(string path);
        long MaxMemoryUsage { get; }
        long CurrentMemoryUsage { get; }
        int CurrentCachedFile { get; }
    }
}
