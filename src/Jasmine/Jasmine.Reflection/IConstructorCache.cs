using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public interface IConstructorCache:IReflectionCache<Constructor,ConstructorInfo>
    {
        Constructor GetDefaultConstructor();
        Constructor FindConstructor(Type[] parameterTypes);
    }
}
