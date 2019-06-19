using System.Threading.Tasks;

namespace Jasmine.Cache
{
    public  interface IFileLoader
    {
        Task<byte[]> LoadAsync(string path);
    }
}
