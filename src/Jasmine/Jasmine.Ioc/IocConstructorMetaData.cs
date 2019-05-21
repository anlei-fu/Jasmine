using Jasmine.Reflection;

namespace Jasmine.Ioc
{
    /// <summary>
    /// use to recorder a constructor info
    /// </summary>
    public   class IocConstructorMetaData
    {
        public IocConstructorMetaData(Constructor constructor)
        {
            Constructor = constructor;
        }
        public Constructor Constructor { get;}
        public IocParameterMetaData[] Parameters { get; set; }
       
    }
}
