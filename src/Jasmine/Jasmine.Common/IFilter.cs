using System.Threading.Tasks;

namespace Jasmine.Common
{
    public interface IFilter<T>:INameFearture
    {
        Task Filt(T context);

        IFilter<T> Next { get; }
    }
}
