using System;

namespace Jasmine.Common
{
    public   interface IMetaDataReflectResolver< out TMetaData>
    {
        TMetaData Resolve(Type type);
    }
}
