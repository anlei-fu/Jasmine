using System;
using System.Collections.Generic;

namespace Jasmine.Ioc
{
    public interface IServiceMetaDataManager : IDictionary<Type, ServiceMetaData>
    {
        /// <summary>
        /// get interface or abstract class implementation
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Type GetImplement(Type type);

        void SetImplement(Type abstrc, Type impl);
    }
}
