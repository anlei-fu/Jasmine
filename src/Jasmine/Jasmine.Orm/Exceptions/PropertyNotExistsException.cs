using System;

namespace Jasmine.Orm.Exceptions
{
    public   class PropertyNotExistsException:Exception
    {
        public PropertyNotExistsException(string msg):base(msg)
        {

        }
    }
}
