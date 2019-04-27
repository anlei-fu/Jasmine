using GrammerTest.Grammer.Scopes;
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
        public Expression OperateExpression { get; set; }

        public Block Block { get; set; }

        public override void Excute()
        {
           
        }

      
    }
}
