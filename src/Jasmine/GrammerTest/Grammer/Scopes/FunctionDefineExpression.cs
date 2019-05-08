using Jasmine.Spider.Grammer;
using System;

namespace GrammerTest.Grammer.Scopes
{
    public class FunctionDefineExpression : Expression
    {
        public FunctionDefineExpression(BreakableBlock parent) : base(parent)
        {
        }
    }
}
