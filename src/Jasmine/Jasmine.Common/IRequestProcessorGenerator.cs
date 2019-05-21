namespace Jasmine.Common
{

    public interface IRequestProcessorGenerator<TContext,in TMetaData>
    {

        IRequestProcessor<TContext>[] Generate(TMetaData metaData);
    }
}
