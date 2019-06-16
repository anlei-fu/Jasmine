using System;

namespace Jasmine.Ioc.Attributes
{

    public  class FromConfigAttribute:Attribute
    {
        public FromConfigAttribute(string key)
        {
            ConfigKey = key;
        }
        public string ConfigKey{ get; }
    }
}
