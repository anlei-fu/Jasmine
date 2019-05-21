using Jasmine.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Extensions
{
  public static  class StreamExtension
    {
        public static void WriteData(this Stream stream,object data, SerializeMode mode)
        {

        }

        public static Task WriteDataAsync(this Stream stream, object data, SerializeMode mode)
        {
            return null;
        }

        public static T ReadData<T>(this Stream stream,SerializeMode mode)
        {
            return default(T);
        }
        public static Task<T> ReadDataAsync<T>(this Stream stream, SerializeMode mode)
        {
            return null;
        }
        public static object ReadData(this Stream stream, Type type, SerializeMode mode)
        {
            switch (mode)
            {
                case SerializeMode.Json:
                    return JsonSerializer.Instance.Deserialize(stream, type);
                case SerializeMode.Xml:
                    break;
                case SerializeMode.Binary:
                    break;
                default:
                    break;
            }

            return null;
        }
        public static Task<object> ReadDataAsync(this Stream stream,Type type, SerializeMode mode)
        {
            return null;
        }
    }
}
