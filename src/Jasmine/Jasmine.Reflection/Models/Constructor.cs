using Jasmine.Reflection.Implements;
using Jasmine.Reflection.Interfaces;
using System;
using System.Reflection;

namespace Jasmine.Reflection.Models
{
    public class Constructor : AttributeSurpport
    {
        public ConstructorInfo RelatedInfo { get; set; }
        public Func<object[], object> Invoker { get; set; }
        public bool IsDefault => DefaultInvoker != null;
        public Func<object> DefaultInvoker { get; internal set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public bool HasParamer => ParamerTypies != null;
        public string[] ParameterNames { get; set; }
        public Type[] ParamerTypies { get; set; }
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
