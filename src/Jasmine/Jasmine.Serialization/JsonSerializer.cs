using Jasmine.Reflection;
using Jasmine.Serialization.Exceptions;
using System;
using System.IO;
using System.Text;

namespace Jasmine.Serialization
{
    public class JsonSerializer : ISerializer
    {
        private JsonSerializer()
        {
         
        }

        public static readonly ISerializer Instance = new JsonSerializer();

        public object Deserialize(byte[] buffer, Type type)
        {
            return Deserialize(buffer, type, Encoding.UTF8);
        }

        public object Deserialize(byte[] buffer, Type type, Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            return Deserialize(encoding.GetString(buffer), type);
        }

        public object Deserialize(string input, Type type)
        {
            return TryDeserialize(input, type, out var result) ? result :
                                                                 throw new DeserializeException();
        }

        public object Deserialize(Stream stream, Type type)
        {
            return Deserialize(stream, type, Encoding.UTF8);
        }

        public object Deserialize(Stream stream, Type type, Encoding encoding)
        {
            return TryDeserialize(stream, type, encoding, out var result) ? result :
                                                                       throw new DeserializeException();
        }

        public T Deserialize<T>(byte[] buffer)
        {
            return Deserialize<T>(buffer, Encoding.UTF8);
        }

        public T Deserialize<T>(byte[] buffer, Encoding encoding)
        {
            return Deserialize<T>(encoding.GetString(buffer));
        }

        public T Deserialize<T>(string input)
        {
            return TryDeserialize<T>(input, out var result) ? result :
                                                           throw new DeserializeException();
        }

        public T Deserialize<T>(Stream stream)
        {
            return Deserialize<T>(stream, Encoding.UTF8);
        }

        public T Deserialize<T>(Stream stream, Encoding encoding)
        {
            return TryDeserialize<T>(stream, encoding, out var result) ? result :
                                                                      throw new DeserializeException();
        }

        public string Serialize(object obj)
        {
            return TrySerialize(obj, out string result) ? result :
                                                    throw new SerializeException();

        }

        public void Serialize(object obj, Stream stream)
        {
            Serialize(obj, stream, Encoding.UTF8);
        }

        public void Serialize(object obj, Stream stream, Encoding encoding)
        {
            if (!TrySerialize(obj, stream, encoding))
            {
                throw new SerializeException();
            }
        }

        public byte[] SerializeToBytes(object obj)
        {
            return SerializeToBytes(obj, Encoding.UTF8);
        }

        public byte[] SerializeToBytes(object obj, Encoding encoding)
        {
            return TrySerialize(obj, encoding, out byte[] result) ? result :
                                                                throw new SerializeException();
        }

        public bool TryDeserialize(byte[] buffer, Type type, out object result)
        {
            return TryDeserialize(buffer, type, Encoding.UTF8, out result);
        }

        public bool TryDeserialize(byte[] buffer, Type type, Encoding encoding, out object result)
        {
            return TryDeserialize(encoding.GetString(buffer), type, out result);
        }

        public bool TryDeserialize(string input, Type type, out object result)
        {
            try
            {
                result = BaseTypes.Base.Contains(type)? JasmineStringValueConvertor.GetValue(input,type)
                                                       : Newtonsoft.Json.JsonConvert.DeserializeObject(input, type);

                return true;
            }
            catch
            {
                result = null;
                return false;
            }

        }

        public bool TryDeserialize(Stream stream, Type type, out object result)
        {
            return TryDeserialize(stream, type, Encoding.UTF8, out result);
        }

        public bool TryDeserialize(Stream stream, Type type, Encoding encoding, out object result)
        {
            try
            {
                using (var reader = new StreamReader(stream, encoding))
                    return TryDeserialize(reader.ReadToEnd(), type, out result);
            }
            catch
            {
                result = null;

                return false;
            }
        }

        public bool TryDeserialize<T>(byte[] buffer, out T result)
        {
            return TryDeserialize(buffer, Encoding.UTF8, out result);
        }

        public bool TryDeserialize<T>(byte[] buffer, Encoding encoding, out T result)
        {
            return TryDeserialize(encoding.GetString(buffer), out result);
        }

        public bool TryDeserialize<T>(string input, out T result)
        {
            try
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(input);

                return true;
            }
            catch
            {
                result = default(T);

                return false;
            }
        }

        public bool TryDeserialize<T>(Stream stream, out T result)
        {
            return TryDeserialize(stream, Encoding.UTF8, out result);
        }

        public bool TryDeserialize<T>(Stream stream, Encoding encoding, out T result)
        {
            if (TryDeserialize(stream, typeof(T), encoding, out object value))
            {
                result = (T)value;
                return true;
            }
            else
            {
                result = default(T);
                return false;
            }
        }

        public bool TrySerialize(object obj, out byte[] result)
        {
            return TrySerialize(obj, Encoding.UTF8, out result);
        }

        public bool TrySerialize(object obj, Encoding encoding, out byte[] result)
        {
            if (TrySerialize(obj, out string value))
            {
                result = encoding.GetBytes(value);

                return true;
            }
            else
            {
                result = null;

                return false;
            }
        }

        public bool TrySerialize(object obj, out string result)
        {
            try
            {
                result = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

                return true;
            }
            catch (Exception)
            {

                result = null;

                return false;
            }
        }

        public bool TrySerialize(object obj, Stream stream)
        {
            return TrySerialize(obj, stream, Encoding.UTF8);
        }

        public bool TrySerialize(object obj, Stream stream, Encoding encoding)
        {
            if (TrySerialize(obj, out string result))
            {
                try
                {
                    using (var writer = new StreamWriter(stream, encoding))
                        writer.Write(result);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
