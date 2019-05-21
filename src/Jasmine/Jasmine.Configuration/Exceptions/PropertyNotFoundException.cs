using System;

namespace Jasmine.Configuration.Exceptions
{
    public  class PropertyNotFoundException:Exception
    {
        public PropertyNotFoundException(string group,string name):base($"property \"{name}\" is not found in group \"{group}\"!")
        {

        }
    }
}
