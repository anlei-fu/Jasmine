using Jasmine.Restful.DefaultFilters;
using System;

namespace Jasmine.Restful.Attributes
{
    public class AuthenticateAttribute:Attribute
    {
        public AuthenticateAttribute(Level level)
        {
            UserLevel = level;
        }
        public Level UserLevel { get;}
    }
}
