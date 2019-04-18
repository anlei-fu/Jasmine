using System;
using System.IO;

namespace Jasmine.Serialization
{
    public interface ISerializer
    {
        bool TryDeserialize(byte[] buffer, Type type, out object result);
        bool TryDeserialize(string input,Type type, out object result);
        bool TryDeserialize(Stream stream,Type type, out object result);
        bool TryDeserialize<T>(byte[] buffer, out T result);
        bool TryDeserialize<T>(string input, out T result);
        bool TryDeserialize<T>(Stream stream, out T result);
        bool TrySerialize(object obj, out byte[] result);
        bool TrySerialize(object obj, out string result);
        bool TrySerialize(object obj, Stream stream);

    }
}
