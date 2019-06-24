using Jasmine.Restful.DefaultFilters;
using System;

namespace Jasmine.Restful.Attributes
{
    public class AuthenticateAttribute:Attribute
    {
        public AuthenticateAttribute(AuthenticateLevel level)
        {
            Level = level;
        }
        public AuthenticateLevel Level { get;}
    }
}
