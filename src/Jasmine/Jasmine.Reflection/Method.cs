using Jasmine.Reflection.Implements;
using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class Method :AttributeSurpport, IInvoker
    {
        public MethodInfo RelatedInfo { get; set; }
        public Func<object,object[],object> Invoker { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public Type ReturnType { get; set; }
        public Type OwnerType { get; set; }
        public bool HasReturn => ReturnType != null;
        public IParameterCache Parameters { get; set; } = new DefaultParameterCache();
        public Type[] ParamerTypies { get; set; }

      
        public object Invoke(object instance, object[] parameters)
        {
            if (Invoker == null)
                throw new ArgumentNullException(nameof(Invoker));

            return Invoker(instance, parameters);
        }
       
    }
}
