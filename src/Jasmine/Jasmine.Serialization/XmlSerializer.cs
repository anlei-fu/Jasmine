using System;
using System.IO;

namespace Jasmine.Serialization
{
    public class XmlSerializer : ISerializer
    {
        public bool TryDeserialize(byte[] buffer, Type type, out object result)
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

        public bool TryDeserialize<T>(byte[] buffer, out T result)
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

        public bool TrySerialize(object obj, out byte[] result)
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
    }
}
