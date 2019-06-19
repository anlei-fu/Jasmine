using System;
using System.Threading.Tasks;

namespace Jasmine.Orm
{
    public  interface IMutipleResultReader:IDisposable
    {
      
        ICursor NextResult();
        Task<ICursor> NextResultAsync();
        bool Closed { get; }
        void Close();
    }
}
