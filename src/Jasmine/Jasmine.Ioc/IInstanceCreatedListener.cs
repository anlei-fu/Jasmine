using Jasmine.Common;

namespace Jasmine.Ioc
{
    /// <summary>
    /// call after <see cref="IInstanceCreator.Create(IocServiceMetaData, DependencyCheckNode)"/>
    /// <seealso cref="IInstanceCreator."/>
    /// </summary>
    public  interface IInstanceCreatedListener:INameFearture
    {
        void OnInstanceCreated(IocServiceMetaData metadata, object instance);
    }
}
