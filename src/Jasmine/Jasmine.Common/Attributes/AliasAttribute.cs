using System;

namespace Jasmine.Common.Attributes
{
    
    public class AliasAttribute:Attribute
    {
        public AliasAttribute(string alias)
        {
            Alias = alias;
        }
        public string Alias { get; }
    }
}
