using System;

namespace Jasmine.Orm.Exceptions
{
    public   class NotConvertableException:Exception
    {
        public NotConvertableException(Type type, Type convertorType) : base($"{type} is not convertable in convertor {convertorType}!")
        {

        }
    }
}
