using System.Threading.Tasks;

namespace Jasmine.Common
{
    public  interface IDispatcher<T>:INameFearture
    {
        Task DispatchAsync(string path, T context);
    }
}
