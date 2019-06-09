using System.Reflection;

namespace Jasmine.Reflection
{
    public class Property :Field
    {
        public PropertyInfo PropertyInfo { get; set; }
        public bool CanRead => Getter != null;
        public bool CanWrite => Setter != null;
    }
}
