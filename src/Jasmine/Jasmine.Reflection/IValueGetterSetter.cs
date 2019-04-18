namespace Jasmine.Reflection
{
    public interface IValueGetterSetter
    {
        object GetValue(object instance);
        void SetValue(object instance, object value);
    }
}
