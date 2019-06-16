using System;

namespace Jasmine.Ioc.Exceptions
{
    public  class CanNotGetInstanceValueFromConfigException:Exception
    {
        public CanNotGetInstanceValueFromConfigException(string msg):base(msg)
        {

        }
    }
}
