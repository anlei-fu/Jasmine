using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Serialization
{
    public interface ISerializer
    {

        byte[] SerializeToBytes(object obj);
        byte[] SerializeToBytes(object obj,Encoding encoding);
        string Serialize(object obj);
        void Serialize(object obj, Stream stream);
        void Serialize(object obj, Stream stream,Encoding encoding);

        Task SerializeAync(object obj, Stream stream);
        Task SerializeAsync(object obj, Stream stream, Encoding encoding);
        object Deserialize(byte[] buffer, Type type);
        object Deserialize(byte[] buffer, Type type,Encoding encoding);
        object Deserialize(string input, Type type);



      
        T Deserialize<T>(byte[] buffer);
        T Deserialize<T>(byte[] buffer, Encoding encoding);
        T Deserialize<T>(string input);
        object Deserialize(Stream stream, Type type);
        object Deserialize(Stream stream, Type type, Encoding encoding);
        T Deserialize<T>(Stream stream);
        T Deserialize<T>(Stream stream,Encoding encoding);

        Task<object> DeserializeAsync(Stream stream, Type type);
        Task<object> DeserializeAsync(Stream stream, Type type, Encoding encoding);
        Task<T> DeserializeAsync<T>(Stream stream);
        Task<T> DeserializeAsync<T>(Stream stream, Encoding encoding);

        bool TrySerialize(object obj, out byte[] result);
        bool TrySerialize(object obj,Encoding encoding, out byte[] result);
        bool TrySerialize(object obj, out string result);
        bool TrySerialize(object obj, Stream stream);
        bool TrySerialize(object obj, Stream stream,Encoding encoding);

        Task<bool> TrySerializeAsync(object obj, Stream stream);
        Task<bool> TrySerializeAsync(object obj, Stream stream, Encoding encoding);

        bool TryDeserialize(byte[] buffer, Type type, out object result);
        bool TryDeserialize(byte[] buffer, Type type,Encoding encoding, out object result);
        bool TryDeserialize(string input, Type type, out object result);
        bool TryDeserialize(Stream stream, Type type, out object result);
        bool TryDeserialize(Stream stream, Type type,Encoding encoding, out object result);

        Task<bool> TryDeserializeAsync(Stream stream, Type type, out object result);
        Task<bool> TryDeserializeAsync(Stream stream, Type type, Encoding encoding, out object result);

        bool TryDeserialize<T>(byte[] buffer, out T result);
        bool TryDeserialize<T>(byte[] buffer,Encoding encoding, out T result);
        bool TryDeserialize<T>(string input, out T result);
        bool TryDeserialize<T>(Stream stream, out T result);
        bool TryDeserialize<T>(Stream stream,Encoding encoding, out T result);

        Task<bool> TryDeserializeAsync<T>(Stream stream, out T result);
        Task<bool> TryDeserializeAsync<T>(Stream stream, Encoding encoding, out T result);


    }
}
