namespace Jasmine.Common
{
   public interface IFilterPipelineBuilder<T>
    {
        IFilterPipelineBuilder<T> AddErrorFirst(IFilter<T> filter);
        IFilterPipelineBuilder<T> AddErrorLast(IFilter<T> filter);
        IFilterPipelineBuilder<T> AddFirst(IFilter<T> filter);
        IFilterPipelineBuilder<T> AddLast(IFilter<T> filter);
        IFilterPipeline<T> Build();

    }
}
