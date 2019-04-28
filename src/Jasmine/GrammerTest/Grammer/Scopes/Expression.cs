using GrammerTest.Grammer.Scopes;

namespace Jasmine.Spider.Grammer
{
    public class Expression:AbstractExcutor
    {
        public override string Name => base.Name+"Expression";
        public OperatorNode Root;
        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
