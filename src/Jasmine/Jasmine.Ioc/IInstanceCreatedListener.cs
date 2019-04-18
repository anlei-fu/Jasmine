using Jasmine.Common;

namespace Jasmine.Ioc
{
    /// <summary>
    /// call after <see cref="IInstanceCreator.Create(ServiceMetaData, DependencyCheckNode)"/>
    /// <seealso cref="IInstanceCreator."/>
    /// </summary>
    public  interface IInstanceCreatedListener:INameFearture
    {
        void OnInstanceCreated(ServiceMetaData metadata, object instance);
    }
}
