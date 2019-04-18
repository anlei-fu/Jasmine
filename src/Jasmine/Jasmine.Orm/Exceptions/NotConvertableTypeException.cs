using System;

namespace Jasmine.Orm.Exceptions
{
    public   class NotConvertableTypeException:Exception
    {
        public NotConvertableTypeException(Type type, Type convertorType) : base($"{type} is not convertable in convertor {convertorType}!")
        {

        }
    }
}
