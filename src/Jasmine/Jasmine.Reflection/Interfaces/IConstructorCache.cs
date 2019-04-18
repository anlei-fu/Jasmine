using Jasmine.Reflection.Models;
using System;
using System.Reflection;

namespace Jasmine.Reflection.Interfaces
{
    public interface IConstructorCache:IReflectionCache<Constructor,ConstructorInfo>
    {
        Constructor GetDefaultConstructor();
        Constructor FindConstructor(Type[] parameterTypes);
    }
}
