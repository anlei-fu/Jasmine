using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class Property :AttributeFearture, IValueGetterSetter
    {
        public string Name => PropertyInfo.Name;
        public Type PropertyType => PropertyInfo.PropertyType;
        public Type OwnerType => PropertyInfo.DeclaringType;
        public bool CanRead => PropertyInfo.CanRead;
        public bool CanWrite => PropertyInfo.CanWrite;
        public Action<object, object> Setter { get; set; }
        public Func<object, object> Getter { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public object GetValue(object instance)
        {
            throw new NotImplementedException();
        }

        public void SetValue(object instance, object value)
        {
            throw new NotImplementedException();
        }
    }
}
