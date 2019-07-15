using System;

namespace Jasmine.Tools.Csv
{
    public   class DefaultConstructorNotFoundException:Exception
    {
        public DefaultConstructorNotFoundException(Type type):base($"default constructor can not be found  of {type}")
        {

        }
    }
}
