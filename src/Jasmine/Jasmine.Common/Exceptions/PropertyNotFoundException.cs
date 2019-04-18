using System;

namespace Jasmine.Common.Exceptions
{
    public   class PropertyNotFoundException:Exception
    {
        public PropertyNotFoundException(string msg):base(msg+" not found!")
        {

        }

    }
}
