using System;

namespace Jasmine.Common
{
    public   interface IServiceMetaDataReflectResolver< out T>
    {
        T Resolve(Type type);
    }
}
