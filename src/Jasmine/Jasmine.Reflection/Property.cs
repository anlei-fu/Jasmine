using System.Reflection;

namespace Jasmine.Reflection
{
    public class Property :Field
    {
        public new PropertyInfo RelatedInfo { get; set; }
        public bool CanRead => Getter != null;
        public bool CanWrite => Setter != null;
    }
}
