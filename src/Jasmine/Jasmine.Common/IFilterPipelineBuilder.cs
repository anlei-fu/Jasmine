namespace Jasmine.Common
{
   public interface IFilterPipelineBuilder<TContext>
    {
        IFilterPipelineBuilder<TContext> AddErrorFirst(IFilter<TContext> filter);
        IFilterPipelineBuilder<TContext> AddErrorLast(IFilter<TContext> filter);
        IFilterPipelineBuilder<TContext> AddFirst(IFilter<TContext> filter);
        IFilterPipelineBuilder<TContext> AddLast(IFilter<TContext> filter);
        IFilterPipelineBuilder<TContext> SetStat();
        IFilterPipeline<TContext> Build();

    }
}
