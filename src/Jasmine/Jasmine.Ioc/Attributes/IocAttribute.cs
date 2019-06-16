using System;

namespace Jasmine.Ioc.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface,AllowMultiple =false)]
    public  class IocAttribute:Attribute
    {
    }
}
