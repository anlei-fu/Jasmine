using GrammerTest.Grammer;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using GrammerTest.Grammer.TypeSystem.Exceptions;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public enum JType
    {
        Object,
        Property,
        Function,

        MappingObject,
        MappingProperty,
        MappingFunction,

        //primitives
        String,
        Number,
        Bool,
        Time,
        Map,
        Array,

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
                case JType.Property:
                    return "property";
                case JType.Function:
                    return "function";
                case JType.MappingObject:
                    return "mappingobject";
                case JType.MappingProperty:
                    return "mappingproperty";
                case JType.MappingFunction:
                    return "mappingfunction";
                case JType.String:
                    return "primitives.string";
                case JType.Number:
                    return "primitives.numeric";
                case JType.Bool:
                    return "primitives.bool";
                case JType.Time:
                    return "primitives.bool";
                case JType.Map:
                    return "primitives.map";
                case JType.Array:
                    return "primitives.array";
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
            {typeof(JTime),JType.Time },
            {typeof(JArray),JType.Array },
            {typeof(JNull),JType.Null },
            {typeof(JVoid),JType.Void },
        };

        public JObject Parent { get; set; }
        public bool HasParent => Parent != null;

        public virtual JObject Clone()
        {
            return null;
        }
        public JType Type => _typeMapping[GetType()];
        public IDictionary<string, JObject> Properties { get; set; }
        public bool HasProperty(string name)
        {
            return Properties.ContainsKey(name);
        }
        public JObject GetProperty(string name)
        {
            return Properties[name];
        }
        public void SetProperty(string name, JObject obj)
        {
            obj.Name = name;
            Properties[name] = obj;
        }

       public virtual void AddProperty(string name,JObject obj)
        {
            obj.Name = name;

            if (Properties.ContainsKey(name))
                throw new Exception();

            Properties.Add(name, obj);
        }
        public virtual void RemoveProperty(string name,JObject obj)
        {
            if (Properties.ContainsKey(name))
                Properties.Remove(name);
        }
       

        public virtual object  GetObject()
        {
            return this;
        }
        public override bool Equals(object obj)
        {
            switch (Type)
            {
                case JType.String:

                    JString str = this as JString;
                    JString otherStr = obj as JString;

                    if (otherStr == null)
                    {
                        return false;
                    }

                    return str.Value == otherStr.Value;

                case JType.Number:

                    JNumber num = this as JNumber;
                    JNumber otherNumber = obj as JNumber;


                    return num.Value == otherNumber.Value;


                case JType.Bool:

                    JBool jBool = this as JBool;
                    JBool otherBool = obj as JBool;

                    if (otherBool == null)
                    {
                        return false;
                    }

                    return jBool.Value == jBool.Value;

                case JType.Time:
                case JType.Map:
                case JType.Array:
                case JType.Object:
                default:
                    return this.Equals(obj);
            }

        }

        public static explicit operator string(JObject obj)
        {
            if (obj is JString str)
                return str.Value;
            else if(obj is JMappingObject jm)
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
            if(obj is JNumber jnum)
            {
                return jnum.Value;
            }
            else if(obj is JMappingObject jma)
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
            if (obj is JBool b)
                return b.Value;

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

   
    public class JFunction : JObject
    {
        public const string RETURN = "__RETURN__";
        public FunctionBlock Block { get; set; } = new FunctionBlock(null);
        public string[] Parameters { get; set; }
      

        public JObject Excute( params JObject[] parameters)
        {

            if (parameters.Length != parameters.Length)
                throw new InvalidMethodCall();

            for (int i = 0; i < parameters.Length; i++)
            {
                Block.Declare(Parameters[i], parameters[i]);
            }

            


             Block.Excute();

            return Block.GetVariable(RETURN);

        }


    }
   

    public class JString:JObject
    {
        public JString(string value)
        {
            Value = value;
        }
        public string Value { get; set; }

     
        public JString SubString(int index,int count)
        {
            return null;
        }
        public JNumber IndexOf(string str)
        {
            return null;
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType()==typeof(string))
            {
                return Value == (string)obj;
            }

            if (obj.GetType() != typeof(JString))
                return false;


            return Value==((JString)obj).Value;
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

    public class JNumber : JObject
    {
        public JNumber(int value)
        {
            Value = (double)value;
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

        public double Value { get; set; }


       

        public static explicit operator int(JNumber number)
        {

            return (int)number;
        }

        public static explicit operator float(JNumber number)
        {
            return (float)number;
        }

        public static explicit operator double(JNumber number)
        {
            return (double)number;
        }

        public static bool operator>(JNumber num1,JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value > num2.Value;
        }
        public static bool operator<(JNumber num1,JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value < num2.Value;
        }

        public static bool operator ==(JNumber num1,JNumber num2)
        {
            if (num2 is null)
                return false;

            return num1.Value == num2.Value;
        }

        public static bool operator !=(JNumber num1,JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value != num2.Value;
        }
        public static bool operator >=(JNumber num1,JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value >= num2.Value;
        }
        public static bool operator<=(JNumber num1,JNumber num2)
        {
            if (num2 == null)
                return false;

            return num1.Value <= num2.Value;
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
    public class JTime:JObject
    {
        public DateTime Value { get; set; }

    }
    public class JArray:JObject
    {
        public void Add(JObject obj)
        {

        }
        public JObject GetItem(int index)
        {
            return null;
        }

        public void SetItem(int index,JObject obj)
        {

        }
        public int Count()
        {
            return 0;
        }
    }
    public class JMap:JObject
    {

        public void Add(string key,JObject obj)
        {

        }
        public void Remove(string key)
        {

        }
        public bool Contains(string key)
        {
            return true;
        }
        public  JObject GetValue(string key)
        {
            return null;
        }
    }
    public class JBool:JObject
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

        public override JObject Clone()
        {
            return new JBool(Value);
        }

    }
    public class JMappingObject:JObject
    {
        public JMappingObject()
        {

        }
        public JMappingObject(object instance,
                              JMappingObject parent,
                               string name,
                               Func<object,object>getter,
                               Action<object,object>setter)
        {
            Instance = instance;
            Parent = parent;
            Name = name;
            Getter = getter;
            Setter = setter;
        }
        private static readonly ITypeCache _cache = JasmineReflectionCache.Instance;
        public new JMappingObject Parent { get; set; }
        public Block Block { get; set; }
        public object Instance { get; set; }
        public Type InstanceType => Instance.GetType();
        private Type _instanceType;
        public new Dictionary<string, JMappingObject> Properties { get; set; } = new Dictionary<string, JMappingObject>();

        public Func<object, object> Getter { get; set; }
        public Action<object, object> Setter { get; set; }

        public static explicit operator string(JMappingObject obj)
        {
            return null;
        }
        public static explicit operator bool(JMappingObject obj)
        {
            return true;
        }
        public static explicit operator double(JMappingObject obj)
        {
            return 2d;
        }

        public  new JMappingObject GetProperty(string name)
        {

            /*
             *  search in the property cache
             */
            if (Properties.ContainsKey(name))

                if (Properties[name].Getter != null)
                {
                    /*
                     * must reget value ,avoid instance has changed in c# code,but we still cache the old value
                     */ 
                    Properties[name].Instance = Properties[name].Getter(Instance);

                    return Properties[name];
                }



            /*
             * type is null
             */ 
            if (_instanceType == null)
                _instanceType = Instance.GetType();

            var property = _cache.GetItem(InstanceType).Properties.GetItemByName(name);

            if (property != null)
            {
                Properties.Add(name, new JMappingObject(property.GetValue(Instance), this,name,property.Getter,property.Setter));

                return Properties[name];
            }


            var field = _cache.GetItem(InstanceType).Fileds.GetItemByName(name);

           if(field!=null)
            {
                Properties.Add(name, new JMappingObject(field.GetValue(Instance), this, name,field.Getter,field.Setter));

                return Properties[name];
            }

            var method = _cache.GetItem(InstanceType).Methods.GetItemByName(name);

            if(method!=null)
            {
                Properties.Add(name, new JMappingFunction(Instance, method.Invoker, name));

                return Properties[name];
            }


            throw new PropertyNotFoundException();
        }

        public  void SetProperty(object obj)
        {
            Instance = obj;

            if(Parent!=null)
            {
                if (BaseTypes.Base.Contains(obj.GetType()))
                    obj = ConverNumber(obj.GetType(), obj);

                Setter.Invoke(Parent.Instance, obj);
            }

        }

        public override object GetObject()
        {
            return Instance;
        }


        public static object ConverNumber(Type type, object value)
        {
            switch (type.FullName)
            {
                case BaseTypes.Int:
                    return (int)value;
                case BaseTypes.UInt:
                    return (uint)value;
                case BaseTypes.UShort:
                    return (ushort)value;
                case BaseTypes.Short:
                    return (short)value;
                case BaseTypes.Long:
                    return (long)value;
                case BaseTypes.ULong:
                    return (ulong)value;
                case BaseTypes.Float:
                    return (float)value;

                case BaseTypes.Decimal:
                    return (decimal)value;
                default:
                    return value;
            }

        }

    }


  




  
    public class JMappingFunction:JMappingObject
    {
        public JMappingFunction(object instance, Func<object,object[],object>invoker, string name) : base()
        {
            Instance = instance;
            Invoker = invoker;
            Name = name;
        }

        public int ParameterLength { get; set; }

        public Func<object, object[], object> Invoker { get; private set; }

        public JObject Excute(params JObject[] parameters)
        {
            return null;
        }
    }



    public class JNull:JObject
    {
        public object Value = null;

    }

    public class JVoid:JObject
    {

    }

   
}
