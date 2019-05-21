using Jasmine.Common;

namespace Jasmine.Common
{
    /// <summary>
    /// call after <see cref="IInstanceCreator.Create(IocServiceMetaData, DependencyCheckNode)"/>
    /// <seealso cref="IInstanceCreator."/>
    /// </summary>
    public  interface IInstanceCreatedListener<TMetaData>:INameFearture
    {
        void OnInstanceCreated(TMetaData metadata, object instance);
    }
}
