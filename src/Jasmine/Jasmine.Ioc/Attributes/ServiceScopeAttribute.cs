using System;

namespace Jasmine.Ioc.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public  class ServiceScopeAttribute:Attribute
    {
        public ServiceScopeAttribute(ServiceScope scope)
        {
            Scope = scope;
        }
        public ServiceScope Scope { get; }
    }
}
