using System;

namespace Jasmine.Orm.Exceptions
{
    public  class BadJoinTableTypeException:Exception
    {
        public BadJoinTableTypeException(Type type):base($" join table must be an array or a list and element type is not a base type ! but this type is:{type}")
        {

        }
    }
}
