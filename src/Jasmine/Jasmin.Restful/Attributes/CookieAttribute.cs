using System;

namespace Jasmine.Restful.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public  class FromCookieAttribute:Attribute
    {
    }
}
