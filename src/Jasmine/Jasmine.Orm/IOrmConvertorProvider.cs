using System;

namespace Jasmine.Orm.Interfaces
{
    public  interface IOrmConvertorProvider
    {
        IOrmConvertor GetConvertor(Type type);
    }
}
