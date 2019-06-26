using Jasmine.Reflection.Implements;
using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class Method :AttributeFearture, IInvoker
    {
        public MethodInfo MethodInfo { get; set; }
        public Func<object,object[],object> Invoker { get; set; }
        public string Name => MethodInfo.Name;
        public string FullName { get; set; }
        public Type ReturnType => MethodInfo.ReturnType;
        public Type OwnerType => MethodInfo.DeclaringType;
        public bool HasReturn => ReturnType != null;
        public IParameterCache Parameters { get; set; } = new DefaultParameterCache();
        public Type[] ParamerTypies { get; set; }
        public bool HasParameter => ParamerTypies.Length != 0;
      
        public object Invoke(object instance, object[] parameters)
        {
            if (Invoker == null)
                throw new ArgumentNullException(nameof(Invoker));

            return Invoker(instance, parameters);
        }
       
    }
}
