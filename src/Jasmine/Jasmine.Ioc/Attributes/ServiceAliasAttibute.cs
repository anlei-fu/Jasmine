using System;

namespace Jasmine.Ioc.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceAliasAttibute:Attribute
    {
        public ServiceAliasAttibute(string alias)
        {
            Alias = alias;
        }
        public string Alias { get; }
    }
}
