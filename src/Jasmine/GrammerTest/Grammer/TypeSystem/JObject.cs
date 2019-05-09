using GrammerTest.Grammer;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using GrammerTest.Grammer.TypeSystem.Exceptions;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jasmine.Spider.Grammer
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


    public class JObject
    {
        public JObject()
        {

        }
        public JObject(string name)
        {
            Name = name;
        }
        private IDictionary<string, JObject> _properties;
        public string Name { get; set; }

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

        public JObject Parent { get; set; }
        public bool HasParent => Parent != null;

        public virtual JObject Clone()
        {
            return null;
        }
        public JType Type => _typeMapping[GetType()];

        public virtual bool HasProperty(string name)
        {
            return _properties.ContainsKey(name);
        }
        public virtual JObject GetProperty(string name)
        {
            if (!_properties.ContainsKey(name))
                _properties.Add(name, new JUndefined(name));

            return _properties[name];
        }
        public virtual void SetProperty(string name, JObject obj)
        {
            obj.Name = name;
            obj.Parent = this;
            _properties[name] = obj;
        }

        public virtual void AddProperty(string name, JObject obj)
        {
            obj.Name = name;

            if (_properties.ContainsKey(name))
                throw new Exception();

            obj.Parent = this;
            _properties.Add(name, obj);
        }
        public virtual void RemoveProperty(string name, JObject obj)
        {
            if (_properties.ContainsKey(name))
            {
                obj.Parent = null;
                _properties.Remove(name);
            }
        }


        public virtual object GetObject()
        {
            throw new NotImplementedException();
        }
        public virtual object ConvertToObject(Type type)
        {
            return null;
        }

        public static explicit operator string(JObject obj)
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

        public static explicit operator double(JObject obj)
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

        public static explicit operator bool(JObject obj)
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


    public sealed class JFunction : HideJObjectSomeProperties
    {
        public const string RETURN = "__RETURN__";
        public FunctionBlock Block { get; set; } = new FunctionBlock(null);
        public string[] Parameters { get; set; }


        public JObject Excute(params JObject[] parameters)
        {

            if (parameters.Length != parameters.Length)
                throw new InvalidMethodCall();

            for (int i = 0; i < parameters.Length; i++)
            {
                Block.Declare(Parameters[i], parameters[i]);
            }

            Block.Excute();

            var result = Block.GetVariable(RETURN);

            Block.UnsetAll();

            return result;

        }


    }

    public abstract class HideJObjectSomeProperties : JObject
    {
        public override JObject GetProperty(string name)
        {
            throw new OnlyJobjectSurpportThisMethodException();
        }

        public override void AddProperty(string name, JObject obj)
        {
            throw new OnlyJobjectSurpportThisMethodException();
        }

        public override bool HasProperty(string name)
        {
            throw new OnlyJobjectSurpportThisMethodException();
        }

        public override void RemoveProperty(string name, JObject obj)
        {
            throw new OnlyJobjectSurpportThisMethodException();
        }
        public override void SetProperty(string name, JObject obj)
        {
            throw new OnlyJobjectSurpportThisMethodException();
        }

        public override object ConvertToObject(Type type)
        {
            throw new OnlyJobjectSurpportThisMethodException();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            else if (obj is JNull)
            {
                return false;
            }
            else if (obj is JVoid)
            {
                return false;
            }
            else if (obj is JUndefined)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }



    public sealed class JString : HideJObjectSomeProperties
    {
        public JString(string value)
        {
            Value = value;
        }
        public string Value { get; set; }


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

        public override JObject Clone()
        {
            return new JString(Value);
        }


    }



    public sealed class JNumber : HideJObjectSomeProperties
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

        public new JMappingFunction GetProperty(string name)
        {
            return null;
        }


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

        public override JObject Clone()
        {
            return new JNumber(Value);
        }
    }

    public sealed class JBool : HideJObjectSomeProperties
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
            else if(obj is JMappingObject jmapping)
            {
                if (jmapping.InstanceType == BaseTypes.TBoolean)
                    return Value == (bool)jmapping.Instance;
                else
                    return false;
            }
            else if(obj is JBool jb)
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

        public override JObject Clone()
        {
            return new JBool(Value);
        }

    }
    public class JMappingObject : HideJObjectSomeProperties
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



        public new virtual JMappingObject GetProperty(string name)
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
                _properties.Add(name,newItem);

                return _properties[name];
            }


            var field = _cache.GetItem(InstanceType).Fileds.GetItemByName(name);

            if (field != null)
            {
                var newItem= new JMappingObject(field.GetValue(Instance), this, name, field.Getter, field.Setter);

                _properties.Add(name,newItem);

                return _properties[name];
            }

            var method = _cache.GetItem(InstanceType).Methods.GetItemByName(name);

            if (method != null)
            {
                _properties.Add(name, new JMappingFunction(this, Instance, method.Invoker, name));

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

        public JObject ToJObject()
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
    }

    public class JMappingFunction : JMappingObject
    {
        public JMappingFunction()
        {

        }
        public JMappingFunction(JMappingObject parent, object instance, Func<object, object[], object> invoker, string name) : base()
        {
            Parent = parent;
            Instance = instance;
            Invoker = invoker;
            Name = name;
        }

        public int ParameterLength { get; set; }
        public Func<object, object[], object> Invoker { get; private set; }


        public override void SetProperty(object obj)
        {
            throw new OnlyJmappingObjectSurpportException();
        }

        public override object GetObject()
        {
            return Invoker;
        }

        public override JMappingObject GetProperty(string name)
        {
            throw new OnlyJmappingObjectSurpportException();
        }
    }



    public sealed class JNull : HideJObjectSomeProperties
    {
    }

    public sealed class JVoid : HideJObjectSomeProperties
    {

    }


    public sealed  class JUndefined : HideJObjectSomeProperties
    {
        public JUndefined(string name)
        {
            Name = name;
        }
    }


    public class JMappingTypeInfo : JObject
    {
        public JMappingObject NewInstance(params JObject[] parameters)
        {
            return null;
        }
        public JMappingStaticFunction FindStaticFunction(string name)
        {
            return null;
        }


    }

    public class JMappingStaticFunction:JMappingFunction
    {

    }
}
