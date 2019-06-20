using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jasmine.Reflection
{
    public static  class TypeUtils
    {
        public static bool CanConvertTo(this Type type,Type _base)
        {

            return _base.IsInterface ? type.GetInterfaces().Contains(_base) :
                                     _base.IsAssignableFrom(type);
        }

        public static bool TryGetType(string name,out Type type)
        {
            type = Type.GetType(name);

            return type != null;
        }
        public static bool IsInterfaceOrAbstractClass(this Type type)
        {
            return type.IsInterface||type.IsAbstract;
        }
        public static ConstructorInfo GetDefaultConstructor(this Type t)
        {
            return t.GetConstructors(BindingFlags.Instance | BindingFlags.Public).SingleOrDefault(c => !c.GetParameters().Any());
        }
        public static bool IsBaseType(this Type type)
        {
            return BaseTypes.Base.Contains(type);
        }

        public static bool HasDefaultConstructor(this Type t)
        {
            return t.IsValueType?true: t.GetDefaultConstructor() != null;
        }

        public static bool IsList(this Type t)
        {
            return t.Name.StartsWith("List<") && t.Name.EndsWith(">");
        }
        public static bool IsNullable(this Type t)
        {
            return t.IsValueType ? (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)) : false;
        }
        public static bool ImplementInterface(this Type type,Type _base)
        {
            return true;
        }

        public  static string GetMethodName(this MethodBase method)
        {
            var name = method.Name;

            foreach (var item in method.GetParameters())
            {
                name += $"_{item.ParameterType.Name}";
            }

            return name;
                
        }

        public static Type[] GetParameterTypes(this MethodBase method)
        {
            var ls = new List<Type>();

            foreach (var item in method.GetParameters())
                ls.Add(item.ParameterType);

            return ls.ToArray();
        }

        public static bool CompareTypes(Type[] types1,Type[] types2)
        {
            if (types1.Length != types2.Length)
                return false;

            for (int i = 0; i < types1.Length; i++)
            {
                /*
                 * null assume  equels
                 */ 
                if (types2[i] == null)
                    continue;

                if (!types2[i].CanConvertTo(types1[i]))
                    return false;
            }

            return true;
        }



    }
}
