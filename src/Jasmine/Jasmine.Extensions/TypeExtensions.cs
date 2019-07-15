using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jasmine.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool CanConvertTo(this Type type, Type destination)
        {

            return destination.IsInterface 
                           ? type.GetInterfaces().Contains(destination)
                           : destination.IsAssignableFrom(type);
        }

        public static bool TryGetType(string name, out Type type)
        {
            type = Type.GetType(name);

            return type != null;
        }
        public static bool IsInterfaceOrAbstractClass(this Type type)
        {
            return type.IsInterface || type.IsAbstract;
        }
        public static ConstructorInfo GetDefaultConstructor(this Type t)
        {
            return t.GetConstructors(BindingFlags.Instance | BindingFlags.Public).SingleOrDefault(c => !c.GetParameters().Any());
        }
        public static bool HasDefaultConstructor(this Type t)
        {
            return t.IsValueType ? true : t.GetDefaultConstructor() != null;
        }

        public static bool IsEnumerable(this Type t)
        {
            return t.GetElementType()!=null;
        }
        public static bool IsList(this Type t)
        {
            return t.Name.StartsWith("List<") && t.Name.EndsWith(">");
        }
        public static bool IsDictionary(this Type t)
        {
            return true;
        }
        public static bool IsNullable(this Type t)
        {
            return t.IsValueType ? (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)) : false;
        }
         

        public static Type[] GetParameterTypes(this MethodBase method)
        {
            var ls = new List<Type>();

            foreach (var item in method.GetParameters())
                ls.Add(item.ParameterType);

            return ls.ToArray();
        }

        public static bool Equesl(this Type[] types1, Type[] types2)
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
