using GrammerTest.Grammer.Scopes;

namespace Jasmine.Spider.Grammer
{
    public class Expression:AbstractExcutor
    {
        public Expression(Block parent) : base(parent)
        {
        }

        public override string Name => base.Name+"Expression";
        public OperatorNode Root { get; set; }
        public override void Excute()
        {
            Root.Excute();
        }
    }
}
