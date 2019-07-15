using Jasmine.Common;
using Jasmine.Extensions;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Tools.Csv
{
    public class CsvReader : ICsvReader
    {
        public  CsvReader(Stream stream,Encoding encoding=null)
        {
            if (stream == null)
                throw new ArgumentException(nameof(stream));

            encoding = encoding ?? Encoding.Default;

            _reader = new StreamReader(stream, encoding);
        }
        private JasmineReflectionCache _reflection;

        private readonly StreamReader _reader;
        public dynamic ReadOne(Type type)
        {
            string line;

            do
            {
                line = _reader.ReadLine()?.Trim();

            } while (line != string.Empty);


            if (line == null)
                return null;

            var records = line.Splite(",");

            var resolveItems = createResolveItems(type);

            var instance = _reflection.GetItem(type)
                                      .Constructors
                                      .GetDefaultConstructor()
                                      ?.DefaultInvoker
                                      ?.Invoke();

            if (instance == null)
                throw new DefaultConstructorNotFoundException(type);

            foreach (var item in resolveItems)
            {
                if (item.Column > records.Count-1)
                    throw new ArgumentOutOfRangeException();

                if (!item.Property.CanWrite)
                    throw new Exception();

                item.Property.SetValue(instance, 
                                       JasmineStringValueConvertor.FromString(records[item.Column], item.Property.PropertyType));
            }

            return instance;
        }

        public IEnumerable<T> Read<T>(int count)
        {
            string line;

            var type = typeof(T);
            var t = 0;

            while (++t<count)
            {
                line = _reader.ReadLine()?.Trim();

                if (line == null)
                    break;

                var records = line.Splite(",");

                var resolveItems = createResolveItems(type);

                var instance = _reflection.GetItem(type)
                                          .Constructors.GetDefaultConstructor()
                                          ?.DefaultInvoker
                                          ?.Invoke();

                if (instance == null)
                    throw new DefaultConstructorNotFoundException(type);

                foreach (var item in resolveItems)
                {
                    if (item.Column > records.Count)
                        throw new ArgumentOutOfRangeException();

                    if (!item.Property.CanWrite)
                        throw new Exception();

                    item.Property.SetValue(instance,
                                           JasmineStringValueConvertor.FromString(records[item.Column], item.Property.PropertyType));
                }

                yield return (T)instance;
            }
        }

        public IEnumerable<T> Read<T>(int count, Func<List<string>, T> factory)
        {
            string line;

            var t = 0;

            while (++t<count)
            {
                line = _reader.ReadLine()?.Trim();

                if (line == null)
                    break;

                var records = line.Splite(",");

                yield return factory(records);
            }

        }

        public dynamic Read(Type type, int count)
        {
            throw new NotImplementedException();
        }

        public void Read(Action<string[]> consumer, int count)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> ReadAsync(Type type)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadAsync<T>(int count)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadAsync<T>(int count, Func<string[], T> factory)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> ReadAsync(Type type, int count)
        {
            throw new NotImplementedException();
        }

        public Task ReadAsync(Action<string[]> consumer, int count)
        {
            throw new NotImplementedException();
        }

        public T ReadOne<T>()
        {
            return ReadOne(typeof(T));
        }

        public T ReadOne<T>(Func<string[], T> fatory)
        {
            throw new NotImplementedException();
        }

        public void ReadOne(Action<string[]> consumer)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadOneAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadOneAsync<T>(Func<string[], T> fatory)
        {
            throw new NotImplementedException();
        }

        public Task ReadOneAsync(Action<string[]> consumer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// do not just cast result of <see cref="ReadToEnd{T}(Type)"/>
        /// copy to a new list spend lots of time ,if has many rows
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> ReadToEnd<T>()
        {
            string line;

            var type = typeof(T);

            while(true)
            {
                line = _reader.ReadLine()?.Trim();

                if (line == null)
                    break;

                var records = line.Splite(",");

                var resolveItems = createResolveItems(type);

                var instance = _reflection.GetItem(type)
                                          .Constructors.GetDefaultConstructor()
                                          ?.DefaultInvoker
                                          ?.Invoke();

                if (instance == null)
                    throw new DefaultConstructorNotFoundException(type);

                foreach (var item in resolveItems)
                {

                    if (item.Column > records.Count)
                        throw new ArgumentOutOfRangeException();

                    if (!item.Property.CanWrite)
                        throw new Exception();

                    item.Property.Setter.Invoke(instance,
                                               JasmineStringValueConvertor.FromString(records[item.Column], item.Property.PropertyType));
                }

                yield return (T)instance;
            } 
        }

        public IEnumerable<dynamic> ReadToEnd<T>(Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// async stream not supported at current version ,
        /// when new version released ,do a change
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ReadToEndAsync<T>()
        {
            string line;

            var type = typeof(T);

            var ls=new List<T>();

            while (true)
            {
                line = await _reader.ReadLineAsync();

                if (line == null)
                    break;

                line = line.Trim();

                var records = line.Splite(",");

                var resolveItems = createResolveItems(type);

                var instance = _reflection.GetItem(type)
                                          .Constructors.GetDefaultConstructor()
                                          ?.DefaultInvoker
                                          ?.Invoke();

                if (instance == null)
                    throw new DefaultConstructorNotFoundException(type);

                foreach (var item in resolveItems)
                {

                    if (item.Column > records.Count)
                        throw new ArgumentOutOfRangeException();

                    if (!item.Property.CanWrite)
                        throw new Exception();

                    item.Property.Setter.Invoke(instance,
                                               JasmineStringValueConvertor.FromString(records[item.Column], item.Property.PropertyType));
                }

                ls.Add((T)instance);
            }

            return ls;
        }

        public Task<IEnumerable<dynamic>> ReadToEndAsync<T>(Type type)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<ResolveItem> createResolveItems(Type type)
        {
            return null;
        }

        private class ResolveItem
        {
            public Property Property { get; set; }
            public int Column { get; set; }
        }



    }
}
