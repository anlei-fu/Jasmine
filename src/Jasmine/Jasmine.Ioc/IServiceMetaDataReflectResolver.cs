using System;

namespace Jasmine.Ioc
{
    public  interface IServiceMetaDataReflectResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        ServiceMetaData Resolve(Type type, ServiceScope scope);
        /// <summary>
        /// resolve metadata by reflecting
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ServiceMetaData Resolve(Type type);
        ServiceMetaData ResolveSingleton(Type type);
        ServiceMetaData ResolveRequest(Type type);
        ServiceMetaData ResolveProtoType(Type type);
    }
}
