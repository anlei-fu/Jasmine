using Jasmine.Cache;
using Jasmine.Ioc.Attributes;
using System.IO;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class RestfulStaticFileProvider : IStaticFileProvider
    {
        public RestfulStaticFileProvider([FromConfig("fileprovider.max-memory-usage")]long maxMemoryUsage)
        {
            _cache = new LruFileCache(2, maxMemoryUsage, 1000 * 60, SimpleFileLoader.Instance);
        }
        [InitiaMethod]
        public void Start()
        {
            _cache.Start();
        }
        [DestroyMethod]
        public void Stop()
        {
            _cache.Stop();
        }

        private LruFileCache _cache;

        public long MaxMemoryUsage => _cache.MaxMemoryUsage;

        public long CurrentMemoryUsage => _cache.CurrentUsage;

        public int CurrentCachedFile => _cache.Count;

        public async Task<Stream> GetStreamAsync(string path)
        {
            var buffer = await _cache.GetFileAsync(path);

            return buffer == null ? null : new MemoryStream(buffer);
        }


        private class SimpleFileLoader : IFileLoader
        {
            private SimpleFileLoader()
            {

            }
            public static readonly IFileLoader Instance = new SimpleFileLoader();
            public async Task<byte[]> LoadAsync(string path)
            {
                var info = new FileInfo(path);

                if(info.Exists)
                {
                    var stream = info.Open(FileMode.Open, FileAccess.Read);

                    var buffer = new byte[stream.Length];

                    await stream.ReadAsync(buffer, 0, buffer.Length);

                    return buffer;
                }


                return null;
            }
        }
    }

    
}
