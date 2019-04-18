namespace Jasmine.Reflection
{
    public interface IInvoker
    {
      
        object Invoke(object instance, object[] parameters);
    }
}
