using Jasmine.Restful.DefaultFilters;
using System;

namespace Jasmine.Restful.Attributes
{
    public class AuthenticateAttribute:Attribute
    {
        public AuthenticateAttribute(AuntenticateLevel level)
        {
            Level = level;
        }
        public AuntenticateLevel Level { get;}
    }
}
