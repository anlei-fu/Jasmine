﻿using Jasmine.Reflection.Models;
using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public  interface IMethodCache:IReflectionCache<Method,MethodInfo>
    {
        Method FindMethod(string name,Type parameterTypes);
    }
}