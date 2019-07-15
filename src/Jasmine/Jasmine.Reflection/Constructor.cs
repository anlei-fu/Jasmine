using Jasmine.Reflection.Implements;
using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public class Constructor : AttributeFearture
    {
        public ConstructorInfo ConstructorInfo { get; set; }

        public Type DeclareType => ConstructorInfo.DeclaringType;
        public string Name => ConstructorInfo.Name;
        public string FullName => "";
        public bool HasParamer => ParamerTypies != null;
        public bool IsDefaultConstructor => DefaultInvoker != null;
        public bool IsPublic => ConstructorInfo.IsPublic;
        public bool IsStatic => ConstructorInfo.IsStatic;
        public bool IsAbstract => ConstructorInfo.IsAbstract;

        public Func<object[], object> Invoker { get; set; }
     
        public Func<object> DefaultInvoker { get; internal set; }
      
        public string[] ParameterNames { get; set; }
        public Type[] ParamerTypies { get; set; }
        public bool HasGenericParameter => ConstructorInfo.GetGenericArguments().Length!= 0;
        public Type[] GenericTypes { get; set; }
        public IParameterCache Parameters { get; set; } = new DefaultParameterCache();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            return ((Constructor)obj).ParamerTypies == ParamerTypies;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
