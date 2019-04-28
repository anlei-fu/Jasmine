using GrammerTest.Grammer;
using GrammerTest.Grammer.Scopes;
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

        private static readonly Dictionary<Type, JType> _typeMapping = new Dictionary<Type, JType>()
        {
            {typeof(JObject),JType.Object },
            {typeof(JProperty),JType.Property },
            {typeof(JFunction),JType.Function},
            {typeof(JMappingObject),JType.MappingObject },
            {typeof(JMappingProperty),JType.MappingProperty},
            {typeof(JMappingFunction),JType.MappingFunction },
            {typeof(JString),JType.String },
            {typeof(JNumber),JType.Number },
            {typeof(JBool),JType.Bool },
            {typeof(JTime),JType.Time },
            {typeof(JArray),JType.Array },
            {typeof(JNull),JType.Null },
            {typeof(JVoid),JType.Void },
        };


        public JType Type => _typeMapping[GetType()];
        public IDictionary<string, JProperty> Properties { get; set; }
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
            Properties.Add(name, (JProperty)obj);
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

                    if (otherNumber == null)
                    {
                        return false;
                    }

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
                    break;
                case JType.Map:
                    break;
                case JType.Array:
                    break;
                case JType.Object:
                    break;
                default:
                    break;
            }

            return true;
        }

        public static explicit operator string(JObject obj)
        {
            return null;
        }
     
        public static explicit operator bool(JObject obj)
        {
            return true;
        }

    }

    public class JProperty:JObject
    {
        public string Name { get; set; }
        public JObject Value { get; set; }
    }
    public class JFunction : JObject
    {
        public string Name { get; set; }
       // private string _returnName => Scope.FunctionName + "_return";
        public FunctionBlock Scope { get; set; }
        public string[] Paramenters { get; set; }
        public Block Body { get; set; }

        //public JObject Excute(Block parent, params JObject[] parameters)
        //{

        //    Scope.Parent = parent;

        //    var ret = Scope.Declare(_returnName, new JVoid());

        //    if (Scope.ParameterNames.Length != parameters.Length)
        //    {

        //    }


        //    for (int i = 0; i < parameters.Length; i++)
        //    {
        //        Scope.Declare(Scope.ParameterNames[i], parameters[i]);
        //    }

        //    Scope.Excute();


        //    return ret;

        //}


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
    }

    public class JNumber:JObject
    {
        public JNumber (int value)
        {

        }
        public JNumber (double value)
        {

        }

        public JNumber(string str)
        {

        }

        public double Value { get; set; }


        public override bool Equals(object obj)
        {

            return  Value.Equals(obj);
        }

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

        public override int GetHashCode()
        {
            return base.GetHashCode();
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

        }

        public bool Value { get; set; }

    }
    public class JMappingObject:JObject
    {
        public object Instance { get; set; }
        public Type InstanceType { get; set; }

        public new Dictionary<string, JMappingProperty> Properties { get; set; }
        public Dictionary<KeyValuePair<string, int>, JMappingFunction> Methods { get; set; }

        public new JMappingProperty GetProperty(string name)
        {
            return null;
        }
        public JMappingFunction GetFunction(string name,int length)
        {
            return null;
        }


    }

    public class JMappingProperty:JMappingObject
    {

        public JMappingObject Parent { get; }

        public Func<object> Getter { get; private set; }
        public Action<object, object> Setter { get; private set; }

        public void SetValue(JObject value)
        {

        }
        public JObject GetValue()
        {
            return null;
        }
        
    }
    public class JMappingFunction:JObject
    {
        public JMappingObject Parent { get; }
        public string Name { get; set; }
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
