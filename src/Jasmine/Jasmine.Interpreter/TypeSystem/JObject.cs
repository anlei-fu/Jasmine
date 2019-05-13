using GrammerTest.Grammer.TypeSystem.Exceptions;
using Jasmine.Interpreter.Scopes;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Jasmine.Interpreter.TypeSystem
{
    public enum JType
    {
        Object,
        Function,

        MappingObject,
        MappingFunction,

        //primitives
        String,
        Number,
        Bool,
        Undefined,
        Null,
        Void,

    }

    public interface IPropertyGetter
    {
        Any GetProperty(string name);
    }
    public interface IClonable
    {
        Any Clone();
    }
    public static class JTypeExtension
    {
        public static string ToString(this JType type)
        {
            switch (type)
            {
                case JType.Object:
                    return "object";
                case JType.Function:
                    return "function";
                case JType.MappingObject:
                    return "mappingobject";
                case JType.MappingFunction:
                    return "mappingfunction";
                case JType.String:
                    return "primitives.string";
                case JType.Number:
                    return "primitives.numeric";
                case JType.Bool:
                    return "primitives.bool";
                case JType.Undefined:
                    return "undefiened";
                case JType.Null:
                    return "null";
                case JType.Void:
                    return "void";
                default:
                    return "undefined";
            }

        }

    }



    public abstract class Any
    {

        private static readonly Dictionary<Type, JType> _typeMapping = new Dictionary<Type, JType>()
        {
            {typeof(JObject),JType.Object },
            {typeof(JFunction),JType.Function},
            {typeof(JMappingObject),JType.MappingObject },
            {typeof(JMappingFunction),JType.MappingFunction },
            {typeof(JString),JType.String },
            {typeof(JNumber),JType.Number },
            {typeof(JBool),JType.Bool },
            {typeof(JNull),JType.Null },
            {typeof(JVoid),JType.Void },
            {typeof(JUndefined),JType.Undefined },
        };
        public JType Type => _typeMapping[GetType()];
        public string Name { get; set; }
        public JObject Parent { get; set; }
        public bool HasParent => Parent != null;
        public abstract Type RuntimeType { get; }
        public abstract object GetObject();
        public static explicit operator string(Any obj)
        {
            if (obj is null)
            {
                return null;
            }
            else if (obj is JString str)
            {
                return str.Value;
            }
            else if (obj is JMappingObject jm)
            {
                return (string)jm.Instance;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static explicit operator double(Any obj)
        {
            if (obj is null)
            {
                throw new InvalidCastException();
            }
            else if (obj is JNumber jnum)
            {
                return jnum.Value;
            }
            else if (obj is JMappingObject jma)
            {
                return (double)(jma.Getter.Invoke(jma.Parent.Instance));
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static explicit operator bool(Any obj)
        {
            if (obj is null)
            {
                throw new InvalidCastException();
            }
            else if (obj is JBool b)
            {
                return b.Value;
            }
            else if (obj is JMappingObject jm)
            {
                return (bool)jm.Instance;
            }
            else
            {
                throw new InvalidCastException();
            }

        }
    }

    public class JObject : Any, IPropertyGetter, IClonable
    {
        public JObject()
        {

        }
        public JObject(string name)
        {
            Name = name;
        }
        private IDictionary<string, Any> _properties = new Dictionary<string, Any>();

        public override Type RuntimeType => GetType();

        public Any Clone()
        {
            return null;
        }


        public virtual bool HasProperty(string name)
        {
            return _properties.ContainsKey(name);
        }
        public virtual Any GetProperty(string name)
        {
            if (!_properties.ContainsKey(name))
                _properties.Add(name, new JUndefined(name));

            return _properties[name];
        }
        public virtual void SetProperty(string name, Any obj)
        {
            obj.Name = name;
            obj.Parent = this;
            _properties[name] = obj;
        }

        public virtual void AddProperty(string name, Any obj)
        {
            obj.Name = name;

            if (_properties.ContainsKey(name))
                throw new Exception();

            obj.Parent = this;
            _properties.Add(name, obj);
        }
        public virtual void RemoveProperty(string name, Any obj)
        {
            if (_properties.ContainsKey(name))
            {
                obj.Parent = null;
                _properties.Remove(name);
            }
        }


        public override object GetObject()
        {
            throw new NotImplementedException();
        }
        public virtual object ConvertToObject(Type type)
        {
            return null;
        }



    }


    public sealed class JFunction : Any
    {
        public const string RETURN = "__RETURN__";
        public FunctionBlock Block { get; set; } = new FunctionBlock(null);
        public string[] Parameters { get; set; }

        public override Type RuntimeType => typeof(Delegate);

        public Any Excute(ExcutingStack stack,params Any[] parameters)
        {

            if (parameters.Length != parameters.Length)
                throw new InvalidMethodCall();

            for (int i = 0; i < parameters.Length; i++)
            {
                Block.Declare(Parameters[i], parameters[i]);
            }

            Block.Excute(stack);

            var result = Block.GetVariable(RETURN);

            Block.UnsetAll();

            return result;

        }

        public override object GetObject()
        {
            throw new NotImplementedException();
        }
    }





    public sealed class JString : Any, IPropertyGetter, IClonable
    {
        public JString(string value)
        {
            Value = value;
        }
        public string Value { get; set; }

        public override Type RuntimeType => BaseTypes.TString;

        public JString SubString(int index, int count)
        {
            return null;
        }
        public JNumber IndexOf(string str)
        {
            return null;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            else if (obj is JMappingObject jmapping)
            {
                if (jmapping.InstanceType == BaseTypes.TString)
                    return (string)jmapping.Instance == Value;
                else
                    return false;
            }
            else if (obj is JString jstr)
            {
                return jstr.Value == Value;
            }
            else
            {
                return false;
            }

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public static explicit operator string(JString obj)
        {
            return obj.Value;
        }

        public override object GetObject()
        {
            return Value;
        }
        public override string ToString()
        {
            return Value;
        }

        public Any Clone()
        {
            return new JString(Value);
        }

        public Any GetProperty(string name)
        {
            throw new NotImplementedException();
        }
    }



    public sealed class JNumber : Any, IPropertyGetter, IClonable
    {

        public JNumber(int value)
        {
            Value = value;
        }
        public JNumber(double value)
        {
            Value = value;
        }


        public JNumber(string str)
        {
            if (!double.TryParse(str, out var value))
            {
                throw new NotConvertableException();
            }

            Value = value;
        }
        public static JNumber CreateJNumber(object obj)
        {
            return new JNumber(Convert.ToDouble(obj));
        }
        public double Value { get; set; }

        public override Type RuntimeType => BaseTypes.TDouble;

        public static explicit operator uint(JNumber number)
        {
            return Convert.ToUInt32(number.Value);
        }

        public static explicit operator short(JNumber number)
        {
            return Convert.ToInt16(number.Value);
        }
        public static explicit operator ushort(JNumber number)
        {
            return Convert.ToUInt16(number.Value);
        }
        public static explicit operator long(JNumber number)
        {
            return Convert.ToInt64(number.Value);
        }
        public static explicit operator ulong(JNumber number)
        {
            return Convert.ToUInt64(number.Value);
        }

        public static explicit operator decimal(JNumber number)
        {
            return Convert.ToDecimal(number.Value);
        }
        public static explicit operator int(JNumber number)
        {
            return Convert.ToInt32(number.Value);
        }

        public static explicit operator float(JNumber number)
        {
            return Convert.ToSingle(number.Value);
        }

        public static explicit operator double(JNumber number)
        {
            return number.Value;
        }

        public static bool operator >(JNumber num1, JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value > num2.Value;
        }
        public static bool operator <(JNumber num1, JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value < num2.Value;
        }
        public static bool operator ==(JNumber num1, JNumber num2)
        {
            if (num2 is null)
                return false;

            return num1.Value == num2.Value;
        }
        public static bool operator !=(JNumber num1, JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value != num2.Value;
        }
        public static bool operator >=(JNumber num1, JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value >= num2.Value;
        }
        public static bool operator <=(JNumber num1, JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value <= num2.Value;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            else if (obj is JMappingObject jmapping)
            {
                if (BaseTypes.Numbers.Contains(jmapping.InstanceType))
                {
                    return JMappingObject.ToDouble(jmapping.InstanceType, jmapping.Instance) == Value;
                }
                else
                {
                    return false;
                }
            }
            else if (obj is JNumber)
            {
                return ((JNumber)obj).Value == Value;
            }
            else
            {
                return false;
            }


        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override object GetObject()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public Any Clone()
        {
            return new JNumber(Value);
        }

        public Any GetProperty(string name)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class JBool : Any, IClonable
    {
        public JBool(bool value)
        {
            Value = value;
        }

        public JBool(string value)
        {
            Value = bool.Parse(value);

        }

        public bool Value { get; set; }

        public override Type RuntimeType => BaseTypes.TBoolean;

        public override string ToString()
        {
            return Value.ToString();
        }
        public static explicit operator bool(JBool obj)
        {
            return obj.Value;
        }
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            else if (obj is JMappingObject jmapping)
            {
                if (jmapping.InstanceType == BaseTypes.TBoolean)
                    return Value == (bool)jmapping.Instance;
                else
                    return false;
            }
            else if (obj is JBool jb)
            {
                return jb.Value == Value;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Any Clone()
        {
            return new JBool(Value);
        }

        public override object GetObject()
        {
            throw new NotImplementedException();
        }
    }
    public class JMappingObject : Any, IPropertyGetter, IClonable
    {
        public JMappingObject(object instance)
        {
            Instance = instance;
        }
        public JMappingObject()
        {

        }
        public JMappingObject(object instance,
                              JMappingObject parent,
                              string name,
                              Func<object, object> getter,
                              Action<object, object> setter)
        {
            Instance = instance;
            Parent = parent;
            Name = name;
            Getter = getter;
            Setter = setter;
        }
        private Dictionary<string, JMappingObject> _properties = new Dictionary<string, JMappingObject>();

        private static readonly ITypeCache _cache = JasmineReflectionCache.Instance;
        public new JMappingObject Parent { get; set; }
        public object Instance { get; set; }
        public virtual Type InstanceType => Instance.GetType();
        public Func<object, object> Getter { get; set; }
        public Action<object, object> Setter { get; set; }

        public override Type RuntimeType => InstanceType;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator JString(JMappingObject obj)
        {
            return new JString((string)obj.Instance);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator JBool(JMappingObject obj)
        {
            return new JBool((bool)obj.Instance);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator JNumber(JMappingObject obj)
        {
            return new JNumber(ToDouble(obj.InstanceType, obj.Instance));
        }



        public JMappingObject GetProperty(string name)
        {

            /*
             *  search in the properties cache
             */
            if (_properties.ContainsKey(name))
            {
                if (_properties[name].Getter != null)
                {
                    /*
                     * mutiple thread run
                     * must reget value ,avoid instance has changed in c# code,but we still cache the old value
                     */
                    _properties[name].Instance = _properties[name].Getter(Instance);

                }

                return _properties[name];
            }


            var property = _cache.GetItem(InstanceType).Properties.GetItemByName(name);

            if (property != null)
            {

                var newItem = new JMappingObject(property.GetValue(Instance), this, name, property.Getter, property.Setter);
                _properties.Add(name, newItem);

                return _properties[name];
            }


            var field = _cache.GetItem(InstanceType).Fileds.GetItemByName(name);

            if (field != null)
            {
                var newItem = new JMappingObject(field.GetValue(Instance), this, name, field.Getter, field.Setter);

                _properties.Add(name, newItem);

                return _properties[name];
            }

            var methods = _cache.GetItem(InstanceType).Methods.GetMethodsByName(name);

            if (methods != null)
            {
                if (methods.Length == 1)
                {
                    _properties.Add(name, new JMappingFunction(this, Instance, methods[0].Invoker, methods[0].ParamerTypies, name));
                }
                else
                {
                    var functions = new List<JMappingFunction>();

                    foreach (var item in methods)
                    {
                        functions.Add(new JMappingFunction(this, Instance, item.Invoker, item.ParamerTypies, name));
                    }

                    _properties.Add(name, new JMappingFunctions(functions));

                }

                return _properties[name];
            }


            throw new PropertyNotFoundException();
        }

        public virtual void SetProperty(object obj)
        {
            if (Setter == null)
                throw new NotSettableException();

            if (BaseTypes.Base.Contains(obj.GetType()))
                Instance = EnsureCorrectNumberType(obj.GetType(), obj);
            else
                Instance = obj;

            Setter.Invoke(Parent.Instance, Instance);
        }

        public override object GetObject()
        {
            return Instance;
        }

        public Any ToJObject()
        {
            if (BaseTypes.Numbers.Contains(InstanceType))
            {
                return new JNumber(ToDouble(InstanceType, Instance));
            }
            else if (BaseTypes.TBoolean == InstanceType)
            {
                return new JBool((bool)Instance);
            }
            else if (BaseTypes.TString == InstanceType)
            {
                return new JString((string)Instance);
            }
            else
            {
                return this;
            }
        }

        public static double ToDouble(Type type, object instance)
        {
            switch (type.FullName)
            {
                case BaseTypes.Int:
                    return Convert.ToInt32(instance);
                case BaseTypes.Short:
                    return Convert.ToInt16(instance);
                case BaseTypes.Byte:
                    return Convert.ToByte(instance);
                case BaseTypes.Float:
                    return Convert.ToSingle(instance);
                case BaseTypes.Decimal:
                    return (double)Convert.ToDecimal(instance);
                case BaseTypes.SByte:
                    return Convert.ToSByte(instance);
                case BaseTypes.UInt:
                    return Convert.ToUInt32(instance);
                case BaseTypes.ULong:
                    return Convert.ToUInt64(instance);
                case BaseTypes.Long:
                    return Convert.ToInt64(instance);
                case BaseTypes.UShort:
                    return Convert.ToUInt16(instance);
                default:
                    return (double)instance;
            }
        }


        public static object EnsureCorrectNumberType(Type type, object value)
        {
            switch (type.FullName)
            {
                case BaseTypes.Int:
                    return Convert.ToInt32(value);
                case BaseTypes.UInt:
                    return Convert.ToUInt32(value);
                case BaseTypes.UShort:
                    return Convert.ToUInt16(value);
                case BaseTypes.Short:
                    return Convert.ToInt16(value);
                case BaseTypes.Long:
                    return Convert.ToInt64(value);
                case BaseTypes.ULong:
                    return Convert.ToUInt64(value);
                case BaseTypes.Float:
                    return Convert.ToSingle(value);
                case BaseTypes.Decimal:
                    return Convert.ToDecimal(value);
                case BaseTypes.Byte:
                    return Convert.ToByte(value);
                case BaseTypes.SByte:
                    return Convert.ToSByte(value);
                default:
                    return value;
            }

        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
            {
                return false;
            }
            else if (obj is JNumber jnum)
            {
                return jnum.Equals(this);
            }
            else if (obj is JString jstr)
            {
                return jstr.Equals(this);
            }
            else if (obj is JBool jb)
            {
                return jb.Equals(this);
            }
            else if (obj is JMappingObject jma)
            {
                if (BaseTypes.Numbers.Contains(InstanceType) && BaseTypes.Numbers.Contains(jma.InstanceType))
                    return Convert.ToDouble(Instance) == Convert.ToDouble(jma.Instance);
                else
                    return jma.Instance == Instance;
            }
            else
            {
                throw new InvalidTypeOccuredException();
            }

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $" mapping object:{InstanceType}";
        }

        public Any Clone()
        {
            throw new NotImplementedException();
        }

        Any IPropertyGetter.GetProperty(string name)
        {
            throw new NotImplementedException();
        }
    }

    public class JMappingFunction : JMappingObject
    {
        public JMappingFunction()
        {

        }
        public JMappingFunction(JMappingObject parent, object instance, Func<object, object[], object> invoker, Type[] parameterTypes, string name) : base()
        {
            Parent = parent;
            Instance = instance;
            Invoker = invoker;
            Name = name;
            ParameterTypes = parameterTypes;
        }

        public Type[] ParameterTypes { get; set; }

        public Func<object, object[], object> Invoker { get; private set; }


        public override void SetProperty(object obj)
        {
            throw new OnlyJmappingObjectSurpportException();
        }

        public override object GetObject()
        {
            return Invoker;
        }


    }

    public class JMappingFunctions : JMappingObject
    {
        public JMappingFunctions(IEnumerable<JMappingFunction> functions)
        {
            _functions = functions.ToArray();
        }
        private JMappingFunction[] _functions;
        public JMappingFunction TryGetFunction(Type[] parameterTypes)
        {
            JMappingFunction ret = null;

            var functions = new List<JMappingFunction>();

            foreach (var item in _functions)
            {
                if (item.ParameterTypes != parameterTypes)
                    continue;

                if (TypeUtils.CompareTypes(item.ParameterTypes, parameterTypes))
                {
                    functions.Add(item);
                }
            }

            if (functions.Count != 0)
            {
                var max = 0;

                ret = functions[0];

                foreach (var function in functions)
                {
                    var t = 0;

                    for (int i = 0; i < function.ParameterTypes.Length; i++)
                    {
                        if (parameterTypes[i] == null)
                            continue;
                        else if (parameterTypes[i] == function.ParameterTypes[i])
                            t += 2;
                        else
                            t += 1;
                    }

                    if (t > max)
                    {
                        ret = function;
                        max = t;
                    }
                }




            }

            return ret;
        }
    }


    public sealed class JNull : Any
    {
        public override Type RuntimeType => GetType();

        public override object GetObject()
        {
            throw new NotImplementedException();
        }
    }

    public sealed class JVoid : Any
    {
        public override Type RuntimeType =>typeof(void);

        public override object GetObject()
        {
            throw new NotImplementedException();
        }
    }


    public sealed class JUndefined : Any
    {
        public JUndefined(string name)
        {
            Name = name;
        }

        public override Type RuntimeType => throw new NotImplementedException();

        public override object GetObject()
        {
            throw new NotImplementedException();
        }
    }


    public class JMappingTypeInfo : Any
    {
        public override Type RuntimeType => throw new NotImplementedException();

        public JMappingObject NewInstance(params JObject[] parameters)
        {
            return null;
        }
        public JMappingStaticFunction FindStaticFunction(string name)
        {
            return null;
        }



        public override object GetObject()
        {
            throw new NotImplementedException();
        }
    }

    public class JMappingStaticFunction : JMappingFunction
    {

    }
}
