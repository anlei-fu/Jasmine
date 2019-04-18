namespace Jasmine.Common
{
   public interface IFilterPipelineBuilder<T>
    {
        IFilterPipelineBuilder<T> AddFirst(IFilter<T> filter);
        IFilterPipelineBuilder<T> AddLast(IFilter<T> filter);
        IFilterPipeline<T> Build();

    }
}
