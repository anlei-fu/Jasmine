namespace Jasmine.Common
{
    public interface IContext:IPathFearture
    {
        IDispatcher<IContext> Dispatcher { get; }
    }
}
