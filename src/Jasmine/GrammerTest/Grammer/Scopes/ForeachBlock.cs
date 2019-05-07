using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;

namespace GrammerTest.Grammer
{
    public class ForeachBlock : BodyBlock
    {
        public ForeachBlock(Block parent) : base(parent)
        {
        }

        public Expression GetCollectionExpression { get; set; }
        public DeclareExpression DeclareExpression { get; set; }
     

        public IEnumerator<object> Enumerabtor { get; set; }

        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Catch(JError error)
        {
            throw new NotImplementedException();
        }

        public override void Continue()
        {
            throw new NotImplementedException();
        }

        public override void Excute()
        {
            throw new NotImplementedException();
        }

        public override void Return(JObject result)
        {
            throw new NotImplementedException();
        }
    }
}
