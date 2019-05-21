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
        /// <summary>
        /// set abstract or interface -- impl mapping
        /// </summary>
        /// <param name="abs"></param>
        /// <param name="impl"></param>
        void SetImplementation(Type abs, Type impl);
    }
}
