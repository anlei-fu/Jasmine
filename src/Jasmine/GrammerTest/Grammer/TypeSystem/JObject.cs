using GrammerTest.Grammer;
using GrammerTest.Grammer.Scopes;
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
        public string Name { get; set; }

        private static readonly Dictionary<Type, JType> _typeMapping = new Dictionary<Type, JType>()
        {
            {typeof(JObject),JType.Object },
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

        public JObject Parent { get; set; }
        public bool HasParent => Parent != null;

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
            Properties.Add(name,obj);
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
      
       // private string _returnName => Scope.FunctionName + "_return";
        public FunctionBlock Block { get; set; }
        public string[] Paramenters { get; set; }
        public Block Body { get; set; }

        public JObject Excute(Block parent, params JObject[] parameters)
        {

            Block.Parent = parent;


            return null;

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


        public override bool Equals(object obj)
        {

            return Value.Equals(obj);
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

        public static bool operator>(JNumber num1,JNumber num2)
        {
            return num1.Value > num2.Value;
        }
        public static bool operator<(JNumber num1,JNumber num2)
        {
            return num1.Value < num2.Value;
        }

        public static bool operator ==(JNumber num1,JNumber num2)
        {
            return num1.Value == num2.Value;
        }

        public static bool operator !=(JNumber num1,JNumber num2)
        {
            return num1.Value != num2.Value;
        }
        public static bool operator >=(JNumber num1,JNumber num2)
        {
            return num1.Value >= num2.Value;
        }
        public static bool operator<=(JNumber num1,JNumber num2)
        {
            return num1.Value <= num2.Value;
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
            Value = bool.Parse(value);

        }

        public bool Value { get; set; }

    }
    public class JMappingObject:JObject
    {
        public new JMappingObject Parent { get; set; }
        public Block Block { get; set; }
        public object Instance { get; set; }
        public Type InstanceType { get; set; }

        public new Dictionary<string, JMappingObject> Properties { get; set; }

        public  new JMappingObject GetProperty(string name)
        {
            if(Parent!=null)
            {

            }
           
            return null;
        }

        public  void SetProperty(string name,object obj)
        {
            if(Parent!=null)
            {

            }
            else
            {

            }
        }
     


    }


    public static class JMappingObjectExtension
    {
        public static bool TrySetValue(this JMappingObject obj, JObject jobject)
        {
            if(jobject is JMappingObject jmp)
            {
               if(obj.Parent!=null)
                {
                    obj.Parent.SetProperty(obj.Name, jmp.Instance);
                }
               else
                {
                    obj.Block.Reset(obj.Name, jmp);
                }

                return true;
            }
            else if(jobject is JString js)
            {
                if (obj.InstanceType == BaseTypes.TString)
                {
                    if(obj.Parent!=null)
                    {
                        obj.Parent.SetProperty(obj.Name, js.GetObject());

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if(jobject is JBool jb)
            {
                if (obj.InstanceType == BaseTypes.TBoolean)
                {

                    if (obj.Parent != null)
                    {
                        obj.Parent.SetProperty(obj.Name, jb.GetObject());

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else if(jobject is JNumber jm)
            {
                if(BaseTypes.Numbers.Contains(obj.InstanceType))
                {
                    if (obj.Parent != null)
                    {
                        if (obj.InstanceType == BaseTypes.TInt)
                        {
                            obj.Parent.SetProperty(obj.Name, (int)jm.Value);
                        }
                        else if (obj.InstanceType == BaseTypes.TLong)
                        {
                            obj.Parent.SetProperty(obj.Name, (long)jm.Value);
                        }
                        else if (obj.InstanceType == BaseTypes.TFloat)
                        {
                            obj.Parent.SetProperty(obj.Name, (float)jm.Value);
                        }
                        else
                        {
                            obj.Parent.SetProperty(obj.Name, jm.Value);
                        }

                        return true;
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
            else if(jobject is JNull jn)
            {
                if(obj.Parent!=null)
                {
                    obj.Parent.SetProperty(obj.Name, null);

                    return true;
                }
                else
                {
                    obj.Block.Reset(obj.Name, jn);

                    return true;
                }
            }
            else
            {
                return false;
            }


        }
    }




    public class JMappingProperty:JMappingObject
    {

        public new JMappingObject Parent { get; }

        public Func<object,object> Getter { get; private set; }
        public Action<object, object> Setter { get; private set; }

        public void SetValue(JObject value)
        {

            if(this.TrySetValue(value))
            {

            }
            else
            {

            }

        }
        public JMappingObject GetValue()
        {
           if(Instance==null)
            {
                if(Parent!=null)
                {
                    Instance = Getter.Invoke(Parent.Instance);
                }
            }

            return this;
        }
        
    }
    public class JMappingFunction:JObject
    {
        public JMappingObject Parent { get; }
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
