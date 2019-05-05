using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public  class ForBlock:BreakableBlock
    {
        public IEnumerable<object> Collection { get; set; }

        public IEnumerator<object> Enumerator { get; set; }
        public object CurrentElement { get; set; }
      
        public DeclareExpression DeclareExpression { get; set; }
        public Expression CheckExpression { get; set; }
        public Expression OperateExpression { get; set; } = new Expression();

        public Block Block { get; set; }

        public override void Break()
        {
            throw new System.NotImplementedException();
        }

        public override void Catch(JError error)
        {
            throw new System.NotImplementedException();
        }

        public override void Continue()
        {
            throw new System.NotImplementedException();
        }

        public override void Excute()
        {
           
        }

        public override void Return(JObject result)
        {
            throw new System.NotImplementedException();
        }
    }
}
