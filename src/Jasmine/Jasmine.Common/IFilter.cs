using System.Threading.Tasks;

namespace Jasmine.Common
{
    public interface IFilter<T>:INameFearture
    {
        Task FiltAsync(T context);

        IFilter<T> Next { get; set; }
    }
}
