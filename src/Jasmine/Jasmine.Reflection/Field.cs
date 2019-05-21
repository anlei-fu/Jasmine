using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class Field : AttributeFearture, IValueGetterSetter
    {
        public FieldInfo RelatedInfo { get; set; }
        public string Name { get; set; }
        public Type PropertyType { get; set; }
        public Type OwnerType { get; set; }
        public Action<object, object> Setter { get; set; }
        public Func<object, object> Getter { get; set; }
        public object GetValue(object instance)
        {
            return  Getter?.Invoke(instance);
        }

        public void SetValue(object instance, object value)
        {
            Setter?.Invoke(instance, value);
        }
    }
}
