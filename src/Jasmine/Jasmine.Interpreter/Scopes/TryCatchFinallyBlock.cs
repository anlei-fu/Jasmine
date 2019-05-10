using Jasmine.Interpreter.TypeSystem;
using System;

namespace Jasmine.Interpreter.Scopes
{
    public class TryCatchFinallyBlock : BreakableBlock
    {
        public TryCatchFinallyBlock(BreakableBlock parent) : base(parent)
        {
        }

        public TryBlock TryBlock { get; set; }
        public CatchBlock CatchBlock { get; set; }
        public FinallyBlock FinallyBlock { get; set; }

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

        public override void Return(Any result)
        {
            throw new NotImplementedException();
        }
    }
}
