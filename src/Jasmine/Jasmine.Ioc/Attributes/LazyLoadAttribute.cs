using System;

namespace Jasmine.Ioc.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public  class LazyLoadAttribute:Attribute
    {
    }
}
