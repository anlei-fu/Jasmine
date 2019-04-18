using Jasmine.Reflection.Implements;

namespace Jasmine.Reflection
{
    public class AttributeSurpport
    {
        public IAttributeCache Attributes { get; set; } = new DefaultAttributesCache();
    }
}
