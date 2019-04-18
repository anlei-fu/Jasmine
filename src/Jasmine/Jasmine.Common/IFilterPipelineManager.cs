using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IFilterPipelineManager<T>:IReadOnlyCollection<IFilterPipeline<T>>,INameFearture
    {
        void AddFilterPipeline(string path, IFilterPipeline<T> pipeline);
        void RemoveFilterPipeline(string path);
        bool Contains(string path);
        IFilterPipeline<T> GetPipeline(string path);

    }
}
