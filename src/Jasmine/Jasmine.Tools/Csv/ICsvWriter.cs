using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Tools.Csv
{
    public interface ICsvWriter
    {
        void Write(dynamic data);
        void Write(IEnumerable<dynamic> datas);
        void Write<T>(T data, Func<T, string> fatory);
        void Write<T>(IEnumerable<T> datas, Func<T, string> fatory);
        Task WriteAsync(dynamic data);
        Task WriteAsync(IEnumerable<dynamic> datas);
        Task WriteAsync<T>(T data, Func<T, string> fatory);
        Task WriteAsync<T>(IEnumerable<T> datas, Func<T, string> fatory);
    }
}
