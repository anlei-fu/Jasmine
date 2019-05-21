using Jasmine.Reflection;

namespace Jasmine.Ioc
{
    public class IocMethodMetaData
    {
        public Method Method { get; internal set; }
        public IocParameterMetaData[] Parameters { get; internal set; }
        public int ParameterLength => Parameters.Length;
       
    }
}
