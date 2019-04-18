using Jasmine.Reflection.Models;

namespace Jasmine.Ioc
{
    /// <summary>
    /// use to recorder a constructor info
    /// </summary>
    public   class ConstructorMetaData
    {
        public ConstructorMetaData(Constructor constructor)
        {
            Constructor = constructor;
        }
        public Constructor Constructor { get;}

        public ParameterMetaData[] Parameters { get; set; }

       

        public override string ToString()
        {
            return Constructor.RelatedInfo.ToString();
        }
    }
}
