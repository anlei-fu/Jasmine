namespace Jasmine.Common
{

    public interface IRequestProcessorGenerator<TContext,in TMetaData>
        where TContext:IFilterContext
    {

        IRequestProcessor<TContext>[] Generate(TMetaData metaData);
    }
}
