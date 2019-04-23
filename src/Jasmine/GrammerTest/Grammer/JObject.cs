using System;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public  class JObject
    {
        public string Name { get; set; }
        public JType Type { get; set; }
        public IDictionary<string,JObject> Items { get; set; }
        public bool Conatins(string name)
        {
            return Items.ContainsKey(name);
        }
        public JObject GetItem(string name)
        {
            return Items[name];
        }
        public void AddItem(string name,JObject obj)
        {
            Items.Add(name, obj);
        }


    }

    public enum JType
    {
        String,
        Number,
        Bool,
        Time,
        Map,
        Array,
        Object,
        Nan
    }

    public class JString:JObject
    {
        public string Value { get; set; }

     
        public JString SubString(int index,int count)
        {
            return null;
        }
        public JNumber IndexOf(string str)
        {
            return null;
        }
    }

    public class JNumber:JObject
    {
        public float Value { get; set; }
      
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
        public bool Value { get; set; }

    }
    public class JMapping:JObject
    {
        public object Instance { get; set; }
        public Type InstanceType { get; set; }
    }
}
