using System;

namespace Jasmine.Configuration.Exceptions
{
    public  class ConfigGroupNotFoundException:Exception
    {
        public ConfigGroupNotFoundException(string group):base($"config group \"{group}\" is not found!")
        {

        }
    }
}
