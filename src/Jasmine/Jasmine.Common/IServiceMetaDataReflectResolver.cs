using System;

namespace Jasmine.Common
{
    public   interface IMetaDataReflectResolver< out T>
    {
        T Resolve(Type type);
    }
}
