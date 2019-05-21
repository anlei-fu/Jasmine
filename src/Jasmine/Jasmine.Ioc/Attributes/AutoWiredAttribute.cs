using System;

namespace Jasmine.Ioc.Attributes
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property,AllowMultiple =false)]
    public class AutoWiredAttribute:Attribute
    {
    }
}
