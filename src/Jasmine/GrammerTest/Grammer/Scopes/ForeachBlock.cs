using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;

namespace GrammerTest.Grammer
{
    public class ForeachBlock : Scope
    {

        public Expression GetCollectionExpression { get; set; }
        public DeclareExpression DeclareExpression { get; set; }

        public IEnumerator<object> Enumerabtor { get; set; }

        public override void Excute()
        {
            throw new NotImplementedException();
        }
    }
}
