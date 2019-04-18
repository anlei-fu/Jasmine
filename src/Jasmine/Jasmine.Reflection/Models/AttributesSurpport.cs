using Jasmine.Reflection.Implements;
using Jasmine.Reflection.Interfaces;

namespace Jasmine.Reflection.Models
{
    public class AttributeSurpport
    {
        public IAttributeCache Attributes { get; set; } = new DefaultAttributesCache();
    }
}
