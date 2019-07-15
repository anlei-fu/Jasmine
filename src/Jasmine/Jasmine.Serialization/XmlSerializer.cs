using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Serialization
{
    public class XmlSerializer : ISerializer
    {
        public object Deserialize(byte[] buffer, Type type)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(byte[] buffer, Type type, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(string input, Type type)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(Stream stream, Type type)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(Stream stream, Type type, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(byte[] buffer, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string input)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(Stream stream, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public Task<object> DeserializeAsync(Stream stream, Type type)
        {
            throw new NotImplementedException();
        }

        public Task<object> DeserializeAsync(Stream stream, Type type, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeserializeAsync<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeserializeAsync<T>(Stream stream, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public string Serialize(object obj)
        {
            throw new NotImplementedException();
        }

        public void Serialize(object obj, Stream stream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(object obj, Stream stream, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public Task SerializeAsync(object obj, Stream stream, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public Task SerializeAync(object obj, Stream stream)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializeToBytes(object obj)
        {
            throw new NotImplementedException();
        }

        public byte[] SerializeToBytes(object obj, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize(byte[] buffer, Type type, out object result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize(byte[] buffer, Type type, Encoding encoding, out object result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize(string input, Type type, out object result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize(Stream stream, Type type, out object result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize(Stream stream, Type type, Encoding encoding, out object result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize<T>(byte[] buffer, out T result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize<T>(byte[] buffer, Encoding encoding, out T result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize<T>(string input, out T result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize<T>(Stream stream, out T result)
        {
            throw new NotImplementedException();
        }

        public bool TryDeserialize<T>(Stream stream, Encoding encoding, out T result)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeserializeAsync(Stream stream, Type type, out object result)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeserializeAsync(Stream stream, Type type, Encoding encoding, out object result)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeserializeAsync<T>(Stream stream, out T result)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeserializeAsync<T>(Stream stream, Encoding encoding, out T result)
        {
            throw new NotImplementedException();
        }

        public bool TrySerialize(object obj, out byte[] result)
        {
            throw new NotImplementedException();
        }

        public bool TrySerialize(object obj, Encoding encoding, out byte[] result)
        {
            throw new NotImplementedException();
        }

        public bool TrySerialize(object obj, out string result)
        {
            throw new NotImplementedException();
        }

        public bool TrySerialize(object obj, Stream stream)
        {
            throw new NotImplementedException();
        }

        public bool TrySerialize(object obj, Stream stream, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TrySerializeAsync(object obj, Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TrySerializeAsync(object obj, Stream stream, Encoding encoding)
        {
            throw new NotImplementedException();
        }
    }
}
