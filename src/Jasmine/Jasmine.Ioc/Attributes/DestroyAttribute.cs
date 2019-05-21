using System;

namespace Jasmine.Ioc.Attributes
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public  class DestroyAttribute: Attribute
    {
    }
}
