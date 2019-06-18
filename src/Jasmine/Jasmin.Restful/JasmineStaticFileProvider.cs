using System.IO;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class JasmineStaticFileProvider : IStaticFileProvider
    {
        public Task<Stream> GetStreamAsync(string path)
        {
            return null;
        }

      
    }
}
