using System;
using System.Collections.Generic;

namespace Jasmine.Ioc
{
    public interface IIocServiceMetaDataManager : IDictionary<Type, IocServiceMetaData>
    {
        /// <summary>
        /// get interface or abstract class's implementation
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Type GetImplementation(Type type);

        void SetImplementation(Type abstrc, Type impl);
    }
}
