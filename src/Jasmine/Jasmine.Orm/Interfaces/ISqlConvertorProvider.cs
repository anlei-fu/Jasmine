using System;

namespace Jasmine.Orm.Interfaces
{
    public  interface ISqlConvertorProvider
    {
        ISqlConvertor GetConvertor(Type type);
        ISqlConvertor GetConvertor<T>();
        void RemoveConvertor(Type type);
        void RemoveConvertor<T>();
        void AddConvertor(Type type, ISqlConvertor convertor);
        void AddConvertor<T>(ISqlConvertor convertor);
    }
}
