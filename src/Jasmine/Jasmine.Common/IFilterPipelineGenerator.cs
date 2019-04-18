namespace Jasmine.Common
{
    public interface IFilterPipelineGenerator<TContext,in TMetaData>
    {
        IFilterPipeline<TContext> Generate(TMetaData metaData);
    }
}
