using Jasmine.Reflection.Implements;

namespace Jasmine.Reflection
{
    public class AttributeFearture
    {
        public IAttributeCache Attributes { get; set; } = new DefaultAttributesCache();
    }
}
