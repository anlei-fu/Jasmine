using Jasmine.Reflection.Implements;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Jasmine.Reflection
{
    public class Method :AttributeFearture, IInvoker
    {
        private static readonly Type TASK_TYPE = typeof(Task);
        public MethodInfo MethodInfo { get; internal set; }
        public Func<object,object[],object> Invoker { get;internal set; }
        public string Name => MethodInfo.Name;
        public string FullName { get;internal set; }
        public Type ReturnType => MethodInfo.ReturnType;
        public Type DeclareType => MethodInfo.DeclaringType;
        public bool HasReturn => ReturnType != null;
        public bool HasGenericPararmeter => MethodInfo.ContainsGenericParameters;
        public bool IsPublic => MethodInfo.IsPublic;
        public bool IsAbstract => MethodInfo.IsAbstract;
        public bool IsStatic => MethodInfo.IsStatic;
        public bool IsAsync => HasReturn && TASK_TYPE.IsAssignableFrom(ReturnType);
        public IParameterCache Parameters { get; set; } = new DefaultParameterCache();
        public Type[] ParamerTypies { get; set; }
        public bool HasParameter => ParamerTypies.Length != 0;
        public MethodInfo MakeGenericMethod(params Type[] types) => MethodInfo.MakeGenericMethod(types);
        public object Invoke(object instance, object[] parameters)
        {
            if (Invoker == null)
                throw new ArgumentNullException(nameof(Invoker));

            return Invoker(instance, parameters);
        }
       
    }
}
