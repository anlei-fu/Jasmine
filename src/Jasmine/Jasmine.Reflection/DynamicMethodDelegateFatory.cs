using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Jasmine.Reflection
{
    /// <summary>
    /// 
    /// ref: https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/Utilities/DynamicReflectionDelegateFactory.cs
    /// 
    /// </summary>
    internal class DynamicMethodDelegateFatory
    {
        public static Func<object, object> CreateFieldGetter(FieldInfo info)
        {
            if (info.IsLiteral)
            {
                var constantValue = info.GetValue(null);

                Func<object, object> getter = o => constantValue;

                return getter;
            }

            var dynamicMethod = createDynamicMethod("Get" + info.Name, typeof(object), new[] { typeof(object) }, info.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            generateCreateGetFieldIL(info, generator);

            return (Func<object, object>)dynamicMethod.CreateDelegate(typeof(Func<object, object>));
        }

        private static void generateCreateGetFieldIL(FieldInfo fieldInfo, ILGenerator generator)
        {
            if (!fieldInfo.IsStatic)
            {
                generator.PushInstance(fieldInfo.DeclaringType);
                generator.Emit(OpCodes.Ldfld, fieldInfo);
            }
            else
            {
                generator.Emit(OpCodes.Ldsfld, fieldInfo);
            }

            generator.BoxIfNeeded(fieldInfo.FieldType);
            generator.Return();
        }

        /// <summary>
        /// 创建字段Setter
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public static Action<object, object> CreateFiledSetter(FieldInfo fieldInfo)
        {
            var dynamicMethod = createDynamicMethod("Set" + fieldInfo.Name, null, new[] { typeof(object), typeof(object) }, fieldInfo.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            generateCreateSetFieldIL(fieldInfo, generator);

            return (Action<object, object>)dynamicMethod.CreateDelegate(typeof(Action<object, object>));
        }
        /// <summary>
        /// 优化jit编译
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="generator"></param>

        private static void generateCreateSetFieldIL(FieldInfo fieldInfo, ILGenerator generator)
        {
            if (!fieldInfo.IsStatic)
            {
                generator.PushInstance(fieldInfo.DeclaringType);
            }

            generator.Emit(OpCodes.Ldarg_1);
            generator.UnboxIfNeeded(fieldInfo.FieldType);

            if (!fieldInfo.IsStatic)
            {
                generator.Emit(OpCodes.Stfld, fieldInfo);
            }
            else
            {
                generator.Emit(OpCodes.Stsfld, fieldInfo);
            }

            generator.Return();
        }
      
        /// <summary>
        /// 创建属性setter
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Action<object, object> CreatePropertySetter(PropertyInfo info)
        {
            var dynamicMethod = createDynamicMethod("Set" + info.Name, null, new[] { typeof(object), typeof(object) }, info.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            generateCreateSetPropertyIL(info, generator);

            return (Action<object, object>)dynamicMethod.CreateDelegate(typeof(Action<object, object>));
        }

       /// <summary>
       /// 创建methodcall
       /// </summary>
       /// <param name="method"></param>
       /// <returns></returns>

        public static Func<object, object[], object> CreateMethodCall(MethodBase method)
        {
            var dynamicMethod = createDynamicMethod(method.ToString(), typeof(object), new[] { typeof(object), typeof(object[]) }, method.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            generateCreateMethodCallIL(method, generator, 1);

            return (Func<object, object[], object>)dynamicMethod.CreateDelegate(typeof(Func<object, object[], object>));
        }


        private static void generateCreateMethodCallIL(MethodBase method, ILGenerator generator, int argsIndex)
        {
            var args = method.GetParameters();

            var argsOk = generator.DefineLabel();

            // throw an error if the number of argument values doesn't match method parameters
            generator.Emit(OpCodes.Ldarg, argsIndex);
            generator.Emit(OpCodes.Ldlen);
            generator.Emit(OpCodes.Ldc_I4, args.Length);
            generator.Emit(OpCodes.Beq, argsOk);
            generator.Emit(OpCodes.Newobj, typeof(TargetParameterCountException).GetConstructor(BaseTypes.EmptyTypes));
            generator.Emit(OpCodes.Throw);

            generator.MarkLabel(argsOk);

            if (!method.IsConstructor && !method.IsStatic)
            {
                generator.PushInstance(method.DeclaringType);
            }

            var localConvertible = generator.DeclareLocal(typeof(IConvertible));
            var localObject = generator.DeclareLocal(typeof(object));

            for (int i = 0; i < args.Length; i++)
            {
                var parameter = args[i];
                var parameterType = parameter.ParameterType;

                if (parameterType.IsByRef)
                {
                    parameterType = parameterType.GetElementType();

                    var localVariable = generator.DeclareLocal(parameterType);

                    // don't need to set variable for 'out' parameter
                    if (!parameter.IsOut)
                    {
                        generator.PushArrayInstance(argsIndex, i);

                        if (parameterType.IsPrimitive)
                        {
                            var skipSettingDefault = generator.DefineLabel();
                            var finishedProcessingParameter = generator.DefineLabel();

                            // check if parameter is not null
                            generator.Emit(OpCodes.Brtrue_S, skipSettingDefault);

                            // parameter has no value, initialize to default
                            generator.Emit(OpCodes.Ldloca_S, localVariable);
                            generator.Emit(OpCodes.Initobj, parameterType);
                            generator.Emit(OpCodes.Br_S, finishedProcessingParameter);

                            // parameter has value, get value from array again and unbox and set to variable
                            generator.MarkLabel(skipSettingDefault);
                            generator.PushArrayInstance(argsIndex, i);
                            generator.UnboxIfNeeded(parameterType);
                            generator.Emit(OpCodes.Stloc_S, localVariable);

                            // parameter finished, we out!
                            generator.MarkLabel(finishedProcessingParameter);
                        }
                        else
                        {
                            generator.UnboxIfNeeded(parameterType);
                            generator.Emit(OpCodes.Stloc_S, localVariable);
                        }
                    }

                    generator.Emit(OpCodes.Ldloca_S, localVariable);
                }
                else if (parameterType.IsValueType)
                {
                    generator.PushArrayInstance(argsIndex, i);
                    generator.Emit(OpCodes.Stloc_S, localObject);

                    // have to check that value type parameters aren't null
                    // otherwise they will error when unboxed
                    var skipSettingDefault = generator.DefineLabel();
                    var finishedProcessingParameter = generator.DefineLabel();

                    // check if parameter is not null
                    generator.Emit(OpCodes.Ldloc_S, localObject);
                    generator.Emit(OpCodes.Brtrue_S, skipSettingDefault);

                    // parameter has no value, initialize to default
                    var localVariable = generator.DeclareLocal(parameterType);
                    generator.Emit(OpCodes.Ldloca_S, localVariable);
                    generator.Emit(OpCodes.Initobj, parameterType);
                    generator.Emit(OpCodes.Ldloc_S, localVariable);
                    generator.Emit(OpCodes.Br_S, finishedProcessingParameter);

                    // argument has value, try to convert it to parameter type
                    generator.MarkLabel(skipSettingDefault);

                    if (parameterType.IsPrimitive)
                    {
                        // for primitive types we need to handle type widening (e.g. short -> int)
                        var toParameterTypeMethod = typeof(IConvertible)
                            .GetMethod("To" + parameterType.Name, new[] { typeof(IFormatProvider) });

                        if (toParameterTypeMethod != null)
                        {
                            var skipConvertible = generator.DefineLabel();

                            // check if argument type is an exact match for parameter type
                            // in this case we may use cheap unboxing instead
                            generator.Emit(OpCodes.Ldloc_S, localObject);
                            generator.Emit(OpCodes.Isinst, parameterType);
                            generator.Emit(OpCodes.Brtrue_S, skipConvertible);

                            // types don't match, check if argument implements IConvertible
                            generator.Emit(OpCodes.Ldloc_S, localObject);
                            generator.Emit(OpCodes.Isinst, typeof(IConvertible));
                            generator.Emit(OpCodes.Stloc_S, localConvertible);
                            generator.Emit(OpCodes.Ldloc_S, localConvertible);
                            generator.Emit(OpCodes.Brfalse_S, skipConvertible);

                            // convert argument to parameter type
                            generator.Emit(OpCodes.Ldloc_S, localConvertible);
                            generator.Emit(OpCodes.Ldnull);
                            generator.Emit(OpCodes.Callvirt, toParameterTypeMethod);
                            generator.Emit(OpCodes.Br_S, finishedProcessingParameter);

                            generator.MarkLabel(skipConvertible);
                        }
                    }

                    // we got here because either argument type matches parameter (conversion will succeed),
                    // or argument type doesn't match parameter, but we're out of options (conversion will fail)
                    generator.Emit(OpCodes.Ldloc_S, localObject);

                    generator.UnboxIfNeeded(parameterType);

                    // parameter finished, we out!
                    generator.MarkLabel(finishedProcessingParameter);
                }
                else
                {
                    generator.PushArrayInstance(argsIndex, i);

                    generator.UnboxIfNeeded(parameterType);
                }
            }

            if (method.IsConstructor)
            {
                generator.Emit(OpCodes.Newobj, (ConstructorInfo)method);
            }
            else
            {
                generator.CallMethod((MethodInfo)method);
            }

            var returnType = method.IsConstructor
                ? method.DeclaringType
                : ((MethodInfo)method).ReturnType;

            if (returnType != typeof(void))
            {
                generator.BoxIfNeeded(returnType);

            }
            else
            {
                generator.Emit(OpCodes.Ldnull);
            }

            generator.Return();


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<object[],object> CreateParameterizedConstructor(MethodBase method)
        {
            var dynamicMethod = createDynamicMethod(method.ToString(), typeof(object), new[] { typeof(object[]) }, method.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            generateCreateMethodCallIL(method, generator, 0);

            return (Func<object[],object>)dynamicMethod.CreateDelegate(typeof(Func<object[],object>));
        }

        /// <summary>
        /// 创建属性 Getter
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static Func<object, object> CreatePropertyGetter(PropertyInfo info)
        {

            var dynamicMethod = createDynamicMethod("Get" + info.Name, typeof(object), new[] { typeof(object) }, info.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            generateCreateGetPropertyIL(info, generator);

            return (Func<object, object>)dynamicMethod.CreateDelegate(typeof(Func<object, object>));
        }

        public static Func<object> CreateDefaultConstructor(Type type)
        {
            var dynamicMethod = createDynamicMethod("Create" + type.FullName, type, null, type);
            dynamicMethod.InitLocals = true;
            var generator = dynamicMethod.GetILGenerator();

            generateCreateDefaultConstructorIL(type, generator, typeof(object));

            return (Func<object>)dynamicMethod.CreateDelegate(typeof(Func<object>));
        }


        private static DynamicMethod createDynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
        {
            var dynamicMethod = !owner.IsInterface
                ? new DynamicMethod(name, returnType, parameterTypes, owner, true)
                : new DynamicMethod(name, returnType, parameterTypes, owner.Module, true);

            return dynamicMethod;
        }
        private static void generateCreateDefaultConstructorIL(Type type, ILGenerator generator, Type delegateType)
        {
            if (type.IsValueType)
            {
                generator.DeclareLocal(type);
                generator.Emit(OpCodes.Ldloc_0);

                // only need to box if the delegate isn't returning the value type
                if (type != delegateType)
                    generator.Emit(OpCodes.Box, type);
            }
            else
            {
                var constructorInfo =
                    type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);

                if (constructorInfo == null)
                    throw new ArgumentException($"can not create default constructor  in type {type} ");

                generator.Emit(OpCodes.Newobj, constructorInfo);
            }

            generator.Emit(OpCodes.Ret);
        }


        private static void generateCreateGetPropertyIL(PropertyInfo propertyInfo, ILGenerator generator)
        {
            var getMethod = propertyInfo.GetGetMethod(true);

            if (getMethod == null)
                throw new InvalidOperationException("the property doesn't have getter method! ");

            if (!getMethod.IsStatic)
            {
                generator.Emit(OpCodes.Ldarg_0);

                if (propertyInfo.DeclaringType.IsValueType)
                    generator.Emit(OpCodes.Unbox, propertyInfo.DeclaringType);
                else
                    generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            }



            if (getMethod.IsFinal || !getMethod.IsVirtual)//set method
                generator.Emit(OpCodes.Call, getMethod);
            else
                generator.Emit(OpCodes.Callvirt, getMethod);


            if (propertyInfo.PropertyType.IsValueType)
                generator.Emit(OpCodes.Box, propertyInfo.PropertyType);
            else
                generator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);


            generator.Emit(OpCodes.Ret);
        }

        private static void generateCreateSetPropertyIL(PropertyInfo propertyInfo, ILGenerator generator)
        {
            var setMethod = propertyInfo.GetSetMethod(true);

            if (!setMethod.IsStatic)
            {
                generator.Emit(OpCodes.Ldarg_0);

                if (propertyInfo.DeclaringType.IsValueType)
                    generator.Emit(OpCodes.Unbox, propertyInfo.DeclaringType);
                else
                    generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            }

            generator.Emit(OpCodes.Ldarg_1);

            if (propertyInfo.PropertyType.IsValueType)
                generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
            else
                generator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);



            if (setMethod.IsFinal || !setMethod.IsVirtual)
                generator.Emit(OpCodes.Call, setMethod);
            else
                generator.Emit(OpCodes.Callvirt, setMethod);

            generator.Emit(OpCodes.Ret);
        }


    }
}
