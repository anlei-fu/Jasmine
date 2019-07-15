using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class Field : AttributeFearture, IValueGetterSetter
    {
        public FieldInfo FieldInfo { get; set; }
        public string Name => FieldInfo.Name;
        public Type FiledType => FieldInfo.FieldType;
        public Type OwnerType => FieldInfo.DeclaringType;
        public bool IsPublic => FieldInfo.IsPublic;
        public bool IsPrivate => FieldInfo.IsPrivate;
        public bool IsStatic => FieldInfo.IsStatic;
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
