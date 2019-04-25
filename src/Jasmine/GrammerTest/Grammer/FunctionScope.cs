using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class FunctionScope : Scope
    {
        public string FunctionName { get; set; }
        public string[] ParameterNames { get; set; }
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
