using Jasmine.Reflection;
using System;

namespace Jasmine.Ioc
{
    public class JasminePropertyStringConvertor
    {
        public static T Convert<T>(string source)
        {
            return (T)Convert(typeof(T), source);
        }
        public static object Convert(Type type, string source)
        {
            return  type.IsBaseType() ? JasmineStringValueConvertor.FromString(source, type) :
                                           Newtonsoft.Json.JsonConvert.DeserializeObject(source,type);
        }
       
    }
}
