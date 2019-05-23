namespace Jasmine.Common
{
    public interface IRequestProcessingContext:IPathFearture
    {
        IDispatcher<IRequestProcessingContext> Dispatcher { get; }
    }
}
